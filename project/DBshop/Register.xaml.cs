using System.Windows;

namespace DBshop
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text != "" && tb2.Text != "" && tb3.Text != "" && tb4.Text != "" && tb5.Text != "")
            {
                Data.db.registration(tb1, tb2, tb3, tb4, tb5, this);
            }
        }
    }
}
