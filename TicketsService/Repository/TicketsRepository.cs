using TicketsService.Abstraction;
using TicketsService.Models;
using TicketsService.Models.Common;
using TicketsService.Models.DBModels;
using TicketsService.Models.ResponseModel;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RoutesSecurity;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TicketsService.Repository
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly AppSettings _appSettings;
        private readonly TicketsServiceContext _context;
        public TicketsRepository(IOptions<AppSettings> appSettings, TicketsServiceContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public dynamic GetTickets(string ticketId, Pagination pageInfo)
        {
            List<Tickets> tickets = new List<Tickets>();
            int recordsCount = 1;
 
            if (!string.IsNullOrEmpty(ticketId))
                tickets = _context.Tickets.Where(r => r.TicketId == Obfuscation.Decode(ticketId)).ToList();
            else
            {
                tickets = _context.Tickets.Skip((pageInfo.offset - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();
                recordsCount = _context.Tickets.Count();
            }

            var page = new Pagination
            {
                offset = pageInfo.offset,
                limit = pageInfo.limit,
                total = recordsCount
            };

            dynamic TicketsData = tickets.Select(r => new TicketsDto {
                    TicketId = Obfuscation.Encode(r.TicketId),
                    Price = r.Price,
                    CurrencyId = Obfuscation.Encode(r.CurrencyId),
                    Validity = r.Validity,
                    CreatedAt = r.CreatedAt,
                }).ToList();       

            return new GetResponse
            {
                data = JArray.Parse(JsonConvert.SerializeObject(TicketsData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })),
                pagination = page
            };
        }

        public Tickets PostTickets(TicketsDto ticketsDto)
        {
            if (ticketsDto == null)
                throw new ArgumentNullException(CommonMessage.InvalidData);
            
            return new Tickets
            {
                Price = ticketsDto.Price,
                CurrencyId = Obfuscation.Decode(ticketsDto.CurrencyId),
                Validity = ticketsDto.Validity,
                CreatedAt = DateTime.Now
            };
        }

        public Tickets UpdateTicket(TicketsDto ticketsDto)
        {
            if (ticketsDto == null || ticketsDto.TicketId == null)
                throw new ArgumentNullException(CommonMessage.InvalidData);

            Tickets ticket = _context.Tickets.Where(r => r.TicketId == Obfuscation.Decode(ticketsDto.TicketId)).FirstOrDefault();
            if (ticket == null)
                throw new ArgumentException(CommonMessage.TicketsNotFound);

            ticket.Price = !string.IsNullOrEmpty(ticketsDto.Price.ToString()) ? ticketsDto.Price : ticket.Price;
            ticket.Validity = !string.IsNullOrEmpty(ticketsDto.Validity.ToString()) ? ticketsDto.Validity : ticket.Validity;
            ticket.CurrencyId = !string.IsNullOrEmpty(ticketsDto.CurrencyId) ? Obfuscation.Decode(ticketsDto.CurrencyId) : ticket.CurrencyId;

            return ticket;
        }

        public Tickets DeleteTickets(string ticketId)
        {
            if (string.IsNullOrEmpty(ticketId))
                throw new ArgumentNullException(CommonMessage.InvalidData);

            int ticketIdDecrypted = Obfuscation.Decode(ticketId);
            Tickets ticket = _context.Tickets.Where(r => r.TicketId == ticketIdDecrypted).FirstOrDefault();
            if (ticket == null)
                throw new KeyNotFoundException(CommonMessage.TicketsNotFound);

            return ticket;
        }
    }
}
