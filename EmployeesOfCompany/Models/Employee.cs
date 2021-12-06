using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesOfCompany.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Пожалуйста, укажите отдел")]
        [DisplayName("Отдел")]
        public string Department { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Пожалуйста, укажите Ф.И.О.")]
        [DisplayName("Ф.И.О.")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Пожалуйста, укажите дату рождения")]
        [DisplayName("Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Пожалуйста, укажите дату устройства на работу")]
        [DisplayName("Дата устройства на работу")]
        public DateTime DateOfEmployment { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "Пожалуйста, укажите заработную плату")]
        [DisplayName("Заработная плата")]
        public int Salary { get; set; }
    }
}
