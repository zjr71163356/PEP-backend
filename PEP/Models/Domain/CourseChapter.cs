using System;
using System.Collections.Generic;

namespace PEP.Model.Domain;

public partial class CourseChapter
{
    public int ChapterId { get; set; }

    public int? CourseId { get; set; }

    public string Title { get; set; } = null!;

    public decimal ChapterNumber { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<SubChapter> SubChapters { get; set; } = new List<SubChapter>();
}
