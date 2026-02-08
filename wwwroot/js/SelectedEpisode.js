CheckDepature = (c, st) => {
    var temp = 0;
    var finalResult = true;
    var result_save = document.getElementById('saveEv_' + c).value;
    var result_rabt = document.getElementById('rabtEv_' + c).value;
    var result_rev = document.getElementById('revEv_' + c).value;
    var result_notes = document.getElementsByName("notes_" + c).value;

    var attends = document.getElementsByName("att_" + c);
    var result_attend = document.getElementsByName("att_" + c)[0].value;
    var lessonstate = false;

    for (var i = 0; i < attends.length; i++) {
        if (attends[i].checked) {
            result_attend = attends[i].value;

            
            if (attends[i].value > 2) {
                document.getElementById('stAtt_' + c).value = attends[i].value;
                document.getElementById('saveEv_' + c).selectedIndex = 0;
                document.getElementById('rabtEv_' + c).selectedIndex = 0;
                document.getElementById('revEv_' + c).selectedIndex = 0;

                document.getElementById('saveEv_' + c).disabled = true;
                document.getElementById('rabtEv_' + c).disabled = true;
                document.getElementById('revEv_' + c).disabled = true;
                //document.getElementById('saveTemp').disabled = false;
                result_save = 0;
                result_rabt = 0;
                result_rev = 0;

                result_notes = document.getElementById('notes_' + c).value;
            }
            else if (attends[i].value <= 2) {
                document.getElementById('stAtt_' + c).value = attends[i].value;
                document.getElementById('saveEv_' + c).disabled = false;
                document.getElementById('rabtEv_' + c).disabled = false;
                document.getElementById('revEv_' + c).disabled = false;
                document.getElementById('notes_' + c).disabled = false;

                result_save = -1;
                result_rabt = -1;
                result_rev = -1;

                result_save = document.getElementById('saveEv_' + c).value;
                result_rabt = document.getElementById('rabtEv_' + c).value;
                result_rev = document.getElementById('revEv_' + c).value;
                result_notes = document.getElementById('notes_' + c).value;

                if (result_rabt < 5 && result_rabt > 0) {

                    temp = result_rabt;
                    document.getElementById('saveEv_' + c).disabled = true;
                }
                else {
                    temp = document.getElementById('saveEv_' + c).value;
                    document.getElementById('saveEv_' + c).disabled = false;
                }
                result_save = temp;
                document.getElementById('saveEv_' + c).value = temp;
            }
            else if (result_save == -1 && result_rabt == -1 && result_rev == -1) {
                result_attend = attends[i].value;
                result_save = -1;
                result_rabt = -1;
                result_rev = -1;
            }
            else {
                result_attend = attends[i].value;
                result_save = -1;
                result_rabt = -1;
                result_rev = -1;
            }

            if (result_save > 4 && result_rabt > 4 && result_rev > 4) {
                //TODO : new save & new rabt & new rev
                lessonstate = true;
            }
            else if (result_save == -1 || result_rabt == -1 || result_rev == -1) {
                //Teacher not evaluate student
                lessonstate = false;
            }
            else {
                //TODO : same save & same rabt & same rev
                lessonstate = false;
            }

            if (result_save > -1 && result_rabt > -1 && result_rev > -1) {
                //document.getElementById('saveTemp').disabled = false;
                finalResult &&= true;
            }
            else {
                //document.getElementById('saveTemp').disabled = true;
                finalResult &&= false;
            }
            break;
        }
    }
    

    if (finalResult) {
        document.getElementById('depature_' + c).disabled = false;
        SelectAllBox(st);
    }

    else {
        document.getElementById('depature_' + c).disabled = true;
        SelectAllBox(st);
    }


    if (result_notes == null)
        result_notes = " ";

    document.getElementById('lessonstate_' + c).value = lessonstate;
    document.getElementById('attendEval_' + c).value = result_attend;
    document.getElementById('saveEval_' + c).value = result_save;
    document.getElementById('rabtEval_' + c).value = result_rabt;
    document.getElementById('revEval_' + c).value = result_rev;
    document.getElementById('notes_' + c).value = result_notes;
}
GCheckDepature = (c, st) => {

    var finalResult = true;
    var result_save = document.getElementById('GsaveEv_' + c).value;
    var result_rabt = 10;
    var result_rev = document.getElementById('GrevEv_' + c).value;
    var result_notes = document.getElementsByName("Gnotes_" + c).value;

    var attends = document.getElementsByName("Gatt_" + c);
    var result_attend = document.getElementsByName("Gatt_" + c)[0].value;
    var lessonstate = false;

    for (var i = 0; i < attends.length; i++) {
        if (attends[i].checked) {
            result_attend = attends[i].value;

            if (result_save == -1 && result_rabt == -1 && result_rev == -1) {
                result_attend = attends[i].value;
                result_save = -1;
                result_rabt = -1;
                result_rev = -1;
            }
            else if (attends[i].value > 2) {

                document.getElementById('GstAtt_' + c).value = attends[i].value;
                document.getElementById('GsaveEv_' + c).selectedIndex = 0;
                document.getElementById('GrevEv_' + c).selectedIndex = 0;

                document.getElementById('GsaveEv_' + c).disabled = true;
                document.getElementById('GrevEv_' + c).disabled = true;
                document.getElementById('Gnotes_' + c).disabled = true;
                //document.getElementById('saveTemp').disabled = false;

                result_save = 0;
                result_rev = 0;
                result_rabt = 10;
            }
            else if (attends[i].value <= 2) {
                document.getElementById('GstAtt_' + c).value = attends[i].value;
                document.getElementById('GsaveEv_' + c).disabled = false;
                document.getElementById('GrevEv_' + c).disabled = false;
                document.getElementById('Gnotes_' + c).disabled = false;
                //document.getElementById('saveTemp').disabled = false;

                result_save = -1;
                result_rev = -1;

                result_save = document.getElementById('GsaveEv_' + c).value;
                result_rev = document.getElementById('GrevEv_' + c).value;
                result_rabt = 10;
                result_notes = document.getElementById('Gnotes_' + c).value;
            }


            if (result_save > 4 && result_rev > 4) {
                //TODO : new save & new rabt & new rev
                lessonstate = true;
            }
            else if (result_save == -1 || result_rev == -1) {
                //Teacher not evaluate student
                lessonstate = false;
            }
            else {
                //TODO : same save & same rabt & same rev
                lessonstate = false;
            }


            if (result_save > -1 && result_rev > -1) {
                finalResult &&= true;
                //document.getElementById('saveTemp').disabled = false;
            }
            else {
                //document.getElementById('saveTemp').disabled = true;
                finalResult &&= false;
            }
            break;
        }
    }

    //alert("attend:" + result_attend + "--- save:" + result_save + "--- rabt:" + result_rabt + "--- rev:" + result_rev + "--- FINAL:" + finalResult)

    if (finalResult) {
        SelectAllBox(st);

        document.getElementById('depature_' + c).disabled = false;
    }

    else {
        SelectAllBox(st);

        document.getElementById('depature_' + c).disabled = true;
    }

    if (result_notes == null)
        result_notes = " ";

    document.getElementById('Glessonstate_' + c).value = lessonstate;
    document.getElementById('GattendEval_' + c).value = result_attend;
    document.getElementById('GsaveEval_' + c).value = result_save;
    document.getElementById('GrabtEval_' + c).value = result_rabt;
    document.getElementById('GrevEval_' + c).value = result_rev;
    document.getElementById('Gnotes_' + c).value = result_notes;

    document.getElementById('surahNameRev_' + c).value = document.getElementById('surahId_revFrom_' + c).options[document.getElementById('surahId_revFrom_' + c).selectedIndex].text;
    document.getElementById('surahNameRevTo_' + c).value = document.getElementById('surahId_revTo_' + c).options[document.getElementById('surahId_revTo_' + c).selectedIndex].text;
    document.getElementById('surahNameSave_' + c).value = document.getElementById('surahId_saveTo_' + c).options[document.getElementById('surahId_saveTo_' + c).selectedIndex].text;

    //document.getElementById('vrsRevFrom_' + c).value = document.getElementById('verseId_revFrom_' + c).options[document.getElementById('verseId_revFrom_' + c).selectedIndex].text;
    //document.getElementById('vrsRevTo_' + c).value = document.getElementById('verseId_revTo_' + c).options[document.getElementById('verseId_revTo_' + c).selectedIndex].text;
    //document.getElementById('vrsSaveTo_' + c).value = document.getElementById('verseId_saveTo_' + c).options[document.getElementById('verseId_saveTo_' + c).selectedIndex].text; 

    document.getElementById('vrsRevFrom_' + c).value = document.getElementById('verseNumRevFrom_' + c).value;
    document.getElementById('vrsRevTo_' + c).value = document.getElementById('verseNumRevTo_' + c).value;
    document.getElementById('vrsSaveTo_' + c).value = document.getElementById('verseNumSaveTo_' + c).value;


    document.getElementById('GRevSurahEval_' + c).value = document.getElementById('surahIdRevFrom_' + c).value;
    document.getElementById('GRevAyahEval_' + c).value = document.getElementById('verseNumRevFrom_' + c).value;

    document.getElementById('GRevSurahEvalTo_' + c).value = document.getElementById('surahIdRevTo_' + c).value;
    document.getElementById('GRevAyahEvalTo_' + c).value = document.getElementById('verseNumRevTo_' + c).value;

    document.getElementById('GSaveSurahEvalTo_' + c).value = document.getElementById('surahIdSaveTo_' + c).value;
    document.getElementById('GSaveAyahEvalTo_' + c).value = document.getElementById('verseNumSaveTo_' + c).value;


}

OCheckDepature = (c, st) => {

    var finalResult = true;
    var result_save = document.getElementById('OsaveEv_' + c).value;
    var result_rabt = 10;
    var result_rev = 10;
    var result_notes = document.getElementsByName("Onotes_" + c).value;
   

    var attends = document.getElementsByName("Oatt_" + c);
    var result_attend = document.getElementsByName("Oatt_" + c)[0].value;
    var lessonstate = false;

    for (var i = 0; i < attends.length; i++) {
        if (attends[i].checked) {
            result_attend = attends[i].value;
            if (result_save == -1 && result_rabt == -1 && result_rev == -1) {
                result_attend = attends[i].value;
                result_save = -1;
                result_rabt = -1;
                result_rev = -1;
            }
            else if (attends[i].value > 2) {

                document.getElementById('OstAtt_' + c).value = attends[i].value;
                document.getElementById('OsaveEv_' + c).selectedIndex = 0;

                document.getElementById('OsaveEv_' + c).disabled = true;
                document.getElementById('Onotes_' + c).disabled = true;
                //document.getElementById('saveTemp').disabled = false;

                result_save = 0;
                result_rev = 10;
                result_rabt = 10;
            }
            else if (attends[i].value <= 2) {
                document.getElementById('OstAtt_' + c).value = attends[i].value;
                document.getElementById('OsaveEv_' + c).disabled = false;
                document.getElementById('Onotes_' + c).disabled = false;
                //document.getElementById('saveTemp').disabled = false;
                result_save = -1;
                
                result_save = document.getElementById('OsaveEv_' + c).value;
                result_rev = 10;
                result_rabt = 10;
                result_notes = document.getElementById('Onotes_' + c).value;
            }
            else {
                result_attend = attends[i].value;
                result_save = -1;
                result_rabt = -1;
                result_rev = -1;
            }

            result_rev = 10;
            result_rabt = 10;

            if (result_save > 4 ) {
                //TODO : new save & new rabt & new rev
                lessonstate = true;
            }
            else if (result_save == -1) {
                //Teacher not evaluate student
                lessonstate = false;
            }
            else {
                //TODO : same save & same rabt & same rev
                lessonstate = false;
            }


            if (result_save > -1 ) {
                finalResult &&= true;
                //document.getElementById('saveTemp').disabled = false;
            }
            else {
                //document.getElementById('saveTemp').disabled = true;
                finalResult &&= false;
            }
            break;
        }
    }

    //alert("attend:" + result_attend + "--- save:" + result_save + "--- rabt:" + result_rabt + "--- rev:" + result_rev + "--- FINAL:" + finalResult)

    if (finalResult) {
        SelectAllBox(st);

        document.getElementById('depature_' + c).disabled = false;
    }

    else {
        SelectAllBox(st);

        document.getElementById('depature_' + c).disabled = true;
    }

    if (result_notes == null)
        result_notes = " ";

    document.getElementById('Olessonstate_' + c).value = lessonstate;
    document.getElementById('OattendEval_' + c).value = result_attend;
    document.getElementById('OsaveEval_' + c).value = result_save;
    document.getElementById('OrabtEval_' + c).value =10
    document.getElementById('OrevEval_' + c).value = 10
    document.getElementById('Onotes_' + c).value = result_notes;

   
    document.getElementById('surahNameSave_' + c).value = document.getElementById('surahId_saveTo_' + c).options[document.getElementById('surahId_saveTo_' + c).selectedIndex].text;

   
    document.getElementById('vrsSaveTo_' + c).value = document.getElementById('verseNumSaveTo_' + c).value;

    document.getElementById('OSaveSurahEvalTo_' + c).value = document.getElementById('surahIdSaveTo_' + c).value;
    document.getElementById('OSaveAyahEvalTo_' + c).value = document.getElementById('verseNumSaveTo_' + c).value;


}

GetPrevRevFrom = (e) => {
    document.getElementById("prevRevData_From").value = e.target.value;
}
SelectAllBox = (st) => {
    var inputElems = document.getElementsByName("depature"),
        activ = 0;
    for (var i = 0; i < inputElems.length; i++) {
        if (inputElems[i].type === "checkbox" && inputElems[i].disabled == false) {
            activ++;
        }
    }

    if (activ === st) {
        document.getElementById('select-all').disabled = false;

    }
    else {
        document.getElementById('select-all').disabled = true;

    }
}
ActiveAll = (st) => {
    if (document.getElementById('select-all').disabled == false && document.getElementById('select-all').checked == true) {
        document.getElementById('finish').disabled = false;
        document.getElementById('stcount').innerHTML = st;
    }
    else {
        document.getElementById('finish').disabled = true;
        document.getElementById('stcount').innerHTML = 0;
    }

}
EvaluateBoxes = (st) => {

    var inputElems = document.getElementsByName("depature"),
        count = 0;
    for (var i = 0; i < inputElems.length; i++) {
        if (inputElems[i].type === "checkbox" && inputElems[i].checked === true) {
            count++;
        }
    }

    document.getElementById('stcount').innerHTML = count;
    if (count === st) {
        document.getElementById('finish').disabled = false;
        document.getElementById('select-all').checked = true;
    }
    else {
        document.getElementById('finish').disabled = true;
        document.getElementById('select-all').checked = false;
    }


}
GetPrevRevTo = (e) => {
    document.getElementById("prevRevData_To").value = e.target.value;
}
GetVerseIdValue = () => {
    var e = document.getElementById("verseId");
    document.getElementById("verseNum").value = e.options[e.selectedIndex].text;
}
SelectedRatio = (total) => {
    for (var c = 0; c < total; c++) {
        if (document.getElementById('plancheck_' + c).value == "zh") {

            if (document.getElementById('stAtt_' + c).value == 0) {
                document.getElementById('att0_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'حاضر';
                document.getElementById('notify_' + c).style.backgroundColor = '#CBF7DC';
            }

            else if (document.getElementById('stAtt_' + c).value == 1) {
                document.getElementById('att1_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'متأخر';
                document.getElementById('notify_' + c).style.backgroundColor = '#FFF085';
            }

            else if (document.getElementById('stAtt_' + c).value == 2) {
                document.getElementById('att2_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'مستأذن';
                document.getElementById('notify_' + c).style.backgroundColor = 'LightSalmon';
            }

            else if (document.getElementById('stAtt_' + c).value == 3) {
                document.getElementById('att3_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'غائب بإذن';
                document.getElementById('notify_' + c).style.backgroundColor = 'Silver';
            }

            else if (document.getElementById('stAtt_' + c).value == 4) {
                document.getElementById('att4_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'غائب';
                document.getElementById('notify_' + c).style.backgroundColor = 'Silver';
            }

        }
        else if (document.getElementById('plancheck_' + c).value == "gp") {
            if (document.getElementById('GstAtt_' + c).value == 0) {
                document.getElementById('Gatt0_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'حاضر';
                document.getElementById('notify_' + c).style.backgroundColor = '#CBF7DC';
            }

            else if (document.getElementById('GstAtt_' + c).value == 1) {
                document.getElementById('Gatt1_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'متأخر';
                document.getElementById('notify_' + c).style.backgroundColor = '#FFF085';
            }

            else if (document.getElementById('GstAtt_' + c).value == 2) {
                document.getElementById('Gatt2_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'مستأذن';
                document.getElementById('notify_' + c).style.backgroundColor = 'LightSalmon';
            }

            else if (document.getElementById('GstAtt_' + c).value == 3) {
                document.getElementById('Gatt3_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'غائب بإذن';
                document.getElementById('notify_' + c).style.backgroundColor = 'Silver';
            }

            else if (document.getElementById('GstAtt_' + c).value == 4) {
                document.getElementById('Gatt4_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'غائب';
                document.getElementById('notify_' + c).style.backgroundColor = 'Silver';
            }

        }
        else if (document.getElementById('plancheck_' + c).value == "op") {
            if (document.getElementById('OstAtt_' + c).value == 0) {
                document.getElementById('Oatt0_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'حاضر';
                document.getElementById('notify_' + c).style.backgroundColor = '#CBF7DC';
            }

            else if (document.getElementById('OstAtt_' + c).value == 1) {
                document.getElementById('Oatt1_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'متأخر';
                document.getElementById('notify_' + c).style.backgroundColor = '#FFF085';
            }

            else if (document.getElementById('OstAtt_' + c).value == 2) {
                document.getElementById('Oatt2_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'مستأذن';
                document.getElementById('notify_' + c).style.backgroundColor = 'LightSalmon';
            }

            else if (document.getElementById('OstAtt_' + c).value == 3) {
                document.getElementById('Oatt3_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'غائب بإذن';
                document.getElementById('notify_' + c).style.backgroundColor = 'Silver';
            }

            else if (document.getElementById('OstAtt_' + c).value == 4) {
                document.getElementById('Oatt4_' + c).checked = true;
                document.getElementById('notify_' + c).innerText = 'غائب';
                document.getElementById('notify_' + c).style.backgroundColor = 'Silver';
            }

        }
    }

}

window.onload = function () {
    //students count | save temp btn | evaluate box
    var inputElems = document.getElementsByName("depature");
    total = inputElems.length;


    SelectedRatio(total);

    for (var c = 0; c < total; c++) {
        if (document.getElementById('plancheck_' + c).value === "gp") {
            //check new lesson state T/F for every student [طلاب الخطة العامة]
            GCheckDepature(c, total);
        }
        if (document.getElementById('plancheck_' + c).value === "op") {
            //check new lesson state T/F for every student [طلاب الخطة المفتوحة]
            OCheckDepature(c, total);
        }
        else if (document.getElementById('plancheck_' + c).value === "zh") { 
            //check new lesson state T/F for every student [طلاب الذكر الحكيم]
            CheckDepature(c, total);
        }
            if (document.getElementById('iseval_' + c).value === 'True') {
                //document.getElementById('saveTemp').disabled = false;
               // document.getElementById('Gdepature_' + c).disabled = false;
            document.getElementById('depature_' + c).disabled = false;
            
        }
    }
    SelectAllBox(total);
}











//window.onload = function () {
//    //Timer count
//    var start = new Date().getTime();
//    var end = new Date(document.getElementById("epEndTime").value).getTime();

//    var diff = end - start;
//    var hours = Math.floor((diff) / (60 * 60 * 1000)%(60));
//    var minutes = Math.floor((diff) / (60 * 1000) % (60));
//    var seconds = 10;

//    let myVar = setInterval(function () {
//        document.getElementById("timerCount").innerHTML = seconds + " : " + minutes + " : " + hours;

//        seconds--;
//        if (seconds == 0){
//            seconds = 59;
//            minutes--;

//            if (minutes == 0) {
//                if (hours == 0) {
//                    document.getElementById("form_data").submit();
//                }
//                else {
//                    minutes = 59;
//                    hours--;
//                }
//            }
//        }
//    }, 1000);
//}
//SaveEvaluation = () => {
//    document.getElementById("form_data").submit();
//}
//FinishedEvaluation = () => {
//    document.getElementById("form_data").submit();
//    document.getElementById('finish').disabled = true;
//    document.getElementById('manualFinshedEpisode').disabled = false;
//}


//FinishedEpisode = () => {
//    //document.getElementById('manualFinshedEpisode').disabled = true;
//    document.getElementById("form_finishEpisode").submit();
//}


