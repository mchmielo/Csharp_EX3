using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateuszChmielowskiLab4.Model
{
    public partial class Customer
    {
        /// <summary>
        /// Funkcja zwraca wszystkie rekordy z bazy danych z tabeli Customer.
        /// </summary>
        /// <param name="dataContext">
        /// Kontekst bazy danych.
        /// </param>
        /// <returns></returns>
        public static List<Customer> GetAll(DataClassesNorthwindDataContext dataContext)
        {
            return (from customer in dataContext.Customers select customer).ToList();
        }
        /// <summary>
        /// Funkcja zwraca wszystkie rekordy z bazy danych z tabeli Customer, gdzie imię jest takie jak
        /// domyślny parametr name = "Janek".
        /// </summary>
        /// <param name="dataContext">
        /// Kontekst bazy danych.
        /// </param>
        /// <param name="name">
        /// Imie do filtrowania.
        /// </param>
        /// <returns></returns>
        public static List<Customer> GetByName(DataClassesNorthwindDataContext dataContext, string name = "Janek")
        {
            return (from customer in dataContext.Customers where customer._ContactName.Equals(name) select customer).ToList();
        }
    }
}
