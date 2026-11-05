using HairSalon.Data.DTOs.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HairSalon.Data.DTOs.Reservations
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public string ClientName { get; set; } = null!;

        public string ClientPhone { get; set; } = null!;

        public DateOnly ReservationDate { get; set; }

        public TimeOnly ReservationTime { get; set; }

        public int HairDresserId { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<ServiceViewModelForReservationView> Services { get; set; } = new HashSet<ServiceViewModelForReservationView>();
    }
}
