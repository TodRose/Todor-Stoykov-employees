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

        #endregion

    }
}
