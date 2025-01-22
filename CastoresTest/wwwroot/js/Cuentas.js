if (typeof currentsID === 'undefined') {
    let currentID;
}

function mostrarTablaHijo(id) {
    currentID = id;
    if (document.getElementById('detalleCuenta_' + currentID).style.display == 'none' && document.getElementById('detalleCuenta_' + currentID).rows.length > 1)
        document.getElementById('detalleCuenta_' + currentID).style.display = 'table'
    else
        document.getElementById('detalleCuenta_' + currentID).style.display = 'none'
}

function cocinar(order, preparado, id) {
    var a = document.getElementById('detalleCuenta_' + id).parentNode;
    $.ajax({
        //url: "/Cuentas/UpdateOrden",
        url: '/Cuentas/TablaCuentas',
        type: "POST",
        data: {
            Cuenta: currentID,
            Orden: order,
            Cooked: preparado
        },
        dataType: 'json',
        success: data => {
            success(data);
        },
        error: error => {
            //swal.fire('Ha sucedido un error inesperado')
            $('#Tabla').html(error.responseText);
        }

    }).done(function (data) {
        $('#Tabla').html(data);
    });
}

