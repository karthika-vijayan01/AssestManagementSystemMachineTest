using AssestManagementSystemMachineTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AssestManagementSystemMachineTest.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AssestManagementExamContext _context;

        public LoginRepository(AssestManagementExamContext context)
        {
            _context = context;
        }

        public async Task<LoginUser> ValidateUsers(string username, string userPass)
        {
            try
            {
                if (_context != null)
                {
                    LoginUser? dbUser = await _context.LoginUsers.FirstOrDefaultAsync(
                        user => user.UserName == username && user.UserPass == userPass);

                    return dbUser;
                }
                return null; 
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
