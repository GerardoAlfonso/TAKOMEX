//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TAKOMEX.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inventario
    {
        public int idInventario { get; set; }
        public int idArticulo { get; set; }
        public int IdCategoria { get; set; }
        public int Existencias { get; set; }
        public System.DateTime Created_at { get; set; }
        public System.DateTime Updated_at { get; set; }
    
        public virtual Categorias Categorias { get; set; }
    }
}
