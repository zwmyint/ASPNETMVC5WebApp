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
    public class RegionsRepository
    {
        //
        public IEnumerable<SelectListItem> GetRegions()
        {
            List<SelectListItem> regions = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = "--- select region ---"
                }
            };
            return regions;
        }

        // for Dropdown
        public IEnumerable<SelectListItem> GetRegions(string Iso3)
        {
            if (!String.IsNullOrWhiteSpace(Iso3))
            {
                // build here
                List<Region> regions = new List<Region>();
                regions.AddRange(GetAllRegion(Iso3));

                List<SelectListItem> regions1 = regions.Select(x => 
                    new SelectListItem
                    {
                        Value = x.RegionCode.ToString(),
                        Text = x.RegionNameEnglish.ToString()
                    }).ToList();

                var regintip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select region ---"
                };
                regions1.Insert(0, regintip);
                return new SelectList(regions1, "Value", "Text");
            }
            else
            {
                return null;
            }
            
        }
        
        //
        public List<Region> GetAllRegion(string Iso3)
        {
            // "sp_GetAllRegion"
            //List<Region> regions = GetRegions("sp_GetAllRegion", null);

            List<Region> regions = new List<Region>();

            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                string sql_str = "SELECT * FROM tbl_region ";
                if (Iso3 != "")
                {
                    sql_str += "WHERE Iso3 = '" + Iso3 + "' ";
                    sql_str += "ORDER BY RegionNameEnglish";
                }
                else
                {
                    sql_str += "ORDER BY Iso3, RegionNameEnglish";
                }

                //using (SqlCommand cmd = new SqlCommand("sp_GetAllRegion", conn)) //sql_str
                using (SqlCommand cmd = new SqlCommand(sql_str, conn)) //sql_str
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dtRegions = new DataTable();
                        dtRegions.Load(dr);

                        foreach (DataRow row in dtRegions.Rows)
                        {
                            regions.Add(new Region
                            {
                                //Id = Convert.ToInt32(row["Id"]),
                                //Price = Convert.ToDecimal(row["Price"]),
                                RegionCode = row["RegionCode"].ToString(),
                                Iso3 = row["Iso3"].ToString(),
                                RegionNameEnglish = row["RegionNameEnglish"].ToString()
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

            return regions;
        }

        //
        public Region GetRegionByID(string Iso3, string RegionCode)
        {
            var _region = new Region();

            if (!String.IsNullOrWhiteSpace(Iso3) && !String.IsNullOrWhiteSpace(RegionCode))
            {
                using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
                {
                    string sql_str = "SELECT * FROM tbl_region WHERE RegionCode = '" + RegionCode + "' AND ";
                    sql_str += "Iso3 = '" + Iso3 + "' ";
                    sql_str += "ORDER BY Iso3, RegionNameEnglish ";

                    //using (SqlCommand cmd = new SqlCommand("sp_GetRegionByID", conn)) //sql_str
                    using (SqlCommand cmd = new SqlCommand(sql_str, conn)) //sql_str
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@Iso3", Iso3);
                        //cmd.Parameters.AddWithValue("@RegionCode", RegionCode);

                        if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                        try
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            DataTable dtRegion = new DataTable();
                            if (dr.HasRows)
                            {
                                dtRegion.Load(dr);

                                DataRow row = dtRegion.Rows[0];
                                //_region.Id = Convert.ToInt32(row["Id"]);
                                _region.RegionCode = row["RegionCode"].ToString();
                                _region.Iso3 = row["Iso3"].ToString();
                                _region.RegionNameEnglish = row["RegionNameEnglish"].ToString();

                            }
                            else
                            {
                                return _region; //Empty
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

            return _region;

        }


        //
    }
}