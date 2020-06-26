import {SlickSlide} from './slick'
import {ScrollAnimation} from './scroll'

class App {
  moveOnTop(height) {
    let moveOnTops = document.getElementsByClassName('move-on-top');
    if (moveOnTops) {
      let moveOnTop = moveOnTops[0];
      document.addEventListener("scroll", function() {
        if (document.body.scrollTop > height || document.documentElement.scrollTop > height) {
          moveOnTop.style.opacity = 1;
        } else {
          moveOnTop.style.opacity = 0;
        }
      })

      moveOnTop.addEventListener("click", function() {
        let scrollAnimation = new ScrollAnimation();
        scrollAnimation.scrollToTop(1000);
      })
    }
  }

  Init() {
    this.moveOnTop(500);
    let slick = new SlickSlide();
    slick.Init();
  }
}

var app = new App();
app.Init();