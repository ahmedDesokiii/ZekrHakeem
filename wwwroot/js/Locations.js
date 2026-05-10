/* =========================================================
   LOCATIONS PAGE SCRIPT
   ========================================================= */

let tableLocation = null;

let map = null;
let marker = null;

const DEFAULT_LOCATION = {
    lat: 25.0413431,
    lng: 37.2597054
};

/* =========================================================
   INIT
   ========================================================= */

$(document).ready(function () {

    initializeDataTable();

    initializeModalEvents();

    initializeLocationTypeEvents();

});


/* =========================================================
   DATATABLE
   ========================================================= */

function initializeDataTable() {

    if ($.fn.DataTable.isDataTable('#tableLocation')) {
        $('#tableLocation').DataTable().destroy();
    }

    tableLocation = $('#tableLocation').DataTable({

        ordering: false,

        autoWidth: false,

        responsive: true,

        pageLength: 10,

        layout: {

            topStart: {
                search: {
                    placeholder: 'بحث عن مسجد / دار ...'
                }
            },

            topEnd: {
                pageLength: {
                    menu: [10, 25, 50, 100, 200, 400, 1000]
                }
            },

            bottomEnd: {
                paging: {
                    buttons: 5
                }
            }
        },

        language: {

            search: '<i class="bi bi-search"></i>',

            lengthMenu: '_MENU_',

            info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ سجل',

            zeroRecords: 'لا توجد بيانات',

            emptyTable: 'لا توجد بيانات متاحة',

            paginate: {
                first: 'الأول',
                previous: 'السابق',
                next: 'التالي',
                last: 'الأخير'
            }
        },

        drawCallback: function () {

            $('.dataTables_paginate .pagination').addClass('pagination-sm');

        }

    });

}


/* =========================================================
   MODAL EVENTS
   ========================================================= */

function initializeModalEvents() {

    const modal = document.getElementById('locationModal');

    if (!modal)
        return;

    modal.addEventListener('shown.bs.modal', function () {

        setTimeout(() => {

            if (map) {

                google.maps.event.trigger(map, 'resize');

                const currentPosition = marker.getPosition();

                map.setCenter(currentPosition);

            }

        }, 200);

    });

    modal.addEventListener('hidden.bs.modal', function () {

        clearValidation();

    });

}


/* =========================================================
   TYPE EVENTS
   ========================================================= */

function initializeLocationTypeEvents() {

    $('#LocationType').on('change', function () {

        handleLocationType($(this).val());

    });

}


/* =========================================================
   ALERTS
   ========================================================= */

function showActionAlert({
    title,
    text,
    icon,
    confirmText,
    url,
    confirmColor = '#dc3545'
}) {

    Swal.fire({

        title: title,

        text: text,

        icon: icon,

        showCancelButton: true,

        confirmButtonColor: confirmColor,

        cancelButtonColor: '#6c757d',

        confirmButtonText: confirmText,

        cancelButtonText: lbcancelButtonText,

        reverseButtons: true

    }).then((result) => {

        if (result.isConfirmed) {

            showLoader();

            window.location.href = url;

        }

    });

}


function Pause(id) {

    showActionAlert({

        title: lbTitleMsgPause,

        text: lbTextMsgPause,

        icon: 'warning',

        confirmText: lbconfirmButtonTextPause,

        url: `/Locations/Pause?Id=${id}`

    });

}


function Resume(id) {

    showActionAlert({

        title: lbTitleMsgResume,

        text: lbTextMsgResume,

        icon: 'success',

        confirmText: lbconfirmButtonTextResume,

        confirmColor: '#198754',

        url: `/Locations/Resume?Id=${id}`

    });

}


function Delete(id) {

    showActionAlert({

        title: lbTitleMsgDelete,

        text: lbTextMsgDelete,

        icon: 'error',

        confirmText: lbconfirmButtonText,

        url: `/Locations/Delete?Id=${id}`

    });

}


function DeleteLog(id) {

    showActionAlert({

        title: lbTitleMsgDeleteLog,

        text: lbTextMsgDeleteLog,

        icon: 'warning',

        confirmText: lbconfirmButtonText,

        url: `/Locations/DeleteLog?Id=${id}`

    });

}


/* =========================================================
   FORM HELPERS
   ========================================================= */

function clearValidation() {

    $('.input-validation-error').removeClass('input-validation-error');

    $('.field-validation-error')
        .removeClass('field-validation-error')
        .addClass('field-validation-valid');

}


function resetFormFields() {

    $('#LocationId').val('');

    $('#LocationName').val('');

    $('#description').val('');

    $('#districtId').prop('selectedIndex', 0);

    $('#CenterIdSelect').prop('selectedIndex', 0);

    $('#LocationType')
        .prop('disabled', false)
        .prop('selectedIndex', 0);

}


/* =========================================================
   TYPE HANDLE
   ========================================================= */

function handleLocationType(type) {

    const centerSelect = $('#CenterIdSelect');

    centerSelect.val('');

    if (type === 'dar') {

        centerSelect.removeClass('men-center');

        centerSelect.addClass('women-center');

    }
    else if (type === 'mosque') {

        centerSelect.removeClass('women-center');

        centerSelect.addClass('men-center');

    }

}


/* =========================================================
   ADD NEW
   ========================================================= */

function Rest() {

    $('#title').html(`
        <i class="bi bi-building ms-2"></i>
        ${lbAddNewLocation}
    `);

    $('#btnSave').html(`
        <i class="bi bi-check-circle ms-2"></i>
        ${lbbtnSave}
    `);

    resetFormFields();

    initializeMap();

}


/* =========================================================
   EDIT
   ========================================================= */

function Edit(
    id,
    name,
    type,
    description,
    centerId,
    latitude,
    longitude,
    district
) {

    $('#title').html(`
        <i class="bi bi-pencil-square ms-2"></i>
        ${lbTitleEditLocation}
    `);

    $('#btnSave').html(`
        <i class="bi bi-check-circle ms-2"></i>
        ${lbEdit}
    `);

    $('#LocationId').val(id);

    $('#LocationName').val(name);

    $('#description').val(description);

    $('#districtId').val(district);

    $('#CenterIdSelect').val(centerId);

    $('#LocationType')
        .val(type)
        .prop('disabled', true);

    handleLocationType(type);

    initializeMap(latitude, longitude);

}


/* =========================================================
   GOOGLE MAP
   ========================================================= */

function initializeMap(
    lat = DEFAULT_LOCATION.lat,
    lng = DEFAULT_LOCATION.lng
) {

    const position = {
        lat: parseFloat(lat),
        lng: parseFloat(lng)
    };

    // إنشاء الخريطة أول مرة
    if (!map) {

        map = new google.maps.Map(document.getElementById('map'), {

            center: position,

            zoom: 16,

            mapTypeId: 'hybrid',

            gestureHandling: 'greedy'

        });

        marker = new google.maps.Marker({

            position: position,

            map: map,

            draggable: true,

            animation: google.maps.Animation.DROP

        });

        // Drag Marker
        marker.addListener('dragend', function (event) {

            updateCoordinates(
                event.latLng.lat(),
                event.latLng.lng()
            );

        });

        // Click Map
        map.addListener('click', function (event) {

            marker.setPosition(event.latLng);

            updateCoordinates(
                event.latLng.lat(),
                event.latLng.lng()
            );

        });

    }
    else {

        map.setCenter(position);

        marker.setPosition(position);

    }

    updateCoordinates(position.lat, position.lng);

    setTimeout(() => {

        google.maps.event.trigger(map, 'resize');

        map.setCenter(position);

    }, 300);

}


/* =========================================================
   MAP TYPE
   ========================================================= */

function changeMapType(type) {

    if (!map)
        return;

    map.setMapTypeId(type);

}


/* =========================================================
   COORDINATES
   ========================================================= */

function updateCoordinates(lat, lng) {

    $('#txtLatitude').val(lat);

    $('#txtLongitude').val(lng);

}


/* =========================================================
   LOADER
   ========================================================= */

function showLoader() {

    Swal.fire({

        title: 'جاري التنفيذ...',

        html: 'يرجى الانتظار',

        allowOutsideClick: false,

        didOpen: () => {

            Swal.showLoading();

        }

    });

}