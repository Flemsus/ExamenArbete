using Microsoft.AspNetCore.Mvc;
using Vick.Api.Endpoints.Internal;
using Vick.Core.Interfaces;
using Vick.Core.Models;
using Vick.Core.Services;

namespace Vick.Api.Endpoints
{
    public class TimeSlotEndpoints : IEndpoints
    {
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {

            app.MapPost("/time-slot/add", AddTimeSlot);
            app.MapPut("/time-slot/update", UpdateTimeSlot);
            app.MapDelete("/time-slot/delete", DeleteTimeSlot);
        }

        private static void AddTimeSlot(int userId, [FromBody] TimeSlot timeslot, ITimeSlotService timeSlotService, IUserService userService)
        {
            try
            {
                var user = userService.GetUser(userId);

                timeSlotService.AddTimeSlot(timeslot, user);

                Console.WriteLine("Time slot added successfully");
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error while adding time slot: {ex.Message}");
            }
        }

        private static void UpdateTimeSlot(TimeSlot timeslot, ITimeSlotService timeSlotService, IUserService userService)
        {
            var user = userService.GetUser();
            timeSlotService.UpdateTimeSlot(timeslot, user);

        }
        private static void DeleteTimeSlot(int timeSlotId, ITimeSlotService timeSlotService, IUserService userService)
        {
            var user = userService.GetUser();
            timeSlotService.DeleteTimeSlot(timeSlotId, user);

        }
    }
}


