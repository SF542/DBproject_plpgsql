using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DBshop
{
    public partial class MainWindow : Window
    {
        int stoimost = 0, number_of_positions = 0;
        List<string> namesOfProducts = new List<string>();
        List<int> pricesOfProducts = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            Kolvo.Text = number_of_positions.ToString();
            Summa.Text = stoimost.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            this.Hide();
            log.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            this.Hide();
            reg.Show();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            if (Data.customerId != null)
            {
                if(Data.admin == true)
                {
                    Add.Visibility = Visibility.Visible;
                    Del.Visibility = Visibility.Visible;
                    delBox.Visibility = Visibility.Visible;
                    Out.Visibility = Visibility.Visible;
                    outBox.Visibility = Visibility.Visible;
                    leftBut.Visibility = Visibility.Visible;
                    rightBut.Visibility = Visibility.Visible;
                }
                orderButton.Visibility = Visibility.Visible;
                Summa.Visibility = Visibility.Visible;
                Kolvo.Visibility = Visibility.Visible;
                if (Data.admin == true)
                {
                    DG.CanUserAddRows = true;
                    DG.CanUserDeleteRows = true;
                    DG.CanUserReorderColumns = true;
                    DG.CanUserResizeColumns = true;
                    DG.CanUserResizeRows = true;
                }
                Data.page = 0;
                Data.db.showProducts(custname, DG);
            }
        }
        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            Data.db.Order(stoimost, number_of_positions);
            Data.db.AddOrderDetails(namesOfProducts, pricesOfProducts);
            string list_of_products = "";
            for (int i = 0; i < namesOfProducts.Count(); i++)
            {
                list_of_products += (i+1).ToString() + ". " + namesOfProducts[i] + "\t" + pricesOfProducts[i] + "\n";
            }
            MessageBox.Show("Имя: " + Data.firstName + "\n" + "Телефон: " + Data.phone + "\n" + "Товары:\n" + list_of_products + "\n" + "Количество позиций в заказе: " + number_of_positions + "\n" +"Общая стоимость заказа: " + stoimost + "\n", "Ваш заказ подтвержден!");
            stoimost = 0;
            number_of_positions = 0;
            Summa.Text = stoimost.ToString();
            Kolvo.Text = number_of_positions.ToString();
            namesOfProducts.Clear();
            pricesOfProducts.Clear();
        }
        private void Button_Click9(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = (DataRowView)((Button)e.Source).DataContext;
            namesOfProducts.Add(rowView["product_name"].ToString());
            pricesOfProducts.Add(Convert.ToInt32(rowView["price"]));
            //pricesOfProducts.Add(Convert.ToInt32(rowView.Row.ItemArray[2]));
            stoimost += Convert.ToInt32(rowView["price"].ToString());
            number_of_positions++;
            Summa.Text = stoimost.ToString();
            Kolvo.Text = number_of_positions.ToString();
        }
        private void Add_click(object sender, RoutedEventArgs e)
        {
            var name = DG.Items[DG.Items.Count - 2] as DataRowView;
            switch (Data.page)
            {
                case 0:
                    Data.db.AddProductToDatabase((string)name.Row.ItemArray[1], (int)name.Row.ItemArray[2]);
                    Data.db.showProducts(custname, DG);
                    break;
                case 1:
                    Data.db.AddCustomerToDatabase((string)name.Row.ItemArray[1], (string)name.Row.ItemArray[2], (string)name.Row.ItemArray[3], (string)name.Row.ItemArray[4], (string)name.Row.ItemArray[5]);
                    Data.db.showUsers(DG);
                    break;
            }
        }
        private void Out_Click(object sender, RoutedEventArgs e)
        {
            int row = Convert.ToInt32(outBox.Text);
            Data.db.OutputFile(row);
            Data.db.showProducts(custname, DG);
            outBox.Text = "";
        }

        private void rightBut_Click(object sender, RoutedEventArgs e)
        {
            Data.page++;
            GenerateTable();
        }
        private void leftBut_Click(object sender, RoutedEventArgs e)
        {
            Data.page--;
            GenerateTable();
        }
        public void GenerateTable()
        {
            switch (Data.page)
            {
                case 0:
                    Data.db.showProducts(custname, DG);
                    Add.Visibility = Visibility.Visible;
                    BuyColumn.Visibility = Visibility.Visible;
                    Del.Visibility = Visibility.Visible;
                    delBox.Visibility = Visibility.Visible;
                    break;
                case 1:
                    Data.db.showUsers(DG);
                    Add.Visibility = Visibility.Visible;
                    BuyColumn.Visibility = Visibility.Collapsed;
                    Del.Visibility = Visibility.Visible;
                    delBox.Visibility = Visibility.Visible;
                    break;
                case 2:
                    Data.db.showOrders(DG);
                    Add.Visibility = Visibility.Collapsed;
                    BuyColumn.Visibility = Visibility.Collapsed;
                    Del.Visibility = Visibility.Visible;
                    delBox.Visibility = Visibility.Visible;
                    break;
                case 3:
                    Data.db.showOrdersDetails(DG);
                    Add.Visibility = Visibility.Collapsed;
                    BuyColumn.Visibility = Visibility.Collapsed;
                    Del.Visibility = Visibility.Collapsed;
                    delBox.Visibility = Visibility.Collapsed;
                    break;
                default:
                    Data.page = 0;
                    Data.db.showProducts(custname, DG);
                    Add.Visibility = Visibility.Visible;
                    BuyColumn.Visibility = Visibility.Visible;
                    Del.Visibility = Visibility.Visible;
                    delBox.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            int row = Convert.ToInt32(delBox.Text);
            switch (Data.page)
            {
                case 0:
                    Data.db.DeleteProduct(row);
                    Data.db.showProducts(custname, DG);
                    break;
                case 1:
                    Data.db.DeleteCustomer(row);
                    Data.db.showUsers(DG);
                    break;
                case 2:
                    Data.db.DeleteOrder(row);
                    Data.db.showOrders(DG);
                    break;
            }
            delBox.Text = "";
        }
    }
}
