using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControlingProjectApp.Data.Entities;

public class Project : EntityBase
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Budget { get; set; }

    public DateTime BeginDate { get; set; }

    public DateTime EndDate { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"\nID:\t\t\t{Id}");
        sb.AppendLine($"Project Name:\t\t{Name}");
        sb.AppendLine($"Description:\t\t{Description}");
        sb.AppendLine($"Project Budget:\t\t{Budget.ToString("N2")}");
        sb.AppendLine($"Start Date:\t\t{BeginDate.ToString("d")}");
        sb.AppendLine($"End Date:\t\t{EndDate.ToString("d")}");
        return sb.ToString();
    }
}
