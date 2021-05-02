using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TicketsService.Abstraction;
using TicketsService.Models;
using TicketsService.Models.DBModels;
using TicketsService.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsService.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("v{version:apiVersion}/")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsRepository _TicketsRepository;
        private readonly TicketsServiceContext _context;
        public TicketsController(ITicketsRepository TicketsRepository, TicketsServiceContext context)
        {
            _TicketsRepository = TicketsRepository;
            _context = context;
        }

        [HttpGet]
        [Route("tickets/{ticketId?}")]
        public IActionResult GetTickets(string ticketId, [FromQuery] Pagination pageInfo)
        {
            GetResponse response = new GetResponse();
            try
            {
                response = _TicketsRepository.GetTickets(ticketId, pageInfo);
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponse{ error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        [Route("tickets")]
        public async Task<IActionResult> PostTickets(TicketsDto ticketsDto)
        {
            try
            {
                Tickets ticket = _TicketsRepository.PostTickets(ticketsDto);
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponse{ error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            return StatusCode(StatusCodes.Status201Created, new SuccessResponse{ message = CommonMessage.TicketsInserted});
        }

        [HttpPut]
        [Route("tickets")]
        public async Task<IActionResult> UpdateTicket(TicketsDto ticketsDto)
        {
            try
            {
                Tickets ticket = _TicketsRepository.UpdateTicket(ticketsDto);
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponse{ error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse{ error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete]
        [Route("tickets/{ticketId}")]
        public async Task<IActionResult> DeleteTickets(string ticketId)
        {
            try
            {
                Tickets ticket = _TicketsRepository.DeleteTickets(ticketId);
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse{ error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
