using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequestDto>
    {
        public EmployeeRequestValidator()
        {
            RuleFor(x => x.Nik)
                .NotEmpty().WithMessage("NIK is Required");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is Required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("First Name is Required")
                .EmailAddress().WithMessage("invalid email address"); ;

            RuleFor(x => x.Salary)
                .NotEmpty().WithMessage("Salary is Required")
                .GreaterThan(0).WithMessage("Salary must greater than 0"); ;

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is Required");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is Required");

        }
    }
}
