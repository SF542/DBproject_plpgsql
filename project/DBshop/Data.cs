using System.Windows.Controls.Primitives;

namespace DBshop
{
    static class Data
    {
        public static int? customerId;
        public static int page = 0, orderId;
        public static string firstName, lastName, email, phone;
        public static DataBase db = new DataBase();
        public static bool admin = false;
    }
}