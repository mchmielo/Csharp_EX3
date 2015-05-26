using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MateuszChmielowskiLab4.Model
{
    public partial class Order
    {
        /// <summary>
        /// Funkcja aktualizuje DataGridView.DataSource podanego jako referencja, tak aby wyswietlic pola z tabeli Order i Customer.
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="dataGridViewContent"></param>
        public static void SelectCustomerOrdersToDataGridView(DataClassesNorthwindDataContext dataContext, ref DataGridView dataGridViewContent)
        {
            var query = from customer in dataContext.Customers
                        join order in dataContext.Orders on customer.CustomerID
                        equals order.CustomerID
                        select new { customer.ContactName, customer.Address, order.OrderDate, order.ShipName };
            dataGridViewContent.DataSource = query;
        }
    }
}
