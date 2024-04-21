const imgs = document.querySelectorAll(".img-click");
const bg = document.getElementById("bg");
const clse = document.getElementById("close");

for(const img of imgs){
    img.addEventListener("click", () => {
        img.classList.add('z-50','fixed', 'top-0', 'left-50', 'h-full', 'object-fit-contain')
        bg.classList.remove("hidden");
        clse.classList.remove("hidden");
    })
}   

// Agregamos un event listener al fondo y al elemento de cerrar
bg.addEventListener("click", () => {
    imgs.forEach(img => {
      img.classList.remove('z-50', 'fixed', 'top-0', 'left-50', 'h-full', 'object-fit-contain');
    });
    bg.classList.add("hidden");
    clse.classList.add("hidden");
  });
  
  clse.addEventListener("click", () => {
    imgs.forEach(img => {
      img.classList.remove('z-50', 'fixed', 'top-0', 'left-50', 'h-full', 'object-fit-contain');
    });
    bg.classList.add("hidden");
    clse.classList.add("hidden");
  });