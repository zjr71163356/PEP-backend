using System;
using System.Collections.Generic;

namespace PEP.Models.Domain;

public partial class CourseChapter
{
    public int ChapterId { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int ChapterNumber { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<SubChapter> SubChapters { get; set; } = new List<SubChapter>();
}
