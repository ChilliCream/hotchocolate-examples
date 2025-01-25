using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace WebsocketAuthentication.Types;

public static class Subscription
{
    public static async IAsyncEnumerable<TimedEvent> TimeBasedSubscriber(
        ClaimsPrincipal principal,
        [EnumeratorCancellation] CancellationToken ct)
    {
        var count = 0;

        while (!ct.IsCancellationRequested)
        {
            yield return new TimedEvent(count++, principal.Identity?.IsAuthenticated ?? false);

            await Task.Delay(250, ct);
        }
    }

    [Subscription]
    [Subscribe(With = nameof(TimeBasedSubscriber))]
    public static TimedEvent OnTimedEvent([EventMessage] TimedEvent @event) => @event;
}