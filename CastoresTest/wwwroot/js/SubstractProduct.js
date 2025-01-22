window.onload = function () {
    start_val = $('#CANTIDAD').val()
}

function actualizar_valor(x) {
    if ($('#CANTIDAD').val() > start_val)
        $('#CANTIDAD').val(start_val)
    else if ($('#CANTIDAD').val() < 0)
        $('#CANTIDAD').val(0)
}