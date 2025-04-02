using MediatR;

namespace AccessControl.Application.UseCases.ClientLogs.Queries
{
    public sealed record GetLogsByClientId : IRequest;
}
