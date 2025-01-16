using Business.FormModel;
using Database.Context;
using Database.Model;

namespace Business.Services
{
    public class RoleService
    {
        CarParkingContext carParkingContext = new CarParkingContext();
        public Result Add(Role user)
        {
            // Assuming logic for adding role
            bool isSuccess = true; // Determine if operation was successful
            return new Result().DBCommit(carParkingContext, "Save Successfully!", null, user);
        }
        public Result Update(Role user)
        {
            // Assuming logic for updating role
            bool isSuccess = true; // Determine if operation was successful
            return new Result().DBCommit(carParkingContext, "Updated Successfully!", null, user);
        }
        public Result List()
        {
            // Assuming logic for listing roles
            bool isSuccess = true; // Determine if operation was successful
            return new Result().DBCommit(carParkingContext, "Updated Successfully!", null);
        }
    }
}
