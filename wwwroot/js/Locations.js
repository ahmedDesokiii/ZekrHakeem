
//// DataTables initialisation

$('#tableLocation').dataTable({
    "autoWidth": true,
    "responsive": true,
    
    layout: {
        topStart: {
            search: {
                placeholder: 'بحث عن بيانات المسجد...'
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
        info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ مسجد', // Customize the info text
        zeroRecords: 'لم يعثر على أية سجلات', // No records message
        paginate: {
            first: 'الأول',
            previous: 'السابق',
            next: 'التالي',
            last: 'الأخير'
        }
    }
});

function Pause(id) {
    Swal.fire({
        title: lbTitleMsgPause,
        text: lbTextMsgPause,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonTextPause,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Locations/Pause?Id=${id}`;
            Swal.fire(
                lbTitlePausedOk,
                lbMsgPausedOkLocation,
                lbSuccess
            )
        }
    })
}
function Resume(id) {
    Swal.fire({
        title: lbTitleMsgResume,
        text: lbTextMsgResume,
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#228B22',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonTextResume,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Locations/Resume?Id=${id}`;
            Swal.fire(
                lbTitleResumedOk,
                lbMsgResumedOkLocation,
                lbSuccess
            )
        }
    })
}

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
            window.location.href = `/Locations/Delete?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkLocation,
                lbSuccess
            )
        }
    })
}

function DeleteLog(id) {

    Swal.fire({
        title: lbTitleMsgDeleteLog,
        text: lbTextMsgDeleteLog,
        icon: 'error',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Locations/DeleteLog?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkLocationLog,
                lbSuccess
            )
        }

    })
}

RequiredTXT = () => {
    //
    document.getElementById('LocationName').className.replace("invalid", "valid");
}

function SelectType() {
    if (document.getElementById('LocationType').selectedIndex == 2) {
        document.getElementById('LocationType').value = "dar";
        document.getElementById("centersM").style.visibility = "hidden";
        document.getElementById("centersW").style.visibility = "visible";
        document.getElementById("lblcentersM").style.visibility = "hidden";
        document.getElementById("lblcentersW").style.visibility = "visible";
        document.getElementById("centersM").disabled = true;
        document.getElementById("centersW").disabled = false;
    }
    else if (document.getElementById('LocationType').selectedIndex == 1) {
        document.getElementById('LocationType').value = "mosque";
        document.getElementById("centersW").style.visibility = "hidden";
        document.getElementById("centersM").style.visibility = "visible";
        document.getElementById("lblcentersW").style.visibility = "hidden";
        document.getElementById("lblcentersM").style.visibility = "visible";
        document.getElementById("centersM").disabled = false;
        document.getElementById("centersW").disabled = true;
    }
}

function Edit(id, name, type, description, centerId, latidude, longitude, district) {

    initMap(Number(latidude), Number(longitude));
    document.getElementById("title").innerHTML = lbTitleEditLocation;
    document.getElementById("btnSave").value = lbEdit;

    document.getElementById("LocationId").value = id;
    document.getElementById("LocationName").value = name;
    document.getElementById("description").value = description;
    document.getElementById("districtId").value = district;

    if (type == "dar") {
        document.getElementById("centersW").value = centerId;
        document.getElementById("centersM").style.visibility = "hidden";
        document.getElementById("centersW").style.visibility = "visible";
        document.getElementById("lblcentersM").style.visibility = "hidden";
        document.getElementById("lblcentersW").style.visibility = "visible";
        document.getElementById("LocationType").selectedIndex = 2;
        document.getElementById("LocationType").disabled = true;
        document.getElementById("centersM").disabled = true;
        document.getElementById("centersW").disabled = false;
    }
    else if (type == "mosque") {
        document.getElementById("centersM").value = centerId;
        document.getElementById("centersW").style.visibility = "hidden";
        document.getElementById("centersM").style.visibility = "visible";
        document.getElementById("lblcentersW").style.visibility = "hidden";
        document.getElementById("lblcentersM").style.visibility = "visible";
        document.getElementById("LocationType").selectedIndex = 1;
        document.getElementById("LocationType").disabled = true;
        document.getElementById("centersM").disabled = false;
        document.getElementById("centersW").disabled = true;
    }
}

function Rest() {

    initMap();
    document.getElementById("title").innerHTML = lbAddNewLocation;
    document.getElementById("btnSave").value = lbbtnSave;

    document.getElementById("LocationId").value = null;
    document.getElementById("LocationName").value = "";
    document.getElementById("description").value = "";
    document.getElementById("LocationType").disabled = false;
    document.getElementById("LocationType").selectedIndex = "0";

    if (document.getElementById("LocationType").value == "dar") {
        document.getElementById("centersW").style.visibility = "visible";
        document.getElementById("centersM").style.visibility = "hidden";
        document.getElementById("lblcentersW").style.visibility = "visible";
        document.getElementById("lblcentersM").style.visibility = "hidden";
        document.getElementById("centersM").disabled = true;
        document.getElementById("centersW").disabled = false;
    }
    else if (document.getElementById("LocationType").value == "mosque") {
        document.getElementById("centersM").style.visibility = "visible";
        document.getElementById("centersW").style.visibility = "hidden";
        document.getElementById("lblcentersM").style.visibility = "visible";
        document.getElementById("lblcentersW").style.visibility = "hidden";
        document.getElementById("centersM").disabled = false;
        document.getElementById("centersW").disabled = true;
    }
    else {
        document.getElementById("centersM").style.visibility = "hidden";
        document.getElementById("centersW").style.visibility = "hidden";
        document.getElementById("lblcentersM").style.visibility = "hidden";
        document.getElementById("lblcentersW").style.visibility = "hidden";
    }
    document.getElementById("centersM").selectedIndex = "0";
    document.getElementById("centersW").selectedIndex = "0";
    document.getElementById("districtId").selectedIndex = "0";
}

var map;
var marker;
var defaultLocation = { lat: 25.0413431, lng: 37.2597054 }; // الموقع الافتراضي للجمعية
function initMap(newlat = 25.0413431, newlng = 37.2597054) {

    defaultLocation.lat = newlat;
    defaultLocation.lng = newlng;

    // إنشاء الخريطة
    map = new google.maps.Map(document.getElementById('map'), {
        center: defaultLocation,
        zoom: 16,
        mapTypeId: 'hybrid' // تعيين العرض الافتراضي إلى "ستالايت"
    });

    // إضافة علامة (Marker) في الموقع الافتراضي
    marker = new google.maps.Marker({
        position: defaultLocation,
        map: map,
        title: 'الموقع المحدد',
        draggable: true // لجعل العلامة قابلة للسحب
    });

    // تحديث الإحداثيات في مربعات النص عند تحريك العلامة
    google.maps.event.addListener(marker, 'dragend', function (event) {
        updateCoordinates(event.latLng.lat(), event.latLng.lng());
    });

    // تحديث الإحداثيات عند النقر على الخريطة
    google.maps.event.addListener(map, 'click', function (event) {
        marker.setPosition(event.latLng);
        updateCoordinates(event.latLng.lat(), event.latLng.lng());
    });

    // تعيين الإحداثيات الافتراضية في المربعات
    updateCoordinates(defaultLocation.lat, defaultLocation.lng);
}
function updateCoordinates(lat, lng) {
    document.getElementById('txtLatitude').value = lat;
    document.getElementById('txtLongitude').value = lng;
}
function changeMapType(type) {
    if (map) {
        map.setMapTypeId(type);
    }
}


