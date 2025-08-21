namespace Likvido.Identity.Roles
{
    public static class LikvidoRoles
    {
        public static string[] VisibleRoles = 
        { 
            Internals.Manager,
            Internals.Product,
            Internals.Operations,
            Internals.Payout,
            Internals.Owner,
            Internals.LikvidoEmployee,
            Creditors.Creditor, 
            Creditors.CreditorEmployee 
        };

        public static string[] All =
        {
            Internals.Manager,
            Internals.Product,
            Internals.Operations,
            Internals.Payout,
            Internals.Owner,
            Internals.LikvidoEmployee,
            Creditors.Creditor,
            Creditors.CreditorEmployee,
            Debtors.Debtor
        };

        public static string[] LikvidoInternalRoles =
        {
            Internals.LikvidoEmployee,
            Internals.Manager,
            Internals.Product,
            Internals.Operations,
            Internals.Payout,
            Internals.Owner,
        };

        public static string[] CreditorRoles = { Creditors.CreditorEmployee, Creditors.Creditor };
        public static string[] HighSensitivityRoles = { Internals.Owner, Internals.Payout };

        public static class Internals
        {
            public const string LikvidoEmployee = "likvidoEmployee";
            public const string Manager = "manager";
            public const string Operations = "operations";
            public const string Product = "product";
            public const string Payout = "payout";
            public const string Owner = "owner";
        }

        public static class Creditors
        {

            /// <summary>
            /// 'Administrator' role for Creditor:
            /// Account owner has this role by default, all creditor's admins have this role aswell
            /// </summary>
            public const string Creditor = "creditor";
            /// <summary>
            /// 'User' role for Creditor
            /// </summary>
            public const string CreditorEmployee = "creditorEmployee";
        }

        public static class Debtors
        {
            public const string Debtor = "debtor";
        }
    }
}
