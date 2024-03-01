using API.DTOs.Accounts;
using API.Models;

namespace API.Utilities.ViewModels
{
    public class AccountVM
    {
        public RegisterDto RegisterDto { get; set; }
        public Employee Employee { get; set; }

        public AccountVM(Employee employee, RegisterDto registerDto)
        {
            Employee = employee;
            RegisterDto = registerDto;
        }
    }
}
