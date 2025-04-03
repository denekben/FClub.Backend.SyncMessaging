using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Tariffs.Commands.Handler
{
    public sealed class CreateTariffHandler : IRequestHandler<CreateTariff, TariffDto?>
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IRepository _repository;

        public CreateTariffHandler(ITariffRepository tariffRepository, IRepository repository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
        }

        public async Task<TariffDto?> Handle(CreateTariff command, CancellationToken cancellationToken)
        {
            var (name, priceForNMonths, discountForSocialGroup, allowMultiBranches) = command;

            var tariff = Tariff.Create(name, priceForNMonths, discountForSocialGroup, allowMultiBranches);

            await _tariffRepository.AddAsync(tariff);
            await _repository.SaveChangesAsync();

            return tariff.AsDto();
        }
    }
}
