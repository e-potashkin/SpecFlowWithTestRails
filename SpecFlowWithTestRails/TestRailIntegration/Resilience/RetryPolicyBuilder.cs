using System.Net;
using Flurl.Http;
using Polly;
using Polly.Retry;

namespace SpecFlowWithTestRails.TestRailIntegration.Resilience;

public class RetryPolicyBuilder
{
    public static AsyncRetryPolicy BuildRetryPolicy()
    {
        var retryPolicy = Policy
            .Handle<FlurlHttpException>(IsTransientError)
            .WaitAndRetryAsync(3, retryAttempt =>
            {
                var nextAttemptIn = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                Console.WriteLine($"Retry attempt {retryAttempt} to make request. Next try on {nextAttemptIn.TotalSeconds} seconds.");
                return nextAttemptIn;
            });

        return retryPolicy;
    }

    private static bool IsTransientError(FlurlHttpException exception)
    {
        int[] httpStatusCodesWorthRetrying =
        {
            (int) HttpStatusCode.RequestTimeout, // 408
			(int) HttpStatusCode.BadGateway, // 502
			(int) HttpStatusCode.ServiceUnavailable, // 503
			(int) HttpStatusCode.GatewayTimeout // 504
        };

        return exception.StatusCode.HasValue && httpStatusCodesWorthRetrying.Contains(exception.StatusCode.Value);
    }
}
