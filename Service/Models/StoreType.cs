using System;
using System.Collections.Generic;
using System.Linq;
using NuciExtensions;

namespace ProfiBotServer.Service.Models
{
    public class StoreType : IEquatable<StoreType>
    {
        static readonly Dictionary<string, StoreType> values = new()
        {
            { nameof(Profi), new StoreType(nameof(Profi)) },
            { nameof(ProfiCity), new StoreType(nameof(ProfiCity)) },
            { nameof(ProfiGo), new StoreType(nameof(ProfiGo)) },
            { nameof(ProfiLoco), new StoreType(nameof(ProfiLoco)) },
            { nameof(ProfiSuper), new StoreType(nameof(ProfiSuper)) },
        };

        public string Id { get; }

        StoreType(string id)
        {
            Id = id;
        }

        public static StoreType Profi => values[nameof(Profi)];
        public static StoreType ProfiCity => values[nameof(ProfiCity)];
        public static StoreType ProfiGo => values[nameof(ProfiGo)];
        public static StoreType ProfiLoco => values[nameof(ProfiLoco)];
        public static StoreType ProfiSuper => values[nameof(ProfiSuper)];

        public static Array GetValues()
            => values.Values.ToArray();

        public bool Equals(StoreType other)
        {
            if (other is null)
            {
                return false;
            }

            if (Id is null && other.Id is null)
            {
                return true;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType().NotEquals(GetType()))
            {
                return false;
            }

            return Equals((StoreType)obj);
        }

        public override int GetHashCode() => 948 ^ Id.GetHashCode();

        public override string ToString() => Id;

        public static StoreType FromId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Store type ID cannot be null or empty.");
            }

            if (!values.TryGetValue(id, out StoreType value))
            {
                throw new KeyNotFoundException($"Store type '{id}' does not exist.");
            }

            return value;
        }

        public static bool operator ==(StoreType current, StoreType other) => current.Equals(other);

        public static bool operator !=(StoreType current, StoreType other) => current.NotEquals(other);

        public static implicit operator string(StoreType current) => current.Id;

        public static implicit operator StoreType(string id) => FromId(id);
    }
}
