function openModal(id) {
    // Show the modal
    document.getElementById(`deleteModal${id}`).classList.remove("hidden");
}

function closeModal(id) {
    // Hide the modal
    document.getElementById(`deleteModal${id}`).classList.add("hidden");
}