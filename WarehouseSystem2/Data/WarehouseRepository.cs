using MySql.Data.MySqlClient;
using WarehouseSystem.Models;

namespace WarehouseSystem.Data
{
    public class WarehouseRepository
    {
        private readonly string _conn;

        public WarehouseRepository(string conn)
        {
            _conn = conn;
        }

        public bool EmailBestaat(string? email)
        {
            using var conn = new MySqlConnection(_conn);
            conn.Open();
            var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM gebruikers WHERE email=@email", conn);
            cmd.Parameters.AddWithValue("@email", email);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public void RegistreerGebruiker(string? naam, string? email, string? wachtwoord)
        {
            using var conn = new MySqlConnection(_conn);
            conn.Open();
            var cmd = new MySqlCommand(
                "INSERT INTO gebruikers (naam, email, wachtwoord) VALUES (@naam, @email, @ww)", conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@ww", wachtwoord);
            cmd.ExecuteNonQuery();
        }

        public Gebruiker? Login(string? email, string? wachtwoord)
        {
            using var conn = new MySqlConnection(_conn);
            conn.Open();
            var cmd = new MySqlCommand(
                "SELECT * FROM gebruikers WHERE email=@email AND wachtwoord=@ww", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@ww", wachtwoord);
            var r = cmd.ExecuteReader();
            if (r.Read())
                return new Gebruiker
                {
                    Id = r.GetInt32("id"),
                    Naam = r.GetString("naam"),
                    Email = r.GetString("email")
                };
            return null;
        }
    }
}