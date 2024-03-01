var leafletMap; // map reference

window.blazorExtensions = {

    initMap: function () {
        leafletMap = L.map('map').setView([51.505, -0.09], 4);

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(leafletMap);
    },

    addMarker: function (lat, lon) {
        var marker = L.marker([lat, lon]).addTo(leafletMap);
    },
};