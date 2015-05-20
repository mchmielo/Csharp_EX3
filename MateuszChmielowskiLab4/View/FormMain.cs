using MateuszChmielowskiLab4.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MateuszChmielowskiLab4
{
    public partial class FormMain : Form
    {
        DataClassesNorthwindDataContext dataContext = new DataClassesNorthwindDataContext();
        public FormMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Metoda pobiera z bazy danych dane po wciśnięciu przycisku "Pobierz".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGet_Click(object sender, EventArgs e)
        {
            dataGridViewContent.DataSource = Customer.GetAll(dataContext);
        }
        /// <summary>
        /// Uaktualnianie klientów w bazie danych, wszyscy, których państwo zaczyna się na P
        /// mają zmienione miasto na Wrocław.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Customer client = (from customer in dataContext.Customers select customer).First();
            client.City = "Wrocław";
            List<Customer> clients = (from customer in dataContext.Customers
                                      where customer.Country.Substring(0, 1) == "P"
                                      select customer).ToList();
            for (int i = 0; i < clients.Count; ++i )
            {
                clients[i].City = "Wrocław";
            }
            dataContext.SubmitChanges();
        }
        /// <summary>
        /// Dodawanie nowego klienta, o imieniu wpisanym w pole textBoxName i ID wpisanym w pole
        /// textBoxID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            if (string.IsNullOrEmpty(textBoxName.Text) || string.IsNullOrEmpty(textBoxID.Text)) 
            { 
                MessageBox.Show("Wypełnij wszystkie pola!");
                return;
            }
            customer.ContactName = textBoxName.Text;
            customer.CustomerID = textBoxID.Text;
            customer.Country = "Polska";
            customer.City = "Wroclaw";
            customer.CompanyName = "Kredek";
            dataContext.Customers.InsertOnSubmit(customer);
            dataContext.SubmitChanges();
        }
        /// <summary>
        /// Usuwanie wszystkich klientów z bazy danych o imieniu Jan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var clients = (from customer in dataContext.Customers select customer).Where(x=>x.ContactName.Equals("Jan"));
            dataContext.Customers.DeleteAllOnSubmit(clients);
            dataContext.SubmitChanges();
        }
        /// <summary>
        /// Metoda wyciąga z bazy danych łącząc dane z tabeli Customer i Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCustomerOrders_Click(object sender, EventArgs e)
        {
            var query = from customer in dataContext.Customers 
                        join order in dataContext.Orders on customer.CustomerID 
                        equals order.CustomerID 
                        select new { customer.ContactName, customer.Address, order.OrderDate, order.ShipName };
            dataGridViewContent.DataSource = query;
        }
    }
}
