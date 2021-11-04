using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodorStoykovEmployees.Business
{
    public class EmployeeProjectCollection
    {
        #region Properties

        public List<EmployeeProject> AllEmployeesProjectsData { get; set; }


        #endregion

        #region Constructor

        public EmployeeProjectCollection()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fethching all the data fromt the data source file and adding it to the List property collectin of EmployeeProject object
        /// </summary>
        /// <param name="reader_stream">The file stream< StreamReader/param>
        /// <param name="FieldSeparator">The character separaring the fields in the data source usualy a comma</param>
        /// <param name="DateFormatsInTheSource">The format for the date in the datetime fields in the DataSource file</param>
        public void GetAllEmployeesProjectsFromStream(StreamReader reader_stream, string FieldSeparator, string DateFormatsInTheSource)
        {
            AllEmployeesProjectsData = new List<EmployeeProject>();

            using (reader_stream)
            {
                while (reader_stream.Peek() >= 0)
                {
                    // Getting the single line of the data from the file
                    string SingleEmployeeProejectDataSource = reader_stream.ReadLine();

                    // Creating the Employee Project data and binding the data to the object properties
                    EmployeeProject SingleEmployeeProject = new EmployeeProject();
                    SingleEmployeeProject.DataBind(SingleEmployeeProejectDataSource, FieldSeparator, DateFormatsInTheSource.Split(',', StringSplitOptions.TrimEntries).ToArray());

                    // Adding the single EmployeeProject object to the list
                    AllEmployeesProjectsData.Add(SingleEmployeeProject);
                }
            }
        }


        public bool FindThePairOfEmployeesWorkingLongest(ref int EmployeeIdOne, ref int EmployeeIdTwo, ref int ProjectId, ref int DaysWorkedTogether )
        {
            var AllEmployeesProject = (from emp_pro1 in this.AllEmployeesProjectsData
                                       join emp_pro2 in this.AllEmployeesProjectsData
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

            if (PairEmployeesProjectWorkedLongest.Count() > 1)
            {
                EmployeeIdOne = PairEmployeesProjectWorkedLongest.First().EmployeedId1;
                EmployeeIdTwo = PairEmployeesProjectWorkedLongest.First().EmployeedId2;
                ProjectId = PairEmployeesProjectWorkedLongest.First().ProjectId;
                DaysWorkedTogether = PairEmployeesProjectWorkedLongest.First().DaysWorkedTogether;
            }
            else
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
