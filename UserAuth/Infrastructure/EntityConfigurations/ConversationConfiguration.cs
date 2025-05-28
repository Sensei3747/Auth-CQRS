using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAuth.Domain.Conversations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        // Table name (optional if class name matches)
        builder.ToTable("Conversations");

        // Primary Key
        builder.HasKey(c => c.Id);

        // RoomId (nullable)
        builder.Property(c => c.RoomId)
               .IsRequired(false);

        // IsGroup
        builder.Property(c => c.IsGroup)
               .IsRequired();

        // CreatedOn
        builder.Property(c => c.CreatedOn)
               .IsRequired();

        // UpdatedOn
        builder.Property(c => c.UpdatedOn)
               .IsRequired();

        // One-to-many: Conversation has many Participants
        builder
            .HasMany(c => c.Participants)
            .WithOne(p => p.Conversation)
            .HasForeignKey("ConversationId") // Shadow property or explicitly defined in ConversationParticipant
            .OnDelete(DeleteBehavior.Cascade);
    }
}
