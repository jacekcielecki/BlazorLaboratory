let map; // Map reference

window.googleMaps = {
    initMap: async function ()
    {
        const { Map } = await google.maps.importLibrary("maps");

        map = new Map(document.getElementById("map"), {
            center: { lat: 52.460203, lng: 16.935870 },
            zoom: 4,
            mapId: 'f2d80162ef610a83',
            disableDefaultUI: true,
        });
    },

    loadDefaultMarkers: async function () {
        const { AdvancedMarkerElement } = await google.maps.importLibrary("marker");

        // The marker, positioned at Poznañ
        const pozPosition = { lat: 52.460203, lng: 16.935870 };

        // The marker, positioned at Uluru
        const uluPosition = { lat: -25.344, lng: 131.031 };

        const marker1 = new AdvancedMarkerElement({
            map: map,
            position: pozPosition,
            title: "Poznañ",
        });

        const marker2 = new AdvancedMarkerElement({
            map: map,
            position: uluPosition,
            title: "Uluru",
        });
    },


    addMarker: async function (lat, lng, name) {
        const { AdvancedMarkerElement } = await google.maps.importLibrary("marker");

        const position = { lat: lat, lng: lng };

        const marker = new AdvancedMarkerElement({
            map: map,
            position: position,
            title: name,
        });
    },
}