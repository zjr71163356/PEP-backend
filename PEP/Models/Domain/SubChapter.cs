﻿using System;
using System.Collections.Generic;

namespace PEP.Models;

public partial class SubChapter
{
    public int SubChapterId { get; set; }

    public int? CourseId { get; set; }

    public int? ParentChapterId { get; set; }

    public int? ParentChapterNumber { get; set; }

    public decimal SubChapterNumber { get; set; }

    public string Title { get; set; } = null!;

    public string MarkdownContent { get; set; } = null!;

    public virtual Course? Course { get; set; }

    public virtual CourseChapter? ParentChapter { get; set; }
}
