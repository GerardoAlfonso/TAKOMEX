using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAKOMEX.Models
{
    public class ModelArticulos
    {
        public int idCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion_corta { get; set; }
        public string Descripcion_larga { get; set; }
        public string IMG { get; set; }
        public string Precio { get; set; }
        public string Estado { get; set; }
    }
}