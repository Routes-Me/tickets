using TicketsService.Models.DBModels;
using TicketsService.Models.ResponseModel;

using TicketsService.Models;

namespace TicketsService.Abstraction
{
    public interface ITicketsRepository
    {
        dynamic GetTickets(string ticketId, Pagination pageInfo);
        Tickets PostTickets(TicketsDto ticketsDto);
        Tickets UpdateTicket(TicketsDto ticketsDto);
        Tickets DeleteTickets(string ticketId);
    }
}
