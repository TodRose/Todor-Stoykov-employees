using System;
using System.Globalization;

namespace TodorStoykovEmployees.Business
{
    /// <summary>
    /// Employee project 
    /// </summary>
    public class EmployeeProject
    {
        # region Properties

        /// <summary>
        /// The Id of the Employee
        /// </summary>
        public int EmployeeId { get; set; }


        /// <summary>
        /// The Id of the Project
        /// </summary>
        public int ProjectId { get; set; }


        /// <summary>
        /// The start date of the employees starting working on the project
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// The End date of the employees starting working on the project
        /// </summary>
        public DateTime? DateTo { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor for the Employee Project
        /// </summary>
        public EmployeeProject()
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Fetch the data from the string Data Source base on the field separator
        /// </summary>
        /// <param name="DataSource">The singale data source string</param>
        /// <param name="Separator">The separator for the fields in the data source usually comma</param>
        /// <param name="DateFormatsInTheSource">The possible format of the date fields in the datasource</param>
        /// <returns></returns>
        public bool DataBind(string DataSource, string Separator, string[] DateFormatsInTheSource)
        {
            try
            {
                string[] Fields = DataSource.Split(Separator);


                if (Fields.Length >= 1)
                {
                    int employeeId = 0;

                    if (int.TryParse(Fields[0].Trim(), out employeeId))
                        EmployeeId = employeeId;
                }

                if (Fields.Length >= 2)
                {
                    int projectId = 0;

                    if (int.TryParse(Fields[1].Trim(), out projectId))
                        ProjectId = projectId;
                }

                if (Fields.Length >= 3)
                {
                    DateTime date_from;
                    
                    if(DateTime.TryParseExact(Fields[2].Trim(), DateFormatsInTheSource, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date_from))
                        DateFrom = date_from;
                    else
                        DateFrom = DateTime.Parse(Fields[2].Trim());
                }

                if (Fields.Length >= 4)
                {
                    DateTime date_to;

                    if (DateTime.TryParseExact(Fields[3].Trim(), DateFormatsInTheSource, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date_to))
                        DateTo = date_to;
                    else
                        DateTo = DateTime.Parse(Fields[3].Trim());                     
                }
                else
                {
                    DateTo = DateTime.Now;
                }
            }
            catch(Exception ex)
            {

                return false;
            }

            return true;
        }


        #endregion
    }
}
