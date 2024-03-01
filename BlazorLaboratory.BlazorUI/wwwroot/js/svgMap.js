window.svgMap = {
    init: function () {
        var tooltip = document.querySelector('.map-tooltip');
        var counties = ["Niihau", "Kauai", "Oahu", "Molokai", "Lanai", "Kahoolawe", "Maui", "Hawaii"];
        var contents = {};
        for (var i = 1; i <= 8; i++) {
            contents['tooltip' + i] = counties[i - 1];
        }

        // iterate throw all `path` tags
        [].forEach.call(document.querySelectorAll('path.HI-map'), function (item) {
            // attach click event, you can read the URL from a attribute for example.
            item.addEventListener('click', function () {
                window.open(this.getAttribute('data-link'));
            });

            // attach mouseenter event
            item.addEventListener('mouseenter', function () {
                tooltip.innerHTML = contents[item.getAttribute('data-tooltip')];
                tooltip.style.display = 'block';
            });

            item.addEventListener('mousemove', function (e) {
                tooltip.style.top = e.clientY + 'px';
                tooltip.style.left = e.clientX + 'px';
            });

            // when mouse leave hide the tooltip
            item.addEventListener('mouseleave', function () {
                tooltip.style.display = 'none';
            });
        });
    },
}
