$(document).ready(function () {
    $('#tableUser').DataTable({
        "autoWidth": true,
        "responsive": true
    });
});
//function Delete(id) {
//    Swal.fire({
//        title: lbTitleMsgDelete,
//        text: lbTextMsgDelete,
//        icon: 'error',
//        showCancelButton: true,
//        confirmButtonColor: '#d33',
//        cancelButtonColor: 'secondary',
//        confirmButtonText: lbconfirmButtonText,
//        cancelButtonText: lbcancelButtonText,

//    }).then((result) => {
//        if (result.isConfirmed) {
//            window.location.href = `/Users/DeleteUser?Id=${id}`;
//        }
//    })
//}

//Edit = (id, name) => {
//    document.getElementById("title").innerHTML = lbTitleEdit;
//    document.getElementById("btnSave").value = lbEdit;
//    document.getElementById("UserId").value = id;
//    document.getElementById("UserName").value = name;

//}

//Rest = () => {
//    document.getElementById("title").innerHTML = lbAddNewUser;
//    document.getElementById("btnSave").value = lbbtnSave;
//    document.getElementById("UserId").value = "";
//    document.getElementById("UserName").value = "";

//}