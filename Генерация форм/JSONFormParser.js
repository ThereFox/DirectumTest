let jsonForm = `{
	"form": {
		"name": "one",
		"items": [
			{
				"type": "filler",
				"message": "<h2>Письмо.</h2>"
			},
			{
				"type": "text",
				"name": "email",
				"placeholder": "",
				"required": true,
				"validationRules": {
					"type": "email"
				},
				"value": "",
				"label": "Email:",
				"class": "",
				"disabled": false
			},
			{
				"type": "textarea",
				"name": "message",
				"placeholder": "",
				"required": true,
				"validationRules": {
					"type": "text"
				},
				"value": "",
				"label": "Ваше сообщение:",
				"class": "elblock",
				"disabled": false
			},
			{
				"type": "checkbox",
				"name": "checkbox",
				"checked": false,
				"placeholder": "",
				"required": true,
				"validationRules": {
					"type": "checkbox"
				},
				"label": "Заказать демонстрацию",
				"class": "",
				"disabled": false
			},
			{
				"type": "button",
				"text": "Отправить",
				"class": ""
			}
		],
		"postmessage": "<p>Благодарим Вас за проявленный интерес, в течении рабочего дня с Вами свяжется наш сотрудник.</p>"
	}
}`;

var result = createDOMElementFromJson(jsonForm);

console.log(result);

function createDOMElementFromJson(json)
{
    let formObject = JSON.parse(json);
    if(formObject == null)
    {
        throw new Error("is not json");
    }
    return createDOMElementFromObject(formObject);

}

function createDOMElementFromObject(ObjectInfo)
{
	var baseElement = document.createElement("div");
	baseElement.classList.add("generatedForm");
	let form = document.createElement("form");

	form.name = ObjectInfo.form.name;

	let postMessage = document.createElement("div");
	let postMessageText = document.createElement("span");
	postMessageText.innerHTML = ObjectInfo.form.postmessage;
	postMessage.appendChild(postMessageText);
	form.addEventListener("submit", 
		(event) => postMessage.classList.add("showing")
	);

    for (let item of ObjectInfo.form.items)
    {
        let element = createElementByObject(item, form);
		
        form.appendChild(element);
    }
	
	baseElement.appendChild(form);
	baseElement.appendChild(postMessage);

	return baseElement;

}
function createElementByObject(item, baseComponent)
{
	if(item.type == "filler")
	{
		return CreateFiller(item);
	}
	if(item.type == "button"){
		return CreateButton(item);
	}
	if(item.type == "radio")
	{
		return CreateRadio(item);
	}


	var label = document.createElement("label");
	label.setAttribute("content", item.label);
	label.setAttribute("for", `#${item.name}`)
	baseComponent.appendChild(label);
	let component;
	if(item.type != "select"){
     	component = document.createElement("input");
	}
	else
	{
		component = document.createElement("select");
	}

	component.setAttribute("type", item.type);
	component.setAttribute("name", item.name);
	component.setAttribute("id", item.name);
	
	if(item.class != ""){
		component.classList.add(`${item.class}`);
	}
	component.disabled = item.disabled;
	component.setAttribute("required", item.required);

	if(item.type != "checkbox"){
		component.setAttribute("value", item.value);
	}
	else
	{
		component.setAttribute("checked", item.checked);
	}

	if(item.placeholder != null)
	{
		component.setAttribute("placeholder", item.placeholder);
	}

	if(item.validationRules.type == "tel")
	{
		component.setAttribute("pattern", "[+0-9]{9,15}");
	}
	else if(item.validationRules.type == "email")
	{
		component.setAttribute("pattern", "[A-Za-z]{3,32}@[a-z]{3,10}.[a-z]{2,4}");
	}

	if(item.type == "select")
	{

		for(let option of item.options)
		{
			var optionComponent = document.createElement("option");

			optionComponent.textContent = option.text;
			optionComponent.setAttribute("value", option.value);
			optionComponent.setAttribute("selected", option.selected)

			component.appendChild(optionComponent);
		}
	}
	return component;
}


function CreateButton(buttonInfo)
{
	let button = document.createElement("button");
	if(buttonInfo.class != ""){
		button.classList.add(buttonInfo.class);
	}
	button.textContent = buttonInfo.text;

	return button;
}
function CreateFiller(fillerInfo)
{
	var titleElement = document.createElement("h5");
	titleElement.innerHTML = fillerInfo.message;

	return titleElement;
}
function CreateRadio(radioOnfo)
{
	
	var parentElement = document.createElement("div");

	for (const item of radioOnfo.items)
	{
		var element = document.createElement("input");
		element.type = "radio";
		element.name = radioOnfo.name;
		element.placeholder = radioOnfo.placeholder;
		element.setAttribute("required", radioOnfo.required);
		element.disabled = radioOnfo.disabled;
		if(radioOnfo.class != "")
		{
			element.classList.add(radioOnfo.class)
		}
		element.id = item.label;
		element.value = item.value;
		element.checked = item.checked;

		let label = document.createElement("label");
		label.setAttribute("for", `#${item.label}`)
		label.textContent = item.label;

		parentElement.appendChild(element);
		parentElement.appendChild(label);
	}
	return parentElement;
}