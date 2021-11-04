using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodorStoykovEmployees.Models
{
    public class HomeModel
    {
        #region Properties


        /// <summary>
        /// The model for the Upload Date file view
        /// </summary>
        public UploadDataFileModel UploadDateFile { get; set;}

        /// <summary>
        /// The model for emplyee project grid
        /// </summary>
        public EmployeesProjectsGridModel EmployeesProjectsGridData { get; set; }


        #endregion


        #region Construtors

        public HomeModel()
        {

        }

        #endregion
    }
}
