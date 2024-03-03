using System;
using System.Collections.Generic;

namespace PEP.Model.Domain;

public partial class ProblemTag
{
    public int TagId { get; set; }

    public int? ProblemId { get; set; }

    public string TagName { get; set; } = null!;

    public int TagColor { get; set; }

    public virtual AlgorithmProblem? Problem { get; set; }
}
