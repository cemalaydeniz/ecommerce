using AutoMapper;
using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Common.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Commands.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommandRequest, ValidationBehaviorResult<SignUpCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public SignUpCommandHandler(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<ValidationBehaviorResult<SignUpCommandResponse>> Handle(SignUpCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByEmailAsync(request.Email, false, false, cancellationToken);
            if (user != null)
                return ValidationBehaviorResult<SignUpCommandResponse>.Fail(ConstantsUtility.User.UserExits);

            User newUser = new User(request.Email,
                BCrypt.Net.BCrypt.HashPassword(request.Password),
                request.Name,
                request.PhoneNumber,
                request.Address == null ? null : new UserAddress(ConstantsUtility.Address.InitialAddressTitle, _mapper.Map<Address>(request.Address)));

            await _unitofWork.UserRepository.AddAsync(newUser, cancellationToken);
            await _unitofWork.SaveChangesAsync(cancellationToken);

            return new SignUpCommandResponse()
            {
                UserId = newUser.Id
            };
        }
    }
}
