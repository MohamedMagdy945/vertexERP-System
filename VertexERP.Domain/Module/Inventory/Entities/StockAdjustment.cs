
using VertexERP.Domain.Common;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Domain.Module.Inventory.Entities;
public sealed class StockAdjustment : Entity
{
    public Guid WarehouseId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Quantity { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public StockAdjustmentStatus Status { get; private set; }
    public Guid RequestedByUserId { get; private set; }
    public Guid? ApprovedByUserId { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public Guid? RejectedByUserId { get; private set; }
    public DateTime? RejectedAt { get; private set; }

    public string? RejectionReason { get; private set; }
    private StockAdjustment()
    {
    }
    private StockAdjustment(
        Guid warehouseId,
        Guid productId,
        decimal quantity,
        string reason,
        Guid requestedByUserId)
    {
        WarehouseId = warehouseId;
        ProductId = productId;
        Quantity = quantity;
        Reason = reason;
        RequestedByUserId = requestedByUserId;

        Status = StockAdjustmentStatus.Pending;
    }
    public static StockAdjustment Create(
        Guid warehouseId,
        Guid productId,
        decimal quantity,
        string reason,
        Guid requestedByUserId)
    {
        return new StockAdjustment(
            warehouseId,
            productId,
            quantity,
            reason,
            requestedByUserId);
    }
    public Result Approve(Guid userId)
    {
        if (Status != StockAdjustmentStatus.Pending)
            return Result.Failure("Only pending adjustments can be approved.");

        Status = StockAdjustmentStatus.Approved;
        ApprovedByUserId = userId;
        ApprovedAt = DateTime.UtcNow;

        MarkAsUpdated();

        return Result.Success();
    }
    public Result Reject(Guid userId, string reason)
    {
        if (Status != StockAdjustmentStatus.Pending)
            return Result.Failure(
                "Only pending adjustments can be rejected.");

        if (string.IsNullOrWhiteSpace(reason))
            return Result.Failure(
                "Rejection reason is required.");

        Status = StockAdjustmentStatus.Rejected;
        RejectedByUserId = userId;
        RejectedAt = DateTime.UtcNow;
        RejectionReason = reason;

        MarkAsUpdated();

        return Result.Success();
    }
    public Result Apply()
    {
        if (Status != StockAdjustmentStatus.Approved)
            return Result.Failure("Only approved adjustments can be applied.");

        Status = StockAdjustmentStatus.Applied;

        MarkAsUpdated();

        return Result.Success();
    }
}
