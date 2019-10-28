﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Models;

namespace TicketsDemo.Controllers
{
    public class RunController : Controller
    {
        private ITicketRepository _tickRepo;
        private IRunRepository _runRepo;
        private IReservationRepository _reservationRepo;
        private IReservationService _resServ;
        private ITicketService _tickServ;
        //private IPriceCalculationStrategy _priceCalc;
        private ICalculationFactory _calcFact;
        private ITrainRepository _trainRepo;

        public RunController(ITicketRepository tick, IRunRepository run, 
            IReservationService resServ,
            ITicketService tickServ,
            ICalculationFactory calcFact,
            IReservationRepository reservationRepo,
            ITrainRepository trainRepo) {
            _tickRepo = tick;
            _runRepo = run;
            _resServ = resServ;
            _tickServ = tickServ;
            _calcFact = calcFact;
            _reservationRepo = reservationRepo;
            _trainRepo = trainRepo;
        }

        public ActionResult Index(int id) {
            var run = _runRepo.GetRunDetails(id);
            var train = _trainRepo.GetTrainDetails(run.TrainId);
            var model = new RunViewModel() {
                RunDate = run.Date,
                Carriages = train.Carriages.ToDictionary(x => x.Number),
                PlacesByCarriage = run.Places.GroupBy(x => x.CarriageNumber).ToDictionary(x => x.Key, x => x.ToList()),
                ReservedPlaces = run.Places.Where(p => _resServ.PlaceIsOccupied(p)).Select(p => p.Id).ToList(),
                Train = train,
            };

            return View(model);
        }

        public ActionResult ReservePlace(int placeId) {
            var place = _runRepo.GetPlaceInRun(placeId);

            var reservation = _resServ.Reserve(place);
            var calcStrat = _calcFact.create(true);

            var model = new ReservationViewModel()
            {
                Reservation = reservation,
                PlaceInRun = place,
                PriceComponents = calcStrat.CalculatePrice(place),
                Date = place.Run.Date,
                Train = _trainRepo.GetTrainDetails(place.Run.TrainId),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTicket(CreateTicketModel model)
        {
            var calcStratFactory = _calcFact.create(model.Beverages_Domestics);
            var tick = _tickServ.CreateTicket(model.ReservationId,model.FirstName,model.LastName, calcStratFactory);
            return RedirectToAction("Ticket", new { id = tick.Id });
        }

        public ActionResult Ticket(int id)
        {
            var ticket = _tickRepo.Get(id);
            var reservation = _reservationRepo.Get(ticket.ReservationId);
            var placeInRun = _runRepo.GetPlaceInRun(reservation.PlaceInRunId);

            var ticketWM = new TicketViewModel();
            ticketWM.Ticket = ticket;
            ticketWM.PlaceNumber = placeInRun.Number;
            ticketWM.Date = placeInRun.Run.Date;
            ticketWM.Train = _trainRepo.GetTrainDetails(placeInRun.Run.TrainId);

            return View(ticketWM);
        }

    }
}