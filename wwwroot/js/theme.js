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