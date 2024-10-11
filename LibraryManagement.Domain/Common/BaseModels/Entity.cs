using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.BaseModels
{
	public abstract class Entity : IEquatable<Entity>, IHasDomainEvent
	{

		private readonly List<IDomainEvent> _domainEvents = new();

		public Entity()
		{

		}

		public Guid Id { get; protected set; } = default!;

		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


		public override bool Equals(object? obj)
		{
			if (obj is null || obj.GetType() != GetType())
				return false;

			var entity = (Entity)obj;

			return entity.Id.Equals(Id);
		}

		public bool Equals(Entity? other)
		{
			if (other == null)
				return false;

			return other.Id.Equals(Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Entity a, Entity b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Entity a, Entity b)
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

	public abstract class Entity<T>
		: IEquatable<Entity>, IHasDomainEvent
	{

		private readonly List<IDomainEvent> _domainEvents = new();

		public Entity()
		{

		}

		public T Id { get; protected set; } = default!;

		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


		public override bool Equals(object? obj)
		{
			if (obj is null || obj.GetType() != GetType())
				return false;

			var entity = (Entity)obj;

			return entity.Id.Equals(Id);
		}

		public bool Equals(Entity? other)
		{
			if (other == null)
				return false;

			return other.Id.Equals(Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Entity<T> a, Entity<T> b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Entity<T> a, Entity<T> b)
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
