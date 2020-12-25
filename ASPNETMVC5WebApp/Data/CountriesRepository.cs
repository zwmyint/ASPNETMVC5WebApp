using ASPNETMVC5WebApp.Config;
using ASPNETMVC5WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Data
{
    public class CountriesRepository
    {
        //
        //public IEnumerable<SelectListItem> GetCountries()
        //{
        //    List<SelectListItem> countries = new List<SelectListItem>()
        //    {
        //        new SelectListItem
        //        {
        //            Value = null,
        //            Text = " "
        //        }
        //    };
        //    return countries;
        //}

        // for Dropdown
        public IEnumerable<SelectListItem> GetCountries()
        {
            // build here
            List<Country> countries = new List<Country>();
            countries.AddRange(GetAllCountry());
            
            List<SelectListItem> countries1 = countries.Select(x => 
                new SelectListItem
                {
                    Value = x.Iso3.ToString(),
                    Text = x.CountryNameEnglish.ToString()
                }).ToList();


            var countrytip = new SelectListItem()
            {
                Value = null,
                Text = "--- select country ---"
            };
            countries1.Insert(0, countrytip);
            return new SelectList(countries1, "Value", "Text");

        }

        //
        public List<Country> GetAllCountry()
        {
            // "sp_GetAllCountry"
            //List<Country> countries = GetCountries("sp_GetAllCountry", null);

            List<Country> countries = new List<Country>();

            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                string sql_str = "SELECT * FROM tbl_country ";
                sql_str += "ORDER BY CountryNameEnglish";
                
                //using (SqlCommand cmd = new SqlCommand("sp_GetAllCountry", conn)) //sql_str
                using (SqlCommand cmd = new SqlCommand(sql_str, conn)) //sql_str
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dtCountries = new DataTable();
                        dtCountries.Load(dr);

                        foreach (DataRow row in dtCountries.Rows)
                        {
                            countries.Add(new Country
                            {
                                //Id = Convert.ToInt32(row["Id"]),
                                //Price = Convert.ToDecimal(row["Price"]),
                                Iso3 = row["Iso3"].ToString(),
                                CountryNameEnglish = row["CountryNameEnglish"].ToString()
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

            return countries;
        }

        //
        public Country GetCountryByID(string Iso3)
        {
            var _country = new Country();

            if (!String.IsNullOrWhiteSpace(Iso3))
            {
                using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
                {
                    string sql_str = "SELECT * FROM tbl_country ";
                    sql_str += "WHERE Iso3 = '" + Iso3 + "' ";

                    //using (SqlCommand cmd = new SqlCommand("sp_GetCountryByID", conn)) //sql_str
                    using (SqlCommand cmd = new SqlCommand(sql_str, conn)) //sql_str
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@Iso3", Iso3);

                        if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                        try
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            DataTable dtCountry = new DataTable();
                            if (dr.HasRows)
                            {
                                dtCountry.Load(dr);

                                DataRow row = dtCountry.Rows[0];
                                //_region.Id = Convert.ToInt32(row["Id"]);
                                _country.Iso3 = row["Iso3"].ToString();
                                _country.CountryNameEnglish = row["CountryNameEnglish"].ToString();

                            }
                            else
                            {
                                return _country; //Empty
                            }

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

                        //
                    }

                }
                //

            }

            return _country;

        }

        //
    }
}
