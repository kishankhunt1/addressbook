using AddressBook_Replica.Areas.LOC_Country.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class HomeController : Controller
    {
        #region PRIVATE_VAR

        private IConfiguration Configuration;

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

            return View("Index", dal.LOC_Country_SelectAll(connectionString));
        }

        #endregion

        #region DELETE_BY_PK

        public IActionResult Delete(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.LOC_Country_Delete(connectionString, CountryID))
            {
                TempData["LOC_Country_Delete_Msg"] = "Country Deleted Successfully.";
            }
            else
            {
                TempData["LOC_Country_Delete_Msg"] = "Error in Country Deletion.";
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? CountryID)
        {
            if (CountryID != null)
            {
                string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
                DAL dal = new DAL();

                return View("LOC_CountryAddEdit", dal.LOC_Country_SelectByPK(connectionString, CountryID));
            }
            return View("LOC_CountryAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(LOC_CountryModel countryModel)
        {

            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (countryModel.CountryID == null)
            {
                if (dal.LOC_Country_Insert(connectionString, countryModel))
                {
                    TempData["LOC_Country_Insert_Msg"] = "Country Inserted Successfully.";
                }
                else
                {
                    TempData["LOC_Country_Insert_Msg"] = "Error in Country Insertion.";
                }
            }
            else
            {
                if (dal.LOC_Country_Update(connectionString, countryModel))
                {
                    TempData["LOC_Country_Update_Msg"] = "Country Updated Successfully.";
                }
                else
                {
                    TempData["LOC_Country_Update_Msg"] = "Error in Country Updation.";
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Add");
        }

        #endregion

        #region SEARCH_BOX

        public IActionResult Search()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            LOC_Country_SearchModel country_SearchModel = new LOC_Country_SearchModel();

            country_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            country_SearchModel.CountryCode = HttpContext.Request.Form["CountryCode"].ToString();

            ViewBag.CountryName = country_SearchModel.CountryName;
            ViewBag.CountryCode = country_SearchModel.CountryCode;

            return View("Index", dal.LOC_Country_Search(connectionString, country_SearchModel));
        }

        #endregion

    }
}
