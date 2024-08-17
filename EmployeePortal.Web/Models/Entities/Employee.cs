using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.Web.Models.Entities
{
	public class Employee
	{
		public int ID { get; set; }
		[Required(ErrorMessage = "Name not specified")]
		[StringLength(100)]
		public string? Name { get; set; }
		[Required(ErrorMessage = "Code not specified")]
		[StringLength(30)]
		public string? Code { get; set; }
        [Required(ErrorMessage = "City not specified")]
        [StringLength(30)]
        public string? CityName { get; set; }

		public City? City { get; set; }
	}
}
