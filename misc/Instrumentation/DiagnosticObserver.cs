using System.Text;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Resolvers;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;

namespace Instrumentation
{
    public class DiagnosticObserver
        : IDiagnosticObserver
    {
        private readonly ILogger _logger;
        public DiagnosticObserver(ILogger<DiagnosticObserver> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [DiagnosticName("HotChocolate.Execution.Query")]
        public void OnQuery(IQueryContext context)
        {
            // This method is used as marker to enable begin and end events 
            // in the case that you want to explicitly track the start and the 
            // end of this event.
        }

        [DiagnosticName("HotChocolate.Execution.Query.Start")]
        public void BeginQueryExecute(IQueryContext context)
        {
            _logger.LogInformation(context.Request.Query);
        }

        [DiagnosticName("HotChocolate.Execution.Query.Stop")]
        public void EndQueryExecute(IQueryContext context)
        {
            if (!_logger.IsEnabled(LogLevel.Information))
            {
                return;
            }
        
            using (var stream = new MemoryStream())
            {
                var resultSerializer = new JsonQueryResultSerializer();
                resultSerializer.SerializeAsync(
                    (IReadOnlyQueryResult)context.Result,
                    stream).Wait();
                _logger.LogInformation(
                    Encoding.UTF8.GetString(stream.ToArray()));
            }
        }

        [DiagnosticName("HotChocolate.Execution.Resolver.Error")]
        public void OnResolverError(
            IResolverContext context,
            IEnumerable<IError> errors)
        {
            foreach (IError error in errors)
            {
                string path = string.Join("/",
                    error.Path.Select(t => t.ToString()));

                if (error.Exception == null)
                {
                    _logger.LogError("{0}\r\n{1}", path, error.Message);
                }
                else
                {
                    _logger.LogError(error.Exception,
                        "{0}\r\n{1}", path, error.Message);
                }
            }
        }
    }
}
