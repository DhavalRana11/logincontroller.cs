using QuickShoap.Helper;
using QuickShoap.Infrastructure;
using QuickShoap.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickShoap.Controllers
{
    public class LoginController : Infrastructure.BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            LogInModel objLogInModel = new LogInModel();
            return View(objLogInModel);
        }

        /// <summary>
        /// Post Login Submit
        /// </summary>
        /// <param name="model"></param>
        /// <param name="formdata"></param>
        /// <param name="txtEmail"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(LogInModel model, FormCollection formdata, string txtEmail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataTable dtUserLogin = DbAccess.User.SelectLoginUser(model.strLoginName, model.strPassword, model.strCompanyCode).Tables[0];


                    if (dtUserLogin.Rows.Count > 0)
                    {
                        ProjectSession.intUserID = Convert.ToInt32(dtUserLogin.Rows[0]["Userid"]);
                        ProjectSession.intUserType = Convert.ToInt32(dtUserLogin.Rows[0]["UserType"]);
                      //  ProjectSession.strUserName = Convert.ToString(dtUserLogin.Rows[0]["Login_Name"]);
                        return RedirectToAction("Index", "ProductCategory");
                    }
                    else {
                        this.ShowMessage(MessageExtensions.MessageType.Error, "In Valid User Name and password", false);
                    }
                }
                else {
                    this.ShowMessage(MessageExtensions.MessageType.Warning, "Please enter valid parameters", false);
                }
            }
            catch (Exception ex)
            {


            }
            return View(model);
        }


        /// <summary>
        /// /user/logoff
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOff()
        {
            try
            {
                ProjectSession.ClearSession();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}