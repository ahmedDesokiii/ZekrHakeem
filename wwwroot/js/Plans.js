$(document).ready(function () {
        new DataTable('#tablePlan')({
        "autoWidth": true,
        "responsive": true,
        //"searching":false
        "language": {"sSearch": "بحث:","sLengthMenu": "_MENU_",}
    });
        new DataTable('#tableLogPlan')({
        "autoWidth": true,
        "responsive": true
    });
});




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
            window.location.href = `/Plans/Delete?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkPlan,
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
            window.location.href = `/Plans/DeleteLog?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkPlanLog,
                lbSuccess
            )
        }

    })
}

function Edit (id, name, description)  {

    document.getElementById("title").innerHTML = lbTitleEditPlan;
    document.getElementById("btnSave").value = lbEdit;

    document.getElementById("PlanId").value = id;
    document.getElementById("PlanName").value = name;
    document.getElementById("description").value = description;

    
}
function Rest  ()  {
    document.getElementById("title").innerHTML = lbAddNewPlan;
    document.getElementById("btnSave").value = lbbtnSave;

    document.getElementById("PlanId").value = null;
    document.getElementById("PlanName").value = "";
    document.getElementById("description").value = "";
   
}




