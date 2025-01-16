//CourseService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database.Model;

namespace Business.Services
{
    public class SlotService
    {
        // Initialize SkillExchangeContext for database operations
        CarParkingContext carParkingContext = new CarParkingContext();

        // Method to retrieve course details by ID
        public Result GetCourseById(int slotId)
        {
            try
            {
                // Retrieve the course by ID from the database
                var slot = carParkingContext.Slot.FirstOrDefault(c => c.SlotId == slotId);
                // Check if course exists
                if (slot == null)
                {
                    // Return failure result if course not found
                    return new Result(false, "Slot not found");
                }
                // Return success result with course details
                return new Result(true, "Success", slot);
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return new Result(false, ex.Message);
            }
        }
        // Method to list all courses
        public Result List()
        { //logics
            try
            {
                // Retrieve all courses from the database
                var slots = carParkingContext.Slot.ToList();
                // Return success result with course list
                return new Result(true, "Success", slots);
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return new Result(false, ex.Message);
            }
        }
        public Result UpdateSlot(Slot updatedSlot)
        {
            try
            {
                // Retrieve the course by ID from the database
                var slot = carParkingContext.Slot.FirstOrDefault(c => c.SlotId == updatedSlot.SlotId);
                // Check if course exists
                if (slot == null)
                {
                    // Return failure result if course not found
                    return new Result(false, "Course not found");
                }
                // Update course properties with new values
               
                slot.Locality = updatedSlot.Locality;
                slot.Name = updatedSlot.Name;
                slot.LatestPrice = updatedSlot.LatestPrice;
                slot.IsBooked = updatedSlot.IsBooked;
                slot.UpdatedDate = DateTime.Now;
                slot.UpdatedBy = updatedSlot.UpdatedBy;

                // Save changes to the database
                carParkingContext.SaveChanges();

                // Return success result after update
                return new Result(true, "Slot updated successfully");
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return new Result(false, ex.Message);
            }
        }

        // Method to delete a course by ID
        public Result DeleteSlot(int slotId)
        {
            try
            {
                // Retrieve the course by ID from the database
                var slot = carParkingContext.Slot.FirstOrDefault(c => c.SlotId == slotId);

                // Check if course exists
                if (slot == null)
                {
                    // Return failure result if course not found
                    return new Result(false, "Slot not found");
                }

                // Remove the course from the database
                carParkingContext.Slot.Remove(slot);

                // Save changes to the database
                carParkingContext.SaveChanges();

                // Return success result after deletion
                return new Result(true, "Slot deleted successfully");
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return new Result(false, ex.Message);
            }
        }
    }
}