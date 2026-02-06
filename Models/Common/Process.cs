using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkflowManager.Models.Common;

public partial class Process
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

    public string Icon { get; set; }

    public bool IsFullscreen { get; set; }

    public string Monitor { get; set; }

    public string Position { get; set; }

    public string Size { get; set; }

    public virtual Task Execute()
    {
        return Task.CompletedTask;
    }



}