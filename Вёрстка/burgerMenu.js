let openIcon = document.querySelector(".header");
let nav = document.querySelector(".header__NavList");

let closeIcon;
openIcon.addEventListener("click",
    (event) =>{
        nav.classList.add("Open");
        closeIcon = nav;
    }
);

closeIcon.addEventListener("click",
(event) =>{
    nav.classList.remove("Open");
}
);