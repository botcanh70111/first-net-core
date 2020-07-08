class BlogChat {
  OpenChatClick() {
    let users = document.querySelectorAll('.jsOpenChat');
    users.forEach(function(u) {
      u.addEventListener("click", function() {
        let email = u.attributes["data-id"].value;
        let chatbox = document.querySelector('.chat-block');
        let emailPanel = document.querySelector('.chat-block .jsChatEmail');
        emailPanel.innerHTML = email;

        chatbox.classList.remove('unshow');
      });
    });
  }

  CloseChatClick() {
    let close = document.querySelector('.jsChatClose');
    close.addEventListener("click", function() {
      let chatbox = document.querySelector('.chat-block');
      chatbox.classList.add('unshow');
    });
  }

  SendMessageClick(connection) {
    let _this = this;
    let sendBtn = document.querySelector('.jsChatSend');
    sendBtn.addEventListener("click", function() {
      _this._SendMessage(connection);
    });

    document.querySelector('.jsChatBox').addEventListener("keypress", function(e) {
      if (e.keyCode == 13) {
        _this._SendMessage(connection);
      }
    })
  }

  _SendMessage(connection) {
    let email = document.querySelector('.chat-block .jsChatEmail').innerHTML;
    let message = document.querySelector('.jsChatBox').value;
    let chatBody = document.querySelector('.jsChatBody');
    let mess = this.TextElement("my-text", message, "/images/zeen-chin- (9).jpg");
    chatBody.appendChild(mess);
    connection.invoke("SendMessage", email, message);
    document.querySelector('.jsChatBox').value = "";
    chatBody.scrollTop = chatBody.scrollHeight;
  }
  
  ReceiveMessage(connection) {
    let _this = this;
    connection.on("ReceiveMessage", function(email, toEmail, message) {
      let chatEmail = document.querySelector('.chat-block .jsChatEmail').innerHTML;
      let chatBody = document.querySelector('.jsChatBody');
      if (chatEmail != email) {
        chatBody.innerHTML = "";
        document.querySelector('.chat-block .jsChatEmail').innerHTML = email;
      }

      let mess = _this.TextElement("your-text", message, "/images/zeen-chin- (9).jpg");
      chatBody.appendChild(mess);
      chatBody.scrollTop = chatBody.scrollHeight;
      document.querySelector('.chat-block').classList.remove('unshow');
    });
  }

  TextElement(className, message, avatarUrl) {
    let div = document.createElement("div");
    let avatar = document.createElement("img");
    let text = document.createElement("span");
    div.classList.add(className);
    avatar.src = avatarUrl;
    avatar.classList.add("chat-avatar");
    text.innerHTML = message;

    div.appendChild(avatar);
    div.appendChild(text);
    return div;
  }

  Init() {
    this.OpenChatClick();
    this.CloseChatClick();
    let connection = new signalR.HubConnectionBuilder().withUrl("/client/chat").build();
    connection.start();
    this.SendMessageClick(connection);
    this.ReceiveMessage(connection);
  }
}

export default BlogChat;