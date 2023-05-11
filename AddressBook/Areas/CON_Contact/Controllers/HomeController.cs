using AddressBook_Replica.Areas.CON_Contact.Models;
using AddressBook_Replica.Areas.LOC_City.Models;
using AddressBook_Replica.Areas.LOC_State.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Areas.CON_Contact.Controllers
{
    [Area("CON_Contact")]
    [Route("CON_Contact/[controller]/[action]")]
    public class HomeController : Controller
    {
        #region PRIVATE_VAR

        private IConfiguration Configuration;
        private static string file_name = "";

        #endregion

        #region DAL_Object

        DAL dal = new DAL();

        #endregion

        #region CONSTRUCTOR

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region SELECT_ALL

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            return View("Index", dal.CON_Contact_SelectAll(connectionString));
        }

        #endregion

        #region DELETE

        public IActionResult Delete(int ContactID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.CON_Contact_Delete(connectionString, ContactID))
            {
                TempData["CON_Contact_Delete_Msg"] = "Contact Deleted Successfully.";
            }
            else
            {
                TempData["CON_Contact_Delete_Msg"] = "Error in Contact Deletion.";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? ContactID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            //DAL dal = new DAL();

            ViewBag.CountryList = dal.LOC_Country_DropDown(connectionString);

            ViewBag.StateList = new List<LOC_State_DropDownModel>();

            ViewBag.CityList = new List<LOC_City_DropDownModel>();

            ViewBag.ContactCategoryList = dal.CON_ContactCategory_DropDown(connectionString);

            // Fetch Tuple

            if (ContactID != null)
            {
                CON_ContactModel contactModel = dal.CON_Contact_SelectByPk(connectionString, ContactID);
                file_name = contactModel.PhotoPath.ToString();

                LOC_State_DropDownByCountry(contactModel.CountryID);

                LOC_City_DropDownByState(contactModel.StateID);

                return View("CON_ContactAddEdit", contactModel);
            }
            return View("CON_ContactAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(CON_ContactModel contactModel)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (contactModel.ContactID == null)
            {
                if (dal.CON_Contact_Insert(connectionString, contactModel))
                {
                    TempData["CON_Contact_Insert_Msg"] = "Contact Inserted Successfully.";
                }
                else
                {
                    TempData["CON_Contact_Insert_Msg"] = "Error in Contact Insertion.";
                }
            }
            else
            {
                if (dal.CON_Contact_Update(connectionString, contactModel, file_name))
                {
                    TempData["CON_Contact_Update_Msg"] = "Contact Updated Successfully.";
                }
                else
                {
                    TempData["CON_Contact_Update_Msg"] = "Error in Contact Updation.";
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Add");
        }

        #endregion

        #region STATE_DROPDOWN

        public IActionResult LOC_State_DropDownByCountry(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.StateList = dal.LOC_State_DropDown(connectionString, CountryID);

            var state_model = ViewBag.StateList;
            return Json(state_model);
        }

        #endregion

        #region CITY_DROPDOWN

        public IActionResult LOC_City_DropDownByState(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.CityList = dal.LOC_City_DropDown(connectionString, StateID);

            var city_model = ViewBag.CityList;
            return Json(city_model);
        }

        #endregion

        #region SEARCH_BOX

        public IActionResult Search()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            CON_Contact_SearchModel contact_SearchModel = new CON_Contact_SearchModel();

            contact_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            contact_SearchModel.StateName = HttpContext.Request.Form["StateName"].ToString();
            contact_SearchModel.CityName = HttpContext.Request.Form["CityName"].ToString();
            contact_SearchModel.Category = HttpContext.Request.Form["ContactCategory"].ToString();
            contact_SearchModel.Name = HttpContext.Request.Form["Name"].ToString();
            contact_SearchModel.Email = HttpContext.Request.Form["Email"].ToString();
            contact_SearchModel.Mobile = HttpContext.Request.Form["Mobile"].ToString();

            ViewBag.CountryName = contact_SearchModel.CountryName;
            ViewBag.StateName = contact_SearchModel.StateName;
            ViewBag.CityName = contact_SearchModel.CityName;
            ViewBag.Name = contact_SearchModel.Name;
            ViewBag.Email = contact_SearchModel.Email;
            ViewBag.ContactCategory = contact_SearchModel.Category;

            return View("Index", dal.CON_Contact_Search(connectionString, contact_SearchModel));
        }

        #endregion

    }
}

