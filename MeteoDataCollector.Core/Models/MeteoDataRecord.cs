using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeteoDataCollector.Core.Models;

public class MeteoDataRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DateTime CollectedAt { get; set; } = DateTime.UtcNow;

    public string? JsonData { get; set; }
    
    public bool IsStationOnline { get; set; }
}