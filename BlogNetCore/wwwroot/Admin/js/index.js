import FileManager from './../../js/fileManager';
import AdminNotification from './notification';

document.addEventListener('DOMContentLoaded', function () {
  // collapse menu
  var elems = document.querySelectorAll('.collapsible');
  var instances = M.Collapsible.init(elems, { accordion: true });

  // dropdown
  var elemsDr = document.querySelectorAll('.dropdown-trigger');
  var instancesDr = M.Dropdown.init(elemsDr, { coverTrigger: false, constrainWidth: false });

  // side bar
  var elemsSb = document.querySelectorAll('#side-bar');
  var instancesSb = M.Sidenav.init(elemsSb, { draggable: true });

  // mobile left side bar
  var leftSideBarBtn = document.getElementById("collapseMenu");
  leftSideBarBtn.addEventListener("click", function () {
    // var sideBarEl = document.querySelector("#sidebardiv");
    // var sideBar = M.Sidenav.getInstance(sideBarEl);
    // sideBar.open();
  });

  // select option init
  var elemsSO = document.querySelectorAll('select');
  var instancesSO = M.FormSelect.init(elemsSO);

  // insert class for button
  var buttons = document.querySelectorAll("button, a.btn");
  buttons.forEach(b => {
    if (b.classList.length == 0) {
      b.className = "waves-effect waves-light btn";
    }
  })


  // CK editor
  // var ckEditors = document.querySelectorAll(".jsCkEditor");
  // ckEditors.forEach(function (e, i) {
  //   e.setAttribute("id", "ckeditorelement" + i)
  //   ClassicEditor.create(document.querySelector('#ckeditorelement' + i))
  //     .catch(error => {
  //       console.error(error);
  //     });
  // })

  // Tinymce
  var textareaElements = document.querySelectorAll(".jsTinyMCE");
  textareaElements.forEach(function (e, i) {
    e.setAttribute("id", "tinymceelement" + i);
    tinymce.init(
      {
        selector: '#tinymceelement' + i,
        plugins: 'print preview paste importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap quickbars emoticons',
        imagetools_cors_hosts: ['picsum.photos'],
        menubar: 'file edit view insert format tools table help',
        toolbar: 'undo redo | bold italic underline strikethrough | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | fullscreen  preview save print | insertfile image media template link anchor codesample | ltr rtl',
        toolbar_sticky: true,
        autosave_ask_before_unload: true,
        autosave_interval: "30s",
        autosave_prefix: "{path}{query}-{id}-",
        autosave_restore_when_empty: false,
        autosave_retention: "2m",
        image_advtab: true,
        content_css: '//www.tiny.cloud/css/codepen.min.css',
        // link_list: [
        //   { title: 'My page 1', value: 'http://www.tinymce.com' },
        //   { title: 'My page 2', value: 'http://www.moxiecode.com' }
        // ],
        // image_list: [
        //   { title: 'My page 1', value: 'http://www.tinymce.com' },
        //   { title: 'My page 2', value: 'http://www.moxiecode.com' }
        // ],
        // image_class_list: [
        //   { title: 'None', value: '' },
        //   { title: 'Some class', value: 'class-name' }
        // ],
        importcss_append: true,
        // file_picker_callback: function (callback, value, meta) {
        //   /* Provide file and text for the link dialog */
        //   if (meta.filetype === 'file') {
        //     callback('https://www.google.com/logos/google.jpg', { text: 'My text' });
        //   }

        //   /* Provide image and alt text for the image dialog */
        //   if (meta.filetype === 'image') {
        //     callback('https://www.google.com/logos/google.jpg', { alt: 'My alt text' });
        //   }

        //   /* Provide alternative source and posted for the media dialog */
        //   if (meta.filetype === 'media') {
        //     callback('movie.mp4', { source2: 'alt.ogg', poster: 'https://www.google.com/logos/google.jpg' });
        //   }
        // },
        //templates: [
        //  { title: 'New Table', description: 'creates a new table', content: '<div class="mceTmpl"><table width="98%%"  border="0" cellspacing="0" cellpadding="0"><tr><th scope="col"> </th><th scope="col"> </th></tr><tr><td> </td><td> </td></tr></table></div>' },
        //  { title: 'Starting my story', description: 'A cure for writers block', content: 'Once upon a time...' },
        //  { title: 'New list with dates', description: 'New List with dates', content: '<div class="mceTmpl"><span class="cdate">cdate</span><br /><span class="mdate">mdate</span><h2>My List</h2><ul><li></li><li></li></ul></div>' }
        //],
        //template_cdate_format: '[Date Created (CDATE): %m/%d/%Y : %H:%M:%S]',
        //template_mdate_format: '[Date Modified (MDATE): %m/%d/%Y : %H:%M:%S]',
        height: 600,
        image_caption: true,
        quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote quickimage quicktable',
        noneditable_noneditable_class: "mceNonEditable",
        toolbar_mode: 'sliding',
        contextmenu: "link image imagetools table",
      }
    );
  });

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
        input.setAttribute("id", "input_" + id)
        inputId = "input_" + id;
      } else {
        inputId = inputIdAttr.value;
      }
      fileManager.Init('#' + id, "#" + inputId, managerUrl);
    });
  }

  // notifications
  var notiSelectors = [".jsPostBlog"];
  var adminNoti = new AdminNotification();
  adminNoti.Init(notiSelectors);
});