using BowlingGame.Infrastructure.Repositories.SQLServer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BowlingGame.Infrastructure.Repositories.SQLServer.Config
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(p => p.PlayerName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasKey(p => p.Id);
        }
    }
}
