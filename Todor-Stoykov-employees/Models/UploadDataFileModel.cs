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

        

        #endregion

        #region Constructors

        public UploadDataFileModel()
        {
           input_date_format = "yyyy/MM/dd, MM/dd/yyyy, MM/dd/yyyy HH:mm:ss, yyyy-MM-dd, yyyy-MM-dd HH:mm:ss.fff, yyyy-MM-dd HH:mm:ss";
        }

        #endregion

    }
}
