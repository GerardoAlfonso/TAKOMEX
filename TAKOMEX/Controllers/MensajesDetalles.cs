using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAKOMEX.Controllers
{
    public class MensajesDetalles
    {
        public int IdMensaje { get; set; }
        public string Correo { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string Created_at { get; set; }
        public int Estado { get; set; }
        public MensajesDetalles()
        {

        }
    }
}