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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult CreateCategory()
        {
            //

            //Creating generic list
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Status 1", Value = "1" },
                new SelectListItem { Text = "Status 2", Value = "2" }

            };
            //Assigning generic list to ViewBag
            ViewBag.CategoryStatus = ObjList;


            return View(new Category { Id = 0 });
        }

        // GET: Category List
        public ActionResult GetAllCategory()
        {
            // "sp_GetAllCategory"
            List<Category> categories = GetCategories("sp_GetAllCategory", null);

            //List<Category> categories = new List<Category>();

            //using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            //{
            //    //string sql_str = "SELECT * FROM tbl_category ORDER BY Id DESC";

            //    using (SqlCommand cmd = new SqlCommand("sp_GetAllCategory", conn)) //sql_str
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        if (conn.State != System.Data.ConnectionState.Open) conn.Open();

            //        try
            //        {
            //            SqlDataReader dr = cmd.ExecuteReader();
            //            DataTable dtCategories = new DataTable();
            //            dtCategories.Load(dr);

            //            foreach (DataRow row in dtCategories.Rows)
            //            {
            //                categories.Add(new Category
            //                {
            //                    Id = Convert.ToInt32(row["Id"]),
            //                    CategoryName = row["CategoryName"].ToString(),
            //                    Status = Convert.ToInt32(row["Status"])
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

            return View(categories);
        }




        // used from GetAllCategory and SearchCategory
        public List<Category> GetCategories(string sp_name, string searchkeyword)
        {
            //
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(Config_Connection.GetConnection()))
            {
                //string sql_str = "SELECT * FROM tbl_category ORDER BY Id DESC";

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
                            categories.Add(new Category
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                CategoryName = row["CategoryName"].ToString(),
                                Status = Convert.ToInt32(row["Status"])
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

            return categories;
        }

        //
    }
}


//CREATE PROCEDURE sp_GetAllCategory
//AS
//BEGIN
//    SELECT* FROM tbl_category ORDER BY Id DESC
//END
//GO


//CREATE PROCEDURE sp_DeleteCategoryByID
//@Id Int
//AS
//BEGIN
//    DELETE FROM tbl_category WHERE Id = @Id
//END
//GO


//CREATE PROCEDURE sp_InsertOrUpdateCategory
//  @Id int,
//	@CategoryName nvarchar(50),
//	@Status int
//AS
//BEGIN
//    IF @Id > 0
//		BEGIN
//			-- Update
//            UPDATE tbl_category SET
//                CategoryName = @CategoryName
//				, Status = @Status
//            WHERE Id = @Id
//        END
//    ELSE
//        BEGIN
//			-- Insert
//            INSERT INTO tbl_category(
//                CategoryName
//                , Status
//            )
//            VALUES(
//                @CategoryName
//                , @Status
//            )
//        END
//END
//GO


//CREATE PROCEDURE sp_GetCategoryByID
//@Id int
//AS
//BEGIN
//    SELECT* FROM tbl_category WHERE Id = @Id
//END
//GO


//CREATE PROCEDURE sp_SearchCategoryByName
//@Filter nvarchar(50)
//AS
//BEGIN
// --SELECT* FROM tbl_category WHERE CategoryName = @Filter
// SELECT* FROM tbl_category
// WHERE CategoryName LIKE '%' + @Filter + '%' 
// ORDER BY Id DESC
//END
//GO