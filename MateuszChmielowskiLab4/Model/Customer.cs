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
        /// <summary>
        /// Funkcja aktualizuje dane w tabeli customer, dla wszystkich klientów o kraju zaczynającym się
        /// na fisrtLetter ustawia miasto na city.
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="city"></param>
        /// <param name="firstLetter"></param>
        public static void UpdateCustomerCity(DataClassesNorthwindDataContext dataContext, string city, string firstLetter)
        {
            List<Customer> clients = (from customer in dataContext.Customers
                                      where customer.Country.Substring(0, 1) == firstLetter
                                      select customer).ToList();
            for (int i = 0; i < clients.Count; ++i)
            {
                clients[i].City = city;
            }
            dataContext.SubmitChanges();
        }
        /// <summary>
        /// Funkcja tworzy nowego klienta o podanym w parametrze imieniu i numerze ID.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ID"></param>
        public static void AddNewCustomer(DataClassesNorthwindDataContext dataContext, string Name, string ID)
        {
            Customer customer = new Customer();
            customer.ContactName = Name;
            customer.CustomerID = ID;
            customer.Country = "Polska";
            customer.City = "Wroclaw";
            customer.CompanyName = "Kredek";
            dataContext.Customers.InsertOnSubmit(customer);
            dataContext.SubmitChanges();
        }
        /// <summary>
        /// Funkcja usuwa wszystkich użytkowników o podanym imieniu w parametrze Name.
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="Name"></param>
        public static void DeleteUsers(DataClassesNorthwindDataContext dataContext, string Name)
        {
            var clients = (from customer in dataContext.Customers select customer).Where(x => x.ContactName.Equals(Name));
            dataContext.Customers.DeleteAllOnSubmit(clients);
            dataContext.SubmitChanges();
        }
    }
}
