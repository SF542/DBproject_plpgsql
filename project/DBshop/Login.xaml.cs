using System.Windows;

namespace DBshop
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text != "" && tb2.Text != "")
            {
                Data.db.loginCheck(tb1, tb2, this);
            }
        }
    }
}
