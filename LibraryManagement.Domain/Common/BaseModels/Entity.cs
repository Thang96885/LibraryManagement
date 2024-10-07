using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.BaseModels
{
	public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvent
		where TId : notnull
	{

		private readonly List<IDomainEvent> _domainEvents = new();

		public Entity()
		{

		}

		public TId Id { get; protected set; } = default!;

		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


		public override bool Equals(object? obj)
		{
			if (obj is null || obj.GetType() != GetType())
				return false;

			var entity = (Entity<TId>)obj;

			return entity.Id.Equals(Id);
		}

		public bool Equals(Entity<TId>? other)
		{
			if (other == null)
				return false;

			return other.Id.Equals(Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Entity<TId> a, Entity<TId> b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Entity<TId> a, Entity<TId> b)
		{
			return !(a == b);
		}

		public void AddDomainEvent(IDomainEvent domainEvent)
		{
			_domainEvents.Add(domainEvent);
		}

		public void ClearDomainEvents()
		{
			_domainEvents.Clear();
		}
	}
}
