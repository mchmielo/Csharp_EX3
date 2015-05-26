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
            Customer.UpdateCustomerCity(dataContext, "Wrocław", "p");
        }
        /// <summary>
        /// Dodawanie nowego klienta, o imieniu wpisanym w pole textBoxName i ID wpisanym w pole
        /// textBoxID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text) || string.IsNullOrEmpty(textBoxID.Text))
            {
                MessageBox.Show("Wypełnij wszystkie pola!");
                return;
            }
            Customer.AddNewCustomer(dataContext, textBoxName.Text, textBoxID.Text);
        }

        /// <summary>
        /// Usuwanie wszystkich klientów z bazy danych o imieniu Jan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Customer.DeleteUsers(dataContext, "Jan");
        }

        
        /// <summary>
        /// Metoda wyciąga z bazy danych łącząc dane z tabeli Customer i Order.
        /// Przekazuje referencje dataGridViewContent po to aby zaktualizować jej DataSource.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCustomerOrders_Click(object sender, EventArgs e)
        {
            Order.SelectCustomerOrdersToDataGridView(dataContext, ref dataGridViewContent);
        }

        
    }
}
