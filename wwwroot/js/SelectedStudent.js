//Evaluations
let minDate, maxDate;

// Custom filtering function which will search data in column four between two values
DataTable.ext.search.push(function (settings, data, dataIndex) {
    let min = minDate.val();
    let max = maxDate.val();
    let date = new Date(data[1]);

    if (
        (min === null && max === null) ||
        (min === null && date <= max) ||
        (min <= date && max === null) ||
        (min <= date && date <= max)
    ) {
        return true;
    }
    return false;
});

// Create date inputs
minDate = new DateTime('#min', {
    format: 'MMMM Do YYYY'
});
maxDate = new DateTime('#max', {
    format: 'MMMM Do YYYY'
});

// DataTables initialisation
let table = new DataTable('#example');

// Refilter the table
document.querySelectorAll('#min, #max').forEach((el) => {
    el.addEventListener('change', () => table.draw());
});




//Demands
let minDateDem, maxDateDem;

// Custom filtering function which will search data in column four between two values
DataTable.ext.search.push(function (settings, data, dataIndex) {
    let minDem = minDateDem.val();
    let maxDem = maxDateDem.val();
    let dateDem = new Date(data[1]);

    if (
        (minDem === null && maxDem === null) ||
        (minDem === null && dateDem <= maxDem) ||
        (minDem <= dateDem && maxDem === null) ||
        (minDem <= dateDem && dateDem <= maxDem)
    ) {
        return true;
    }
    return false;
});

// Create date inputs
minDateDem = new DateTime('#minDem', {
    format: 'MMMM Do YYYY'
});
maxDateDem = new DateTime('#maxDem', {
    format: 'MMMM Do YYYY'
});

// DataTables initialisation
let tableDem = new DataTable('#exampleDem');

// Refilter the table
document.querySelectorAll('#minDem, #maxDem').forEach((el) => {
    el.addEventListener('change', () => tableDem.draw());
});