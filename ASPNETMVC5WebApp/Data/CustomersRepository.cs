using ASPNETMVC5WebApp.Config;
using ASPNETMVC5WebApp.Models;
using ASPNETMVC5WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.Data
{
    public class CustomersRepository
    {
        //
        public List<CustomerDisplayViewModel> GetCustomers()
        {

            // build customer list here
            List<Customer> customers = new List<Customer>();
            customers.AddRange(GetAllCustomer("",""));

            // build country list here
            List<Country> countries = new List<Country>();
            CountriesRepository c = new CountriesRepository();
            countries.AddRange(c.GetAllCountry());

            // build region list here
            List<Region> regions = new List<Region>();
            RegionsRepository r = new RegionsRepository();
            regions.AddRange(r.GetAllRegion(""));

            if (customers != null)
            {
                List<CustomerDisplayViewModel> customersDisplay = new List<CustomerDisplayViewModel>();
                foreach (var x in customers)
                {
                    var customerDisplay = new CustomerDisplayViewModel()
                    {
                        CustomerID = x.CustomerID,
                        CustomerName = x.CustomerName,
                        CountryName = (from xc in countries
                                       where xc.Iso3 == x.CountryIso3
                                       select xc.CountryNameEnglish).FirstOrDefault(),
                        RegionName = (from xr in regions
                                      where xr.RegionCode == x.RegionCode && xr.Iso3 == x.CountryIso3
                                      select xr.RegionNameEnglish).FirstOrDefault()
                    };
                    customersDisplay.Add(customerDisplay);
                }
                return customersDisplay;
            }
            else
            {
                return null;
            }
            
            
        }

        // for DropDown
        public CustomerEditViewModel CreateCustomer()
        {
            var cRepo = new CountriesRepository();
            var rRepo = new RegionsRepository();
            var customer = new CustomerEditViewModel()
            {
                CustomerID = Guid.NewGuid().ToString(),
                Countries = cRepo.GetCountries(),
                Regions = rRepo.GetRegions()
            };
            return customer;
        }

        //
        public bool SaveCustomer(CustomerEditViewModel customeredit)
        {
            if (customeredit != null)
            {
                //using (var context = new ApplicationDbContext())
                //{
                //    if (Guid.TryParse(customeredit.CustomerID, out Guid newGuid))
                //    {
                //        var customer = new Customer()
                //        {
                //            CustomerID = newGuid,
                //            CustomerName = customeredit.CustomerName,
                //            CountryIso3 = customeredit.SelectedCountryIso3,
                //            RegionCode = customeredit.SelectedRegionCode
                //        };
                //        customer.Country = context.Countries.Find(customeredit.SelectedCountryIso3);
                //        customer.Region = context.Regions.Find(customeredit.SelectedRegionCode);

                //        context.Customers.Add(customer);
                //        context.SaveChanges();
                //        return true;
                //    }
                //}
            }
            // Return false if customeredit == null or CustomerID is not a guid
            return false;
        }

        //
        public List<Customer> GetAllCustomer(string Iso3, string RegionCode)
        {
            // "sp_GetAllCustomer"
            //List<Customer> customers = GetCustomers("sp_GetAllCustomer", null);

            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                string sql_str = "SELECT * FROM tbl_customer ";
                if (Iso3 != "" && RegionCode != "")
                {
                    sql_str += "WHERE Iso3 = '" + Iso3 + "' AND RegionCode = '" + RegionCode + "' ";
                }
                else
                {
                    if (Iso3 != "")
                    {
                        sql_str += "WHERE Iso3 = '" + Iso3 + "' ";
                    }
                    if (RegionCode != "")
                    {
                        sql_str += "WHERE RegionCode = '" + RegionCode + "' ";
                    }
                }
                sql_str += "ORDER BY CustomerName";

                //using (SqlCommand cmd = new SqlCommand("sp_GetAllCustomer", conn)) //sql_str
                using (SqlCommand cmd = new SqlCommand(sql_str, conn)) //sql_str
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //
                    //

                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dtCustomers = new DataTable();
                        dtCustomers.Load(dr);

                        foreach (DataRow row in dtCustomers.Rows)
                        {
                            customers.Add(new Customer
                            {
                                //Id = Convert.ToInt32(row["Id"]),
                                //Price = Convert.ToDecimal(row["Price"]),
                                CustomerID = Convert.ToInt32(row["CustomerID"]),//(Guid)(row["CustomerID"]),
                                CustomerName = row["CustomerName"].ToString(),
                                CountryIso3 = row["CountryIso3"].ToString(),
                                RegionCode = row["RegionCode"].ToString()
                            });
                        }
                        //
                    }
                    catch (Exception ex)
                    {
                        //

                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }

                }

            }

            return customers;
        }



        //
    }
}

