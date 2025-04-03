using MediatR;

namespace Management.Application.UseCases.Tariffs.Commands
{
    public sealed record DeleteTariff(Guid Id) : IRequest;
}
