using FClub.Backend.Common.Exceptions;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Tariffs.Commands.Handler
{
    public sealed class UpdateTariffHandler : IRequestHandler<UpdateTariff, TariffDto?>
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IRepository _repository;

        public UpdateTariffHandler(ITariffRepository tariffRepository, IRepository repository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
        }

        public async Task<TariffDto?> Handle(UpdateTariff command, CancellationToken cancellationToken)
        {
            var (id, name, priceForNMonths, discountForSocialGroup, allowMultiBranches) = command;

            var tariff = await _tariffRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find tariff {command.Id}");

            tariff.UpdateDetails(name, priceForNMonths, discountForSocialGroup, allowMultiBranches);

            await _tariffRepository.UpdateAsync(tariff);
            await _repository.SaveChangesAsync();

            return tariff.AsDto();
        }
    }
}
