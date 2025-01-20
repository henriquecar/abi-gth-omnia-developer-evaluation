namespace Ambev.DeveloperEvaluation.Domain.Enums;

public enum SaleStatus
{
    Unknown = 0,
    /// <summary>
    /// Purchase in cart not made
    /// </summary>
    Draft,
    /// <summary>
    /// Awaiting payment
    /// </summary>
    AwaitingPayment,
    /// <summary>
    /// Awaiting transport
    /// </summary>
    AwaitingTransport,
    /// <summary>
    /// Items being transported
    /// </summary>
    Transporting,
    /// <summary>
    /// Payment done and items delivered
    /// </summary>
    Completed,
    /// <summary>
    /// Sale cancelled
    /// </summary>
    Cancelled
}
