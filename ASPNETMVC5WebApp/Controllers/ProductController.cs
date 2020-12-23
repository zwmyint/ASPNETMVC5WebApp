using ASPNETMVC5WebApp.Config;
using ASPNETMVC5WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult CreateProduct()
        {
            //
            

            return View();
        }


        // POST: Product
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            //
            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                string sql_str = "INSERT INTO tbl_product (ProductName, Price, Supplier) ";
                sql_str += "VALUES ('" + product.ProductName +"', " + product.Price +", '" + product.Supplier +"')";

                using (SqlCommand cmd = new SqlCommand(sql_str, conn))
                {
                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    cmd.Connection = conn;
                    cmd.Transaction = sqlTransaction;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        //
                        sqlTransaction.Rollback();
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                    //
                }

            }

            //return View();
            return Content("Record is Saved in the Product Table.");
        }



        // GET: Product List
        public ActionResult GetAllProduct()
        {
            //
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                string sql_str = "SELECT * FROM tbl_product ORDER BY Id DESC";


                using (SqlCommand cmd = new SqlCommand(sql_str, conn))
                {
                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dtProducts = new DataTable();
                        dtProducts.Load(dr);

                        foreach(DataRow row in dtProducts.Rows)
                        {
                            products.Add(new Product
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    ProductName = row["ProductName"].ToString(),
                                    Price = Convert.ToDecimal(row["Price"]),
                                    Supplier = row["Supplier"].ToString()
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

            return View(products);
        }

        //

    }
}