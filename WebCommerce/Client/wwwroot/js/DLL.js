function mostrarTabla() {
    if ($('#tabla'))
        $('#tabla').DataTable();
}

function destruirTabla() {
    if ($('#tabla'))
        $('#tabla').Destroy();
}



function previewData() {

    var reader = new FileReader();

    reader.readAsDataURL(document.getElementById("fuFoto").files[0]);

    reader.onloadend = function () {
        document.getElementById("imgFoto").src = reader.result;
    }

}

function ObterImagem() {
    return document.getElementById("imgFoto").src;
}

function Mensagem(msg) {
    alert(msg);
}

getDocumentCookie = function () {
    return Promise.resolve(document.cookie);
};

window.cultura = {
    get: () => window.localStorage['cultura'],
    set: (value) => window.localStorage['cultura'] = value
};