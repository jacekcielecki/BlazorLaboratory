function addEvents() {
    var tooltip = document.querySelector('.map-tooltip');

    // iterate throw all `path` tags
    [].forEach.call(document.querySelectorAll('path.HI-map'), function (item) {
        // attach click event, you can read the URL from a attribute for example.

        // attach mouseenter event
        item.addEventListener('mouseenter', function () {
            var sel = this,
                // get the borders of the path - see this question: http://stackoverflow.com/q/10643426/863110
                pos = sel.getBoundingClientRect()

            tooltip.style.display = 'block';
            tooltip.style.top = pos.top + 'px';
            tooltip.style.left = pos.left + 'px';
        });

        // when mouse leave hide the tooltip
        item.addEventListener('mouseleave', function () {
            tooltip.style.display = 'none';
        });
    });
}