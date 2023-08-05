function initSwiper() {

    console.log('Entered initSwiper!');
    console.log('swiper text:' + document.querySelector('.mySwiper').textContent);
    console.log(document.querySelector('.mySwiper'));

    var swiper = new Swiper(".mySwiper", {
        slidesPerView: 3,
        spaceBetween: 30,
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
        },
    });
}