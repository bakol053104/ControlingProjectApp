using System.Text;

namespace ControlingProjectApp.Entities
{
    public class Employee : EntityBase
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public JobPosition JobPositon { get; set; }

        public DateOnly EmploymentDate { get; set; }

        public decimal HourlyRate { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"ID:\t\t\t{Id}");
            sb.AppendLine($"FirstName:\t\t{FirstName}");
            sb.AppendLine($"LastName:\t\t{LastName}");
            sb.AppendLine($"JobPosition:\t\t{JobPositon}");
            sb.AppendLine($"Hourly Rate:\t\t{HourlyRate}");
            sb.AppendLine($"Employment Date:\t{EmploymentDate.ToString()}");
            return sb.ToString();
        }
    }
}

