function toggleModal(type, id) {
    if (id == null) {
        let addBox = document.getElementById(type)
        addBox.classList.toggle("modal-show")
        return
    }
    let box = document.getElementById("delete-" + type + "-" + id)
    box.classList.toggle("modal-show")
}

function bookDetails(id) {

    fetch(`/Book/Details/${id}`)
        .then(r => r.json())
        .then(json => moreDetails(json))
        .catch(e => alert(e.message))
}

function moreDetails(book) {

    let bkdesc = document.getElementById(`desc-${book.id}`)
    let authors = ""
    book.bookAuth.forEach(a =>{
        authors = authors + a.auth.name + "  "
    })
    let genres = ""
    book.bookGen.forEach(g=>{
        genres = genres + g.gen.name + "  " 
    })

    let fullDescription = ` ISBN: ${book.isbn} <br/> 
                            Author/s: ${authors} <br />
                            Genre/s: ${genres}<br />
                            Publisher: ${book.pub.name} <br />                 
                           `
    bkdesc.innerHTML = fullDescription
}