namespace ecommerce.Domain.SeedWork
{
    /// <summary>
    /// Declares an entity as soft deletable, giving the impleted class the property <see cref="IsDeleted"/>
    /// </summary>
    public interface ISoftDelete
    {
        bool Delete();

        bool IsDeleted { get; }
    }
}
