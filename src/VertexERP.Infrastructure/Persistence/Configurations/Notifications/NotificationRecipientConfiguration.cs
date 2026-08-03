using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VertexERP.Domain.Module.Notifications.Entities;

public sealed class NotificationRecipientConfiguration
    : IEntityTypeConfiguration<NotificationRecipient>
{
    public void Configure(EntityTypeBuilder<NotificationRecipient> builder)
    {
        builder.ToTable("NotificationRecipients");

        builder.HasKey(x => new
        {
            x.NotificationId,
            x.UserId
        });

        builder.Property(x => x.IsRead)
            .IsRequired();

        builder.Property(x => x.ReadAt);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.IsRead
        });

        builder.HasOne(x => x.Notification)
            .WithMany(x => x.Recipients)
            .HasForeignKey(x => x.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}