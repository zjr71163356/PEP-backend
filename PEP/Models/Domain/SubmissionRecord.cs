using System;
using System.Collections.Generic;

namespace PEP.Models;

public partial class SubmissionRecord
{
    public int RecordId { get; set; }

    public int? ProblemId { get; set; }

    public int? UserId { get; set; }

    public string? UserName { get; set; }

    public string? Title { get; set; }

    public string? ResultState { get; set; }

    public string? Compiler { get; set; }

    public int? Memory { get; set; }

    public decimal? Runtime { get; set; }

    public DateTime? SubmitTime { get; set; }

    public string? Code { get; set; }

    public string? CompilerOutput { get; set; }

    public virtual AlgorithmProblem? Problem { get; set; }

    public virtual User? User { get; set; }
}
