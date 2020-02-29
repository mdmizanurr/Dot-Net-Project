using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace ToyDemoProj.Models
{
    public class Order
    {
      
        public int OrderId { get; set; }


        public System.DateTime OrderDate { get; set; }

        public string Username { get; set; }

        [Required(ErrorMessage ="First Name is requied")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string  FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is requied")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is requied")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is requied")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage ="State is required")]
        [StringLength(40)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is requied")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is requied")]
        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(40)]
        public string Phone { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-za-z0-9.-]+\.[A-za-z]{2,4}",
            ErrorMessage ="Email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        [ScaffoldColumn(false)]
        public string paymentTransectionId { get; set; }

        [ScaffoldColumn(false)]
        public bool HasBeenShipped { get; set; }

        public List<OrderDetail> orderDetails { get; set; }




    }
}