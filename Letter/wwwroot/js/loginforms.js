let hideelement = document.querySelector("header");
hideelement.style.display = "none";

let brdiv = document.getElementById("brdiv");

for (let i = 0; i < 3; i++) {
    brdiv.appendChild(document.createElement("br"))
}