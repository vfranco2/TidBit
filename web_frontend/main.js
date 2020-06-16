const hearts = document.querySelectorAll('.header-favorite-icon i');

hearts.forEach((heart) => {
    heart.addEventListener('click', runEvent)
})

function runEvent(e){
    console.log(`Event Type: ${e.type}`)
    
    e.target.classList.toggle("fas");


}