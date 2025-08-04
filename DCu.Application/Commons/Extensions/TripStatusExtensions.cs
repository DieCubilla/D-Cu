using DCu.Domain.Enums;

namespace DCu.Application.Common.Extensions;

public static class TripStatusExtensions
{
    public static string ToDisplayName(this TripStatus status)
    {
        return status switch
        {
            TripStatus.Pending => "Pendiente",
            TripStatus.InProgress => "En curso",
            TripStatus.Completed => "Finalizado",
            TripStatus.Cancelled => "Cancelado",
            _ => "Desconocido"
        };
    }
}
