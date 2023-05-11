using AddressBook_Replica.Areas.LOC_City.Models;
using AddressBook_Replica.Areas.LOC_State.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
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

            return View("Index", dal.LOC_City_SelectAll(connectionString));
        }

        #endregion

        #region DELETE

        public IActionResult Delete(int CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.LOC_City_Delete(connectionString, CityID))
            {
                TempData["LOC_City_Delete_Msg"] = "City Deleted Successfully.";
            }
            else
            {
                TempData["LOC_City_Delete_Msg"] = "Error in City Deletion.";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.CountryList = dal.LOC_Country_DropDown(connectionString);

            ViewBag.StateList = new List<LOC_State_DropDownModel>();

            // Fetch Tuple

            if (CityID != null)
            {
                LOC_CityModel cityModel = dal.LOC_City_SelectByPk(connectionString, CityID);

                LOC_State_DropDownByCountry(cityModel.CountryID);

                return View("LOC_CityAddEdit", cityModel);
            }
            return View("LOC_CityAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(LOC_CityModel CityModel)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (CityModel.CityID == null)
            {
                if (dal.LOC_City_Insert(connectionString, CityModel))
                {
                    TempData["LOC_City_Insert_Msg"] = "City Inserted Successfully.";
                }
                else
                {
                    TempData["LOC_City_Insert_Msg"] = "Error in City Insertion.";
                }
            }
            else
            {
                if (dal.LOC_City_Update(connectionString, CityModel))
                {
                    TempData["LOC_City_Update_Msg"] = "City Updated Successfully.";
                }
                else
                {
                    TempData["LOC_City_Update_Msg"] = "Error in City Updation.";
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

        #region SEARCH_BOX

        public IActionResult Search()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            LOC_City_SearchModel city_SearchModel = new LOC_City_SearchModel();

            city_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            city_SearchModel.StateName = HttpContext.Request.Form["StateName"].ToString();
            city_SearchModel.CityName = HttpContext.Request.Form["CityName"].ToString();

            ViewBag.CountryName = city_SearchModel.CountryName;
            ViewBag.StateName = city_SearchModel.StateName;
            ViewBag.CityName = city_SearchModel.CityName;

            return View("Index", dal.LOC_City_Search(connectionString, city_SearchModel));
        }

        #endregion

    }
}
