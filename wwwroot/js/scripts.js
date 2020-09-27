function letSee() {
    alert("hello world");
    var form = document.getElementById('my-form');
    console.log(form);
    if (form.attachEvent) {
        form.attachEvent("submit", processForm);
    } else {
        form.addEventListener("submit", processForm);
    }
}

function processForm(e) {
    if (e.preventDefault) e.preventDefault();
    
    return false;
}
