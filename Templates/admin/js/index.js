document.addEventListener('DOMContentLoaded', function () {
  // collapse menu
  var elems = document.querySelectorAll('.collapsible');
  var instances = M.Collapsible.init(elems, { accordion: true });

  // dropdown
  var elemsDr = document.querySelectorAll('.dropdown-trigger');
  var instancesDr = M.Dropdown.init(elemsDr, { coverTrigger: false, constrainWidth: false });

  // side bar
  var elemsSb = document.querySelectorAll('#side-bar');
  var instancesSb = M.Sidenav.init(elemsSb, {draggable: true});

  // mobile left side bar
  var leftSideBarBtn = document.getElementById("collapseMenu");
  leftSideBarBtn.addEventListener("click", function () {
    // var sideBarEl = document.querySelector("#sidebardiv");
    // var sideBar = M.Sidenav.getInstance(sideBarEl);
    // sideBar.open();
  });
});