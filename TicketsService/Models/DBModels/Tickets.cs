using System;

namespace TicketsService.Models.DBModels
{
    public partial class Tickets
    {
        public int TicketId { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }
        public int Validity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
