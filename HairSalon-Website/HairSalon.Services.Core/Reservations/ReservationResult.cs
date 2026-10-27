using System;
using HairSalon.Data.Models;
using HairSalon.Services.Core.Reservations.Enums;

namespace HairSalon.Services.Core.Reservations
{
    public class ReservationResult
    {
        public ReservationStatus Status { get; set; }

        public Reservation? Reservation { get; set; }

    }
}
