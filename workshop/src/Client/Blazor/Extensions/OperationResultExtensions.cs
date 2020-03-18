using System.Linq;
using StrawberryShake;

namespace Client.Extensions
{
    public static class OperationResultExtensions
    {
        
        public static bool InvalidCredentials<T>(this IOperationResult<T> operation)
            where T : class
        {
            return operation.HasErrors &&
                   operation.Errors.Any(e => e.Code == "INVALID_CREDENTIALS");
        }
        
        public static bool IsNonNullViolation<T>(this IOperationResult<T> operation)
            where T : class
        {
            return operation.HasErrors &&
                operation.Errors.Any(e => e.Code == "EXEC_NON_NULL_VIOLATION");
        }
        
        public static bool IsNotAuthenticted<T>(this IOperationResult<T> operation)
            where T : class
        {
            return operation.HasErrors &&
                operation.Errors.Any(e => e.Code == "AUTH_NOT_AUTHENTICATED");
        }
    }
}