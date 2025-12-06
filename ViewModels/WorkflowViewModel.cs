using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkflowManager.Models;

namespace WorkflowManager.ViewModels;

public class WorkflowViewModel : ViewModelBase
{
    [Required]
    [StringLength(30)]
    public string Title { get; set; }
    
    [ForeignKey(nameof(Process))]
    public List<Process> Processes { get; set; }
}