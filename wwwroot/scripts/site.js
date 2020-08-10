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


function getElement(id) {
    let wara = document.getElementById(id)
    if (wara == null) {
        alert("Please select an existing option or create a new one!")
        return
    }
    return wara
}

function changePub(publisher) {
    let pub = getElement(`pub-option-${publisher}`)
    if (pub != null) {
        let actualPub = document.getElementsByName("selectedPub")[0]
        actualPub.setAttribute("name", "")
        pub.setAttribute("name","selectedPub")
    }
}

function addBadge(divId, name, typo) {
    let div = document.getElementById(divId)
    let badges = Array.from(document.getElementsByClassName("badge"))
    let existantBadges = []
    console.log(badges)
    badges.forEach(e => {
        existantBadges.push(e.children[0].innerText)
        })
    console.dir(existantBadges)
    if (!existantBadges.includes(name)) {
        div.innerHTML += `
                                <div class="badge">
                                    <span>${name}</span>
                                    <span onclick="removeFromList('${typo}-option-'+this.parentElement.children[0].innerText,this.parentElement)" >x</span>
                                </div>
                                `

    }
}

function addBookGenre(genre) {
    let gen = getElement(`genre-option-${genre}`)
    if (gen != null) {
        gen.setAttribute("name", "selectedGenres")
        addBadge("genres-div", genre, "genre")
    }
}

function addBookAuthor(author) {
    let au = getElement(`author-option-${author}`)
    if (au != null) {
        au.setAttribute("name", "selectedAuthors")
        addBadge("authors-div", author,"author")
    }
}
function sendBookForm(bookId) {
    let pub = document.getElementsByName("selectedPub")
    let aus = document.getElementsByName("selectedGenres")
    let gens = document.getElementsByName("selectedAuthors")
    console.log(`The id of this shitty book is ${bookId}. 
                The pub is ${pub}. 
                The Genres ${gens}. 
                The Authors ${aus}`)
    console.dir(pub)
    console.dir(gens)
    console.dir(aus)

}

function removeFromList(id,thing) {
    thing.outerHTML = ""
    let elem = document.getElementById(id)
    elem.setAttribute("name","")
}