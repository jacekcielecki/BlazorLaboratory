
window.googleMaps = {
    createAlert: function () {
        alert("Js interop test");
    },

    initMap: async function ()
    {
        let map; // Initialize and add the map

        // The location of Uluru
        const position = { lat: -25.344, lng: 131.031 };

        // Request needed libraries.
        //@ts-ignore
        const { Map } = await google.maps.importLibrary("maps");
        const { AdvancedMarkerElement } = await google.maps.importLibrary("marker");

        // The map, centered at Uluru
            map = new Map(document.getElementById("googleMap"), {
            zoom: 4,
            center: position,
            mapId: "DEMO_MAP_ID",
        });

        // The marker, positioned at Uluru
        const marker = new AdvancedMarkerElement({
            map: map,
            position: position,
            title: "Uluru",
        });
    }
}