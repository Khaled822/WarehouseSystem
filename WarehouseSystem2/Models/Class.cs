namespace WarehouseSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Categorie { get; set; }
        public int Voorraad { get; set; }
        public string Status { get; set; }
        public int MagazijnId { get; set; }
    }
}