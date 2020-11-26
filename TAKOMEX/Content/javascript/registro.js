function comprobar(obj)
{
    let submit = document.getElementById('submit');

    if (obj.checked){
       submit.removeAttribute("disabled");
       submit.style.cursor = 'pointer';

    } else if(!obj.checked){
        submit.style.cursor = 'not-allowed';
        submit.removeAttribute("disabled");
        submit.setAttribute("disabled", "disabled")
   }
}
$(document).ready(function() {
  $("#form-registro").validate({
    rules: {
      nombre: {
        required: true,
        minlength: 3,
        maxlength: 25
      },
      apellido: {
        required: true,
        minlength: 3,
        maxlength: 25
      },
      edad: {
        required: true,
        number: true,
        minlength: 1,
        maxlength: 2
      },
      correo: {
        required: true,
        email: true,
        maxlength: 67
      },
      password: {
        required: true,
        minlength: 8,
        maxlength: 25
        }
    },
    messages : {
      nombre: {
        required: "Debe completar este campo",
        minlength: "Nombre demasiado corto",
        maxlength: "Nombre demasiado largo"
      },
      apellido: {
        required: "Debe completar este campo",
        minlength: "Apellido demasiado corto",
        maxlength: "Apellido demasiado largo"
      },
      edad: {
        required: "Debe completar este campo",
        minlength: "Numero demasiado corto",
        maxlength: "Numero demasiado largo",
        number: "Debe introducir un numero de telefono valido"
      },
      correo: {
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
    highlight: function(element) {
            $(element).addClass('error');
        }, unhighlight: function(element) {
            $(element).removeClass('error');
        }
  });
});
