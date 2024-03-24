using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControlingProjectApp.Entities
{
    public class Employee : EntityBase
    {
        private StringBuilder.AppendInterpolatedStringHandler d;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public JobPosition JobPositon { get; set; }

        public DateTime EmploymentDate { get; set; }

        [Column (TypeName= "decimal(18,2)")]
        public decimal HourlyRate { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"\nID:\t\t\t{Id}");
            sb.AppendLine($"FirstName:\t\t{FirstName}");
            sb.AppendLine($"LastName:\t\t{LastName}");
            sb.AppendLine($"JobPosition:\t\t{JobPositon}");
            sb.AppendLine($"Hourly Rate:\t\t{HourlyRate}");
            sb.AppendLine($"Employment Date:\t{EmploymentDate.ToString("d")}");
            return sb.ToString();
        }
    }
}

