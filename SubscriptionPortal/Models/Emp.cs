using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SubscriptionPortal.Models
{
    public class Emp
    {
        [Key]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MaxLength(250)]
        [DisplayName("Employee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [DisplayName("Email")]
        public string EmailId { get; set; }

        [MaxLength(20)]
        [DisplayName("Department")]
        public string Department { get; set; }

        [MaxLength(200)]
        [DisplayName("Work Location")]
        public string Location { get; set; }


        //public PaginatedList<Emp> Emps { get; set; }
    }

   
}
