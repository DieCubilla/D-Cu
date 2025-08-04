using DCu.Domain.Enums;

namespace DCu.Application.Common.Extensions;

public static class PaymentStatusExtensions
{
    public static string ToDisplayName(this PaymentStatus status)
    {
        return status switch
        {
            PaymentStatus.Unpaid => "No pagado",
            PaymentStatus.PartiallyPaid => "Parcialmente pagado",
            PaymentStatus.Paid => "Pagado",
            _ => "Desconocido"
        };
    }
}
