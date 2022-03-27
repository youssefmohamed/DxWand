using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DxWand.Application.Commands;
using DxWand.Application.Responses;
using DxWand.Application.Settings;
using DxWand.Core.Enums;
using DxWand.Core.Repositories;

namespace DxWand.Application.Users.Handlers
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, ResponseMessage<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserLoginCommand> _validator;
        private readonly JwtSettings _jwtSettings;
        public UserLoginHandler(IUserRepository userRepository, IValidator<UserLoginCommand> validator, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _validator = validator;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<ResponseMessage<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {

            var validator = await _validator.ValidateAsync(request);
            if (!validator.IsValid) 
            {
                return new ResponseMessage<string> 
                {
                    IsSuccess = false,
                    StatusCode = Convert.ToInt32(StatusCodeEnum.BadRequest),
                    Message = validator.Errors.FirstOrDefault()?.ErrorMessage
                };
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user != null) 
            {
                var isValidUserPassword = await _userRepository.IsValidUserPassword(user, request.Password);
                if (isValidUserPassword) 
                {
                    #region Generate Token
                    var userRoles = await _userRepository.GetUserRolesAsync(user);
                    
                    var claims = new List<Claim> 
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("Id", user.Id ),
                        new Claim("BirthDate", user.BirthDate.ToString()),
                        new Claim("Gender", Convert.ToInt32(user.Gender).ToString()),
                    };
                    foreach (string role in userRoles)
                        claims.Add(new Claim(ClaimTypes.Role, role));

                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                    var tokenOptions = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireInMintues),
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                    return new ResponseMessage<string>
                    {
                        IsSuccess = true,
                        Data = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                        StatusCode = Convert.ToInt32(StatusCodeEnum.Success)
                    };
                    #endregion
                }
            }

            return new ResponseMessage<string> 
            {
                IsSuccess = false,
                Message = "Invalid email or password",
                StatusCode = Convert.ToInt32(StatusCodeEnum.BadRequest),
            };
        }

    }
}
