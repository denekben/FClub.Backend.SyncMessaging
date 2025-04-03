using FClub.Backend.Common.Exceptions;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Tariffs.Commands.Handler
{
    public sealed class DeleteTariffHandler : IRequestHandler<DeleteTariff>
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IRepository _repository;

        public DeleteTariffHandler(ITariffRepository tariffRepository, IRepository repository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteTariff command, CancellationToken cancellationToken)
        {
            var tariff = _tariffRepository.GetAsync(command.Id)
                ?? throw new NotFoundException($"Cannot find tariff {command.Id}");

            await _tariffRepository.DeleteAsync(command.Id);
            await _repository.SaveChangesAsync();
        }
    }
}
