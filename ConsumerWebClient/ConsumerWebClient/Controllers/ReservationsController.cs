using APIClient;
using APIClient.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsumerWebClient.TestData;
using ConsumerWebClient.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System;

namespace ConsumerWebClient.Controllers {

    public class ReservationsController : Controller {

        readonly DataPopulation _data;
        private IJacketOffApiClient _client;


        public ReservationsController(IJacketOffApiClient client) {
            _client = client;
            _data = new DataPopulation();

        }

        //Retrieves all the user's reservations
        [HttpGet]
        public async Task<ActionResult> MyReservations() {

            try {
                IEnumerable<ReservationDTO> reservations = await _client.GetReservationsByGuestEmail(_data.Guest.Email);

                return View(reservations);

            } catch (Exception e) {
                ViewBag.ErrorMessage = e.Message;
            }
            return View("OhNo");
        }

        //This method is purely used to generate the view
        //for creating a reservation. The data from
        //the view is not utilised before our Post method.
        //Therefore, nothing is added to this method.
        public async Task<ActionResult> Reservation() {

            try {
                IEnumerable<ItemTypeDTO> itemTypes = await _client.GetAllItemTypes();

                ReservationViewModel reservationViewModel = new ReservationViewModel();
                reservationViewModel.ItemTypes = itemTypes;

                return View(reservationViewModel);

            } catch (Exception e) {
                ViewBag.ErrorMessage = e.Message;
            }
            return View("OhNo");
        }

        [HttpPost]
        public async Task<ActionResult> Reservation(ReservationViewModel reservationViewModel) {

            //We extract the reservationDTO from our view model, to post it's data
            ReservationDTO newReservation = reservationViewModel.Reservation;

            //Placeholder for retrieving guestID
            //from logged in user 
            newReservation.GuestID_FK = _data.Guest.GuestId;
            newReservation.WardrobeID_FK = _data.WardrobeID;

            //We attempt to pass our reservation to
            //the CreateReservation method in our APIClient
            //If we fail, we show the error.
            try {
                if (await _client.CreateReservation(newReservation) > 0) {
                    TempData["Message"] = "Reservation oprettet!";
                    return RedirectToAction(nameof(MyReservations));
                } else {
                    ViewBag.ErrorMessage = "Reservationen blev ikke oprettet!";
                }
            } catch (Exception e) {
                ViewBag.ErrorMessage = e.Message;
            }
            return View("OhNo");
        }
    }
}
