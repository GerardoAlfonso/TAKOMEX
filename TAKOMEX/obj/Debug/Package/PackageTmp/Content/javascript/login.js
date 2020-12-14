
$(document).ready(function () {
    $("#form-login").validate({
        rules: {
            cemail: {
                required: true,
                email: true,
                maxlength: 70
            },
            password: {
                required: true,
                minlength: 8,
                maxlength: 25
            }
        },
        messages: {
            cemail: {
                required: "Debe completar este campo",
                email: "Debe introducir un email valido",
                maxlength: "Direccion de correo demasiado larga"
            },
            password: {
                required: "Debe completar este campo",
                minlength: "Su contraseña debe tener al menos 8 caracteres",
                maxlength: "Contraseña demasiado larga"
            }
        },
        invalidHandler: function (event, validator) {

        },
        submitHandler: function (form) {
            form.submit();

        }
    });
});
