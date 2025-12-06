using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowManager.Models;

public class Workflow()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Title { get; set; }
    
    [ForeignKey(nameof(Process))]
    public List<Process> Processes { get; set; }
}