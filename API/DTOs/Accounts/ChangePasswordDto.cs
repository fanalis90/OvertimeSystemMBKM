namespace API.DTOs.Accounts
{
    public record ChangePasswordDto(
            string Email,
            string OTP,
            string NewPassword,
            string ConfirmPassword
        );
   
}
