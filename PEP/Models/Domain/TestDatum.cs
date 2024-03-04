using System;
using System.Collections.Generic;

namespace PEP.Models;

public partial class TestDatum
{
    public int TestDataId { get; set; }

    public int? ProblemId { get; set; }

    public int? SequenceNumber { get; set; }

    public string InputData { get; set; } = null!;

    public string OutputData { get; set; } = null!;

    public virtual AlgorithmProblem? Problem { get; set; }
}
