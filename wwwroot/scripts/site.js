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
    let arrow = document.getElementById("details-arrow")
    arrow.innerHTML=""
    let bkdesc = document.getElementById(`desc-${book.id}`)
    let authors = ""
    book.bookAuth.forEach(a =>{
        authors = authors + a.auth.name + "  "
    })
    let genres = ""
    book.bookGen.forEach(g=>{
        genres = genres + g.gen.name + "  " 
    })

    bkdesc.classList.add("fade-out")

    let fullDescription = ` ISBN: ${book.isbn} <br/> 
                            Author/s: ${authors} <br />
                            Genre/s: ${genres}<br />
                            Publisher: ${book.pub.name} <br />                 
                           `

    setTimeout(() => {
        bkdesc.innerHTML = fullDescription
        bkdesc.classList.remove("fade-out")
    }, 500)
}


function auto_grow(desc) {
    desc.style.height = "1rem"
    desc.style.height = (desc.scrollHeight) + "px"
}

window.onload = () => {
    if (document.URL.indexOf("Genre/Edit") != -1) {
        let lgDescription = document.getElementById("Description")
        auto_grow(lgDescription)
    }
}
