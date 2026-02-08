
$('#tableCenter').dataTable({
        "autoWidth": true,
        "responsive": true,
        //"searching":false
        "language": { "sSearch": "بحث:", "sLengthMenu": "_MENU_", }
    });
$('#tableLogCenter').dataTable({
        "autoWidth": true,
        "responsive": true
    });

function Pause(id) {
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
            window.location.href = `/Centers/Pause?Id=${id}`;
            Swal.fire(
                lbTitlePausedOk,
                lbMsgPausedOkCenter,
                lbSuccess
            )
        }
    })
}
function Resume(id) {
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
            window.location.href = `/Centers/Resume?Id=${id}`;
            Swal.fire(
                lbTitleResumedOk,
                lbMsgResumedOkCenter,
                lbSuccess
            )
        }
    })
}


function Delete(id) {
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
            window.location.href = `/Centers/Delete?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkCenter,
                lbSuccess
            )
        }
    })
}
function DeleteLog(id) {

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
            window.location.href = `/Centers/DeleteLog?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkCenterLog,
                lbSuccess
            )
        }

    })
}
function SelectType() {
    if (document.getElementById('CenterType').selectedIndex == 1) {
        document.getElementById("CenterType").value = "men";
        document.getElementById("superW").style.visibility = "hidden";
        document.getElementById("superM").style.visibility = "visible";
        document.getElementById("lblsuperM").style.visibility = "visible";
        document.getElementById("lblsuperW").style.visibility = "hidden";
        document.getElementById("selectedCenterType").value = "men";

    }
    else if (document.getElementById('CenterType').selectedIndex == 2) {
        document.getElementById("CenterType").value = "women";
        document.getElementById("superM").style.visibility = "hidden";
        document.getElementById("superW").style.visibility = "visible";
        document.getElementById("lblsuperW").style.visibility = "visible";
        document.getElementById("lblsuperM").style.visibility = "hidden";
        document.getElementById("selectedCenterType").value = "women";
    }
}
function Edit(id, name, type, description, appuserid) {

    document.getElementById("title").innerHTML = lbTitleEditCenter;
    document.getElementById("btnSave").value = lbEdit;

    document.getElementById("CenterId").value = id;
    document.getElementById("CenterName").value = name;
    document.getElementById("description").value = description;
    document.getElementById("Super").value = appuserid;

    if (type == "men") {
        document.getElementById("CenterType").selectedIndex = 1;
        document.getElementById("CenterType").disabled = true;
    }
    else if (type == "women") {
        document.getElementById("CenterType").selectedIndex = 2;
        document.getElementById("CenterType").disabled = true;
    }
}
function Rest() {
    document.getElementById("title").innerHTML = lbAddNewCenter;
    document.getElementById("btnSave").value = lbbtnSave;

    document.getElementById("CenterId").value = null;
    document.getElementById("CenterName").value = "";
    document.getElementById("description").value = "";
    document.getElementById("CenterType").disabled = false;
    document.getElementById("CenterType").selectedIndex = "0";
    document.getElementById("Super").selectedIndex = "0";



}




