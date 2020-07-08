import { SlickSlide } from './slick';
import { ScrollAnimation } from './scroll';
import FileManager from './../../js/fileManager';
import OnlineBlock from './online-block';
import BlockChat from './chat';

class App {
  moveOnTop(height) {
    let moveOnTops = document.getElementsByClassName('move-on-top');
    if (moveOnTops) {
      let moveOnTop = moveOnTops[0];
      document.addEventListener("scroll", function () {
        if (document.body.scrollTop > height || document.documentElement.scrollTop > height) {
          moveOnTop.style.opacity = 1;
        } else {
          moveOnTop.style.opacity = 0;
        }
      });

      moveOnTop.addEventListener("click", function () {
        let scrollAnimation = new ScrollAnimation();
        scrollAnimation.scrollToTop(1000);
      });
    }
  }

  Init() {
    this.moveOnTop(500);
    let slick = new SlickSlide();
    slick.Init();

    let onlineBlock = new OnlineBlock();
    onlineBlock.Init();

    let chat = new BlockChat();
    chat.Init();

    var managerContainer = document.querySelectorAll(".manager-container");
    if (managerContainer.length > 0) {
      var fileManager = new FileManager();
      managerContainer.forEach(function (e) {
        var id = e.attributes["id"].value;
        var managerUrl = e.attributes["data-manager-url"].value;
        var input = document.querySelector("[data-target-input='#" + id + "']");
        var inputIdAttr = input.attributes["id"];
        var inputId = "";
        if (inputIdAttr == undefined) {
          input.setAttribute("id", "input_" + id);
          inputId = "input_" + id;
        } else {
          inputId = inputIdAttr.value;
        }
        fileManager.Init('#' + id, "#" + inputId, managerUrl);
      });
    }
  }
}

var app = new App();
app.Init();