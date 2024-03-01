using API.Models;

namespace API.Utilities.ViewModels
{
    public class AccountRoleVM
    {
        public Account Account {  get; set; }
        public Role Role { get; set; }

        public AccountRoleVM(Account account, Role role)
        {
            Account = account;
            Role = role;
        }
    }
}
