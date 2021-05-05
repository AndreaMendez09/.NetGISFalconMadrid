using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Usuario
    {
        public Usuario()
        {
            HistoricoEstados = new HashSet<HistoricoEstado>();
            OperadoresZonas = new HashSet<OperadoresZona>();
            PeticionOperadores = new HashSet<PeticionOperadore>();
            Peticions = new HashSet<Peticion>();
        }

        public Usuario(string nombre, string apellido, string email, string genero, string idioma, string contraseña, string foto, string municipio, DateTime? fechaNacimiento, int? rol)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Genero = genero;
            Idioma = idioma;
            Contraseña = contraseña;
            Foto = foto;
            Municipio = municipio;
            FechaNacimiento = fechaNacimiento;
            Rol = rol;
        }

        public Usuario(string nombre, string apellido, string email, string genero, string idioma, string foto, string municipio, DateTime? fechaNacimiento, int? rol)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Genero = genero;
            Idioma = idioma;
            Foto = foto;
            Municipio = municipio;
            FechaNacimiento = fechaNacimiento;
            Rol = rol;
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public string Idioma { get; set; }
        public string Contraseña { get; set; }
        public string Foto { get; set; }
        public string Municipio { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Rol { get; set; }

        public virtual ICollection<HistoricoEstado> HistoricoEstados { get; set; }
        public virtual ICollection<OperadoresZona> OperadoresZonas { get; set; }
        public virtual ICollection<PeticionOperadore> PeticionOperadores { get; set; }
        public virtual ICollection<Peticion> Peticions { get; set; }
    }
}
