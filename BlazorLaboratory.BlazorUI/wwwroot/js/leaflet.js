var leafletMap; // map reference

window.blazorExtensions = {

    initMap: function () {
        leafletMap = L.map('map').setView([51.505, -0.09], 13);

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(leafletMap);
    },

    addMarker: function (lon, lat) {
        var marker = L.marker([lon, lat]).addTo(leafletMap);
    },
};