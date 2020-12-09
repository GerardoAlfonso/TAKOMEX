﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DataBase : DbContext
    {
        public DataBase()
            : base("name=DataBase")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Articulos> Articulos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Ventas> Ventas { get; set; }
        public virtual DbSet<Mensajes> Mensajes { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    
        public virtual int sp_e_Personas(Nullable<int> id, Nullable<int> estado)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_e_Personas", idParameter, estadoParameter);
        }
    
        public virtual int sp_i_empleado(Nullable<int> idPersona)
        {
            var idPersonaParameter = idPersona.HasValue ?
                new ObjectParameter("idPersona", idPersona) :
                new ObjectParameter("idPersona", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_i_empleado", idPersonaParameter);
        }
    
        public virtual int sp_i_Personas(Nullable<int> idRol, string nombre, string apellido, Nullable<int> edad, string correo, string sexo, string contraseña, Nullable<int> estado)
        {
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("idRol", idRol) :
                new ObjectParameter("idRol", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidoParameter = apellido != null ?
                new ObjectParameter("Apellido", apellido) :
                new ObjectParameter("Apellido", typeof(string));
    
            var edadParameter = edad.HasValue ?
                new ObjectParameter("Edad", edad) :
                new ObjectParameter("Edad", typeof(int));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var sexoParameter = sexo != null ?
                new ObjectParameter("Sexo", sexo) :
                new ObjectParameter("Sexo", typeof(string));
    
            var contraseñaParameter = contraseña != null ?
                new ObjectParameter("Contraseña", contraseña) :
                new ObjectParameter("Contraseña", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_i_Personas", idRolParameter, nombreParameter, apellidoParameter, edadParameter, correoParameter, sexoParameter, contraseñaParameter, estadoParameter);
        }
    
        public virtual int sp_i_Roles(string nombre)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_i_Roles", nombreParameter);
        }
    
        public virtual int sp_i_usuario(Nullable<int> idPersona)
        {
            var idPersonaParameter = idPersona.HasValue ?
                new ObjectParameter("idPersona", idPersona) :
                new ObjectParameter("idPersona", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_i_usuario", idPersonaParameter);
        }
    
        public virtual int sp_u_Personas(Nullable<int> id, string nombre, string apellido, Nullable<int> edad, string correo, string sexo, string contraseña, Nullable<int> estado)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidoParameter = apellido != null ?
                new ObjectParameter("Apellido", apellido) :
                new ObjectParameter("Apellido", typeof(string));
    
            var edadParameter = edad.HasValue ?
                new ObjectParameter("Edad", edad) :
                new ObjectParameter("Edad", typeof(int));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var sexoParameter = sexo != null ?
                new ObjectParameter("Sexo", sexo) :
                new ObjectParameter("Sexo", typeof(string));
    
            var contraseñaParameter = contraseña != null ?
                new ObjectParameter("Contraseña", contraseña) :
                new ObjectParameter("Contraseña", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_u_Personas", idParameter, nombreParameter, apellidoParameter, edadParameter, correoParameter, sexoParameter, contraseñaParameter, estadoParameter);
        }
    
        public virtual int sp_u_Roles(Nullable<int> id, string nombre)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_u_Roles", idParameter, nombreParameter);
        }
    
        public virtual ObjectResult<sp_l_persona_Result> sp_l_persona(string correo)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_l_persona_Result>("sp_l_persona", correoParameter);
        }
    
        public virtual int sp_i_venta(Nullable<int> idUsuario, string detalles, string importe, string iVA, string total)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("idUsuario", idUsuario) :
                new ObjectParameter("idUsuario", typeof(int));
    
            var detallesParameter = detalles != null ?
                new ObjectParameter("Detalles", detalles) :
                new ObjectParameter("Detalles", typeof(string));
    
            var importeParameter = importe != null ?
                new ObjectParameter("Importe", importe) :
                new ObjectParameter("Importe", typeof(string));
    
            var iVAParameter = iVA != null ?
                new ObjectParameter("IVA", iVA) :
                new ObjectParameter("IVA", typeof(string));
    
            var totalParameter = total != null ?
                new ObjectParameter("Total", total) :
                new ObjectParameter("Total", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_i_venta", idUsuarioParameter, detallesParameter, importeParameter, iVAParameter, totalParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_i_mensaje(Nullable<int> idPersona, string asunto, string mensaje, Nullable<int> estado)
        {
            var idPersonaParameter = idPersona.HasValue ?
                new ObjectParameter("IdPersona", idPersona) :
                new ObjectParameter("IdPersona", typeof(int));
    
            var asuntoParameter = asunto != null ?
                new ObjectParameter("Asunto", asunto) :
                new ObjectParameter("Asunto", typeof(string));
    
            var mensajeParameter = mensaje != null ?
                new ObjectParameter("Mensaje", mensaje) :
                new ObjectParameter("Mensaje", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_i_mensaje", idPersonaParameter, asuntoParameter, mensajeParameter, estadoParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_u_mensajes(Nullable<int> idMensaje, Nullable<int> estado)
        {
            var idMensajeParameter = idMensaje.HasValue ?
                new ObjectParameter("IdMensaje", idMensaje) :
                new ObjectParameter("IdMensaje", typeof(int));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_u_mensajes", idMensajeParameter, estadoParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
