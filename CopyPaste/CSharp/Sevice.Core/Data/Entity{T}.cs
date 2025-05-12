namespace Services.Core.Data
{
    /// <inheritdoc />
    public abstract class Entity<TId> : IEntity<TId>
    {
        /// <inheritdoc />
        public abstract TId Id { get; protected set; }
    }
}
