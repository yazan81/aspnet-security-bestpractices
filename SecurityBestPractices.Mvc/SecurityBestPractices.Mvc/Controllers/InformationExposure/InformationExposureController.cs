﻿using DevExpress.DashboardWeb;
using SecurityBestPractices.Mvc.Models;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml.Linq;

namespace SecurityBestPractices.Mvc.Controllers {
    public class InformationExposureController : Controller {
        public const string UpdateStatusKey = "UpdateStatusKey";
        /* EditForm */

        [HttpGet]
        public ActionResult FormWithErrorMessage() {
            return View(new EditFormItem() { Id = 1, ProductName = "Test 123" });
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult FormWithErrorMessage(EditFormItem item) {
            if(ModelState.IsValid) {
                try {
                    // DoSomething()
                    throw new InvalidOperationException("Some sensitive information");
                } catch(Exception ex) {
                    // Unsafe approach - showing an Exception text
                    // ViewData[UpdateStatusKey] = ex.Message;

                    // Safe approach - showing text without sensitive information
                    if(ex is InvalidOperationException) {
                        ViewData[UpdateStatusKey] = "Some error occured...";
                    } else {
                        ViewData[UpdateStatusKey] = "General error occured...";
                    }
                }
            } else
                ViewData[UpdateStatusKey] = "Please, correct all errors.";

            return View(item);
        }

        
    }
}