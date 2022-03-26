using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UserManagement.Application.Commands;

namespace UserManagement.Application.Validation
{
    public class UserLoginValidation : AbstractValidator<UserLoginCommand>
    {
        public UserLoginValidation()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Please enter a valid email address");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter a valid password");
        }
    }
}
