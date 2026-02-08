$(document).ready(function () {
    $('#tableRole').DataTable({
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
        cancelButtonText: lbcancelButtonText,
        
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Roles/DeleteRole?Id=${id}`;
        }
    })
}

Edit = (id, name) => {
    document.getElementById("title").innerHTML = lbTitleEdit;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;

}

Rest = () => {
    document.getElementById("title").innerHTML = lbAddNewRole;
    document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";

}