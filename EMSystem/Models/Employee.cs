using System.Text.Json.Serialization;

namespace EMSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        [JsonIgnore]
        public int DepartmentId { get; set; }
        //[JsonIgnore]
        public virtual Department Department { get; set; }
    }
}
