using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.Models.Common;

namespace WorkflowManager.Models;

public class Workflow
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Title { get; set; }

    public DateTime LastStartup { get; set; } = DateTime.Now;

    public List<Common.Process> Processes { get; set; } =  new List<Common.Process>();

    /// <summary>
    ///  A utility method for displaying info about each WorkflowCard
    /// </summary>
    [NotMapped]
    public string JoinedTitles => 
        string.Join(" • ", Processes.Select(x => x.Title)).ToUpper(); 
}