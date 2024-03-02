using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crud_operation.Models
{
    public class RmpiStuInfoModel
    {
        [Key]
        public int StuID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [DisplayName("Student Name")]
        public string StuName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Student Roll")]
        public string StuRoll { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Student Registration No.")]
        public string StuReg { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [DisplayName("Student Technology/Dept.")]
        public string StuDept { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Student Mail")]
        public string StuEmail { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("IMG Name")]
        public string StuPhoto { get; set; }

        [NotMapped]
        [DisplayName("Photo")]
        public IFormFile StuPhotoFile { get; set; }
    }
}
