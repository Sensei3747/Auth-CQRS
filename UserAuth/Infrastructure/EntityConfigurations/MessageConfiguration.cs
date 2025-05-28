using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAuth.Domain.Conversations;

namespace UserAuth.Infrastructure.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasMaxLength(36) // if it's a GUID as string, otherwise adjust
            .IsRequired();

        builder.Property(m => m.ConversationId)
            .IsRequired();

        builder.Property(m => m.SenderId)
            .IsRequired();

        builder.Property(m => m.SenderName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Content)
            .IsRequired();

        builder.Property(m => m.CreatedOn)
            .IsRequired();

        // If you later define a relationship with Conversation entity:
        // builder.HasOne<Conversation>()
        //        .WithMany()
        //        .HasForeignKey(m => m.ConversationId)
        //        .OnDelete(DeleteBehavior.Cascade);
    }
}
