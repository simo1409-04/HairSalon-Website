namespace HairSalon.Data.DTOs.Service
{
    public class ServiceInReservationCreateDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
