using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using YuNotes.Models;

namespace YuNotes.Data.Configurations
{
    public class NoteGroupConfiguration : IEntityTypeConfiguration<NoteGroup>
    {
        public void Configure(EntityTypeBuilder<NoteGroup> builder)
        {
            builder
            .HasKey(g => g.Id);

            builder
                .HasMany(g => g.Notes)
                .WithOne(n => n.Group)
                .HasForeignKey(n => n.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(n => n.User)
                .WithMany(u => u.Groups)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
