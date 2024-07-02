﻿using MediatR;
using Microservice.Register.Function.Data.Repository.Interfaces;
using Microservice.Register.Function.Helpers;
using Microservice.Register.Function.Helpers.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using static Microservice.Register.Function.Helpers.Enums;
using BC = BCrypt.Net.BCrypt;

namespace Microservice.Register.Function.MediatR.RegisterUser;

public class RegisterUserCommandHandler(IUserRepository userRepository,
                                        ILogger<RegisterUserCommandHandler> logger,
                                        IAzureServiceBusHelper azureServiceBusHelper,
                                        IAuthenticationHelper authenticationHelper) : IRequestHandler<RegisterUserRequest, Unit>
{
    private ILogger<RegisterUserCommandHandler> _logger { get; set; } = logger; 
    private IUserRepository _userRepository { get; set; } = userRepository; 
    private IAzureServiceBusHelper _azureServiceBusHelper { get; set; } = azureServiceBusHelper;
    private IAuthenticationHelper _authenticationHelper { get; set; } = authenticationHelper;

    public async Task<Unit> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.AddAsync(CreateUserAsync(request)); 

        var responses = GetSerialisedRegisteredUserResponses(request, user);

        await _azureServiceBusHelper.SendMessage(Constants.AzureServiceBusQueueRegisteredUserCustomer, responses.Item1);
        await _azureServiceBusHelper.SendMessage(Constants.AzureServiceBusQueueRegisteredUserCustomerAddress, responses.Item2);
          
        return Unit.Value;
    }

    private Domain.User CreateUserAsync(RegisterUserRequest registerUserRequest)
    {
        return new Domain.User()
        {
            Id = Guid.NewGuid(),
            Email = registerUserRequest.Email,
            Role = Role.User, 
            Verified = DateTime.Now,
            VerificationToken = _authenticationHelper.CreateRandomToken(),
            PasswordHash = BC.HashPassword(registerUserRequest.Password),
            Password = registerUserRequest.Password,
            ConfirmPassword = registerUserRequest.ConfirmPassword
        };
    }

    private Tuple<string, string> GetSerialisedRegisteredUserResponses(RegisterUserRequest request, Domain.User user)
    { 
        return new Tuple<string, string>(GetSerializedRegisteredUser(user.Id, request), 
                                            GetSerializedRegisteredUserAddress(user.Id, request.Address));
    }

    private string GetSerializedRegisteredUser(Guid id, RegisterUserRequest request)
    {
        return JsonSerializer.Serialize(new RegisterUserResponse(id,
                                                                 request.Email,
                                                                 request.Surname,
                                                                 request.FirstName));
    }

    private string GetSerializedRegisteredUserAddress(Guid id, RegisterUserAddress registerUserAddress)
    {
        return JsonSerializer.Serialize(new RegisterUserAddressResponse(Guid.Empty,
                                                                        id,
                                                                        registerUserAddress.AddressLine1,
                                                                        registerUserAddress.AddressLine2,
                                                                        registerUserAddress.AddressLine3,
                                                                        registerUserAddress.TownCity,
                                                                        registerUserAddress.County,
                                                                        registerUserAddress.Postcode,
                                                                        registerUserAddress.CountryId));
    }
}