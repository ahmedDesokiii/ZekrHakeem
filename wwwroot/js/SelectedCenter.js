

var map; var marker; var defaultLocation = { lat: 25.0413431, lng: 37.2597054 }; // الموقع الافتراضي للجمعية

Details = () => {
        initMap(Number(document.getElementById('txtLatitudeLoc')).value, Number(document.getElementById('txtLongitudeLoc')).value);  
}

initMap = (newlat = 25.0413431, newlng = 37.2597054) => {
    defaultLocation.lat = newlat;
    defaultLocation.lng = newlng;

        // إنشاء الخريطة
        map = new google.maps.Map(document.getElementById('mapLoc'), {
            center: defaultLocation,
            zoom: 16,
            mapTypeId: 'satellite' // تعيين العرض الافتراضي إلى "ستالايت"
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
    for (var i = 0; i < 2; i++) {
        document.getElementById('txtLatitudeLoc').value = lat;
        document.getElementById('txtLongitudeLoc').value = lng;
    }
    
}
