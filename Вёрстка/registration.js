let ShowButton = document.querySelector(".registrateButton")
let HideButton = document.querySelector(".closePicture");
let Popup = document.querySelector(".popup");

ShowButton.addEventListener("click", 
    (event) => Popup.classList.remove("hide")
);

HideButton.addEventListener("click",
    (event) =>{
        Popup.classList.add("hide");
    }
);