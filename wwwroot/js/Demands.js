


//// DataTables initialisation

//var table = new DataTable('#tableDistrict')({
//    orderCellsTop: true,
//    layout: {
//        topStart: {
//            search: {
//                placeholder: ''
//            }

//        },
//        topEnd: {
//            pageLength: {
//                menu: [10, 25, 50, 100, 200, 400, 1000]
//            }

//        },
//        bottomEnd: {
//            paging: {
//                buttons: 6
//            }
//        }
//    },

//    "autoWidth": true,
//    //"responsive": true,
//    //"searching":false
//    "language": {
//        "sSearch": "بحث:", "sLengthMenu": "_MENU_"
//    },
//    displayLength: 10,
//});

//$(document).ready(function () {
//    new DataTable('#tableDemands')({
//        "autoWidth": true,
//        "responsive": true,
//        //"searching":false
//        "language": { "sSearch": "بحث:", "sLengthMenu": "_MENU_", }
//    });
//});

new DataTable('#tableDemands', {
    //"autoWidth": true,
    //"responsive": true,
    layout: {
        topStart: {
            search: {
                placeholder: ' بحث ... '
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
        info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ طلب', // Customize the info text
        zeroRecords: 'لم يعثر على أية سجلات', // No records message
        paginate: {
            first: 'الأول',
            previous: 'السابق',
            next: 'التالي',
            last: 'الأخير'
        }
    }
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
            window.location.href = `/Districts/Delete?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkDistrict,
                lbSuccess
            )
        }
    })
}
//function DeleteLog(id) {

//    Swal.fire({
//        title: lbTitleMsgDeleteLog,
//        text: lbTextMsgDeleteLog,
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#d33',
//        cancelButtonColor: 'secondary',
//        confirmButtonText: lbconfirmButtonText,
//        cancelButtonText: lbcancelButtonText
//    }).then((result) => {
//        if (result.isConfirmed) {
//            window.location.href = `/Districts/DeleteLog?Id=${id}`;
//            Swal.fire(
//                lbTitleDeletedOk,
//                lbMsgDeletedOkDistrictLog,
//                lbSuccess
//            )
//        }

//    })
//}
function Edit(id, name, centerId, description) {


    document.getElementById("title").innerHTML = lbTitleEditDistrict;
    document.getElementById("btnSave").value = lbEdit;

    document.getElementById("DistrictId").value = id;
    document.getElementById("DistrictName").value = name;
    document.getElementById("description").value = description;
    document.getElementById("disCenterId").value = centerId;

}
function Rest() {
    document.getElementById("title").innerHTML = lbAddNewDistrict;
    document.getElementById("btnSave").value = lbbtnSave;

    document.getElementById("DistrictId").value = null;
    document.getElementById("DistrictName").value = "";
    document.getElementById("description").value = "";
    document.getElementById("disCenterId").SelectedIndex = "0";
}




