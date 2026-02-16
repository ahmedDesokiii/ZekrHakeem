

if (!$.fn.DataTable.isDataTable('#tableRole')) {
    $('#tableRole').DataTable({
        "ordering": false, // يمنع الترتيب التلقائي
        columnControl: {
            target: 1,
            content: ['search']
        },
        "autoWidth": true,
        "responsive": true,
        layout: {
            topStart: {
                search: {
                    placeholder: 'ابحث هنا... !'
                }
            },
            topEnd: {
                pageLength: {
                    menu: [10, 25, 50, 100, 200, 400, 1000]
                }
            },
            bottomEnd: {
                paging: {
                    buttons: 6
                }
            }
        },
        language: {
            search: ' <i class="bi bi-search"></i>',
            lengthMenu: ' _MENU_ ',
            info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ سجل',
            zeroRecords: 'لم يعثر على أي سجل',
            paginate: {
                first: 'الأول',
                previous: 'السابق',
                next: 'التالي',
                last: 'الأخير'
            }
        }
    });
}

function Delete(id) {
    Swal.fire({
        title: lbTitleMsgDelete,
        text: lbTextMsgDelete,
        icon: 'error',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#BFC6C4',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText,
        
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Roles/DeleteRole?Id=${id}`;
        }
    })
}

Edit = (id, name) => {
    //document.getElementById("title").innerHTML = lbTitleEdit;
   // document.getElementById("btnSave").value = lbEdit;
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}

Rest = () => {
    //document.getElementById("title").innerHTML = lbAddNewRole;
    //document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";

}