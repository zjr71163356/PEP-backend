using System;
using System.Collections.Generic;

namespace PEP.Models.Domain;

public partial class AlgorithmProblem
{
    public int ProblemId { get; set; }

    public string Title { get; set; } = null!;

    public int? AcceptRate { get; set; }

    public string Difficulty { get; set; } = null!;

    public int? TestAmount { get; set; }

    public string ProblemContent { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<ProblemTag> ProblemTags { get; set; } = new List<ProblemTag>();

    public virtual ICollection<SubmissionRecord> SubmissionRecords { get; set; } = new List<SubmissionRecord>();

    public virtual ICollection<TestDatum> TestData { get; set; } = new List<TestDatum>();
}
