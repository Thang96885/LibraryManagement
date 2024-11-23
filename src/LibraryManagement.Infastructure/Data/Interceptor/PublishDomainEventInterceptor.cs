using LibraryManagement.Domain.Common.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Infastructure.Data.Identity.Models;

namespace LibraryManagement.Infastructure.Data.Interceptor
{
	public class PublishDomainEventInterceptor : SaveChangesInterceptor
	{
		private readonly IMediator _mediator;

		public PublishDomainEventInterceptor(IMediator mediator)
		{
			_mediator = mediator;
		}

		public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
		{
			PublishDomainEventsExceptDelete(eventData.Context).GetAwaiter().GetResult();

			return base.SavedChanges(eventData, result);
		}

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			PublishDomainDeleteEvents(eventData.Context).GetAwaiter().GetResult();
			return base.SavingChanges(eventData, result);
		}

		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
			CancellationToken cancellationToken = new CancellationToken())
		{
			await PublishDomainDeleteEvents(eventData.Context);
			
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
		{
			await PublishDomainEventsExceptDelete(eventData.Context);

			return await base.SavedChangesAsync(eventData, result, cancellationToken);
		}

		private async Task PublishDomainDeleteEvents(DbContext? dbContext)
		{
			if (dbContext is null)
			{
				return;
			}

			// Get all domain entities that have domain events
			var domainEntitiesHavingEvents = dbContext.ChangeTracker.Entries<IHasDomainEvent>()
				.Where(entry => entry.Entity.DomainEvents.Any() && entry.State == EntityState.Deleted)
				.ToList();

			// Get all domain events from those entities
			var domainEvents = domainEntitiesHavingEvents.SelectMany(entry => entry.Entity.DomainEvents).ToList();

			// Clear domain events from entities
			foreach (var entity in domainEntitiesHavingEvents)
			{
				entity.Entity.ClearDomainEvents();
			}

			// Publish domain events
			foreach (var domainEvent in domainEvents)
			{
				await _mediator.Publish(domainEvent);
			}

		}

		private async Task PublishDomainEventsExceptDelete(DbContext? dbContext)
		{
			if (dbContext is null)
			{
				return;
			}

			// Get all domain entities that have domain events
			var domainEntitiesHavingEvents = dbContext.ChangeTracker.Entries<IHasDomainEvent>()
				.Where(entry => entry.Entity.DomainEvents.Any())
				.ToList();

			// Get all domain events from those entities
			var domainEvents = domainEntitiesHavingEvents.SelectMany(entry => entry.Entity.DomainEvents).ToList();

			// Clear domain events from entities
			foreach (var entity in domainEntitiesHavingEvents)
			{
				entity.Entity.ClearDomainEvents();
			}

			// Publish domain events
			foreach (var domainEvent in domainEvents)
			{
				await _mediator.Publish(domainEvent);
			}

		}
	}
}
