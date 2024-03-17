using System.Text;

namespace ControlingProjectApp.Entities
{
    public class Employee : EntityBase
    {
        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public JobPosition JobPositon { get; init; }

        public DateOnly EmploymentDate { get; init; }

        public decimal HourlyRate
        {
            get
            {
                var hourly = 0.0m; ;
                var jobPosition = JobPositon;
                switch (jobPosition)
                {
                    case JobPosition.Employee:
                        hourly = 100.0m;
                        break;
                    case JobPosition.Engineer:
                        hourly = 200.0m;
                        break;
                    case JobPosition.Manager:
                        hourly = 300.0m;
                        break;
                    case JobPosition.Supervisor:
                        hourly = 400.0m;
                        break;
                }
                return hourly;
            }
        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
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

