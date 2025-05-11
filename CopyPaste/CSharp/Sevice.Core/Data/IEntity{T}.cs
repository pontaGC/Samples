namespace Sevices.Core.Data
{
    /// <summary>
    /// The base entity.
    /// </summary>
    /// <typeparam name="TId">The type of an enitty ID.</typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Gets an entity ID.
        /// </summary>
        public TId Id { get; }
    }
}
