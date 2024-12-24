namespace ChallengeFront.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string Cuit { get; set; }

        public string Domicilio { get; set; }
   
        public string Celular { get; set; }
   
        public string Email { get; set; }
    }
}
