using System.Text;

namespace ControlingProjectApp.Entities
{
    public class Project : EntityBase
    {
        public string? Name { get; init; }

        public string? Description { get; init; }

        public decimal Budget { get; init; }

        public DateOnly BeginDate { get; init; }

        public DateOnly EndDate { get; init; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ID:\t\t\t{Id}");
            sb.AppendLine($"Project Name:\t\t{Name}");
            sb.AppendLine($"Description:\t\t{Description}");
            sb.AppendLine($"Project Budget:\t\t{Budget.ToString()}");
            sb.AppendLine($"Start Date:\t\t{BeginDate.ToString()}");
            sb.AppendLine($"End Date:\t\t{EndDate.ToString()}");
            return sb.ToString();
        }
    }
}
