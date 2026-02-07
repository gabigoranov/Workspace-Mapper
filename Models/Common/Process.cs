using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkflowManager.Models.Common;

/// <summary>
/// A base class for processes containing information needed for every single type.
/// Configured to use TPH inheritance.
/// </summary>
public abstract class Process
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public string Title { get; set; }

    [Required]
    [ForeignKey(nameof(Workflow))]
    public int WorkflowId { get; set;  }

    public Workflow Workflow { get; set; }

    public string Icon { get; set; } = "Power";

    public bool IsFullscreen { get; set; } = false;

    public string Monitor { get; set; } = "1";

    public string Position { get; set; } = "100,100";

    public string Size { get; set; } = "800x450";

    public ProcessType Discriminator { get; set; }

    public abstract Task Execute();



}