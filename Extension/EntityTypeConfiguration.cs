using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlugApi.Extension;

public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
{
    #region Métodos/Operadores Públicos
    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    public abstract void Map(EntityTypeBuilder<TEntity> builder);
    #endregion
}
