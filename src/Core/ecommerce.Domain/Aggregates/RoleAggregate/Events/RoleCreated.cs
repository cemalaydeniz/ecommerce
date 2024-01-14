﻿using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.RoleAggregate.Events
{
    public record RoleCreated(Role CreatedRole) : IDomainEvent;
}
