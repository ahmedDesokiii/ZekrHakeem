
//window.onload = function () {
//    var currentTime = new Date();
//    var epTime = new Date(document.getElementById("startEp_Fajr").value);

//    var hours = Math.abs(epTime.getHours() - currentTime.getHours());
//    var minutes = Math.abs(epTime.getMinutes() - currentTime.getMinutes());
//    var seconds = Math.abs(epTime.getSeconds() - currentTime.getSeconds());

//    setInterval(function () {
//            document.getElementById("timer_fajr").innerHTML = seconds + " : " + minutes + " : " + hours;
//        seconds--;
//        if (seconds == 0) {
//            minutes--;
//            seconds = 60;

//            if (minutes == 0) {

//                hours--;
//                minutes = 59;

//                if (hours <= 0) {
//                    //Episode Start ...
//                }
//            }
//        }
//    }, 1000);
//}
//window.onload = function () {
//    var currentTime = new Date();
//    var epTime = new Date(document.getElementById("startEp_Zohr").value);

//    var hours = Math.abs(epTime.getHours() - currentTime.getHours());
//    var minutes = Math.abs(epTime.getMinutes() - currentTime.getMinutes());
//    var seconds = Math.abs(epTime.getSeconds() - currentTime.getSeconds());

//    setInterval(function () {
//        document.getElementById("timer_zohr").innerHTML = seconds + " : " + minutes + " : " + hours;
//        seconds--;
//        if (seconds == 0) {
//            minutes--;
//            seconds = 60;

//            if (minutes == 0) {

//                hours--;
//                minutes = 59;

//                if (hours == 0) {
//                    //Episode Start ...
//                }
//            }
//        }
//    }, 1000);
//}


//Current Location | Onload

window.onload = function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            function (position) {
                // user lat & lng
                document.getElementById('userLat').value = position.coords.latitude;
                document.getElementById('userLng').value = position.coords.longitude;
                // in time episode lat & lng
                // intimeEpLat & intimeEpLng
                const userLat = document.getElementById('userLat').value; // User's latitude
                const userLng = document.getElementById('userLng').value; // User's longitude
                const circleLat = document.getElementById('intimeEpLat').value; // Circle's center latitude
                const circleLng = document.getElementById('intimeEpLng').value; // Circle's center longitude
                const circleRadius = 0.5; // Circle's radius in kilometers
                const userInside = isUserInCircle(userLat, userLng, circleLat, circleLng, circleRadius);

                if (userInside) {
                    document.getElementById('startepi_').style.visibility = "visible";
                }
            },
            function (error) {
                console.error("Error getting location: " + error.message);
                alert("Error getting location: " + error.message);
            }
        );
    } else {
        console.error("Geolocation is not supported by this browser.");
        alert("Geolocation is not supported by this browser.");
    }
};



//function getLocation() {
//    if (navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(success, error);
//    } else {
//        x.innerHTML = "Geolocation is not supported by this browser.";
//    }
//}

//function success(position) {
//    document.getElementById('userLat').value = position.coords.latitude;
//    document.getElementById('userLng').value = position.coords.longitude;
//    for (var i = 1; i <= 6; i++) {
//        //document.getElementById('startepi_' + i).disabled = true; // button
//        const userLat = document.getElementById('userLat').value; // User's latitude
//        const userLng = document.getElementById('userLng').value; // User's longitude
//        const circleLat = document.getElementById('epLat_' + i).value; // Circle's center latitude
//        const circleLng = document.getElementById('epLng_' + i).value; // Circle's center longitude
//        const circleRadius = .15; // Circle's radius in kilometers
//        const userInside = isUserInCircle(userLat, userLng, circleLat, circleLng, circleRadius);
//        const userIntime = document.getElementById('intime_' + i).checked;

//        if (userInside && userIntime) {
//            document.getElementById('inside_' + i).checked = true;
//            document.getElementById('intime_' + i).checked = true;
//            document.getElementById('startepi_' + i).style.visibility = "visible";

//        } else {
//            document.getElementById('inside_' + i).checked = false;
//            document.getElementById('intime_' + i).checked = false;
//            document.getElementById('startepi_' + i).style.visibility = "hidden";
//        }
//    }

//}

//function error() {
//    alert("Sorry, no position available.");
//}

function isUserInCircle(userLat, userLng, circleLat, circleLng, circleRadius) {
    const earthRadius = 6371; // Radius of the earth in kilometers

    // Convert latitude and longitude from degrees to radians
    const userLatRad = toRadians(userLat);
    const userLngRad = toRadians(userLng);
    const circleLatRad = toRadians(circleLat);
    const circleLngRad = toRadians(circleLng);

    // Calculate the differences between the coordinates
    const latDiff = circleLatRad - userLatRad;
    const lngDiff = circleLngRad - userLngRad;

    // Apply the Haversine formula to calculate distance
    const a =
        Math.sin(latDiff / 2) * Math.sin(latDiff / 2) +
        Math.cos(userLatRad) *
        Math.cos(circleLatRad) *
        Math.sin(lngDiff / 2) *
        Math.sin(lngDiff / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    const distance = earthRadius * c;

    // Check if the distance is within the circle's radius
    return distance <= circleRadius;
}

// Helper function to convert degrees to radians
function toRadians(degrees) {
    return degrees * (Math.PI / 180);
}

var att = document.getElementById("AttendType"); var loc = document.getElementById("LocationType");

$('#tableEpisode').dataTable({
    "autoWidth": true,
    "responsive": true,
    "displaylength": 25,
    layout: {
        topStart: {
            search: {
                placeholder: 'بحث عن بيانات الحلقة...'
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
        info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ حلقة', // Customize the info text
        zeroRecords: 'لم يعثر على أية سجلات', // No records message
        paginate: {
            first: 'الأول',
            previous: 'السابق',
            next: 'التالي',
            last: 'الأخير'
        }
    }
});
$('#tableLogEpisode').dataTable({
    "autoWidth": true,
    "responsive": true,
    "displaylength": 25,
    layout: {
        topStart: {
            search: {
                placeholder: 'بحث ...'
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
        info: 'إظهار _START_ إلى _END_ من أصل _TOTAL_ صف', // Customize the info text
        zeroRecords: 'لم يعثر على أية سجلات', // No records message
        paginate: {
            first: 'الأول',
            previous: 'السابق',
            next: 'التالي',
            last: 'الأخير'
        }
    }
});


//$(document).ready(function () {
//    new DataTable('#tableEpisode')({
//        "autoWidth": true,
//        "responsive": true,
//        //"searching":false
//        "language": { "sSearch": "بحث:", "sLengthMenu": "_MENU_", }
//    });
//    new DataTable('#tableLogEpisode')({
//        "autoWidth": true,
//        "responsive": true
//    });
//});

Pause = (id) => {
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
            window.location.href = `/Episodes/Pause?Id=${id}`;
            Swal.fire(
                lbTitlePausedOk,
                lbMsgPausedOkEpisode,
                lbSuccess
            )
        }
    })
}
Resume = (id) => {
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
            window.location.href = `/Episodes/Resume?Id=${id}`;
            Swal.fire(
                lbTitleResumedOk,
                lbMsgResumedOkEpisode,
                lbSuccess
            )
        }
    })
}
Delete = (id) => {
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
            window.location.href = `/Episodes/Delete?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkEpisode,
                lbSuccess
            )
        }
    })
}
DeleteLog = (id) => {

    Swal.fire({
        title: lbTitleMsgDeleteLog,
        text: lbTextMsgDeleteLog,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Episodes/DeleteLog?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkEpisodeLog,
                lbSuccess
            )
        }

    })
}
Edit = (id, name, titletype, limit, status, attendtype, description, locationId, teacherId, latidude, longitude, locationType, period) => {

    initMap(Number(latidude), Number(longitude));
    document.getElementById("title").innerHTML = lbTitleEditEpisode;
    document.getElementById("btnSave").value = lbEdit;

    document.getElementById("EpisodeId").value = id;
    document.getElementById("EpisodeName").value = name;
    document.getElementById("TitleType").value = titletype;
    document.getElementById("Limit").value = limit;
    document.getElementById("Status").value = status;
    document.getElementById("AttendType").value = attendtype;
    document.getElementById("description").value = description;
    document.getElementById("LocationType").value = locationType;
    document.getElementById("prayerTime").value = period;
    document.getElementById("LocationType").disabled = true;
    // selectPrayerTimeAPI();
    selectPrayerTime();



    //selectPrayerTime();
    //document.getElementById("epIcon").value = icon;

    // حضوري
    if (att.selectedIndex == 0 && loc.value == "mosque") { // حضوري | mosque
        document.getElementById('prayerTime').disabled = false;
        document.getElementById("locationsM").value = locationId;
        document.getElementById("teachersM").value = teacherId;
        VisibilityType("visible", "visible", "hidden", "visible", "hidden", "visible");
        document.getElementById("locationsM").disabled = true;
        document.getElementById('EpLocId').value = locationId;
    }
    else if (att.selectedIndex == 0 && loc.value == "dar") { // حضوري | dar
        document.getElementById('prayerTime').disabled = false;
        document.getElementById("locationsW").value = locationId;
        document.getElementById("teachersW").value = teacherId;
        VisibilityType("visible", "hidden", "visible", "hidden", "visible", "visible");
        document.getElementById("locationsW").disabled = true;
        document.getElementById('EpLocId').value = locationId;
    }
    else if (att.selectedIndex == 0 && loc.value != "dar" && loc.value != "mosque") { // حضوري | None
        document.getElementById('prayerTime').disabled = false;
        VisibilityType("visible", "hidden", "hidden", "hidden", "hidden", "visible");
        document.getElementById("locationsM").disabled = true;
        document.getElementById("locationsW").disabled = true;
        document.getElementById('EpLocId').value = locationId;
    }
    // عن بعد
    else if (att.selectedIndex == 1 && loc.value == "mosque") { // عن بعد | mosque
        document.getElementById("teachersM").value = teacherId;


        document.getElementById('prayerTime').selectedIndex = 0;
        document.getElementById("starttime").value = "";
        document.getElementById("endtime").value = "";
        document.getElementById('prayerTime').disabled = true;
        document.getElementById('EpLocId').value = "00000000-0000-0000-0000-000000000003"
        VisibilityType("visible", "hidden", "hidden", "visible", "hidden", "visible");
    }
    else if (att.selectedIndex == 1 && loc.value == "dar") { // عن بعد | dar
        document.getElementById("teachersW").value = teacherId;
        document.getElementById('prayerTime').selectedIndex = 0;
        document.getElementById("starttime").value = "";
        document.getElementById("endtime").value = "";
        document.getElementById('prayerTime').disabled = true;
        document.getElementById('EpLocId').value = "00000000-0000-0000-0000-000000000005"
        VisibilityType("visible", "hidden", "hidden", "hidden", "visible", "visible");
    }
    else if (att.selectedIndex == 1 && loc.value != "dar" && loc.value != "mosque") { // عن بعد | None
        document.getElementById('prayerTime').selectedIndex = 0;
        document.getElementById("starttime").value = "";
        document.getElementById("endtime").value = "";
        document.getElementById('prayerTime').disabled = true;
        VisibilityType("visible", "hidden", "hidden", "hidden", "hidden", "visible");
        //document.getElementById('EpLocId').value = null;
    }
}

Rest = () => {
    initMap();
    document.getElementById("title").innerHTML = lbAddNewEpisode;
    document.getElementById("btnSave").value = lbbtnSave;

    document.getElementById("EpisodeId").value = null;
    document.getElementById("EpisodeName").value = "";
    document.getElementById("TitleType").value = "";
    document.getElementById("Limit").value = "";
    document.getElementById("Status").value = false;
    //document.getElementById("AttendType").value = "حضوري";
    document.getElementById("AttendType").selectedIndex = 0;
    document.getElementById("prayerTime").selectedIndex = 0;
    document.getElementById("prayerTime").disabled = false;
    document.getElementById("epIcon").value = null;
    document.getElementById("description").value = "";
    document.getElementById('empEpM').value = "";
    
    document.getElementById("LocationType").selectedIndex = 0;
    document.getElementById("prayerTime").selectedIndex = 0;
    document.getElementById("LocationType").disabled = false;
    document.getElementById("locationsM").disabled = false;
    document.getElementById("locationsW").disabled = false;
    document.getElementById("locationsM").selectedIndex = 0;
    document.getElementById("teachersM").selectedIndex = 0;
    document.getElementById("locationsW").selectedIndex = 0;
    document.getElementById("teachersW").selectedIndex = 0;
    SelectType();
}
selectWLocId = (lim) => {
    var _lat, _lng;
    for (c = 0; c < lim; c++) {
        if (document.getElementById('w_' + c).selected == true) {
            _lat = document.getElementById('txtLatitudeEp').value = document.getElementById('w_' + c).getAttribute("lat");
            _lng = document.getElementById('txtLongitudeEp').value = document.getElementById('w_' + c).getAttribute("lng");
            document.getElementById('locationsW').value = document.getElementById('w_' + c).value;
            initMap(Number(_lat), Number(_lng));
        }
        else {
            document.getElementById('LocationType').value = "dar";
        }
    }
}
selectMLocId = (lim) => {
    var _lat, _lng;
    for (c = 0; c < lim; c++) {
        if (document.getElementById('m_' + c).selected === true) {
            _lat = document.getElementById('txtLatitudeEp').value = document.getElementById('m_' + c).getAttribute("lat");
            _lng = document.getElementById('txtLongitudeEp').value = document.getElementById('m_' + c).getAttribute("lng");
            document.getElementById('locationsM').value = document.getElementById('m_' + c).value;
            initMap(Number(_lat), Number(_lng));
        }
        else {
            document.getElementById('LocationType').value = "mosque";
        }
    }
}
SelectType = () => {
    document.getElementById('prayerTime').selectedIndex = "0";
    // حضوري
    if (att.selectedIndex == 0 && loc.value === "mosque") { // حضوري | mosque
        document.getElementById('empEpM').value = "";
        document.getElementById('locationsM').disabled = false;
        document.getElementById('prayerTime').disabled = false;
        VisibilityType("visible", "visible", "hidden", "visible", "hidden", "visible");
    }
    else if (att.selectedIndex == 0 && loc.value == "dar") { // حضوري | dar
        document.getElementById('empEpW').value = "";
        document.getElementById('locationsW').disabled = false;
        document.getElementById('prayerTime').disabled = false;
        VisibilityType("visible", "hidden", "visible", "hidden", "visible", "visible");
    }
    else if (att.selectedIndex == 0 && loc.value != "dar" && loc.value != "mosque") { // حضوري | None
        document.getElementById('prayerTime').disabled = false;
        VisibilityType("visible", "hidden", "hidden", "hidden", "hidden", "visible");
    }
    // عن بعد
    else if (att.selectedIndex == 1 && loc.value == "mosque") { // عن بعد | mosque
        document.getElementById("starttime").value = "";
        document.getElementById("endtime").value = "";
        document.getElementById('empEpis').value = "عن بعد";

        document.getElementById('empEpM').value = "00000000-0000-0000-0000-000000000003";
        document.getElementById('locationsM').selectedIndex = 0;
       

        VisibilityType("hidden", "hidden", "hidden", "visible", "hidden", "visible");

    }
    else if (att.selectedIndex == 1 && loc.value == "dar") { // عن بعد | dar
        document.getElementById("starttime").value = "";
        document.getElementById("endtime").value = "";
        document.getElementById('empEpis').value = "عن بعد";

        document.getElementById('empEpW').value = "00000000-0000-0000-0000-000000000005";
        document.getElementById('locationsW').selectedIndex = 0;
        
        VisibilityType("hidden", "hidden", "hidden", "hidden", "visible", "visible");
    }
    else if (att.selectedIndex == 1 && loc.value != "dar" && loc.value != "mosque") { // عن بعد | None
        document.getElementById("starttime").value = "";
        document.getElementById("endtime").value = "";
        document.getElementById('empEpis').value = "عن بعد";
        document.getElementById('empEpis').innerHTML = "عن بعد";
        document.getElementById('prayerTime').value = "عن بعد";
        document.getElementById('prayerTime').disabled = true;
        VisibilityType("visible", "hidden", "hidden", "hidden", "hidden", "visible");
        document.getElementById('EpLocId').value = null;
    }
}
VisibilityType = (visPT = "visible", visLocM = "hidden", visLocW = "hidden", visTeachM = "hidden", visTeachW = "hidden", visMap = "visible") => {

    //visPT
    document.getElementById("lblPrayerTime").style.visibility = visPT;
    document.getElementById("prayerTime").style.visibility = visPT;
    document.getElementById("timer").style.visibility = visPT;
    document.getElementById("lblST").style.visibility = visPT;
    document.getElementById("lblET").style.visibility = visPT;
    document.getElementById("starttime").style.visibility = visPT;
    document.getElementById("endtime").style.visibility = visPT;

    //visLocM
    document.getElementById("locationsM").style.visibility = visLocM;
    document.getElementById("lbllocationsM").style.visibility = visLocM;

    //visLocW
    document.getElementById("locationsW").style.visibility = visLocW;
    document.getElementById("lbllocationsW").style.visibility = visLocW;

    //visTeachM
    document.getElementById("teachersM").style.visibility = visTeachM;
    document.getElementById("lblteachersM").style.visibility = visTeachM;

    //visTeachW
    document.getElementById("teachersW").style.visibility = visTeachW;
    document.getElementById("lblteachersW").style.visibility = visTeachW;

    //visMap
    document.getElementById('mapEp').style.visibility = visMap;
}
selectPrayerTime = () => {
    if (document.getElementById('prayerTime').selectedIndex == 1) { // الفجر
        document.getElementById('starttime').value = document.getElementById('FajrTime').getAttribute("startEpTime");
        document.getElementById('endtime').value = document.getElementById('FajrTime').getAttribute("endEpTime");
        document.getElementById('prayerTimeResult').value = "الفجر"
        if (att.selectedIndex == 1) {
            document.getElementById("endtime").value = "";
        }
    }

    if (document.getElementById('prayerTime').selectedIndex == 2) { // الشروق
        document.getElementById('starttime').value = document.getElementById('SunriseTime').getAttribute("startEpTime");
        document.getElementById('endtime').value = document.getElementById('SunriseTime').getAttribute("endEpTime");
        document.getElementById('prayerTimeResult').value = "الشروق"
        if (att.selectedIndex == 1) {
            document.getElementById("endtime").value = "";
        }

    }
    else if (document.getElementById('prayerTime').selectedIndex == 3) { // الظهر
        document.getElementById('starttime').value = document.getElementById('ZohrTime').getAttribute("startEpTime");
        document.getElementById('endtime').value = document.getElementById('ZohrTime').getAttribute("endEpTime");
        document.getElementById('prayerTimeResult').value = "الظهر"
        if (att.selectedIndex == 1) {
            document.getElementById("endtime").value = "";
        }

    }
    else if (document.getElementById('prayerTime').selectedIndex == 4) { //العصر
        document.getElementById('starttime').value = document.getElementById('AsrTime').getAttribute("startEpTime");
        document.getElementById('endtime').value = document.getElementById('AsrTime').getAttribute("endEpTime");
        document.getElementById('prayerTimeResult').value = "العصر"
        if (att.selectedIndex == 1) {
            document.getElementById("endtime").value = "";
        }
    }
    else if (document.getElementById('prayerTime').selectedIndex == 5) { // المغرب
        document.getElementById('starttime').value = document.getElementById('MghrbTime').getAttribute("startEpTime");
        document.getElementById('endtime').value = document.getElementById('MghrbTime').getAttribute("endEpTime");
        document.getElementById('prayerTimeResult').value = "المغرب"
        if (att.selectedIndex == 1) {
            document.getElementById("endtime").value = "";
        }

    }
    else if (document.getElementById('prayerTime').selectedIndex == 6) { // العشاء
        document.getElementById('starttime').value = document.getElementById('IshaTime').getAttribute("startEpTime");
        document.getElementById('endtime').value = document.getElementById('IshaTime').getAttribute("endEpTime");
        document.getElementById('prayerTimeResult').value = "العشاء"
        if (att.selectedIndex == 1) {
            document.getElementById("endtime").value = "";
        }

    }
}
selectEpisodeType = (lim) => {
    for (c = 0; c < lim; c++) {
        if (document.getElementById('et_' + c).selected == true) {
            document.getElementById('epIcon').value = document.getElementById('et_' + c).getAttribute("episodeIcon");
        }
    }
}

var map; var marker; var defaultLocation = { lat: 25.0413431, lng: 37.2597054 }; // الموقع الافتراضي للجمعية
initMap = (newlat = 25.0413431, newlng = 37.2597054) => {

    defaultLocation.lat = newlat;
    defaultLocation.lng = newlng;

    // إنشاء الخريطة
    map = new google.maps.Map(document.getElementById('mapEp'), {
        center: defaultLocation,
        zoom: 16,
        mapTypeId: 'terrain' // تعيين العرض الافتراضي إلى "ستالايت"
    });

    // إضافة علامة (Marker) في الموقع الافتراضي
    marker = new google.maps.Marker({
        position: defaultLocation,
        map: map,
        title: 'الموقع المحدد',
        draggable: false // لجعل العلامة قابلة للسحب
    });

    // تعيين الإحداثيات الافتراضية في المربعات
    updateCoordinates(defaultLocation.lat, defaultLocation.lng);
}
updateCoordinates = (lat, lng) => {
    document.getElementById('txtLatitudeEp').value = lat;
    document.getElementById('txtLongitudeEp').value = lng;
}


RestEpType = () => {
    document.getElementById("title").innerHTML = lbAddNewEpTpe;
    document.getElementById("btnSaveEpType").value = lbbtnSave;

    document.getElementById("EpTypeId").value = null;
    document.getElementById("EpTypeName").value = "";
    document.getElementById("epIcon").value = "";
}

EditEpType = (id, name, icon) => {
    document.getElementById("title").innerHTML = lbTitleEditEpType;
    document.getElementById("btnSaveEpType").value = lbEdit;

    document.getElementById("EpTypeId").value = id;
    document.getElementById("EpTypeName").value = name;
    document.getElementById("epIcon").value = icon;
}

DeleteEpType = (id) => {
    Swal.fire({
        title: lbTitleMsgDeleteType,
        text: lbTextMsgDeleteType,
        icon: 'error',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: 'secondary',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Episodes/DeleteEpisodeType?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkEpisodetype,
                lbSuccess
            )
        }
    })
}

//window.onload = function () {
//    document.getElementById("resetEp_form").submit();
//}

//function resetAtMidnight() {
//    var now = new Date();
//    var night = new Date(
//        now.getFullYear(),
//        now.getMonth(),
//        now.getDate() + 1, // the next day, ...
//        0, 0, 0 // ...at 00:00:00 hours
//    );
//    var msToMidnight = night.getTime() - now.getTime();

//    setTimeout(function () {
//        document.getElementById("resetEp_form").submit();
//        reset();              //      <-- This is the function being called at midnight.
//        resetAtMidnight();    //      Then, reset again next midnight.
//    }, msToMidnight);
//}