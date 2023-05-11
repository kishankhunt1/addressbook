using AddressBook_Replica.Areas.LOC_State.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
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

            return View("Index", dal.LOC_State_SelectAll(connectionString));
        }

        #endregion

        #region DELETE

        public IActionResult Delete(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.LOC_State_Delete(connectionString, StateID))
            {
                TempData["LOC_State_Delete_Msg"] = "State Deleted Successfully.";
            }
            else
            {
                TempData["LOC_State_Delete_Msg"] = "Error in State Deletion.";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.CountryList = dal.LOC_Country_DropDown(connectionString);

            if (StateID != null)
            {
                return View("LOC_StateAddEdit", dal.LOC_State_SelectByPk(connectionString, StateID));
            }
            return View("LOC_StateAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(LOC_StateModel stateModel)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (stateModel.StateID == null)
            {
                if (dal.LOC_State_Insert(connectionString, stateModel))
                {
                    TempData["LOC_State_Insert_Msg"] = "State Inserted Successfully.";
                }
                else
                {
                    TempData["LOC_State_Insert_Msg"] = "Error in State Insertion.";
                }
            }
            else
            {
                if (dal.LOC_State_Update(connectionString, stateModel))
                {
                    TempData["LOC_State_Update_Msg"] = "State Updated Successfully.";
                }
                else
                {
                    TempData["LOC_State_Update_Msg"] = "Error in State Updation.";
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

            LOC_State_SearchModel state_SearchModel = new LOC_State_SearchModel();

            state_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            state_SearchModel.StateName = HttpContext.Request.Form["StateName"].ToString();

            ViewBag.CountryName = state_SearchModel.CountryName;
            ViewBag.StateName = state_SearchModel.StateName;

            return View("Index", dal.LOC_State_Search(connectionString, state_SearchModel));
        }

        #endregion

    }
}
