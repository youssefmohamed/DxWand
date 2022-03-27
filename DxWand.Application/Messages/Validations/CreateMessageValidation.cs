using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DxWand.Application.Messages.Commands;
using DxWand.Core.Repositories;
using FluentValidation;

namespace DxWand.Application.Messages.Validations
{
    public class CreateMessageValidation : AbstractValidator<CreateMessageCommand>
    {
        private readonly IUserRepository _userRepository;
        public CreateMessageValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Message content is required")
                
                .Matches(new Regex(@"^[A-Za-z0-9\s@]*$"))
                .WithMessage("Message content must contains only characters and numbers");


        }
    }
}
