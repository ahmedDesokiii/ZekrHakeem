
$(document).ready(function () {
    $('#tableStudentsWithEpProgs').DataTable({
        "autoWidth": true,
        "responsive": true,

        paging: false, // Disables pagination
        //searching : false,
        "language": {"sSearch": "بحث:"}
    });
    
});

window.onload = function () {
    document.getElementById('epProgForStudents').selectedIndex = 0;

    CountCheckBox();
    GetStudentsList();
    StudentLastAssigned();
}

GetStudentsList = () => {
    if (document.getElementById('epProgForStudents').selectedIndex == 0) {
        document.getElementById('listStus').style.visibility = "hidden";
        document.getElementById('selectedEpProgId').value = null;

    } else { 
        document.getElementById('listStus').style.visibility = "visible";
        document.getElementById('selectedEpProgId').value = document.getElementById('epProgForStudents').value;

    }    
}

StudentLastAssigned = () => {
    var inputElems = document.getElementsByName("checkerProg");
    for (var i = 1; i <= inputElems.length; i++) {
        if (document.getElementById('inProgState_' + i).value == "True" && document.getElementById('user').value == "Teacher") {
            document.getElementById('stn_' + i).style.color = "#ff0000";
            document.getElementById('stepp_' + i).style.color = "#ff0000";
            document.getElementById('stp_' + i).style.color = "#ff0000";
            //document.getElementById('stt_' + i).style.color = "#ff0000";

            document.getElementById('stProgCheck_' + i).disabled = true;

        }
        else if (document.getElementById('inProgState_' + i).value == "True" && document.getElementById('user').value == "NoTeacher") {
            document.getElementById('stn_' + i).style.color = "#ff0000";
            document.getElementById('stepp_' + i).style.color = "#ff0000";
            document.getElementById('stp_' + i).style.color = "#ff0000";
            //document.getElementById('stt_' + i).style.color = "#ff0000";

            document.getElementById('stProgCheck_' + i).disabled = false;

        }
        else {
            document.getElementById('stProgCheck_' + i).checked = false;
        }
    }
}

Reset = () => {
    document.getElementById('stName').value = "";
    document.getElementById('stParentPhonenum').value = "";
    document.getElementById('stGender').selectedIndex = 0;
    document.getElementById('stNationalityId').selectedIndex = 0;
    document.getElementById('stEpProgId').value = document.getElementById('epProgForStudents').value;



}

Edit = (id, name, pphone, nation, gender,) => {
    document.getElementById('stId').value = id;
    document.getElementById('stName').value = name;
    document.getElementById('stParentPhonenum').value = pphone;

    document.getElementById('stNationalityId').value = nation;

    if (gender == "male")
        document.getElementById('stGender').selectedIndex = 0;
    else if (gender == "female")
        document.getElementById('stGender').selectedIndex = 1;

    document.getElementById('stEpProgId').value = document.getElementById('epProgForStudents').value;
}

CheckProgState = (Total) => {

    CountCheckBox();
    for (var i = 1; i < Total; i++) {

        if (document.getElementById('stProgCheck_' + i).checked == true ) {
            document.getElementById('inProgState_' + i).value = true;
        }
        else
            document.getElementById('inProgState_' + i).value = false;
    }
}

CountCheckBox = () => {
    let checkboxes = document.querySelectorAll('input[type="checkbox"]');
    let checkedCheckboxes = Array.from(checkboxes).filter(checkbox => checkbox.checked);
    let count = checkedCheckboxes.length;
    if (count == 0)
        document.getElementById('assignStudents').disabled = true;
    else
        document.getElementById('assignStudents').disabled = false;
}


  