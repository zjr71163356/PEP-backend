using System;
using System.Collections.Generic;

namespace PEP.Models.Domain;

public partial class SubmissionRecord
{
    public int RecordId { get; set; }

    public int ProblemId { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ResultState { get; set; } = null!;

    public string Compiler { get; set; } = null!;

    public int Memory { get; set; }

    public decimal Runtime { get; set; }

    public DateTime SubmitTime { get; set; }

    public string Code { get; set; } = null!;

    public string CompilerOutput { get; set; } = null!;

    public virtual AlgorithmProblem Problem { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
