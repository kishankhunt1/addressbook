using AddressBook_Replica.Areas.CON_ContactCategory.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Areas.CON_ContactCategory.Controllers
{
    [Area("CON_ContactCategory")]
    [Route("CON_ContactCategory/[controller]/[action]")]
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

            return View("Index", dal.CON_ContactCategory_SelectAll(connectionString));
        }

        #endregion

        #region DELETE_BY_PK

        public IActionResult Delete(int ContactCategoryID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.CON_ContactCategory_Delete(connectionString, ContactCategoryID))
            {
                TempData["CON_ContactCategory_Delete_Msg"] = "Contact Category Deleted Successfully.";
            }
            else
            {
                TempData["CON_ContactCategory_Delete_Msg"] = "Error in Contact Category Deletion.";
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? ContactCategoryID)
        {
            if (ContactCategoryID != null)
            {
                string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
                DAL dal = new DAL();

                return View("CON_ContactCategoryAddEdit", dal.CON_ContactCategory_SelectByPK(connectionString, ContactCategoryID));
            }
            return View("CON_ContactCategoryAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(CON_ContactCategoryModel ContactCategoryModel)
        {

            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (ContactCategoryModel.ContactCategoryID == null)
            {
                if (dal.CON_ContactCategory_Insert(connectionString, ContactCategoryModel))
                {
                    TempData["CON_ContactCategory_Insert_Msg"] = "Contact Category Inserted Successfully.";
                }
                else
                {
                    TempData["CON_ContactCategory_Insert_Msg"] = "Error in Contact Category Insertion.";
                }
            }
            else
            {
                if (dal.CON_ContactCategory_Update(connectionString, ContactCategoryModel))
                {
                    TempData["CON_ContactCategory_Update_Msg"] = "Contact Category Updated Successfully.";
                }
                else
                {
                    TempData["CON_ContactCategory_Update_Msg"] = "Error in Contact Category Updation.";
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

            CON_ContactCategory_SearchModel contactCategory_SearchModel = new CON_ContactCategory_SearchModel();

            contactCategory_SearchModel.ContactCategory = HttpContext.Request.Form["ContactCategory"].ToString();

            ViewBag.ContactCategory = contactCategory_SearchModel.ContactCategory;

            return View("Index", dal.CON_ContactCategory_Search(connectionString, contactCategory_SearchModel));
        }

        #endregion
    }
}

