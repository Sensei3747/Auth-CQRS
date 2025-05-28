using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAuth.Domain.Conversations;

namespace UserAuth.Infrastructure.EntityConfigurations;

public class ConversationParticipantConfiguration : IEntityTypeConfiguration<ConversationParticipant>
{
    public void Configure(EntityTypeBuilder<ConversationParticipant> builder)
    {
        // Define composite key
        builder.HasKey(cp => new { cp.UserId, cp.ConversationId });

        // Define relationship
        builder.HasOne(cp => cp.Conversation)
               .WithMany(c => c.Participants)
               .HasForeignKey(cp => cp.ConversationId);
    }
}

