namespace Virgin.API
{
    public static class VirginAuthorizationConstants
    {
        public const string AdminPolicy = "ADMIN_POLICY";
        public const string RoleClaim = "extension_Role";
    }

    public static class VirginRoles
    {
        public const string VRGAdmin = "ADMIN";
        public const string VRGCustomer = "CUSTOMER";
    }

    public static class Environment
    {
        public const string Production = "Production";
        public const string Development = "Development";
    }

    public static class Regex
    {
        public const string OnlyDigits = "^[0-9]*$";
    }
}