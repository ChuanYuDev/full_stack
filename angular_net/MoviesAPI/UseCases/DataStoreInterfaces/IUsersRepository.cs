using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Identity;

namespace UseCases.DataStoreInterfaces;

public interface IUsersRepository
{
    Task<string> GetUserId();
    Task<(IdentityResult identityResult, AuthenticationResponseDto? authenticationResponseDto)> Register(UserCredentialsDto userCredentialsDto);
    Task<(bool loginResult, AuthenticationResponseDto? authenticationResponseDto)> Login(UserCredentialsDto userCredentialsDto);
    IEnumerable<IdentityError> BuildIncorrectLoginErrorMessage();
    Task<bool> MakeAdmin(EditClaimDto editClaimDto);
    Task<bool> RemoveAdmin(EditClaimDto editClaimDto);
    Task<int> Count();
    Task<List<UserDto>> Get(PaginationDto paginationDto);
}