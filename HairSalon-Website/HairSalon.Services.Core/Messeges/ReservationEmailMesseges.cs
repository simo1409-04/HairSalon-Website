using HairSalon.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HairSalon.Services.Core.Messeges
{
    public static class ReservationEmailMessages
    {
      
            private static readonly CultureInfo BgCulture =
                CultureInfo.GetCultureInfo("bg-BG");

            private static string GetServicesList(Reservation reservation)
            {
                return string.Join(Environment.NewLine,
                    reservation.Services.Select(service =>
                        $"- {service.Name}: {service.Price.ToString("N2", BgCulture)} €"));
            }

            public static string GetClientMessage(Reservation reservation)
            {
                ArgumentNullException.ThrowIfNull(reservation);

                string services = GetServicesList(reservation);

                return $"""
                Здравейте, {reservation.ClientName}!

                Вашата резервация в Graphite Barber Studio
                е успешно потвърдена!

                Детайли за резервацията:

                Дата: {reservation.ReservationDate:dd.MM.yyyy}
                Час: {reservation.ReservationTime:HH:mm}
                Фризьор: {reservation.HairDresser.Name}

                Избрани услуги:
                {services}

                Обща цена: {reservation.TotalPrice.ToString("N2", BgCulture)} €

                Очакваме Ви!

                С уважение,
                Екипът на Graphite Barber Studio
                """;
            }

            public static string GetHairdresserMessage(Reservation reservation)
            {
                ArgumentNullException.ThrowIfNull(reservation);

                string services = GetServicesList(reservation);

                return $"""
                Нова резервация в Graphite Barber Studio!

                Информация за клиента:

                Име: {reservation.ClientName}
                Телефон: {reservation.ClientPhone}
                Имейл: {reservation.ClientEmail ?? "Не е посочен"}

                Детайли за резервацията:

                Дата: {reservation.ReservationDate:dd.MM.yyyy}
                Час: {reservation.ReservationTime:HH:mm}

                Избрани услуги:
                {services}

                Обща цена: {reservation.TotalPrice.ToString("N2", BgCulture)} €

                Можете да прегледате резервацията
                и в административния панел.

                """;
            }
        

    }
}