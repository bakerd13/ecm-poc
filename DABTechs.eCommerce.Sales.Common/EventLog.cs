using System;
using System.Diagnostics;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class Logger
    {
        public static void Error(Exception ex, string message = "")
        {
            Trace.TraceError($"{message}, Error: Source: {ex.TargetSite}, Error: {ex.ToString()}, StackTrace: { ex.StackTrace}");
        }

        public static void Error(string message)
        {
            Trace.TraceError($"Error message: {message}");
        }

        public static void Info(string message)
        {
            Trace.TraceInformation($"{message}");
        }
    }
}