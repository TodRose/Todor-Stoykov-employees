using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodorStoykovEmployees.Models
{
    public class EmployeesProjectsGridModel
    {
        #region Properties

        /// <summary>
        /// The ID of the first employee id of the pair that has worked together the longest
        /// </summary>
        public int EmployeeIdPairOne { get; set; }

        /// <summary>
        /// The ID of the second employee id of the pair that has worked together the longest
        /// </summary>        
        public int EmployeeIdPairTwo { get; set; }


        /// <summary>
        /// The ID of the project for which the employees has worked together the longest
        /// </summary>        
        public int ProjectIdWorkedtogether { get; set; }


        /// <summary>
        /// The numbers of days the employees worked together on the project
        /// </summary>
        public int MaxDaysWoredTogether { get; set; }

        #endregion

        #region Constructors
        public EmployeesProjectsGridModel()
        { 
        }

        #endregion
    }
}
