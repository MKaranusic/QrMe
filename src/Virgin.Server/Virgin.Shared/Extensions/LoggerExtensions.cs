using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;

namespace Virgin.Shared.Extensions
{
    internal static class LoggerExtensions
    {
        public static void LogMethodStart(this ILogger logger, IInvocation caller)
        {
            var methodName = caller.Method.Name;
            var methodArguments = new List<object>();

            foreach (var arg in caller.Arguments)
            {
                if (arg.GetType().IsValueType)
                {
                    methodArguments.Add(arg);
                    continue;
                }

                var serializedArg = JsonSerializer.Serialize(arg, new JsonSerializerOptions());
                methodArguments.Add(serializedArg);
            }

            logger.LogDebug("Start[{methodName}], Args: {args}", methodName, methodArguments);
        }

        public static void LogMethodEnd(this ILogger logger, IInvocation caller, object returnValue)
        {
            var methodName = caller.Method.Name;
            var returnValueDebug = returnValue;

            if (!returnValue.GetType().IsValueType)
            {
                var serializedReturnValue = JsonSerializer.Serialize(returnValue, new JsonSerializerOptions());
                returnValueDebug = serializedReturnValue;
            }

            logger.LogDebug("End[{methodName}], Return: {return}", methodName, returnValueDebug);
        }
    }
}