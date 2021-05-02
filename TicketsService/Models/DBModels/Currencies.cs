using System;

namespace TicketsService.Models.DBModels
{
    public partial class Currencies
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Tickets Ticket { get; set; }
    }
}
