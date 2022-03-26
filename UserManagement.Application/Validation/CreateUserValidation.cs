using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UserManagement.Application.Commands;
using UserManagement.Core.Enums;
using UserManagement.Core.Repositories;

namespace UserManagement.Application.Validation
{
    public class CreateUserValidation : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Please enter a valid email address")
                
                .MustAsync(async (email, cancellation) => {
                    return await NotExists(email);
                })
                .WithMessage("Email address is already exists");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter a valid password");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage("Please enter a valid gender");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Now)
                .WithMessage("Please enter a valid birth date");
        }

        private async Task<bool> NotExists(string email) 
        {
            return await _userRepository.GetByEmailAsync(email) == null;
        }
    }
}
