let slides = document.querySelectorAll(".slider picture");
let controllersBlock = document.querySelector(".controllers")

let rightSideController = document.querySelector(".RigthSideController")
let leftSideController = document.querySelector(".LeftSideController")

let controllers = [];

let currentElementIndex = 0;

for (let index = 0; index < slides.length; index++) {
    
    let controller = document.createElement("button");
    controller.classList.add("controllers__item");
    
    controller.addEventListener("click",
        (event) =>{
            currentElementIndex = index;
            ShowItemIndex(index);
        }
    );
    
    controllers[index] = controller;

    controllersBlock.appendChild(controller);
}

rightSideController.addEventListener("click",
    (event) =>
    {
        currentElementIndex++;

        if(slides.length <= currentElementIndex)
        {
            currentElementIndex = 0;
            ShowItemIndex(0);
        }
        else
        {
            ShowItemIndex(currentElementIndex);
        }
    }
);

leftSideController.addEventListener("click",
    (event) =>
    {
        currentElementIndex--;

        if(0 > currentElementIndex)
        {
            currentElementIndex = slides.length - 1;
            ShowItemIndex(currentElementIndex);
        }
        else
        {
            ShowItemIndex(currentElementIndex);
        }
    }
);

leftSideController.addEventListener("mouseover",
    (event) => slides[currentElementIndex].classList.add("movedRight")
)
rightSideController.addEventListener("mouseover",
    (event) => slides[currentElementIndex].classList.add("movedLeft")
)

leftSideController.addEventListener("mouseout",
    (event) => slides[currentElementIndex].classList.remove("movedRight")
)
rightSideController.addEventListener("mouseout",
    (event) => slides[currentElementIndex].classList.remove("movedLeft")
)

ShowItemIndex(0);

function ShowItemIndex(index)
{
    slides.forEach((slide) => slide.classList.add("HidedImage"));
    slides[index].classList.remove("HidedImage")

    controllers.forEach((element) => element.classList.remove("Active"))
    controllers[index].classList.add("Active");
}