using WebAPI.Z3_Application.Dtos;
using WebAPI.Z4_Domain.Entities;

namespace WebAPI.Z3_Application.Interfaces;

public interface IAuthService
{
    Task<User?> RegisterUser(UserDto request);
    //Task<string?> LoginAsync(UserDto request);
    Task<TokenResponseDto?> LoginAsync(UserDto request);
    Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
}
