using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowManager.Models;

public class Process()
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Title { get; set; } 
    
    [Required]
    public string Directory { get; set; }
    
    [ForeignKey(nameof(Workflow))]
    public int? WorkflowId { get; set; }
    
    public virtual Workflow  Workflow { get; set; }
    
    public string? Command  { get; set; }
}