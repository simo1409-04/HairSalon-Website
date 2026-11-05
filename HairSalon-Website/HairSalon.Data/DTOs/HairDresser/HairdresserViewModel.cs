using System;
using System.Collections.Generic;
using System.Text;
using HairSalon.Data.DTOs.Reservations;

namespace HairSalon.Data.DTOs.HairDresser
{
    public class HairdresserViewModel
    {

        public string Name { get; set; } = null!;


        public int Id { get; set; }

     public   ICollection<ReservationViewModel> Reservations { get; set; } = new HashSet<ReservationViewModel>();


    }
}
