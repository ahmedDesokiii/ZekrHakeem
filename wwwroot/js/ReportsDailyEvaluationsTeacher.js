

// DataTables initialisation
var table = $('#exampleDailyEvalTeacher').DataTable({
    orderCellsTop: true,
    layout: {
        topStart: {
            search: {
                placeholder: ''
            } 
        },
        topEnd: {
            pageLength: {
                menu: [10, 25, 50, 100, 200,400 ,1000]
            }
        },
        bottomEnd: {
            paging: {
                buttons: 6
            }
        }
    },
    "autoWidth": true,
    "language": {
        "sSearch": "بحث:", "sLengthMenu": "_MENU_"
    },
    displayLength: 25,

    footerCallback: function (row, data, start, end, display) {
        let api = this.api();

        // Remove the formatting to get integer data for summation
        let intVal = function (i) {
            return typeof i === 'string'
                ? i.replace(/[\$,]/g, '') * 1
                : typeof i === 'number'
                    ? i
                    : 0;
        };

       //----------------------------------------------------------
        var totalRows = api.rows({ page: 'current' }).count();
        //----------------------------------------------------------

        // Update footer
        $(api.column(0).footer()).html(totalRows);
            
    }
});

