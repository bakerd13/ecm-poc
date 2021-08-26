using System;

namespace DABTechs.eCommerce.Sales.Domain.Helpers
{
    public static class Argument
    {
        public static void IsNotNull(object arg, string propertyName, string exceptionMessage = "The property cannot be null!")
        {
            if (arg is null)
            {
                throw new ArgumentNullException(propertyName, exceptionMessage);
            }
        }

        public static void IsNotEmpty(object arg, string propertyName, string exceptionMessage = "The property cannot be an empty!")
        {
            IsNotNull(arg, propertyName);

            if (arg is string)
            {
                if (string.IsNullOrEmpty(arg as string))
                {
                    throw new ArgumentException(propertyName, exceptionMessage);
                }
            }
            else
            {
                throw new NotSupportedException("Type '" + arg.GetType() + "' is not supported!");
            }
        }
    }
}