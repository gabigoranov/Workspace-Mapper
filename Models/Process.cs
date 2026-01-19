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
    [StringLength(255)]
    public string Directory { get; set; } 

    public int WorkflowId { get; set; }
    
    public virtual Workflow  Workflow { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Command  { get; set; } 
}