﻿using AccessControl.Domain.Entities.Pivots;
using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class Service
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public List<ServiceTariff> ServiceTariffs { get; set; } = [];
        public List<ServiceBranch> ServiceBranches { get; set; } = [];
        public List<Turnstile> Turnstiles { get; set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Service() { }

        private Service(Guid id, string name)
        {
            Id = id;
            Name = name;
            CreatedDate = DateTime.UtcNow;
        }

        public static Service Create(Guid id, string name)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for Service[id]. Entered value {id}");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Service[name]. Entered value: {name}");

            return new(id, name);
        }

        public void UpdateDetails(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Service[name]. Entered value: {name}");

            Name = name;
        }
    }
}
