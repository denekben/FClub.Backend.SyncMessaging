using MediatR;

namespace Management.Application.UseCases.Branches.Commands
{
    public sealed record DeleteBranch(Guid Id) : IRequest;
}
