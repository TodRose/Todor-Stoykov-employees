using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodorStoykovEmployees.Models;
using TodorStoykovEmployees.Business;


namespace TodorStoykovEmployees.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly ILogger<HomeController> _logger;

        #endregion

        #region Constructors

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {            
            HomeModel model = new HomeModel();

            model.UploadDateFile = new UploadDataFileModel();
            

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Post request methods

        /// <summary>
        /// Reading the Posted file with the employees project data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFiles(UploadDataFileModel model)
        {

            HomeModel ModelForHomeView = new HomeModel();
            ModelForHomeView.UploadDateFile = model;
            ModelForHomeView.EmployeesProjectsGridData = new EmployeesProjectsGridModel();
                        

            try
            {
                // Collection To Get All The Emplyees Project Data from the Data Source file
                EmployeeProjectCollection AllEmplyeesProjectFromTheFile = new EmployeeProjectCollection();

                foreach (var formFile in model.input_file_data)
                {            
                    //Providing the Stream from the upload file to the Bind methods to extract the data for the employees and projects
                    AllEmplyeesProjectFromTheFile.GetAllEmployeesProjectsFromStream(new StreamReader(formFile.OpenReadStream()), ",", model.input_date_format);
                }


                List<EmployeeProject> AllParsedEmployeesProjects = AllEmplyeesProjectFromTheFile.AllEmployeesProjectsData;

                int EmployeeIdPairOne = 0;
                int EmployeeIdPairTwo = 0;
                int ProjectIdWorkedtogether = 0;
                int MaxDaysWoredTogether = 0;

                bool FoundIt = AllEmplyeesProjectFromTheFile.FindThePairOfEmployeesWorkingLongest(ref EmployeeIdPairOne, ref EmployeeIdPairTwo, ref ProjectIdWorkedtogether, ref MaxDaysWoredTogether);

                if(FoundIt)
                {
                    ModelForHomeView.EmployeesProjectsGridData.EmployeeIdPairOne = EmployeeIdPairOne;
                    ModelForHomeView.EmployeesProjectsGridData.EmployeeIdPairTwo = EmployeeIdPairTwo;
                    ModelForHomeView.EmployeesProjectsGridData.ProjectIdWorkedtogether = ProjectIdWorkedtogether;
                    ModelForHomeView.EmployeesProjectsGridData.MaxDaysWoredTogether = MaxDaysWoredTogether;
                }
            }

            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }             



            return View("Index", ModelForHomeView);
        }


        #endregion 
    }
}
