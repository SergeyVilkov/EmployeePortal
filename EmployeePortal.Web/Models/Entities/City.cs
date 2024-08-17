using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.Web.Models.Entities
{
    public class City
    {
        public int? ID { get; set; }

        public string? Name { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
