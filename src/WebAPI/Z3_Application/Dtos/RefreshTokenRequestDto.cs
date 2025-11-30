namespace WebAPI.Z3_Application.Dtos;

public class RefreshTokenRequestDto
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }

}
