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
                "SELECT COUNT(*) FROM gebruiker WHERE email=@email", conn);
            cmd.Parameters.AddWithValue("@email", email);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public void RegistreerGebruiker(string? naam, string? email, string? wachtwoord)
        {
            using var conn = new MySqlConnection(_conn);
            conn.Open();
            // Include the Rol column in the INSERT and provide a default role so
            // the database doesn't reject the insert when Rol has no default value.
            var cmd = new MySqlCommand(
                "INSERT INTO gebruiker (naam, email, wachtwoord, Rol) VALUES (@naam, @email, @ww, @rol)", conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@ww", wachtwoord);
            // Set a sane default role for new users. Adjust value if your DB uses integers.
            cmd.Parameters.AddWithValue("@rol", "user");
            cmd.ExecuteNonQuery();
        }

        public Gebruiker? Login(string? email, string? wachtwoord)
        {
            using var conn = new MySqlConnection(_conn);
            conn.Open();
            var cmd = new MySqlCommand(
                "SELECT * FROM gebruiker WHERE email=@email AND wachtwoord=@ww", conn);
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
