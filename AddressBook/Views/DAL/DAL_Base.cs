using AddressBook_Replica.Areas.CON_Contact.Models;
using AddressBook_Replica.Areas.CON_ContactCategory.Models;
using AddressBook_Replica.Areas.LOC_City.Models;
using AddressBook_Replica.Areas.LOC_Country.Models;
using AddressBook_Replica.Areas.LOC_State.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AddressBook_Replica.Views.DAL
{
    public class DAL_Base
    {
        #region Country

        #region Select_All

        public DataTable LOC_Country_SelectAll(string connectionString)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_Country_SelectAll]");

                DataTable dt = new DataTable();

                using (IDataReader dr = database.ExecuteReader(command))
                {
                    dt.Load(dr);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region SELECT_BY_PK

        public LOC_CountryModel LOC_Country_SelectByPK(string connectionString, int? CountryID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_Country_SelectByPK]");
                database.AddInParameter(command, "@CountryID", DbType.Int32, CountryID);

                DataTable dt = new DataTable();
                LOC_CountryModel countryModel = new LOC_CountryModel();

                using(IDataReader dataReader= database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if(dt.Rows.Count > 0)
                    {
                        foreach(DataRow row in dt.Rows)
                        {
                            countryModel.CountryName = Convert.ToString(row["CountryName"]);
                            countryModel.CountryCode = Convert.ToString(row["CountryCode"]); 
                        }
                    }
                }

                return countryModel;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region INSERT

        public bool LOC_Country_Insert(string connectionString, LOC_CountryModel countryModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_Country_Insert]");
                database.AddInParameter(command, "@CountryName", DbType.String, countryModel.CountryName);
                database.AddInParameter(command, "@CountryCode", DbType.String, countryModel.CountryCode);
                database.AddInParameter(command, "@CreationDate", DbType.DateTime, DBNull.Value);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region DELETE

        public bool LOC_Country_Delete(string connectionString, int CountryID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_Country_DeleteByPK]");
                database.AddInParameter(command, "@CountryID", DbType.Int32, CountryID);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region UPDATE

        public bool LOC_Country_Update(string connectionString,LOC_CountryModel countryModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_Country_UpdateByPK]");

                database.AddInParameter(command, "@CountryID", DbType.Int32, countryModel.CountryID);
                database.AddInParameter(command, "@CountryName", DbType.String, countryModel.CountryName);
                database.AddInParameter(command, "@CountryCode", DbType.String, countryModel.CountryCode);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region SEARCH

        public DataTable LOC_Country_Search(string connectionString , LOC_Country_SearchModel country_SearchModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_Country_SelectPage]");
                database.AddInParameter(command, "@CountryName", DbType.String, country_SearchModel.CountryName);
                database.AddInParameter(command, "@CountryCode", DbType.String, country_SearchModel.CountryCode);

                DataTable dt = new DataTable();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #endregion

        #region STATE

        #region SELECT_ALL

        public DataTable LOC_State_SelectAll(string connectionString)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_State_SelectAll]");

                DataTable dt = new DataTable();
                using (IDataReader dr = database.ExecuteReader(command)){
                    dt.Load(dr);
                }
                return dt;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region SELECT_BY_PK

        public LOC_StateModel LOC_State_SelectByPk(string connectionString, int? StateID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_State_SelectByPK]");

                database.AddInParameter(command, "@StateID", DbType.Int32, StateID);

                DataTable dt = new DataTable();
                LOC_StateModel stateModel = new LOC_StateModel();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dt.Rows)
                        {
                            stateModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                            stateModel.StateName = Convert.ToString(dr["StateName"]);
                        }
                    }
                }
                return stateModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region DELETE

        public bool LOC_State_Delete(string connectionString, int StateID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_State_DeleteByPK]");

                database.AddInParameter(command, "@StateID", DbType.Int32, StateID);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region INSERT

        public bool LOC_State_Insert(string connectionString, LOC_StateModel stateModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_State_Insert]");

                database.AddInParameter(command, "@StateName", DbType.String, stateModel.StateName);
                database.AddInParameter(command, "@CountryID", DbType.Int32, stateModel.CountryID);
                database.AddInParameter(command, "@CreationDate", DbType.DateTime, DBNull.Value);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region UPDATE

        public bool LOC_State_Update(string connectionString, LOC_StateModel stateModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_State_UpdateByPK]");
                
                database.AddInParameter(command, "@StateID", DbType.Int32, stateModel.StateID);
                database.AddInParameter(command, "@StateName", DbType.String, stateModel.StateName);
                database.AddInParameter(command, "@CountryID", DbType.String, stateModel.CountryID);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region SEARCH

        public DataTable LOC_State_Search(string connectionString, LOC_State_SearchModel state_SearchModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_State_SelectPage]");
                database.AddInParameter(command, "@CountryName", DbType.String, state_SearchModel.CountryName);
                database.AddInParameter(command, "@StateName", DbType.String, state_SearchModel.StateName);

                DataTable dt = new DataTable();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #endregion

        #region CITY

        #region SELECT_ALL

        public DataTable LOC_City_SelectAll(string connectionString)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_City_SelectAll]");

                DataTable dt = new DataTable();
                using (IDataReader dr = database.ExecuteReader(command))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region SELECT_BY_PK

        public LOC_CityModel LOC_City_SelectByPk(string connectionString, int? CityID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_City_SelectByPK]");

                database.AddInParameter(command, "@CityID", DbType.Int32, CityID);

                DataTable dt = new DataTable();
                LOC_CityModel cityModel = new LOC_CityModel();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            cityModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                            cityModel.StateID = Convert.ToInt32(dr["StateID"]);
                            cityModel.CityName = Convert.ToString(dr["CityName"]);
                        }
                    }
                }
                return cityModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region DELETE

        public bool LOC_City_Delete(string connectionString, int CityID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_City_DeleteByPK]");

                database.AddInParameter(command, "@CityID", DbType.Int32, CityID);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region INSERT

        public bool LOC_City_Insert(string connectionString, LOC_CityModel CityModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_City_Insert]");

                database.AddInParameter(command, "@CityName", DbType.String, CityModel.CityName);
                database.AddInParameter(command, "@CountryID", DbType.Int32, CityModel.CountryID);
                database.AddInParameter(command, "@StateID", DbType.Int32, CityModel.StateID);
                database.AddInParameter(command, "@CreationDate", DbType.DateTime, DBNull.Value);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region UPDATE

        public bool LOC_City_Update(string connectionString, LOC_CityModel CityModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_City_UpdateByPK]");

                database.AddInParameter(command, "@CityID", DbType.Int32, CityModel.CityID);
                database.AddInParameter(command, "@CityName", DbType.String, CityModel.CityName);
                database.AddInParameter(command, "@CountryID", DbType.String, CityModel.CountryID);
                database.AddInParameter(command, "@StateID", DbType.Int32, CityModel.StateID);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region SEARCH

        public DataTable LOC_City_Search(string connectionString, LOC_City_SearchModel city_SearchModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_City_SelectPage]");
                database.AddInParameter(command, "@CountryName", DbType.String, city_SearchModel.CountryName);
                database.AddInParameter(command, "@StateName", DbType.String, city_SearchModel.StateName);
                database.AddInParameter(command, "@CityName", DbType.String, city_SearchModel.CityName);

                DataTable dt = new DataTable();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #endregion

        #region ContactCategory

        #region Select_All

        public DataTable CON_ContactCategory_SelectAll(string connectionString)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_CON_ContactCategory_SelectAll]");

                DataTable dt = new DataTable();

                using (IDataReader dr = database.ExecuteReader(command))
                {
                    dt.Load(dr);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region SELECT_BY_PK

        public CON_ContactCategoryModel CON_ContactCategory_SelectByPK(string connectionString, int? ContactCategoryID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_CON_ContactCategory_SelectByPK]");
                database.AddInParameter(command, "@ContactCategoryID", DbType.Int32, ContactCategoryID);

                DataTable dt = new DataTable();
                CON_ContactCategoryModel contactCategoryModel = new CON_ContactCategoryModel();

                using(IDataReader dataReader= database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if(dt.Rows.Count > 0)
                    {
                        foreach(DataRow row in dt.Rows)
                        {
                            contactCategoryModel.ContactCategory = Convert.ToString(row["ContactCategory"]);
                        }
                    }
                }
                return contactCategoryModel;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region INSERT

        public bool CON_ContactCategory_Insert(string connectionString, CON_ContactCategoryModel ContactCategoryModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_CON_ContactCategory_Insert]");
                database.AddInParameter(command, "@ContactCategory", DbType.String, ContactCategoryModel.ContactCategory);
                database.AddInParameter(command, "@CreationDate", DbType.DateTime, DBNull.Value);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region DELETE

        public bool CON_ContactCategory_Delete(string connectionString, int ContactCategoryID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_CON_ContactCategory_DeleteByPK]");
                database.AddInParameter(command, "@ContactCategoryID", DbType.Int32, ContactCategoryID);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region UPDATE

        public bool CON_ContactCategory_Update(string connectionString,CON_ContactCategoryModel ContactCategoryModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_CON_ContactCategory_UpdateByPK]");

                database.AddInParameter(command, "@ContactCategoryID", DbType.Int32, ContactCategoryModel.ContactCategoryID);
                database.AddInParameter(command, "@ContactCategory", DbType.String, ContactCategoryModel.ContactCategory);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region SEARCH

        public DataTable CON_ContactCategory_Search(string connectionString, CON_ContactCategory_SearchModel contactCategory_SearchModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_CON_ContactCategory_SelectPage]");
                database.AddInParameter(command, "@ContactCategory", DbType.String, contactCategory_SearchModel.ContactCategory);

                DataTable dt = new DataTable();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #endregion

        #region CONTACT

        #region SELECT_ALL

        public DataTable CON_Contact_SelectAll(string connectionString)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_MST_Contact_SelectAll]");

                DataTable dt = new DataTable();
                using (IDataReader dr = database.ExecuteReader(command))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region SELECT_BY_PK

        public CON_ContactModel CON_Contact_SelectByPk(string connectionString, int? ContactID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_MST_Contact_SelectByPK]");

                database.AddInParameter(command, "@ContactID", DbType.Int32, ContactID);

                DataTable dt = new DataTable();
                CON_ContactModel contactModel = new CON_ContactModel();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            contactModel.ContactID = Convert.ToInt32(dr["ContactID"]);
                            contactModel.Name = Convert.ToString(dr["Name"]);
                            contactModel.Mobile = Convert.ToString(dr["Mobile"]);
                            contactModel.Email= Convert.ToString(dr["Email"]);
                            contactModel.Address = Convert.ToString(dr["Address"]);
                            contactModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                            contactModel.StateID = Convert.ToInt32(dr["StateID"]);
                            contactModel.CityID = Convert.ToInt32(dr["CityID"]);
                            contactModel.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                            contactModel.PhotoPath = Convert.ToString(dr["PhotoPath"]);
                        }
                    }
                }
                return contactModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region DELETE

        public bool CON_Contact_Delete(string connectionString, int ContactID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_MST_Contact_DeleteByPK]");
                database.AddInParameter(command, "@ContactID", DbType.Int32, ContactID);

                #region DELETE_FILE_IN_DATABASE

                DbCommand command2 = database.GetStoredProcCommand("[dbo].[PR_MST_Contact_SelectPhotoPathByPK]");
                database.AddInParameter(command2, "@ContactID", DbType.Int32, ContactID);
                DataTable dt = new DataTable();

                string file_name, file_name_with_path;
                string full_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload");

                using (IDataReader reader = database.ExecuteReader(command2))
                {
                    dt.Load(reader);
                    file_name = Convert.ToString(dt.Rows[0][1]);
                    file_name_with_path = Path.Combine(full_path, file_name);
                }

                //Delete File
                if (File.Exists(file_name_with_path))
                {
                    File.Delete(file_name_with_path);
                }

                #endregion

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region INSERT

        public bool CON_Contact_Insert(string connectionString, CON_ContactModel contactModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_MST_Contact_Insert]");

                database.AddInParameter(command, "@Name", DbType.String, contactModel.Name);
                database.AddInParameter(command, "@Mobile", DbType.String, contactModel.Mobile);
                database.AddInParameter(command, "@Email", DbType.String, contactModel.Email);
                database.AddInParameter(command, "@Address", DbType.String, contactModel.Address);
                database.AddInParameter(command, "@CountryID", DbType.Int32, contactModel.CountryID);
                database.AddInParameter(command, "@StateID", DbType.Int32, contactModel.StateID);
                database.AddInParameter(command, "@CityID", DbType.Int32, contactModel.CityID);
                database.AddInParameter(command, "@ContactCategoryID", DbType.Int32, contactModel.ContactCategoryID);
                database.AddInParameter(command, "@CreationDate", DbType.DateTime, DBNull.Value);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                #region FILE_UPLOAD

                if (contactModel.File != null) 
                {
                    string file_loc = "wwwroot\\upload";
                    string full_path = Path.Combine(Directory.GetCurrentDirectory(), file_loc);

                    if (!Directory.Exists(full_path))
                    {
                        Directory.CreateDirectory(full_path);
                    }

                    string file_name_with_path = Path.Combine(full_path, contactModel.File.FileName);
                    //contactModel.PhotoPath = "~" + file_loc.Replace("wwwroot\\", "//") + contactModel.File.FileName;
                    contactModel.PhotoPath = contactModel.File.FileName;

                    database.AddInParameter(command, "@PhotoPath", DbType.String, contactModel.PhotoPath);

                    using(var stream = new FileStream(file_name_with_path, FileMode.Create))
                    {
                        contactModel.File.CopyTo(stream);
                    }
                }

                #endregion

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region UPDATE

        public bool CON_Contact_Update(string connectionString, CON_ContactModel contactModel,string delete_file_name)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_MST_Contact_UpdateByPK]");

                database.AddInParameter(command, "@ContactID", DbType.Int32, contactModel.ContactID);
                database.AddInParameter(command, "@Name", DbType.String, contactModel.Name);
                database.AddInParameter(command, "@Mobile", DbType.String, contactModel.Mobile);
                database.AddInParameter(command, "@Email", DbType.String, contactModel.Email);
                database.AddInParameter(command, "@Address", DbType.String, contactModel.Address);
                database.AddInParameter(command, "@CountryID", DbType.Int32, contactModel.CountryID);
                database.AddInParameter(command, "@StateID", DbType.Int32, contactModel.StateID);
                database.AddInParameter(command, "@CityID", DbType.Int32, contactModel.CityID);
                database.AddInParameter(command, "@ContactCategoryID", DbType.Int32, contactModel.ContactCategoryID);
                database.AddInParameter(command, "@ModificationDate", DbType.DateTime, DBNull.Value);

                #region FILE_UPLOAD

                if (contactModel.File != null)
                {
                    string file_loc = "wwwroot\\upload";
                    string full_path = Path.Combine(Directory.GetCurrentDirectory(), file_loc);

                    if (!Directory.Exists(full_path))
                    {
                        Directory.CreateDirectory(full_path);
                    }

                    string file_name_with_path = Path.Combine(full_path, contactModel.File.FileName);

                    string delete_old_file = Path.Combine(full_path, delete_file_name);
                    if (File.Exists(delete_old_file))
                    {
                        File.Delete(delete_old_file);
                    }

                    //contactModel.PhotoPath = "~" + file_loc.Replace("wwwroot\\", "//") + contactModel.File.FileName;
                    contactModel.PhotoPath = contactModel.File.FileName;
                    database.AddInParameter(command, "@PhotoPath", DbType.String, contactModel.PhotoPath);

                    using (var stream = new FileStream(file_name_with_path, FileMode.Create))
                    {
                        contactModel.File.CopyTo(stream);
                    }
                }

                #endregion

                return Convert.ToBoolean(database.ExecuteNonQuery(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        #endregion

        #region SEARCH

        public DataTable CON_Contact_Search(string connectionString, CON_Contact_SearchModel contact_SearchModel)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_MST_Contact_SelectPage]");
                database.AddInParameter(command, "@CountryName", DbType.String, contact_SearchModel.CountryName);
                database.AddInParameter(command, "@StateName", DbType.String, contact_SearchModel.StateName);
                database.AddInParameter(command, "@CityName", DbType.String, contact_SearchModel.CityName);
                database.AddInParameter(command, "@ContactCategory", DbType.String, contact_SearchModel.Category);
                database.AddInParameter(command, "@Name", DbType.String, contact_SearchModel.Name);
                database.AddInParameter(command, "@Email", DbType.String, contact_SearchModel.Email);
                database.AddInParameter(command, "@Mobile", DbType.String, contact_SearchModel.Mobile);

                DataTable dt = new DataTable();

                using(IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);
                }
                return dt;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #endregion
    }
}