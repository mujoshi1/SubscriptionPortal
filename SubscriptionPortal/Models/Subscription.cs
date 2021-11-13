using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SubscriptionPortal.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        
        [DisplayName("Application Name")]
        public string ApplicationName { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MaxLength(25)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        //[Required(ErrorMessage = "This field required")]
        [MaxLength(20)]
        [DisplayName("Database Name")]
        public string DBName { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MaxLength(25)]
        [DisplayName("Database UserId")]
        public string DBUserId { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MaxLength(25)]
        [DisplayName("Database Password")]
        public string DBUserPassword { get; set; }

        [Required(ErrorMessage = "This field required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [DisplayName("Admin Email")]
        public string EmailId { get; set; }
        
        [MaxLength(25)]
        [DisplayName("Work Location")]
        public string Location { get; set; }

        [MaxLength(2)]
        [DisplayName("UserId")]
        public string Userid { get; set; }

        //public List<SelectListItem> ApplicationNames { get; } = new List<SelectListItem>
        //{
        //   new SelectListItem { Value = "0", Text = "Select Application" },
        //        new SelectListItem { Value = "1", Text = "Gift Register" },
        //        new SelectListItem { Value = "2", Text = "Meeting Expanse" },
        //        new SelectListItem { Value = "3", Text = "Man Power" },
        //        new SelectListItem { Value = "4", Text = "New Hire"},
        //};
    }
}
