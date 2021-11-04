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

                var AllEmployeesProject = (from emp_pro1 in AllParsedEmployeesProjects
                                          join emp_pro2 in AllParsedEmployeesProjects
                                          on emp_pro1.ProjectId equals emp_pro2.ProjectId
                                          where emp_pro1.EmployeeId != emp_pro2.EmployeeId
                                          select new
                                          {
                                              EmployeedId1 = (emp_pro1.EmployeeId),
                                              EmployeedId2 = emp_pro2.EmployeeId,
                                              ProjectId = emp_pro1.ProjectId,
                                              DateFrom = emp_pro1.DateFrom.CompareTo(emp_pro2.DateFrom) < 0 ? emp_pro2.DateFrom : emp_pro1.DateFrom,
                                              DateTo = emp_pro1.DateTo.Value.CompareTo(emp_pro2.DateTo.Value) > 0 ? emp_pro2.DateTo.Value : emp_pro1.DateTo.Value,
                                          }).Distinct();


                var PairEmployeesProjectWorkedLongest = (from all_empl_pro in AllEmployeesProject
                                                         select new
                                                         {
                                                             EmployeedId1 = all_empl_pro.EmployeedId1,
                                                             EmployeedId2 = all_empl_pro.EmployeedId2,
                                                             ProjectId = all_empl_pro.ProjectId,
                                                             DaysWorkedTogether = all_empl_pro.DateTo.Subtract(all_empl_pro.DateFrom).Days
                                                         }).OrderByDescending(emp_proj => emp_proj.DaysWorkedTogether);

                if(PairEmployeesProjectWorkedLongest.Count() > 1)
                {
                    ModelForHomeView.EmployeesProjectsGridData.EmployeeIdPairOne = PairEmployeesProjectWorkedLongest.First().EmployeedId1;
                    ModelForHomeView.EmployeesProjectsGridData.EmployeeIdPairTwo = PairEmployeesProjectWorkedLongest.First().EmployeedId2;
                    ModelForHomeView.EmployeesProjectsGridData.ProjectIdWorkedtogether = PairEmployeesProjectWorkedLongest.First().ProjectId;
                    ModelForHomeView.EmployeesProjectsGridData.MaxDaysWoredTogether = PairEmployeesProjectWorkedLongest.First().DaysWorkedTogether;
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
