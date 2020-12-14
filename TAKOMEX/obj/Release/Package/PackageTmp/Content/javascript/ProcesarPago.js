
$(document).ready(function () {
    $("#form-ProcesarPago").validate({
        rules: {
            Email: {
                required: true,
                email: true,
                maxlength: 76
            },
            Telefono: {
                required: true,
                number: true,
                minlength: 8,
                maxlength: 25
            },
            Direccion: {
                required: true,
                minlength: 20,
                maxlength: 150
            },
            Ciudad: {
                required: true,
                minlength: 5,
                maxlength: 50
            }
        },
        messages: {
            Email: {
                required: "Debe completar este campo",
                email: "Debe introducir Email valido",
                maxlength: "Email demasiado largo"
            },
            Telefono: {
                required: "Debe completar este campo",
                number: "DDebe introducir un numero de telefono valido",
                minlength: "Telefono demasiado corto",
                maxlength: "Telefono demasiado largo"
            },
            Direccion: {
                required: "Debe completar este campo",
                minlength: "Direccion demasiado corta",
                maxlength: "Direccion demasiado larga"
            },
            Ciudad: {
                required: "Debe completar este campo",
                minlength: "Nombre de ciudad demasiado corta",
                maxlength: "Nombre de ciudad demasiado larga"
            }
        },

    });
});

