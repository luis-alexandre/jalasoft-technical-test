using BowlingGame.Infrastructure.Repositories.SQLServer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BowlingGame.Infrastructure.Repositories.SQLServer.Config
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.Property(p => p.Value)
                   .IsRequired();

            builder.HasKey(p => p.Id);

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Scores)
                   .HasForeignKey(x => x.GameId);
        }
    }
}
