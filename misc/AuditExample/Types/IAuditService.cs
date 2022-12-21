using System.Security.Claims;

namespace AuditExample.Types;

public interface IAuditService
{
    void ReportUsage(ClaimsPrincipal user, IReadOnlySet<string> categories);
}

public class ConsoleAuditService : IAuditService
{
    public void ReportUsage(ClaimsPrincipal user, IReadOnlySet<string> categories)
    {
        Console.WriteLine(string.Join(",", categories));
    }
}
