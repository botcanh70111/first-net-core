export class SlickSlide {
  Init() {
    $('.slick').each(function (i, e) {
      let show = $(e).data('show');
      if (!show) {
        show = 2;
      }

      $(e).slick({
        mobileFirst: true,
        infinite: true,
        slidesToShow: show,
        slidesToScroll: show,
        responsive: [
          {
            breakpoint: 1024,
            settings: {
              slidesToShow: show,
              slidesToScroll: show,
            }
          },
          {
            breakpoint: 600,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1
            }
          },
          {
            breakpoint: 480,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1
            }
          }
        ]
      });
    })
  }
}
