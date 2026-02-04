using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkflowManager.Models;

public class Workflow()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Title { get; set; }

    public DateTime LastStartup { get; set; } = DateTime.Now;

    public List<Process> Processes { get; set; } =  new List<Process>();

    [NotMapped]
    public string JoinedTitles => string.Join(" • ", Processes.Select(x => x.Title)).ToUpper();
}