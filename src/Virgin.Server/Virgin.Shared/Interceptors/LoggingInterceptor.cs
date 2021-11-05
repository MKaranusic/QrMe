using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Virgin.Shared.Extensions;

namespace Virgin.Shared.Interceptors
{
    public class LoggingInterceptor<T> : IInterceptor
    {
        private readonly ILogger<T> _logger;

        public LoggingInterceptor(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            _logger.LogMethodStart(invocation);

            invocation.Proceed();

            var task = (Task)invocation.ReturnValue;
            task.ContinueWith(priorTask =>
            {
                var methodReturnValue = priorTask.GetType().GetProperty("Result").GetValue(priorTask);

                _logger.LogMethodEnd(invocation, methodReturnValue);
            });
        }
    }
}