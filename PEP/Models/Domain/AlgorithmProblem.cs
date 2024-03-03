﻿using System;
using System.Collections.Generic;

namespace PEP.Model.Domain;

public partial class AlgorithmProblem
{
    public int ProblemId { get; set; }

    public string Title { get; set; } = null!;

    public int? AcceptRate { get; set; }

    public string? Difficulty { get; set; }

    public int? TestAmount { get; set; }

    public string? ProblemContent { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<ProblemTag> ProblemTags { get; set; } = new List<ProblemTag>();

    public virtual ICollection<SubmissionRecord> SubmissionRecords { get; set; } = new List<SubmissionRecord>();

    public virtual ICollection<TestData> TestData { get; set; } = new List<TestData>();
}
