using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Services.Commands
{
    public sealed record UpdateService(Guid Id, string Name) : IRequest<ServiceDto?>;
}
