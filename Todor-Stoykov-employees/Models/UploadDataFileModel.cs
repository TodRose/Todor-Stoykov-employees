using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodorStoykovEmployees.Models
{
    public class UploadDataFileModel
    {
        #region Properties
        
        /// <summary>
        /// List of the posted files
        /// </summary>
        public List<IFormFile> input_file_data { get; set; }


        /// <summary>
        /// The format of the date fields in the data source file
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string input_date_format { get; set; }

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

        public UploadDataFileModel()
        {
           input_date_format = "yyyy/MM/dd, MM/dd/yyyy, MM/dd/yyyy HH:mm:ss, yyyy-MM-dd, yyyy-MM-dd HH:mm:ss.fff, yyyy-MM-dd HH:mm:ss";
        }

        #endregion

    }
}
