using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class TicketServiceLoggerDecorator : ITicketService
    {
        private ITicketService _ticketService;
        private ILogger _logger;

        public TicketServiceLoggerDecorator(ITicketService ticketService, ILogger logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        public Ticket CreateTicket(int reservationId, string fName, string lName, IPriceCalculationStrategy priceCalcStrat)
        {
            return _ticketService.CreateTicket(reservationId, fName, lName, priceCalcStrat);
        }

        public void SellTicket(Ticket ticket)
        {
            LogSeverity logSev = LogSeverity.Info;
            string logMessage = "New ticket has been bought. Ticket ID: " + ticket.Id;

            //_logger.Log(logMessage, logSev);

            _ticketService.SellTicket(ticket);

            _logger.Log(logMessage, logSev);
        }
    }
}
