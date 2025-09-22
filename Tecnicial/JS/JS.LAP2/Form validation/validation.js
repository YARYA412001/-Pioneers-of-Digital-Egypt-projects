const form = document.getElementById("form");
const errors = document.getElementsByClassName("msg-error");

function displayError(input, eleId, msg, color = "red") {
    document.getElementById(eleId).innerText = msg;
    document.getElementById(eleId).style.color = color;
}

function clearErrors() {
    for (let item of errors) {
        item.innerText = "";
    }
}

form.onsubmit = function(e) {
    e.preventDefault();
    clearErrors();
    let isValid = true;

    // Name validation
    if (form['name'].value === "") {
        displayError(form['name'], "name-error", "Name is required");
        isValid = false;
    } else if (!/^[a-zA-Z ]{3,20}$/.test(form['name'].value)) {
        displayError(form['name'], "name-error", "Invalid name (3-20 letters only)");
        isValid = false;
    }

    // Email validation
    if (form['email'].value === "") {
        displayError(form['email'], "email-error", "Email is required");
        isValid = false;
    } else if (!/^[a-zA-Z]+[a-zA-Z0-9._]*@[a-z]+\.[a-z]{2,}$/.test(form['email'].value)) {
        displayError(form['email'], "email-error", "Invalid email");
        isValid = false;
    }

    // Password validation
    if (form['password'].value === "") {
        displayError(form['password'], "password-error", "Password is required");
        isValid = false;
    } else if (!/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(form['password'].value)) {
        displayError(form['password'], "password-error", "Password must be at least 8 characters with letters and numbers");
        isValid = false;
    }

    // Confirm password validation
    if (form['confirm'].value === "") {
        displayError(form['confirm'], "confirm-error", "Confirm password is required");
        isValid = false;
    } else if (form['confirm'].value !== form['password'].value) {
        displayError(form['confirm'], "confirm-error", "Passwords do not match");
        isValid = false;
    }

    // Phone validation
if (form['phone'].value === "") {
        displayError(form['phone'], "phone-error", "Phone is required");
        isValid = false;
    } else if (!/^\d{11}$/.test(form['phone'].value)) {
        displayError(form['phone'], "phone-error", "Invalid phone number (11 digits required)");
        isValid = false;
    }

    if (isValid) {
        alert("Form submitted successfully!");
        form.reset();
    }
    return isValid;
};


