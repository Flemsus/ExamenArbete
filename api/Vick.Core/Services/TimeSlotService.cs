using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vick.Core.Interfaces;
using Vick.Core.Models;


namespace Vick.Core.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        public void AddTimeSlot(TimeSlot timeslot, User user)
        {
            user.TimeSlots.Add(timeslot);
        }

        public void UpdateTimeSlot(TimeSlot timeslot, User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteTimeSlot(int id, User user)
        {
            var timeslot = user.TimeSlots.Find(x => x.Id == id);
            user.TimeSlots.Remove(timeslot);
        }

    }
}
