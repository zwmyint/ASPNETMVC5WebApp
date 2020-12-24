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
            return View( new Product { Id = 0 });
        }

        // GET: Product List
        public ActionResult GetAllProduct()
        {
            // "sp_GetAllProduct"
            List<Product> products = GetProducts("sp_GetAllProduct", null);

            //List<Product> products = new List<Product>();

            //using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            //{
            //    //string sql_str = "SELECT * FROM tbl_product ORDER BY Id DESC";

            //    using (SqlCommand cmd = new SqlCommand("sp_GetAllProduct", conn)) //sql_str
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        if (conn.State != System.Data.ConnectionState.Open) conn.Open();

            //        try
            //        {
            //            SqlDataReader dr = cmd.ExecuteReader();
            //            DataTable dtProducts = new DataTable();
            //            dtProducts.Load(dr);

            //            foreach (DataRow row in dtProducts.Rows)
            //            {
            //                products.Add(new Product
            //                {
            //                    Id = Convert.ToInt32(row["Id"]),
            //                    ProductName = row["ProductName"].ToString(),
            //                    Price = Convert.ToDecimal(row["Price"]),
            //                    Supplier = row["Supplier"].ToString()
            //                });
            //            }
            //            //
            //        }
            //        catch (Exception ex)
            //        {
            //            //

            //        }
            //        finally
            //        {
            //            conn.Close();
            //            conn.Dispose();
            //        }
            
            //    }

            //}

            return View(products);
        }

        // POST: Product
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            //
            if (ModelState.IsValid)
            {
                //
                //string sql_str_insert = "INSERT INTO tbl_product (ProductName, Price, Supplier) ";
                //sql_str_insert += "VALUES ('" + product.ProductName + "', " + product.Price + ", '" + product.Supplier + "')";

                //string sql_str_update = "UPDATE tbl_product SET ProductName = '" + product.ProductName + "', ";
                //sql_str_update += "Price = " + product.Price + ", Supplier = '" + product.Supplier + "' ";
                //sql_str_update += "WHERE Id = " + product.Id + " ";

                //string sql_str = "";

                using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
                {

                    //using (SqlCommand cmd = new SqlCommand((product.Id > 0) ? sql_str_update : sql_str_insert, conn))
                    using (SqlCommand cmd = new SqlCommand("sp_InsertOrUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", product.Id);
                        cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Supplier", product.Supplier);

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
                //return Content("Record is Saved in the Product Table.");
                return RedirectToAction("GetAllProduct");
                //
            }

            return View("CreateProduct");
            //
            
        }


        // Edit Product Action
        public ActionResult Edit(int? id)
        {
            var _product = new Product();

            if (id == null) return HttpNotFound();

            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                //string sql_str = "SELECT * FROM tbl_product WHERE Id = " + id + "";

                using (SqlCommand cmd = new SqlCommand("sp_GetProductByID", conn)) //sql_str
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dtProduct = new DataTable();
                        if (dr.HasRows)
                        {
                            dtProduct.Load(dr);

                            DataRow row = dtProduct.Rows[0];
                            _product.Id = Convert.ToInt32(row["Id"]);
                            _product.ProductName = row["ProductName"].ToString();
                            _product.Price = Convert.ToDecimal(row["Price"]);
                            _product.Supplier = row["Supplier"].ToString();
                            
                        }
                        else
                        {
                            return HttpNotFound();
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

            return View("CreateProduct", _product);

            //
        }


        // Delete Product Action
        public ActionResult Delete(int? id)
        {
            if (id == null) return HttpNotFound();

            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                //string sql_str = "DELETE FROM tbl_product WHERE Id = " + id + "";

                using (SqlCommand cmd = new SqlCommand("sp_DeleteProductByID", conn)) //sql_str
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                    
                    try
                    {
                        cmd.ExecuteNonQuery();
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

            return RedirectToAction("GetAllProduct");
            //
        }

        public ActionResult SearchProduct(string searchkeyword)
        {
            // "sp_GetAllProduct"
            List<Product> products = GetProducts("sp_SearchProductByName", searchkeyword);
            return View("GetAllProduct", products);
        }


        // used from GetAllProduct and SearchProduct
        public List<Product> GetProducts(string sp_name, string searchkeyword)
        {
            //
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                //string sql_str = "SELECT * FROM tbl_product ORDER BY Id DESC";

                using (SqlCommand cmd = new SqlCommand(sp_name, conn)) //sql_str
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (searchkeyword != null)
                    {
                        cmd.Parameters.AddWithValue("@Filter", searchkeyword);
                    }

                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dtProducts = new DataTable();
                        dtProducts.Load(dr);

                        foreach (DataRow row in dtProducts.Rows)
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

            return products;
        }


        //

    }
}


//CREATE PROCEDURE sp_GetAllProduct
//AS
//BEGIN
//    SELECT* FROM tbl_product ORDER BY Id DESC
//END
//GO


//CREATE PROCEDURE sp_DeleteProductByID
//@Id Int
//AS
//BEGIN
//    DELETE FROM tbl_product WHERE Id = @Id
//END
//GO


//CREATE PROCEDURE sp_InsertOrUpdate
//@Id int,
//	@ProductName nvarchar(150),
//	@Price decimal (18,2),
//	@Supplier nvarchar(50)
//AS
//BEGIN
//    IF @Id > 0
//		BEGIN
//			-- Update
//            UPDATE tbl_product SET
//                ProductName = @ProductName
//				, Price = @Price
//				, Supplier = @Supplier
//            WHERE Id = @Id
//        END
//    ELSE
//        BEGIN
//			-- Insert
//            INSERT INTO tbl_product(
//                ProductName
//                , Price
//                , Supplier
//            )
//            VALUES(
//                @ProductName
//                , @Price
//                , @Supplier
//            )
//        END
//END
//GO


//CREATE PROCEDURE sp_GetProductByID
//@Id int
//AS
//BEGIN
//    SELECT* FROM tbl_product WHERE Id = @Id
//END
//GO


//CREATE PROCEDURE sp_SearchProductByName
//@Filter nvarchar(50)
//AS
//BEGIN
// --SELECT* FROM tbl_product WHERE ProductName = @Filter
// SELECT* FROM tbl_product
// WHERE ProductName LIKE '%' + @Filter + '%' 
// ORDER BY Id DESC
//END
//GO