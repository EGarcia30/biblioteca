// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const scrollLeftButton = document.getElementById("scroll-left-button");
const scrollRightButton = document.getElementById("scroll-right-button");
const scrollContainer = document.querySelector(".hide-scroll-bar");

scrollLeftButton.addEventListener("click", function() {
    scrollContainer.scrollBy({
        left: -100,
        behavior: "smooth"
    });
});

scrollRightButton.addEventListener("click", function() {
    scrollContainer.scrollBy({
        left: 100,
        behavior: "smooth"
    });
});