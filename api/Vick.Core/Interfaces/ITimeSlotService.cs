using Vick.Core.Models;

namespace Vick.Core.Interfaces
{
    public interface ITimeSlotService
    {

        void AddTimeSlot(TimeSlot timeslot, User user);
        void UpdateTimeSlot(TimeSlot timeslot, User user);
        void DeleteTimeSlot(int timeSlotId, User user);


    }
}