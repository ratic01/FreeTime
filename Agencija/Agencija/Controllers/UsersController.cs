using Agencija.Dto;
using Agencija.Feautures.Commands;
using Agencija.Feautures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agencija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IMediator _mediator;


        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAdminCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);
                return Ok(new { Token = token }); // Vraća token kao odgovor
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message }); // Vraća 401 status kod
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == false)
            {
                return Unauthorized("Neispravan email ili lozinka.");
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("getuserbyid/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var order = await _mediator.Send(new GetUserByIdQuery(id));

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }



        [Authorize(Roles = "Admin")]       
        [HttpPost("register-agent")]
        public async Task<IActionResult> RegisterAgent([FromBody] RegisterAgentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == "Agent registered successfully.")
            {
                return Ok(result);
            }
            return BadRequest(result); // promenjeno da vrati BadRequest ako je nešto pošlo po zlu
        }


        [Authorize(Roles = "Agent")]
        [HttpPost("create-package")]
        public async Task<IActionResult> CreatePackage([FromBody] CreateTourPackageCommand command)
        {
            var result = await _mediator.Send(command);
            if (result==command.PackageName)
            {
                return Ok("Tour package created successfully.");
            }
            return BadRequest(result); // promenjeno da vrati BadRequest ako je nešto pošlo po zlu
        }

        [Authorize]
        [HttpGet("getpackagebyid/{id}")]
        public async Task<IActionResult> GetTourPackageById(int id)
        {
            var order = await _mediator.Send(new GetTourPackagebyIdQuery(id));

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }



        [Authorize(Roles = "Agent,Admin,Customer")]
        [HttpGet("getallpackages")]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages=await _mediator.Send(new GetAllTourPackagesQuery());
            return Ok(packages);
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPut("update-package/{id}")]
        public async Task<IActionResult> UpdatePackage(int id,[FromBody] UpdateTourPackageCommand command)
        {
            if (id != command.TourPackageId)
            {
                return BadRequest("ID in URL does not match ID in the body.");
            }

            var result = await _mediator.Send(command);

            if (result == "Tour package updated successfully.")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[Authorize(Roles ="Customer")]
        //[HttpPost("reserve")]
        //public async Task<IActionResult> ReservePackage([FromBody] ReserveTourPackageCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    if (result)
        //        return Ok("Package reserved successfully.");
        //    return BadRequest("Unable to reserve package.");
        //}

        [Authorize(Roles ="Customer")]
        [HttpGet("getreservationbyid/{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _mediator.Send(new GetReservationByIdQuery(id));

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPost("accept-reservation")]
        public async Task<IActionResult> AcceptReservation([FromBody] AcceptReservationCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return NotFound(); // Paket nije pronađen ili nije u 'Processing' statusu
            return Ok("Reservation accepted"); // Rezervacija je prihvaćena
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpDelete("deny-reservation")]
        public async Task<IActionResult> DenyReservation([FromBody] RejectReservationCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return NotFound(); // Paket nije pronađen ili nije u 'Processing' statusu
            return Ok("Reservation denied"); // Rezervacija je prihvaćena
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("create-reservation")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request
            }
            catch (Exception ex)
            {
                // Loguj grešku
                Console.WriteLine($"Error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error"); // 500 Internal Server Error
            }
        }


        [Authorize(Roles = "Customer")]
        [HttpDelete("User-cancel-reservation/{reservationId}")]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            var command = new UserCancelReservationCommand { ReservationId = reservationId };

            try
            {
                var result = await _mediator.Send(command);

                if (result)
                {
                    return Ok("Reservation canceled successfully.");
                }

                return BadRequest("Reservation cannot be canceled because it's not in processing state.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }

        [Authorize(Roles ="Customer")]
        [HttpGet("getreservationsbyuserid/{userId}")]
        public async Task<IActionResult> GetReservationsByUserId(int userId)
        {
            var reservations = await _mediator.Send(new GetReservationsByUserIdQuery(userId));

            if (reservations == null || !reservations.Any())
            {
                return NotFound(); // Vraća 404 ako nema rezervacija
            }

            return Ok(reservations); // Vraća 200 i listu rezervacija
        }


        [Authorize(Roles ="Customer")]
        [HttpGet("getpackagefromreservationPackageId/{tourPackageId}")]
        public async Task<IActionResult> GetPackageFromReservationPackageId(int tourPackageId)
        {
            // Dobavi informacije o paketu koristeći ID paketa
            var packageDto = await _mediator.Send(new GetPackagesFromReservationPackageIdQuery(tourPackageId));

            if (packageDto == null)
            {
                return NotFound("Package not found.");
            }

            // Vraća informacije o paketu
            return Ok(packageDto);
        }


        [Authorize(Roles ="Agent,Admin")]
        [HttpGet("allreservations")]
        public async Task<ActionResult<List<CreateReservationDto>>> GetAllReservations()
        {
            var query = new GetAllReservationsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles ="Customer")]
        [HttpGet("getpackagefromreservation/{reservationId}")]
        public async Task<IActionResult> GetPackageFromReservation(int reservationId)
        {
            // Kreiraj query za dobijanje paketa na osnovu ID-a rezervacije
            var query = new GetPackagesFromReservationPackageIdQuery(reservationId);

            // Pošalji query Mediatoru
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound("Package not found for the given reservation.");
            }

            // Vrati rezultat
            return Ok(result);
        }


        [Authorize(Roles = "Admin,Agent")]
        [HttpGet("events/{reservationId}")]
        public async Task<IActionResult> GetUserReservationEvents(int reservationId)
        {
            try
            {
                // Poziva query handler da dobavi sve događaje za korisnika
                var events = await _mediator.Send(new GetReservationEventsQuery
                {
                    ReservationId = reservationId
                });

                if (events == null || events.Count == 0)
                {
                    return NotFound("No reservation events found for this reservation.");
                }

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }











    }
}
