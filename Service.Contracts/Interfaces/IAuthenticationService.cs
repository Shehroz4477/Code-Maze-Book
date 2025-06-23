using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts.Interfaces;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
    Task<bool> ValidateUser(UserForAuthentictionDto userForAuthentictionDto);
    Task<TokenDto> CreateToken(bool poplateExp);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}
