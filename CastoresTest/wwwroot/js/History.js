function FilterEntradas(x) {
    Filter(1)
}
function FilterSalidas(x) {
    Filter(2)
}
function Filter(x) {
    $.ajax({
        url: '/History/ListadoMovimientos',
        type: "GET",
        data: {
            filtro: x
        },
        success: data => {
            $('#TablaMovimientos').html(data)
        },
        error: error => {
            $('#TablaMovimientos').html(error)
        }
    });
}
