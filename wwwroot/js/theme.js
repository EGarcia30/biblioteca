const btnTheme = document.getElementById('btnTheme')
const iconTheme = document.getElementById('iconTheme')
const logoTheme = document.getElementById('logo')

document.addEventListener('DOMContentLoaded', () =>{
    if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        document.documentElement.classList.add('dark');
        iconTheme.classList.replace('fa-moon','fa-sun')
        logoTheme.src = "/img/logo-oscuro.png"
    } else {
        document.documentElement.classList.remove('dark');
        iconTheme.classList.replace('fa-sun','fa-moon')
        logoTheme.src = "/img/logo-claro.png"
    }
});

const handleTheme = () => {
    if(document.documentElement.classList.contains('dark')){
        document.documentElement.classList.remove('dark');
        iconTheme.classList.replace('fa-sun','fa-moon')
        localStorage.theme = "light";
        logoTheme.src = "/img/logo-claro.png"
    }
    else{
        document.documentElement.classList.add('dark');
        iconTheme.classList.replace('fa-moon','fa-sun')
        localStorage.theme = "dark";
        logoTheme.src = "/img/logo-oscuro.png"
    }
}

btnTheme.addEventListener('click', handleTheme);