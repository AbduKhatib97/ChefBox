$(document).ready(() => {
    ClassicEditor
        .create(document.querySelector('#Recipe_Description'))
        .then(editor => {
            console.log(editor);
        })
        .catch(error => {
            console.error(error);
        });
});
