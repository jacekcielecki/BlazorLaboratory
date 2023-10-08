let map; // Map reference

window.googleMaps = {
    createAlert: function () {
        alert("Js interop test");
    },

    initMap: async function ()
    {
        const { Map } = await google.maps.importLibrary("maps");

        map = new Map(document.getElementById("map"), {
            center: { lat: 52.460203, lng: 16.935870 },
            zoom: 4,
            mapId: 'f2d80162ef610a83'
        });
    },

    loadDefaultMarker: async function () {
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

    geocodeAddress: function (address) {
        const geocoder = new google.maps.Geocoder();
        geocoder.geocode({ address: address }, function (results, status) {
            if (status === "OK" && results[0]) {
                const location = results[0].geometry.location;
                console.log(location);
                return location;
            } else {
                console.error("Geocode was not successful for the following reason: " + status);
            }
        });
    }
}