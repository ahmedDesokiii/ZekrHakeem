$('#tableEpisodeOfStudents').dataTable({
    "autoWidth": true,
    "responsive": true,
    "displaylength": 25,
    layout: {
        topStart: {
            search: {
                placeholder: 'بحث   ...'
            }
        },
        topEnd: {
            pageLength: {
                menu: [25, 50, 100, 200, 400, 1000]
            }
        },
        bottomEnd: {
            paging: {
                buttons: 6
            }
        }
    },
    language: {
        "autoWidth": true,
        "responsive": true,
        search: 'بحث', // Customize the search input label
        lengthMenu: ' _MENU_ ', // Customize the "Show X entries" label
        info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ حلقة', // Customize the info text
        zeroRecords: 'لم يعثر على أية سجلات', // No records message
        paginate: {
            first: 'الأول',
            previous: 'السابق',
            next: 'التالي',
            last: 'الأخير'
        }
    }
});

$('#tableStudents').dataTable({
    "autoWidth": true,
    "responsive": true,
    "displaylength": 25,
    layout: {
        topStart: {
            search: {
                placeholder: 'بحث عن بيانات الطالب...'
            }
        },
        topEnd: {
            pageLength: {
                menu: [25, 50, 100, 200, 400, 1000]
            }
        },
        bottomEnd: {
            paging: {
                buttons: 6
            }
        }
    },
    language: {
        "autoWidth": true,
        "responsive": true,
        search: 'بحث', // Customize the search input label
        lengthMenu: ' _MENU_ ', // Customize the "Show X entries" label
        info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ طالب', // Customize the info text
        zeroRecords: 'لم يعثر على أية سجلات', // No records message
        paginate: {
            first: 'الأول',
            previous: 'السابق',
            next: 'التالي',
            last: 'الأخير'
        }
    }
});


//$(document).ready(function () {
//        new DataTable('#tableEpisodeOfStudents')({
//        "autoWidth": true,
//        "responsive": true,
//        //"searching":false
//            "language": {
//                "sSearch": "بحث:", "sLengthMenu": "_MENU_",
//            },
//            pageLength: {
//                menu: [25, 50, 100, 200, 400, 1000]
//            }
//        });
  
//});
//$(document).ready(function () {
//    new DataTable('#tableStudents')({
//        "autoWidth": true,
//        "responsive": true,
//        //"searching":false
//        "language": { "sSearch": "بحث:", "sLengthMenu": "_MENU_", }
//    });

//});


// لعرض البحث في كل حلقة منفردة داخل نفس الصفحة
/*new DataTable('table.display');*/

getHijriDate = () => {
    //alert(document.getElementById("txtHijriDate").value);
    if (document.getElementById("stBD").style.visibility == "hidden")
        document.getElementById("stBD").value = document.getElementById("txtHijriDate").value;
}


Delete = (id) => {
    Swal.fire({
        title: lbTitleMsgDelete,
        text: lbTextMsgDelete,
        icon: 'error',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Students/Delete?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkStudent,
                lbSuccess
            )
        }
    })
}

DeleteLog = (id) => {

    Swal.fire({
        title: lbTitleMsgDeleteLog,
        text: lbTextMsgDeleteLog,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Students/DeleteLog?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkStudentLog,
                lbSuccess
            )
        }

    })
}

function Rest() {
   
    document.getElementById("stId").value = new Guid("00000000-0000-0000-0000-000000000000");
    document.getElementById("stName").value = ""; 
    document.getElementById("stCategory").selectedIndex = "0";
    document.getElementById("stIdentitynum").value = "";
    document.getElementById("stParentIdentitynum").value = null;
    document.getElementById("stParentPhonenum").value = "";
    document.getElementById("stNationalityId").selectedIndex = "0";
    document.getElementById("stHealthId").selectedIndex = "0";
    document.getElementById("stGender").selectedIndex = "0";
    document.getElementById("stEducationType").selectedIndex = "0";
    document.getElementById("stEpisodeId").selectedIndex = "0";
    document.getElementById("selectlimit").value = "";
    document.getElementById("selectin").value = "";
    document.getElementById("stateLimit").value = "";
    document.getElementById("stPlanId").selectedIndex = "0";
    document.getElementById("lblLevel").style.visibility = "hidden";
    document.getElementById("saveDir").style.visibility = "hidden";
    document.getElementById("saveDir").selectedIndex = "0";
    document.getElementById("stLevelId").style.visibility = "hidden";
    document.getElementById("stLevelId").selectedIndex = "0";
    document.getElementById("surahId_ctp").style.visibility = "hidden";
    document.getElementById("surahId_ctp").selectedIndex = "0";
    document.getElementById("verseId").style.visibility = "hidden";
    document.getElementById("verseId").selectedIndex = "0";
    document.getElementById("lblLesson").style.visibility = "hidden";
    document.getElementById("lblLesson").selectedIndex = "0";
   
    document.getElementById("stBD").style.visibility = "hidden";
    document.getElementById("txtHijriDate").value = "";
    document.getElementById("stBD").value = null;
    document.getElementById("stPhoto").value = null;
    document.getElementById("stAdress").value = "";
    document.getElementById("stNotes").value = "";

    document.getElementById("title").innerHTML = lbAddNewStudent;
    document.getElementById("btnSave").value = lbbtnSave;
}

//function Assigned(id, name) {
//    alert("message");
//    document.getElementById("studId").value = id;
//    document.getElementById("studName").value = name;
//}
//'@student.Id','@student.Name','@student.StEpisode.Name','@student.StEpisode.Students.Count()','@student.StEpisode.ApplicationUser.FullName'
function Assigned(sid, sname, currentEp, currentEpStuds,teacher) {
    document.getElementById("studId").value = sid;
    document.getElementById("studName").value = sname;
    document.getElementById("studCurrentEpName").value = currentEp;
    document.getElementById("studCurrentEpStuents").value = currentEpStuds;
    document.getElementById("epTeacher").value = teacher;
}
function Edit(id, name, category, identitynum, paridentitynum, parphone, nationality, health, gender, education, episode, epLimit, epStudents, plan, lesson, birthdate, photo, aderss, notes, saveDire, stVerse, stSurah, stSurahName) {

    document.getElementById("stId").value = id;
    document.getElementById("stName").value = name;
    document.getElementById("stCategory").value = category;
    document.getElementById("stIdentitynum").value = identitynum;
    document.getElementById("stParentIdentitynum").value = paridentitynum;
    document.getElementById("stParentPhonenum").value = parphone;
    document.getElementById("stNationalityId").value = nationality;
    document.getElementById("stHealthId").value = health;
    document.getElementById("stGender").value = gender;
    document.getElementById("stEducationType").value = education;
    document.getElementById("stEpisodeId").value = episode;
    document.getElementById("selectlimit").value = epLimit;
    document.getElementById("selectin").value = Number(epLimit) - Number(epStudents);

    if (Number(document.getElementById("selectin").value) == 0)
        document.getElementById("stateLimit").value = "عدد طلاب الحلقة مكتمل";
    else
        document.getElementById("stateLimit").value = "";

    document.getElementById("stPlanId").value = plan;

    if (document.getElementById("stPlanId").selectedIndex == "2") { // الذكر الحكيم
        document.getElementById("lblLevel").style.visibility = "visible";
        document.getElementById("lblLevel").innerHTML = "اتجاه الحفظ";
        document.getElementById("saveDir").style.visibility = "visible";
        document.getElementById("saveDir").selectedIndex = "2";
        document.getElementById("saveDir").disabled = true;

        document.getElementById("lblzhLesson").style.visibility = "visible";
        document.getElementById("lblzhLesson").innerHTML = "اسم الدرس";
        document.getElementById("stLevelId").style.visibility = "visible";
        document.getElementById("stLevelId").value = lesson;
        document.getElementById("saveDir").value = saveDire;

        document.getElementById("lblsurah_ctp").style.visibility = "hidden";
        document.getElementById("surahId_ctp").style.visibility = "hidden";
        document.getElementById("surahId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").style.visibility = "hidden";

        document.getElementById("lblsurah_ptc").style.visibility = "hidden";
        document.getElementById("surahId_ptc").style.visibility = "hidden";
        document.getElementById("surahId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").style.visibility = "hidden";
        document.getElementById("sdir").value = "ptc";
    }
    else {
        document.getElementById("lblLevel").style.visibility = "visible";
        document.getElementById("lblLevel").innerHTML = "اتجاه الحفظ";
        document.getElementById("saveDir").style.visibility = "visible";
        document.getElementById("saveDir").value = saveDire;
        document.getElementById("saveDir").disabled = false;

        document.getElementById("lblzhLesson").style.visibility = "hidden";
        document.getElementById("lblzhLesson").innerHTML = "اسم الدرس";
        document.getElementById("stLevelId").style.visibility = "hidden";
        document.getElementById("stLevelId").selectedIndex = "0";

        if (document.getElementById("saveDir").selectedIndex == "1") // ctp
        {
            document.getElementById("lblsurah_ctp").style.visibility = "visible";
            document.getElementById("surahId_ctp").style.visibility = "visible";
            document.getElementById("surahId_ctp").value = stSurah;
            document.getElementById("verseId_ctp").text = stVerse;
            document.getElementById("verseId_ctp").style.visibility = "hidden";

            document.getElementById("lblsurah_ptc").style.visibility = "hidden";
            document.getElementById("surahId_ptc").style.visibility = "hidden";
            document.getElementById("surahId_ptc").selectedIndex = "0";
            document.getElementById("verseId_ptc").selectedIndex = "0";
            document.getElementById("verseId_ptc").style.visibility = "hidden";
            document.getElementById("sdir").value = "ctp";

        }
        else if (document.getElementById("saveDir").selectedIndex == "2") // ptc
        {
            document.getElementById("lblsurah_ctp").style.visibility = "hidden";
            document.getElementById("surahId_ctp").style.visibility = "hidden";
            document.getElementById("surahId_ctp").selectedIndex = "0";
            document.getElementById("surahId_ctp").selectedIndex = "0";
            document.getElementById("surahId_ctp").style.visibility = "hidden";
           
            document.getElementById("lblsurah_ptc").style.visibility = "visible";
            document.getElementById("surahId_ptc").style.visibility = "visible";
            document.getElementById("surahId_ptc").value = stSurah; 
            document.getElementById("verseId_ptc").text = stVerse;
            document.getElementById("verseId_ptc").style.visibility = "hidden";
            document.getElementById("sdir").value = "ptc";
        }
        else {
            document.getElementById("lblsurah_ctp").style.visibility = "hidden";
            document.getElementById("surahId_ctp").style.visibility = "hidden";
            document.getElementById("surahId_ctp").selectedIndex = "0";
            document.getElementById("verseId_ctp").selectedIndex = "0";
            document.getElementById("verseId_ctp").style.visibility = "hidden";

            document.getElementById("lblsurah_ptc").style.visibility = "hidden";
            document.getElementById("surahId_ptc").style.visibility = "hidden";
            document.getElementById("surahId_ptc").selectedIndex = "0";
            document.getElementById("verseId_ptc").selectedIndex = "0";
            document.getElementById("verseId_ptc").style.visibility = "hidden";
            document.getElementById("sdir").value = "ptc";
        }
    }

        document.getElementById("surahName").value = stSurahName;
        document.getElementById("verseNum").value = stVerse;

        document.getElementById("txtHijriDate").value = "";
        document.getElementById("stBD").style.visibility = "visible";
        document.getElementById("stBD").value = birthdate;
        document.getElementById("stPhoto").value = photo;
        document.getElementById("stAdress").value = aderss;
        document.getElementById("stNotes").value = notes;

        document.getElementById("title").innerHTML = lbTitleEditStudent;
        document.getElementById("btnSave").value = lbEdit;
    }

var map; var marker; var defaultLocation = { lat: 25.0413431, lng: 37.2597054 }; // الموقع الافتراضي للجمعية
function initMap(newlat = 25.0413431, newlng = 37.2597054) {

    defaultLocation.lat = newlat;
    defaultLocation.lng = newlng;

    // إنشاء الخريطة
    map = new google.maps.Map(document.getElementById('map'), {
        center: defaultLocation,
        zoom: 16,
        mapTypeId: 'satellite' // تعيين العرض الافتراضي إلى "ستالايت"
    });

    // إضافة علامة (Marker) في الموقع الافتراضي
    marker = new google.maps.Marker({
        position: defaultLocation,
        map: map,
        title: 'الموقع المحدد',
        draggable: true // لجعل العلامة قابلة للسحب
    });

    // تحديث الإحداثيات في مربعات النص عند تحريك العلامة
    google.maps.event.addListener(marker, 'dragend', function (event) {
        updateCoordinates(event.latLng.lat(), event.latLng.lng());
    });

    // تحديث الإحداثيات عند النقر على الخريطة
    google.maps.event.addListener(map, 'click', function (event) {
        marker.setPosition(event.latLng);
        updateCoordinates(event.latLng.lat(), event.latLng.lng());
    });

    // تعيين الإحداثيات الافتراضية في المربعات
    updateCoordinates(defaultLocation.lat, defaultLocation.lng);
}
function updateCoordinates(lat, lng) {
    document.getElementById('txtLatitude').value = lat;
    document.getElementById('txtLongitude').value = lng;
}

function RestStHealth() {
    document.getElementById("title").innerHTML = lbAddNewStudentHealth;
    document.getElementById("btnSaveStHealth").value = lbbtnSave;

    document.getElementById("StHealthId").value = null;
    document.getElementById("StHealthName").value = "";
}
function EditStHealth (id, name) {
    document.getElementById("title").innerHTML = lbAddNewStNationality;
    document.getElementById("btnSaveStHealth").value = lbbtnSave;

    document.getElementById("StHealthId").value = id;
    document.getElementById("StHealthName").value = name;
}
function DelHealth(id) {
    Swal.fire({
        title: lbTitleMsgDeleteHealth,
        text: lbTextMsgDeleteHealth,
        icon: 'error',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonText, 
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Students/DeleteHealthCondition?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkStudentHealth,
                lbSuccess
            )
        }
    })
}

function RestStNationality() {
    document.getElementById("title").innerHTML = lbAddNewStudentNat;
    document.getElementById("btnSaveStNationality").value = lbbtnSave;

    document.getElementById("StNationalityId").value = null;
    document.getElementById("StNationalityName").value = "";
}
function DelNationality(id) {
    Swal.fire({
        title: lbTitleMsgDeleteNat,
        text: lbTextMsgDeleteNat,
        icon: 'error',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Students/DeleteNationality?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkStudentNat,
                lbSuccess
            )
        }
    })
}
function SelectCategory (){
    // مقيم | مواطن
    if (document.getElementById("stCategory").selectedIndex == "1") {
        document.getElementById('stIdentitynum').type = "number";
        document.getElementById('stParentIdentitynum').type = "number";
        document.getElementById('stIdentitynum').readOnly = false;
        document.getElementById('stParentIdentitynum').readOnly = false;
    }
    // زائر
    else if (document.getElementById("stCategory").selectedIndex == "2") {
        document.getElementById('stIdentitynum').type = "text";
        document.getElementById('stParentIdentitynum').type = "text";
        document.getElementById('stIdentitynum').readOnly = false;
        document.getElementById('stParentIdentitynum').readOnly = false;
    }
} 
function SelectCategoryR() {
    // مقيم | مواطن
    if (document.getElementById("stCategoryR").selectedIndex == "1") {
        document.getElementById('stIdentitynumR').type = "number";
        document.getElementById('stParentIdentitynumR').type = "number";
        document.getElementById('stIdentitynumR').readOnly = false;
        document.getElementById('stParentIdentitynumR').readOnly = false;
    }
    // زائر
    else if (document.getElementById("stCategoryR").selectedIndex == "2") {
        document.getElementById('stIdentitynumR').type = "text";
        document.getElementById('stParentIdentitynumR').type = "text";
        document.getElementById('stIdentitynumR').readOnly = false;
        document.getElementById('stParentIdentitynumR').readOnly = false;
    }
} 


function RequestTransfer(id, name, episode) {
    document.getElementById("title").innerHTML = lbTitleEditStudent;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("trstId").value = id;
    document.getElementById("trname").value = name;
    document.getElementById("trepisode").value = episode;
    //document.getElementById("trcenter").value = center;
    //initMap(Number(latidude), Number(longitude));
    initMap();
}
function RequestVacation(id, name, episode) {
    document.getElementById("title").innerHTML = lbTitleEditStudent;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("vastId").value = id;
    document.getElementById("vaname").value = name;
    document.getElementById("vaepisode").value = episode;
    //document.getElementById("vacenter").value = center;
}
function RequestAsk(id, name, episode) {
    document.getElementById("title").innerHTML = lbTitleEditStudent;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("askstId").value = id;
    document.getElementById("askname").value = name;
    document.getElementById("askepisode").value = episode;
}

function RequestComplaint(id, name, episode) {
    document.getElementById("title").innerHTML = lbTitleEditStudent;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("costId").value = id;
    document.getElementById("coname").value = name;
    document.getElementById("coepisodeNew").value = episode;
    document.getElementById("coepisodeOld").value = episode;
}
function RequestEditPersonal(id, name, identityNum,phone,adress,notes) {
    document.getElementById("title").innerHTML = lbTitleEditStudent;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("editstId").value = id;
    document.getElementById("editname").value = name;
    document.getElementById("editstIdentityNum").value = identityNum;
    document.getElementById("editprPhoneNum").value = phone;
    document.getElementById("editstAdress").value = adress;
    document.getElementById("editstNotes").value = notes;
}

GetLimitStudent = (lim) => {
    for (c = 0; c < lim; c++) {
        if (document.getElementById("stEpisodeId").value == document.getElementById("eplimit_" + c).value) {
            document.getElementById("selectlimit").value = document.getElementById('eplimit_' + c).getAttribute("limit");
            document.getElementById("selectin").value = (document.getElementById('eplimit_' + c).getAttribute("limit")) - (document.getElementById('eplimit_' + c).getAttribute("epin"));
        }
    }
    if (Number(document.getElementById("selectin").value) == 0) {
        document.getElementById("stateLimit").value = "عدد طلاب الحلقة مكتمل";
        //document.getElementById("stEpisodeId").value = null;
    }  
    else
        document.getElementById("stateLimit").value = "";
}


SelectSaveDir = () => {
    if (document.getElementById("saveDir").selectedIndex == "1") // ctp
    {
        document.getElementById("lblsurah_ctp").style.visibility = "visible";
        document.getElementById("surahId_ctp").style.visibility = "visible";
        document.getElementById("surahId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").style.visibility = "hidden";

        document.getElementById("lblsurah_ptc").style.visibility = "hidden";
        document.getElementById("surahId_ptc").style.visibility = "hidden";
        document.getElementById("surahId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").style.visibility = "hidden";
        document.getElementById("sdir").value = "ctp";

    }
    else if (document.getElementById("saveDir").selectedIndex == "2") // ptc
    {
        document.getElementById("lblsurah_ctp").style.visibility = "hidden";
        document.getElementById("surahId_ctp").style.visibility = "hidden";
        document.getElementById("surahId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").style.visibility = "hidden";

        document.getElementById("lblsurah_ptc").style.visibility = "visible";
        document.getElementById("surahId_ptc").style.visibility = "visible";
        document.getElementById("surahId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").style.visibility = "hidden";
        document.getElementById("sdir").value = "ptc";
    }
    else {
        document.getElementById("lblsurah_ctp").style.visibility = "hidden";
        document.getElementById("surahId_ctp").style.visibility = "hidden";
        document.getElementById("surahId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").style.visibility = "hidden";

        document.getElementById("lblsurah_ptc").style.visibility = "hidden";
        document.getElementById("surahId_ptc").style.visibility = "hidden";
        document.getElementById("surahId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").style.visibility = "hidden";
        document.getElementById("sdir").value = "ptc";
    }
}

SelectPlan = () => {
    if (document.getElementById("stPlanId").selectedIndex == "2") { // الذكر الحكيم
        document.getElementById("lblLevel").style.visibility = "visible";
        document.getElementById("lblLevel").innerHTML = "اتجاه الحفظ";
        document.getElementById("saveDir").style.visibility = "visible";
        document.getElementById("saveDir").selectedIndex = "2";
        document.getElementById("saveDir").disabled = true;

        document.getElementById("lblzhLesson").style.visibility = "visible";
        document.getElementById("lblzhLesson").innerHTML = "اسم الدرس";
        document.getElementById("stLevelId").style.visibility = "visible";
        document.getElementById("stLevelId").selectedIndex = "0";
        document.getElementById("sdir").value = "ptc";
    }
    else {
        document.getElementById("lblLevel").style.visibility = "visible";
        document.getElementById("lblLevel").innerHTML = "اتجاه الحفظ";
        document.getElementById("saveDir").style.visibility = "visible";
        document.getElementById("saveDir").selectedIndex = "0";
        document.getElementById("saveDir").disabled = false;

        document.getElementById("lblzhLesson").style.visibility = "hidden";
        document.getElementById("lblzhLesson").innerHTML = "اسم الدرس";
        document.getElementById("stLevelId").style.visibility = "hidden";
        document.getElementById("stLevelId").selectedIndex = "0";
        SelectSaveDir();
    }
}

GetVerseIdValue_ctp = () => {
    var e = document.getElementById("verseId_ctp");
    document.getElementById("verseNum").value = e.options[e.selectedIndex].text;
}
GetVerseIdValue_ptc = () => {
    var e = document.getElementById("verseId_ptc");
    document.getElementById("verseNum").value = e.options[e.selectedIndex].text;
}
VisableVerse = () => {
    if (document.getElementById("surahId_ctp").style.visibility == "visible") {
        document.getElementById("verseId_ctp").style.visibility = "visible";
        document.getElementById("verseId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").innerHTML = "الآية";
        document.getElementById("verseId_ptc").style.visibility = "hidden";
        document.getElementById("verseId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").innerHTML = "الآية";
        document.getElementById("verseNum").value = "1";
        document.getElementById('surahName').value = document.getElementById('surahId_ctp').options[document.getElementById('surahId_ctp').selectedIndex].text;

    }
    else if (document.getElementById("surahId_ptc").style.visibility == "visible") {
        document.getElementById("verseId_ptc").style.visibility = "visible";
        document.getElementById("verseId_ptc").selectedIndex = "0";
        document.getElementById("verseId_ptc").innerHTML = "الآية";
        document.getElementById("verseId_ctp").style.visibility = "hidden";
        document.getElementById("verseId_ctp").selectedIndex = "0";
        document.getElementById("verseId_ctp").innerHTML = "الآية";
        document.getElementById("verseNum").value = "1";
        document.getElementById('surahName').value = document.getElementById('surahId_ptc').options[document.getElementById('surahId_ptc').selectedIndex].text;


    }
    
   
}