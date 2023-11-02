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