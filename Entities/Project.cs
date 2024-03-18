using System.Text;

namespace ControlingProjectApp.Entities
{
    public class Project : EntityBase
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Budget { get; set; }

        public DateOnly BeginDate { get; set; }

        public DateOnly EndDate { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
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
