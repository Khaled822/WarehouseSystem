namespace WarehouseSystem.Models
{
    public class Gebruiker
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
        public string? Email { get; set; }
        public string? Wachtwoord { get; set; }
    }

    public class LoginViewModel
    {
        public string? Email { get; set; }
        public string? Wachtwoord { get; set; }
    }

    public class RegisterViewModel
    {
        public string? Naam { get; set; }
        public string? Email { get; set; }
        public string? Wachtwoord { get; set; }
        public string? WachtwoordBevestig { get; set; }
    }
}