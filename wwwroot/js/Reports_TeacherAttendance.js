
//$(document).ready(function () {


//Teacher Attendance Report
let minDateTea, maxDateTea;

// Custom filtering function which will search data in column four between two values
DataTable.ext.search.push(function (settings, data, dataIndex) {
    let minTea = minDateTea.val();
    let maxTea = maxDateTea.val();
    let dateTea = new Date(data[1]);

    if (
        (minTea === null && maxTea === null) ||
        (minTea === null && dateTea <= maxTea) ||
        (minTea <= dateTea && maxTea === null) ||
        (minTea <= dateTea && dateTea <= maxTea)
    ) {
        return true;
    }
    return false;
});

// Create date inputs
minDateTea = new DateTime('#minTea', {
    format: 'MMMM Do YYYY'
});
maxDateTea = new DateTime('#maxTea', {
    format: 'MMMM Do YYYY'
});



$('#exampleTea').dataTable({
    "autoWidth": true,
    "responsive": true,
    "displaylength": 25,
    layout: {
        topStart: {
            search: {
                placeholder: 'بحث عن الحضور والغياب...'
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
        "autoWidth": true,
        "responsive": true,
        search: 'بحث', // Customize the search input label
        lengthMenu: ' _MENU_ ', // Customize the "Show X entries" label
        info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ بيان', // Customize the info text
        zeroRecords: 'لم يعثر على أية سجلات', // No records message
        paginate: {
            first: 'الأول',
            previous: 'السابق',
            next: 'التالي',
            last: 'الأخير'
        }
    }
});


// DataTables initialisation
//let tableTea = new DataTable('#exampleTea');

// Refilter the table
document.querySelectorAll('#minTea, #maxTea').forEach((el) => {
    el.addEventListener('change', () => tableTea.draw());
});