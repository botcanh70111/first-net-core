import axios from 'axios';

class FileManager {
    LoadImages(managerId, managerUrl) {
        let _this = this;
        axios.get(managerUrl)
            .then(function (response) {
                let imageContainer = document.querySelector(managerId + " .manager-images");
                if (response.data != null) {
                    response.data.forEach(function (e, i) {
                        let img = document.createElement('img');
                        img.src = e;
                        if (i === 0) {
                            img.classList.add('active');
                        }
                        imageContainer.appendChild(img);
                    });
                    let managerContainer = document.querySelector(managerId);
                    managerContainer.classList.add('loaded');
                    _this.SelectImage(managerId);
                }
                else {
                    let message = document.createElement('p');
                    message.innerHTML = "The list is empty";
                    imageContainer.appendChild(message);
                }
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    SelectImage(managerId) {
        let images = document.querySelectorAll(managerId + " .manager-images img");
        images.forEach(function (img) {
            img.addEventListener("click", function () {
                images.forEach(function (i) {
                    i.classList.remove('active');
                });
                img.classList.add('active');
            });
        });
    }

    ChooseImage(managerId, targetInput) {
        let chooseBtn = document.querySelector(managerId + " .choose-image");
        chooseBtn.addEventListener("click", function() {
            let imgActive = document.querySelector(managerId + " img.active");
            let src = imgActive.attributes["src"].value;
            let modal = document.querySelector(managerId);
            let input = document.querySelector(targetInput);
            input.setAttribute("value", src);
            modal.classList.add("unshow");
        });
    }

    Close(managerId) {
        let closeBtn = document.querySelector(managerId + " .close-manager");
        closeBtn.addEventListener("click", function () {
            let modal = document.querySelector(managerId);
            setTimeout(() => {
                modal.classList.add("unshow");
            }, 0);
        });
    }

    Open(managerId, managerUrl) {
        let _this = this;
        let openBtn = document.querySelector("[data-target='" + managerId + "']");
        openBtn.addEventListener("click", function () {
            let modal = document.querySelector(managerId);
            modal.classList.remove("unshow");
            if (!modal.classList.contains('loaded')) {
                _this.LoadImages(managerId, managerUrl);
            }
        });
    }

    Init(managerId, targetInput, managerUrl) {
        this.Open(managerId, managerUrl);
        this.Close(managerId);
        // this.SelectImage(managerId);
        this.ChooseImage(managerId, targetInput);
    }
}

export default FileManager;