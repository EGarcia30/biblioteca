// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const btnTheme = document.getElementById('btnTheme')
const iconTheme = document.getElementById('iconTheme')

document.addEventListener('DOMContentLoaded', () =>{
    if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        document.documentElement.classList.add('dark');
        iconTheme.classList.replace('fa-moon','fa-sun')

    } else {
        document.documentElement.classList.remove('dark');
        iconTheme.classList.replace('fa-sun','fa-moon')
    }
});

const handleTheme = () => {
    if(document.documentElement.classList.contains('dark')){
        document.documentElement.classList.remove('dark');
        iconTheme.classList.replace('fa-sun','fa-moon')
        localStorage.theme = "light";
    }
    else{
        document.documentElement.classList.add('dark');
        iconTheme.classList.replace('fa-moon','fa-sun')
        localStorage.theme = "dark";
    }
}

btnTheme.addEventListener('click', handleTheme);

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

const btnMenu = document.getElementById('btnMenu');
const bgBlackBtn = document.getElementById('bgBlackBtn');
const btnClose = document.getElementById('btnClose');
const menu = document.getElementById('menu');
const links = document.querySelectorAll('.link');

const handleMenu = () => {
    if(window.innerWidth <= 1024){
        if(menu.classList.contains('translate-x-full')){
            menu.classList.replace('translate-x-full', 'translate-x-0');
            bgBlackBtn.classList.replace('hidden', 'block');
        }
        else{
            menu.classList.replace('translate-x-0','translate-x-full');
            bgBlackBtn.classList.replace('block','hidden');
        }
    } 
}

btnMenu.addEventListener('click', handleMenu);
bgBlackBtn.addEventListener('click', handleMenu);
btnClose.addEventListener('click', handleMenu);
links.forEach(link =>{
    link.addEventListener('click', handleMenu);
});