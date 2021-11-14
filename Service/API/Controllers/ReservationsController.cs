﻿using DataAccess;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase {

        ReservationDB _reservationDB;

        public ReservationsController() {
            _reservationDB = new ReservationDB("data Source=hildur.ucn.dk; database=dmaa0920_1086259; user=dmaa0920_1086259; password=Password1!");
        }

        //GET: api/reservations        
        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAllReservations() {

            //We create a variable to store our list of reservations
            var reservations = await _reservationDB.GetAllReservations();

            //If no reservations are found we return NotFound
            if (reservations == null) {
                return NotFound("Ingen reservationer blev fundet");
            } else {
                //Else we return 200 OK and the list of reservations
                return Ok(reservations);
            }
        }

        //GET: api/reservations/1
        [HttpGet("{iD}")]
        public async Task<ActionResult<Reservation>> GetByID(int iD) {
            var reservation = await _reservationDB.GetByID(iD);
            if (reservation == null) {
                return NotFound();
            } else {
                return Ok(reservation);
            }
        }

        //POST: api/reservations
        [HttpPost]
        public async Task<ActionResult<int>> CreateReservation([FromBody] Reservation reservation) {
            return Ok(await _reservationDB.CreateReservation(reservation));
        }

        //PUT: api/reservations/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation([FromBody] Reservation reservation) {
            if (!await _reservationDB.UpdateReservation(reservation)) {
                return NotFound("Opdatering af reservationen mislykkedes");
            } else {
                return Ok();
            }
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            if (!await _reservationDB.DeleteByID(id)) { return NotFound(); } else { return Ok(); }
        }

        //Get api/reservations/
        //[HttpGet{""}]
        //public async Task<ActionResult<IEnumerable<Reservation>>> GetByGuestID([FromBody] int guestID) {
        //IEnumerable<Reservation> reservations = null;
        //reservations = await _reservationDB.GetByGuestID(guestID);
        //if (reservations == null) {
        //    return NotFound("Ingen reservationer blev fundet");
        //} else {
        //    return Ok(reservations);
        //}
    }
}
}
