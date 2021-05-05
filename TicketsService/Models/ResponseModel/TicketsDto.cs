using System;

namespace TicketsService.Models.ResponseModel
{
    public class TicketsDto
    {
        public string TicketId { get; set; }
        public double Price { get; set; }
        public string CurrencyId { get; set; }
        public int Validity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
