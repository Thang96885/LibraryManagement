using ErrorOr;
using LibraryManagement.Application.Auth.Common;
using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
    {
        private readonly IIdentityService _identityService;
        private readonly IBaseRepository<Patron> _patronRepository;
        private readonly ITokenGennerator _tokenGennerator;

        public RegisterCommandHandler(IIdentityService identityService, IBaseRepository<Patron> patronRepository, ITokenGennerator tokenGennerator)
        {
            _identityService = identityService;
            _patronRepository = patronRepository;
            _tokenGennerator = tokenGennerator;
        }

        public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var patron = await _patronRepository.FindAsync(request.PatronId);

            var user = await _identityService.FindUserByPatronIdAsync(request.PatronId);

            if (patron == null)
                return Error.Failure("PatronId does not exsist");
            if (user != null)
                return Error.Failure("Patron alrealdy has account");

            var result = await _identityService.SignInAsync(new(patron, request.userName, request.password));

            if (result.IsError)
                return result.Errors;

            var token = new AuthResult(_tokenGennerator.GenerateJwt(result.Value), _tokenGennerator.GenerateRefreshToken());

            await _identityService.AddRefreshToken(result.Value.UserName, token.refreshToken);

            return token;
        }
    }
}
