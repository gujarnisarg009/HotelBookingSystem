using HotelBooking.Application.DTOs.Auth;
using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Auth.Register
{
    public class RegisterCommand : IRequest<AuthResponseDto>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    // 2. Handler
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        IApplicationDbContext _context;
        IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(IApplicationDbContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users
                .AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (userExists)
            {
                throw new Exception("Email already registered hai!");
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                PhoneNumber = request.PhoneNumber,
                Role = UserRole.Customer
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = token
            };
        }
    }
}
