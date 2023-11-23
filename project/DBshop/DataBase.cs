using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DBshop
{
    internal class DataBase
    {
        string connString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=postgres;";
        //string connString = "Host=pgdb.uni-dubna.ru;Port=5432;Username=student15;Password=884042;Database=student15;";
        public void showProducts(Label custname, DataGrid DG)
        {
            custname.Content = "Добро пожаловать, " + Data.firstName + "!";
            custname.Visibility = Visibility.Visible;
            DG.ItemsSource = null;
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string selectQuery = "SELECT * FROM public.\"Products\" ORDER BY product_id ASC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                }
                conn.Close();
            }
        }
        public void showUsers(DataGrid DG)
        {
            DG.ItemsSource = null;
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string selectQuery = "SELECT * FROM public.\"Customers\" ORDER BY customer_id ASC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                }
                conn.Close();
            }
        }
        public void showOrders(DataGrid DG)
        {
            DG.ItemsSource = null;
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string selectQuery = "SELECT * FROM public.\"Orders\" ORDER BY order_id ASC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                }
                conn.Close();
            }
        }
        public void showOrdersDetails(DataGrid DG)
        {
            DG.ItemsSource = null;
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string selectQuery = "SELECT * FROM public.\"ordersdetails\" ORDER BY order_id ASC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, conn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                }
                conn.Close();
            }
        }
        public void loginCheck(TextBox tb1, TextBox tb2, Window wn)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string selectQuery = "SELECT customer_id, first_name, last_name, email, phone, password FROM public.\"Customers\" WHERE email = @email AND password = @password";
                using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@email", tb1.Text);
                    cmd.Parameters.AddWithValue("@password", tb2.Text);
                    if (tb1.Text == "admin" && tb2.Text == "admin")
                        Data.admin = true;
                    else 
                        Data.admin = false;
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Data.customerId = reader.GetInt32(0);
                            Data.firstName = reader.GetString(1);
                            Data.lastName = reader.GetString(2);
                            Data.email = reader.GetString(3);
                            Data.phone = reader.GetString(4);
                            MainWindow main = new MainWindow();
                            wn.Hide();
                            main.Show();
                        }
                    }
                }
                conn.Close();
            }
        }
        public void registration(TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, Window wn)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string insertQuery = "INSERT INTO public.\"Customers\" (first_name, last_name, email, phone, password) VALUES (@first_name, @last_name, @email, @phone, @password)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@first_name", tb1.Text);
                    cmd.Parameters.AddWithValue("@last_name", tb2.Text);
                    cmd.Parameters.AddWithValue("@email", tb3.Text);
                    cmd.Parameters.AddWithValue("@phone", tb4.Text);
                    cmd.Parameters.AddWithValue("@password", tb5.Text);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                MainWindow main = new MainWindow();
                wn.Hide();
                main.Show();
            }
        }
        public void Order(int price, int kolvo)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string insertQuery = "INSERT INTO public.\"Orders\" (customer_id, order_date, total_amount, total_positions) VALUES (@customer_id, @order_date, @total_amount, @total_positions) RETURNING order_id;";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@customer_id", Data.customerId);
                    cmd.Parameters.AddWithValue("@order_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@total_amount", price);
                    cmd.Parameters.AddWithValue("@total_positions", kolvo);
                    Data.orderId = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void AddOrderDetails(List<string> namesOfProducts, List<int> pricesOfProducts)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                for (int i = 0; i < namesOfProducts.Count(); i++)
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO public.\"ordersdetails\" (customer_id, order_id, product_name, price) VALUES (@customer_id, @order_id, @product_name, @price);";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customer_id", Data.customerId);
                        cmd.Parameters.AddWithValue("@order_id", Data.orderId);
                        cmd.Parameters.AddWithValue("@product_name", namesOfProducts[i]);
                        cmd.Parameters.AddWithValue("@price", pricesOfProducts[i]);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }
        public void AddProductToDatabase(string productName, int price)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string insertQuery = "INSERT INTO public.\"Products\" (product_name, price) VALUES (@product_name, @price)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@product_name", productName);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public void AddCustomerToDatabase(string firstName, string lastName, string email, string phone, string pass)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string insertQuery = "INSERT INTO public.\"Customers\" (first_name, last_name, email, phone, password) VALUES (@first_name, @last_name, @email, @phone, @password)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@first_name", firstName);
                    cmd.Parameters.AddWithValue("@last_name", lastName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@password", pass);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public void DeleteCustomer(int userId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string insertQuery = "DELETE FROM public.\"Customers\" WHERE customer_id = @customer_id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@customer_id", userId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public void DeleteOrder(int orderId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {                
                conn.Open();
                string insertQuery = "DELETE FROM public.\"ordersdetails\" WHERE order_id = @order_id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", orderId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                conn.Open();
                insertQuery = "DELETE FROM public.\"Orders\" WHERE order_id = @order_id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", orderId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public void DeleteProduct(int productId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string insertQuery = "DELETE FROM public.\"Products\" WHERE product_id = @product_id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@product_id", productId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public void OutputFile(int cId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT get_total_amount_by_customer_id(@customer_id)", conn))
                {
                    cmd.Parameters.AddWithValue("@customer_id", cId);
                    int totalAmountSum = (int)cmd.ExecuteScalar();
                    string filePath = "total_amount.txt";
                    File.WriteAllText(filePath, totalAmountSum.ToString());
                }
                conn.Close();
            }
        }
    }
}