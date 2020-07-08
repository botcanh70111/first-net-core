class AdminNotification {
  MessageTemplate(email, url, message) {
    let li = document.createElement("li");
    let a = document.createElement("a");
    let p = document.createElement("p");
    let title = document.createElement("strong");
    let dateP = document.createElement("p");
    let dateSmall = document.createElement("small");
    let currentTime = new Date();
    a.href = url;
    title.innerHTML = email;
    p.appendChild(title);
    p.append(message);
    dateSmall.append(currentTime.toLocaleTimeString());
    dateSmall.append(" ");
    dateSmall.append(currentTime.toLocaleDateString());
    dateP.appendChild(dateSmall);
    a.appendChild(p);
    a.appendChild(dateP);
    li.appendChild(a);
    return li;
    // return `
    // <li>
    //     <a href="${url}">
    //         <p><strong>${email}</strong> ${message}</p>
    //         <p><small>${Date.now()}</small></p>
    //     </a>
    // </li>
    // `;
  }

  ClickNotification() {
    let notiBtn = document.querySelector("[data-target='adminNotification']");
    let badge = document.getElementsByClassName("jsAdminNotificationBadge")[0];

    notiBtn.addEventListener("click", function () {
      badge.innerHTML = "";
      badge.classList.add("hide");
    });
  }

  AddNotificationListeners(querySelectors, connection) {
    querySelectors.forEach(function(q) {
      let element = document.querySelector(q);
      if (element != undefined) {
        element.addEventListener("click", function() {
          connection
            .invoke("SendNotification", "has posted a blog.", "/")
            .catch(function (err) {
              return console.error(err.toString());
            });
        });
      }
    })
  }

  Init(querySelectors) {
    let _this = this;
    this.ClickNotification();

    let connection = new signalR.HubConnectionBuilder().withUrl("/admin/notification").build();
    connection.on("ReceiveNotification", function (email, message, url) {
      let msg = _this.MessageTemplate(email, url, message);
      let notiUl = document.querySelector("#adminNotification ul")
      notiUl.insertBefore(msg, notiUl.firstChild);
      let badge = document.getElementsByClassName("jsAdminNotificationBadge")[0];
      let notiCount = 0;
      if (!badge.innerHTML == "") {
        notiCount = parseInt(badge.innerHTML) + 1;
      } else {
        notiCount = 1;
      }
      
      badge.innerHTML = notiCount;
      badge.classList.remove("hide");
    });

    connection.start().then(function () {

    });

    this.AddNotificationListeners(querySelectors, connection);
  }
}

export default AdminNotification;