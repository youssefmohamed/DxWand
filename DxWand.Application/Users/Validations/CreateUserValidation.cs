using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using DxWand.Application.Commands;
using DxWand.Core.Enums;
using DxWand.Core.Repositories;

namespace DxWand.Application.Users.Validations
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
