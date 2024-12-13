using AssestManagementSystemMachineTest.Models;

namespace AssestManagementSystemMachineTest.Repository
{
    public interface ILoginRepository
    {
       
         public  Task<LoginUser> ValidateUsers(string username, string userPass);
        
    }
}
