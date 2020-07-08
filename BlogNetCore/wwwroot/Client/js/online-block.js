class OnlineBlock {
  ShowOnline(id) {
    let countOnline = document.querySelector(".online-block .online-count");
    let count = parseInt(countOnline.innerHTML) + 1;
    countOnline.innerHTML = count;
    let sign = document.querySelector(".online-block li[data-id='" + id + "'] .online-sign");
    sign.classList.remove("offline");
  }

  ShowOnlines(ids) {
    let countOnline = document.querySelector(".online-block .online-count");
    let count = ids.length - 1; // yourself
    countOnline.innerHTML = count;
    ids.forEach(function(id) {
      let sign = document.querySelector(".online-block li[data-id='" + id + "'] .online-sign");
      sign.classList.remove("offline");
    });
  }

  ShowOffline(id) {
    let countOnline = document.querySelector(".online-block .online-count");
    let count = parseInt(countOnline.innerHTML) - 1;
    countOnline.innerHTML = count;
    let sign = document.querySelector(".online-block li[data-id='" + id + "'] .online-sign");
    sign.classList.add("offline");
  }

  ToggleBlock() {
    let head = document.getElementsByClassName('online-head')[0];
    head.addEventListener("click", function() {
      let block = document.getElementsByClassName('online-block')[0];
      if (block.classList.contains("expand")) {
        block.classList.remove("expand");
      } 
      else {
        block.classList.add("expand");
      }
    });
  }

  Init() {
    let _this = this;
    this.ToggleBlock();
    let connection = new signalR.HubConnectionBuilder().withUrl("/client/online").build();
    connection.start();
    connection.on("ShowOnlines", function (ids) {
      _this.ShowOnlines(ids);
    });
    connection.on("ShowOnline", function (id) {
      _this.ShowOnline(id);
    });
    connection.on("ShowOffline", function (id) {
      _this.ShowOffline(id);
    });
  }
}

export default OnlineBlock;