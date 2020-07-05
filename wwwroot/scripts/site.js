function toggleModal(type, id) {
    console.log("delete-"+type+id)
    let box = document.getElementById("delete-" + type + "-" + id)
    console.log(box)
    box.classList.toggle("modal-show")
}