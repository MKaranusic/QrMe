namespace Virgin.Core
{
    internal static class ExceptionMessages
    {
        public const string CustomerRedirectNotFound = "CustomerRedirect Not Found!";
        public const string QrCodeNotFound = "Qr code not found!";
        public const string CustomerEmailExists = "Customer with this email already exists";
    }

    internal static class CustomerRedirecConfig
    {
        public const int Take = 20;
    }
}