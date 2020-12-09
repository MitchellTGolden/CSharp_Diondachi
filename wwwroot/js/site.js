// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function CrackEgg() {
    fetch("http://localhost:5000/crack")
        .then(res => res.json())
        .then(res => {
            document.getElementById("crack").innerHTML = res.num;
        })
}

var clicks = 0;
function onClick() {
    clicks += 1;
    document.getElementById("clicks").innerHTML = clicks;
};