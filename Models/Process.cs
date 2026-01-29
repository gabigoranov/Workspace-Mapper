using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkflowManager.Models;

public partial class Process : ObservableValidator
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    [ObservableProperty] 
    private string _title = string.Empty;

    [Required]
    [StringLength(255)]
    [ObservableProperty]
    private string _directory = string.Empty;

    public int WorkflowId { get; set; }

    public virtual Workflow Workflow { get; set; }

    [Required]
    [StringLength(255)]
    [ObservableProperty]
    private string _command = string.Empty;
}