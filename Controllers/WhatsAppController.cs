using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class WhatsAppController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesEpisode _servicesOfEpisode;

        public WhatsAppController(ILogger<HomeController> logger, IConfiguration configuration, IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, IServicesEpisode servicesOfEpisode)
        {
            _logger = logger;
            _configuration = configuration;
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _servicesOfEpisode = servicesOfEpisode;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MasterViewModel master = new MasterViewModel()
            {

                DemandVM = new()
                {
                    NewDemand = new Demand(),
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
                }
            };
            return View(master);
        }
        [HttpPost]
        public async Task<IActionResult> SendWhatsApp(Guid id)
        {
            try
            {
                // 1 → Get Students List Data
                //
                var lstStudents = _servicesOfEpisode.GetStudentsForEpisode(id);

                var failedMessages = new List<string>();
                //var successMessages = new List<string>();

                var instanceId = _configuration["Ultramsg:InstanceId"];
                var token = _configuration["Ultramsg:Token"];
                var clients = lstStudents;

                // 2→ Check UltraMsg Connection Data [instance id | token]
                // 
                if (string.IsNullOrEmpty(instanceId) || string.IsNullOrEmpty(token))
                {
                    return Json(new WhatsAppResponse
                    {
                        Success = false,
                        Message = $"UltraMsg provider connection data [instanceid | token] "
                    });
                }

                // 3→ Check Parent Phone of every student | Set Message Data
                // 
                foreach (var client in clients.Students)
                {
                    // check phone
                    if (string.IsNullOrEmpty(client.ParentPhoneNumber))
                    {
                        failedMessages.Add($"لم يتم الارسال الي الرقم {client.ParentPhoneNumber} الموجود في بيانات الطالب {client.Name}: برجاء مراجعة بياناته");
                        continue;
                    }

                    //set message data


                    string? apiUrl = $"https://api.ultramsg.com/{instanceId}/messages/image";
                    string? imageUrl = string.Empty;
                    string? captionText = string.Empty;

                    var formData = new List<KeyValuePair<string, string>>
                               {
                                   new KeyValuePair<string, string>("token", token),
                                   new KeyValuePair<string, string>("to", client.ParentPhoneNumber)
                               };
                    // Image
                    //imageUrl = "https://www.kanzalebda3.com/img/Parents2.jpg";
                    imageUrl = "https://zekrhakeem.premiumasp.net/images/loclogo.png";
                    formData.Add(new KeyValuePair<string, string>("image", imageUrl));

                    // Caption Text
                    captionText = BuildCaptionFromFields(client);
                    formData.Add(new KeyValuePair<string, string>("caption", captionText));

                    _logger.LogInformation($"إرسال صورة مع caption: {imageUrl}, النص: {"نص افتراضي"}");

                    using var httpClient = new HttpClient();

                    // convert form-urlencoded
                    var formContent = new FormUrlEncodedContent(formData);


                    _logger.LogInformation($"إرسال طلب إلى: {apiUrl}");
                    _logger.LogInformation($"البيانات المرسلة: {string.Join(", ", formData.Select(kv => $"{kv.Key}={kv.Value}"))}");


                    var response = await httpClient.PostAsync(apiUrl, formContent);


                    var responseContent = await response.Content.ReadAsStringAsync();


                    _logger.LogInformation($"استجابة API: Status={response.StatusCode}, Content={responseContent}");

                }
                return Json(new WhatsAppResponse
                {
                    Success = true,
                    Message = "تم إرسال الرسالة بنجاح",
                });
            }
            catch (Exception ex)
            {
                // معالجة أي استثناءات غير متوقعة
                _logger.LogError(ex, "خطأ في إرسال رسالة WhatsApp");
                return Json(new WhatsAppResponse
                {
                    Success = false,
                    Message = $"حدث خطأ: {ex.Message}"
                });
            }


        }

        private string BuildCaptionFromFields(Student studentData)
        {
            var caption = "";
            // Attendance
            string att = "";
            string save = "";
            string rev = "";
            string rabt = "";
            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 0)
                att = "حاضر";
            else if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 1)
                att = "متأخر";
            else if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 2)
                att = "مستأذن";
            else if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 3)
                att = "غائب يإذن";
            else if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 4)
                att = "غائب";
            //Save
            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Save == -1)
                save = "لم يقيم ";
            else
                save = $"{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Save} / 10 ";
            //Rev
            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Rev == -1)
                rev = "لم يقيم ";
            else
                rev = $"{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Rev} / 10 ";
            //Rabt
            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Rabt == -1)
                rabt = "لم يقيم ";
            else
                rev = $"{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Rabt} / 10 ";



            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend < 3)
            {
                if (studentData.PlanData.Name == "الذكر الحكيم")
                {
                    caption =
                                    $"\t\t *السلام عليكم ورحمة الله وبركاته*  \n\n" +
                                    $"\t📋 *تقرير اليوم* \n " +
                                    $"\t\t\t{DateTime.Now.ToString("dddd")}\t{DateTime.Now.ToString("dd/MM/yyyy")} \n\n" +
                                    $"\t\t 👳‍♀️ *الطالب*  \t{studentData.Name}\n" +
                                    $"\t\t 🕌  *الحلقة*  \t{studentData.StEpisode.Name}\t[{studentData.StEpisode.Period}]\n" +
                                    $"\t\t  *معلم الحلقة* \t{studentData.StEpisode.ApplicationUser.FullName}\n\n" +

                                    $"\t\t  ۞ *بيانات الحضور* ۞  \n" +
                                    $"\t\t\t  {att}  \n\n" +

                                    $"\t\t  ۞ *بيانات الحفظ* ۞  \n" +
                                    $"\t\t\t  *من سورة* \t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameFrom}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdFrom}]\n" +
                                    $"\t\t\t  *إلي سورة*\t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameTo}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdTo}]\n" +
                                    $"\t\t\t 📿 *تقييم الحفظ*\t{save}" +
                                    $"\n\n" +

                                    $"\t\t  ۞ *بيانات المراجعة* ۞  \n" +
                                    $"\t\t\t  *من سورة* \t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevSurahNameFrom}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevAyahIdFrom}]\n" +
                                    $"\t\t\t  *إلي سورة*\t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevSurahNameTo}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevAyahIdTo}]\n" +
                                    $"\t\t\t 📿 *تقييم المراجعة*\t{rev}" +
                                    $"\n\n" +

                                    $"\t\t  ۞ *بيانات الربط* ۞  \n" +
                                    $"\t\t\t  *من سورة* \t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RabtSurahNameFrom}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RabtAyahIdFrom}]\n" +
                                    $"\t\t\t  *إلي سورة*\t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RabtSurahNameTo}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RabtAyahIdTo}]\n" +
                                    $"\t\t\t 📿 *تقييم الربط*\t{rabt} \n\n" +

                                    $"\t\t\t للإطلاع علي كل ما يخص الطلاب \n" +
                                    $"\t\t\t يمكنك تسجيل الدخول \n" +
                                    $"\t\t\t لتطبيق جمعية الذكر الحكيم \n" +
                                    $"\t\t\t من خلال الرابط التالي \n" +
                                    $"\t\t\t https://zekrhakeem.premiumasp.net/ \n\n";



                    //$"📖 من سورة {""} آية {""}\n\n" +
                    //$"📖 إلى سورة {""} آية {""}\n\n" +
                    //$"متمنين له التوفيق 🌟";
                }
                else if (studentData.PlanData.Name == "الخطة العامة")
                {
                    caption =
                                    $"\t\t *السلام عليكم ورحمة الله وبركاته*  \n\n" +
                                    $"\t📋 *تقرير اليوم* \n " +
                                    $"\t\t\t{DateTime.Now.ToString("dddd")}\t{DateTime.Now.ToString("dd/MM/yyyy")} \n\n" +
                                    $"\t\t 👳‍♀️ *الطالب*  \t{studentData.Name}\n" +
                                    $"\t\t 🕌  *الحلقة*  \t{studentData.StEpisode.Name}\t[{studentData.StEpisode.Period}]\n" +
                                    $"\t\t  *معلم الحلقة*  \t{studentData.StEpisode.ApplicationUser.FullName}\n\n" +

                                    $"\t\t  ۞ *بيانات الحضور* ۞  \n" +
                                    $"\t\t\t  {att}  \n\n" +

                                    $"\t\t  ۞ *بيانات الحفظ* ۞  \n" +
                                    $"\t\t\t  *من سورة* \t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameFrom}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdFrom}]\n" +
                                    $"\t\t\t  *إلي سورة*\t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameTo}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdTo}]\n" +
                                    $"\t\t\t 📿 *تقييم الحفظ*\t{save}" +
                                    $"\n\n" +

                                    $"\t\t  ۞ *بيانات المراجعة* ۞  \n" +
                                    $"\t\t\t  *من سورة* \t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevSurahNameFrom}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevAyahIdFrom}]\n" +
                                    $"\t\t\t  *إلي سورة*\t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevSurahNameTo}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevAyahIdTo}]\n" +
                                    $"\t\t\t 📿 *تقييم المراجعة*\t{rev} \n\n" +

                                   $"\t\t\t للإطلاع علي كل ما يخص الطلاب \n" +
                                    $"\t\t\t يمكنك تسجيل الدخول \n" +
                                    $"\t\t\t لتطبيق جمعية الذكر الحكيم \n" +
                                    $"\t\t\t من خلال الرابط التالي \n" +
                                    $"\t\t\t https://zekrhakeem.premiumasp.net/ \n\n";
                }
                else if (studentData.PlanData.Name == "الخطة المفتوحة")
                {
                    caption =
                                    $"\t\t *السلام عليكم ورحمة الله وبركاته*  \n\n" +
                                    $"\t📋 *تقرير اليوم* \n " +
                                    $"\t\t\t{DateTime.Now.ToString("dddd")}\t{DateTime.Now.ToString("dd/MM/yyyy")} \n\n" +
                                    $"\t\t 👳‍♀️ *الطالب*  \t{studentData.Name}\n" +
                                    $"\t\t 🕌  *الحلقة*  \t{studentData.StEpisode.Name}\t[{studentData.StEpisode.Period}]\n\n" +


                                    $"\t\t  ۞ *بيانات الحضور* ۞  \n" +
                                   $"\t\t\t  {att}  \n\n" +

                                    $"\t\t  ۞ *بيانات المقدار* ۞  \n" +
                                    $"\t\t\t  *من سورة* \t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameFrom}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdFrom}]\n" +
                                    $"\t\t\t  *إلي سورة*\t ۩{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameTo}۩ [{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdTo}]\n" +
                                    $"\t\t\t 📿 *تقييم المقدار*\t{save}" +

                                 $"\t\t\t للإطلاع علي كل ما يخص الطلاب \n" +
                                    $"\t\t\t يمكنك تسجيل الدخول \n" +
                                    $"\t\t\t لتطبيق جمعية الذكر الحكيم \n" +
                                    $"\t\t\t من خلال الرابط التالي \n" +
                                    $"\t\t\t https://zekrhakeem.premiumasp.net/ \n\n";

                }
            }
            else
            {
                caption =
                                    $"\t\t *السلام عليكم ورحمة الله وبركاته*  \n\n" +
                                    $"\t📋 *تقرير اليوم* \n " +
                                    $"\t\t\t{DateTime.Now.ToString("dddd")}\t{DateTime.Now.ToString("dd/MM/yyyy")} \n\n" +
                                    $"\t\t 👳‍♀️ *الطالب*  \t{studentData.Name}\n" +
                                    $"\t\t 🕌  *الحلقة*  \t{studentData.StEpisode.Name}\t[{studentData.StEpisode.Period}]\n" +
                                    $"\t\t  *معلم الحلقة* \t{studentData.StEpisode.ApplicationUser.FullName}\n\n" +

                                   $"\t\t  ۞ *بيانات الحضور* ۞  \n" +
                                   $"\t\t\t  {att}  \n\n" +

                                $"\t\t\t للإطلاع علي كل ما يخص الطلاب \n" +
                                    $"\t\t\t يمكنك تسجيل الدخول \n" +
                                    $"\t\t\t لتطبيق جمعية الذكر الحكيم \n" +
                                    $"\t\t\t من خلال الرابط التالي \n" +
                                    $"\t\t\t https://zekrhakeem.premiumasp.net/ \n\n";
            }


            return caption;
        }




    }


}
