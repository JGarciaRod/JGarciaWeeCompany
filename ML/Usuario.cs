namespace ML
{
    public class Usuario
    {
        public int? IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Empresa { get; set; }

        public List<object>? Usuarios { get; set; }
    }
}