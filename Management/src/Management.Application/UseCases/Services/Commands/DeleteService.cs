using MediatR;

namespace Management.Application.UseCases.Services.Commands
{
    public sealed record DeleteService(Guid Id) : IRequest;
}
