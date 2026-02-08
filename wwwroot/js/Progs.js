
$(document).ready(function () {
        new DataTable('#tableProg')({
        "autoWidth": true,
        "responsive": true,
        //"searching":false
        "language": {"sSearch": "بحث:","sLengthMenu": "_MENU_",}
    });
        new DataTable('#tableLogProg')({
        "autoWidth": true,
        "responsive": true
    });
});
function PauseProg(id) {
    Swal.fire({
        title: lbTitleMsgPause,
        text: lbTextMsgPause,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonTextPause,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Progs/Pause?Id=${id}`;
            Swal.fire(
                lbTitlePausedOk,
                lbMsgPausedOkProg,
                lbSuccess
            )
        }
    })
}
function ResumeProg(id) {
    Swal.fire({
        title: lbTitleMsgResume,
        text: lbTextMsgResume,
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#228B22',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonTextResume,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Progs/Resume?Id=${id}`;
            Swal.fire(
                lbTitleResumedOk,
                lbMsgResumedOkProg,
                lbSuccess
            )
        }
    })
}


function DeleteProg(id) {
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
            window.location.href = `/Progs/Delete?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkProg,
                lbSuccess
            )
        }
    })
}
function DeleteLogProg(id) {

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
            window.location.href = `/Progs/DeleteLog?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkProgLog,
                lbSuccess
            )
        }

    })
}
function SelectType() {
    if (document.getElementById('ProgType').selectedIndex == 1) {
        document.getElementById("ProgType").value = "men";
        document.getElementById("superW").style.visibility = "hidden";
        document.getElementById("superM").style.visibility = "visible";
        document.getElementById("lblsuperM").style.visibility = "visible";
        document.getElementById("lblsuperW").style.visibility = "hidden";
    }
    else if (document.getElementById('ProgType').selectedIndex == 2) {
        document.getElementById("ProgType").value = "women";
        document.getElementById("superM").style.visibility = "hidden";
        document.getElementById("superW").style.visibility = "visible";
        document.getElementById("lblsuperW").style.visibility = "visible";
        document.getElementById("lblsuperM").style.visibility = "hidden";
    }
}

function EditProg(id, name, type, start, end, plan, kind, description, appuserid)  {

    document.getElementById("title").innerHTML = lbTitleEditProg;
    document.getElementById("btnSave").value = lbEdit;

    document.getElementById("ProgId").value = id;
    document.getElementById("ProgName").value = name;
    document.getElementById("ProgPlanId").value = plan;
    document.getElementById("ProgStart").value = start;
    document.getElementById("ProgEnd").value = end;
    document.getElementById("ProgKind").value = kind;
    document.getElementById("ProgType").value = type;
    document.getElementById("ProgDescription").value = description;

    if (document.getElementById("ProgType").value == "men")
        document.getElementById("superM").value = appuserid;
    if (document.getElementById("ProgType").value == "women")
        document.getElementById("superW").value = appuserid;
}
function RestProg  ()  {
    document.getElementById("title").innerHTML = lbAddNewProg;
    document.getElementById("btnSave").value = lbbtnSave;

    document.getElementById("ProgId").value = null;
    document.getElementById("ProgName").value = "";
    document.getElementById("description").value = "";
    document.getElementById("ProgStart").value = "";
    document.getElementById("ProgEnd").value = "";
    document.getElementById("ProgPlanId").selectedIndex = 0;
    document.getElementById("ProgKind").value = "";
    document.getElementById("ProgKind").selectedIndex = 0;
    document.getElementById("ProgDescription").value = "";
    document.getElementById("ProgType").selectedIndex = 0;


}




