const username = document.getElementById("username");
const imagename = document.getElementById("imagename");
const fonts = document.getElementById("fonts");
const plus = document.getElementById("plus");
const minus = document.getElementById("minus");
const clickBtn = document.getElementById("clickBtn");
const message = document.getElementById("message");
const userImg = document.getElementById("userImg");
const expand = document.getElementById("expand");
const box = document.getElementById("box");
let fontSize = 16;
clickBtn.onclick = function () {
  // check name
  if (username.value.trim() !== "") {
    message.innerText = "Welcome " + username.value;
  } else {
    message.innerText = "Invalid name!";
  }

  // check image
  if (imagename.value.trim().endsWith(".jpg")) {
    userImg.src = imagename.value;
    userImg.style.display = "block";
  } else {
    userImg.style.display = "none";
  }
};

fonts.onchange = function () {
  message.style.fontFamily = fonts.value;
};

plus.onclick = function () {
  if (fontSize < 40) {
    fontSize += 2;
    message.style.fontSize = fontSize + "px";
  }
};

minus.onclick = function () {
  if (fontSize > 10) {
    fontSize -= 2;
    message.style.fontSize = fontSize + "px";
  }
};

expand.onclick = function () {
  let currentWidth = userImg.offsetWidth;
  userImg.style.width = (currentWidth + 20) + "px";
};
