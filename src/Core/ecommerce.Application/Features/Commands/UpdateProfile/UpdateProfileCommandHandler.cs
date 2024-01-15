using AutoMapper;
using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommandRequest, ValidationBehaviorResult<UpdateProfileCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public UpdateProfileCommandHandler(IUnitofWork unitofWork,
            IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<ValidationBehaviorResult<UpdateProfileCommandResponse>> Handle(UpdateProfileCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, false, false, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<UpdateProfileCommandResponse>.Fail(ConstantsUtility.User.UserNotFound);

            bool isUpdated = false;
            if (!string.IsNullOrWhiteSpace(request.NewName) && user.UpdateName(request.NewName))
            {
                isUpdated = true;
            }
            if (!string.IsNullOrWhiteSpace(request.NewPhoneNumber) && user.UpdatePhoneNumber(request.NewPhoneNumber))
            {
                isUpdated = true;
            }

            if (request.TitleofAddressToUpdate == null && request.UserAddress != null)
            {
                UserAddress newAddress = _mapper.Map<UserAddress>(request.UserAddress);
                if (user.AddAddress(newAddress))
                {
                    isUpdated = true;
                }
            }
            else if (request.TitleofAddressToUpdate != null && request.UserAddress != null)
            {
                UserAddress address = _mapper.Map<UserAddress>(request.UserAddress);
                if (user.Addresses.Contains(address))
                    return ValidationBehaviorResult<UpdateProfileCommandResponse>.Fail(ConstantsUtility.Address.DuplicatedTitle);

                if (user.UpdateAddress(request.TitleofAddressToUpdate, address))
                {
                    isUpdated = true;
                }
            }
            else if (request.TitleofAddressToUpdate != null && request.UserAddress == null)
            {
                UserAddress? address = user.Addresses.FirstOrDefault(a => a.Title == request.TitleofAddressToUpdate);
                if (address != null && user.RemoveAddress(address))
                {
                    isUpdated = true;
                }
            }

            if (isUpdated)
            {
                _unitofWork.UserRepository.Update(user);
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new UpdateProfileCommandResponse();
        }
    }
}
