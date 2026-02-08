

// DataTables initialisation
var table = $('#exampleDailyEval').DataTable({
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


// Create chart
const chart = Highcharts.chart('demo-output', {
    chart: {
        backgroundColor: '#FBF8EF !important',
        type: 'pie',
        styledMode: true,
    },
    title: {
        text: ' الطلاب داخل الحلقات'
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b> <br> {point.percentage:.1f} %'
            }
        }
    },

    series: [{
        name: 'طلاب الحلقة ',
        colorByPoint: true,
        data: chartData(table)
    }]

});

chart.Highcharts


const chart2 = Highcharts.chart('demo-output2', {
    chart: {
        backgroundColor: '#FBF8EF !important',
        polar: true,

        type: 'pie',
        colorByPoint: true,
        //type: 'column',
        //styledMode: true
    },
    title: {
        text: '  علاقة الطلاب بالخطط'
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b> <br> {point.percentage:.1f} %'
            }
        }
    },

    series: [
        {
            name: 'طلاب الخطة',
            colorByPoint: true,
            data: chartData2(table)
        }
    ]
});



// On each draw, update the data in the chart
table.on('draw', function () {
    chart.series[0].setData(chartData(table));

    chart.series[2].setData(chartData2(table));

});

function chartData(table) {
    var counts = {};

    // Count the number of entries for each position
    table
        .column(13, { search: 'applied' })
        .data()
        .each(function (val) {
            if (counts[val]) {
                counts[val] += 1;
            }
            else {
                counts[val] = 1;
            }
        });

    return Object.entries(counts).map((e) => ({

        name: e[0],
        y: e[1]
    }));
}

function chartData2(table) {
    var counts = {};

    // Count the number of entries for each position
    table
        .column(12, { search: 'applied' })
        .data()
        .each(function (val) {
            if (counts[val]) {
                counts[val] += 1;
            }
            else {
                counts[val] = 1;
            }
        });

    return Object.entries(counts).map((e) => ({
        name: e[0],
        y: e[1]
    }));
}


