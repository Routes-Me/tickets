using System;
using System.Collections.Generic;

namespace TicketsService.Models.DBModels
{
    public partial class Tickets
    {
        public int TicketId { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }
        public int Validity { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Currencies Currency { get; set; }
    }
}
