function toggleModal(type, id) {
    if (id == null) {
        let addBox = document.getElementById(type)
        addBox.classList.toggle("modal-show")
        return
    }
    let box = document.getElementById("delete-" + type + "-" + id)
    box.classList.toggle("modal-show")
}
