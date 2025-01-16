//CourseService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;

namespace Business.Services
{
    public class SlotService
    {
       CarParkingContext carParkingContext = new CarParkingContext();

        public Result GetCourseById(int slotId)
        {
            try
            {
                // Retrieve the course by ID from the database
                var slot = carParkingContext.Slot.FirstOrDefault(c => c.SlotId == slotId);

                if (slot == null)
                {
                    return new Result(false, "Slot not found");
                }

                return new Result(true, "Success", slot);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public Result List()
        { //logics
            try
            {
                var slots = carParkingContext.Slot.ToList();
                return new Result(true, "Success", slots);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}