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

        public Dictionary<DateOnly, int> ReservationsCountPerDay { get; set; } = new Dictionary<DateOnly, int>();


    }
}
