using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PEP.Models.Domain;

namespace PEP.Data;

public partial class FinalDesignContext : DbContext
{
    public FinalDesignContext()
    {
    }

    public FinalDesignContext(DbContextOptions<FinalDesignContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlgorithmProblem> AlgorithmProblems { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseChapter> CourseChapters { get; set; }

    public virtual DbSet<CourseTag> CourseTags { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<ProblemTag> ProblemTags { get; set; }

    public virtual DbSet<Reply> Replies { get; set; }

    public virtual DbSet<SubChapter> SubChapters { get; set; }

    public virtual DbSet<SubmissionRecord> SubmissionRecords { get; set; }

    public virtual DbSet<TestDatum> TestData { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCourse> UserCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:PEPString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlgorithmProblem>(entity =>
        {
            entity.HasKey(e => e.ProblemId).HasName("PK__algorith__69B87CECB40E0363");

            entity.ToTable("algorithm_problems");

            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.AcceptRate).HasColumnName("accept_rate");
            entity.Property(e => e.Difficulty)
                .HasMaxLength(20)
                .HasColumnName("difficulty");
            entity.Property(e => e.ProblemContent)
                .HasColumnType("text")
                .HasColumnName("problem_content");
            entity.Property(e => e.TestAmount).HasColumnName("test_amount");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__comments__E795768775FC3BBB");

            entity.ToTable("comments");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.CommentContent).HasColumnName("comment_content");
            entity.Property(e => e.FromUserId).HasColumnName("from_user_id");
            entity.Property(e => e.FromUsername)
                .HasMaxLength(255)
                .HasColumnName("from_username");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.Timestamp)
                .HasMaxLength(255)
                .HasColumnName("timestamp");

            entity.HasOne(d => d.FromUser).WithMany(p => p.Comments)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_comment_to_user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_comments_post_id");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__courses__2AA84FD131A1069E");

            entity.ToTable("courses");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.ChapterCount).HasColumnName("chapter_count");
            entity.Property(e => e.CourseName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("course_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Introduction)
                .HasColumnType("text")
                .HasColumnName("introduction");
        });

        modelBuilder.Entity<CourseChapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__course_c__745EFE87783BE697");

            entity.ToTable("course_chapters");

            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.ChapterNumber).HasColumnName("chapter_number");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseChapters)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_course_chapters_course_id");
        });

        modelBuilder.Entity<CourseTag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__course_t__50FC01572FB805B7");

            entity.ToTable("course_tags");

            entity.Property(e => e.TagId).HasColumnName("tagId");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.TagColor).HasColumnName("tag_color");
            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tag_name");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseTags)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_tag_course");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__posts__3ED78766341668CE");

            entity.ToTable("posts");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.PostContent).HasColumnName("post_content");
            entity.Property(e => e.PostTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("post_time");
            entity.Property(e => e.PostType).HasColumnName("post_type");
            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Problem).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_posts_algorithm_problems");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_posts_user_id");
        });

        modelBuilder.Entity<ProblemTag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__problem___4296A2B61260FB1B");

            entity.ToTable("problem_tags");

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.TagColor).HasColumnName("tag_color");
            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .HasColumnName("tag_name");

            entity.HasOne(d => d.Problem).WithMany(p => p.ProblemTags)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_problem_tags_problem_id");
        });

        modelBuilder.Entity<Reply>(entity =>
        {
            entity.HasKey(e => e.ReplyId).HasName("PK__replies__EE40569834EB4973");

            entity.ToTable("replies");

            entity.Property(e => e.ReplyId).HasColumnName("reply_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.FromUserId).HasColumnName("from_user_id");
            entity.Property(e => e.FromUsername)
                .HasMaxLength(255)
                .HasColumnName("from_username");
            entity.Property(e => e.ReplyContent).HasColumnName("reply_content");
            entity.Property(e => e.Timestamp)
                .HasMaxLength(255)
                .HasColumnName("timestamp");
            entity.Property(e => e.ToUserId).HasColumnName("to_user_id");
            entity.Property(e => e.ToUsername)
                .HasMaxLength(255)
                .HasColumnName("to_username");

            entity.HasOne(d => d.Comment).WithMany(p => p.Replies)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_replies_comment_id");

            entity.HasOne(d => d.FromUser).WithMany(p => p.ReplyFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_replies_from_user_id");

            entity.HasOne(d => d.ToUser).WithMany(p => p.ReplyToUsers)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_replies_to_user_id");
        });

        modelBuilder.Entity<SubChapter>(entity =>
        {
            entity.HasKey(e => e.SubChapterId).HasName("PK__sub_chap__2B141C0DACD75C26");

            entity.ToTable("sub_chapters");

            entity.Property(e => e.SubChapterId).HasColumnName("sub_chapter_id");
            entity.Property(e => e.MarkdownContent).HasColumnName("markdown_content");
            entity.Property(e => e.ParentChapterId).HasColumnName("parent_chapter_id");
            entity.Property(e => e.ParentChapterNumber).HasColumnName("parent_chapter_number");
            entity.Property(e => e.SubChapterNumber)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("sub_chapter_number");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.ParentChapter).WithMany(p => p.SubChapters)
                .HasForeignKey(d => d.ParentChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_sub_chapters_parent_chapter_id");
        });

        modelBuilder.Entity<SubmissionRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__tmp_ms_x__BFCFB4DD68476F48");

            entity.ToTable("submission_records");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Compiler)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("compiler");
            entity.Property(e => e.CompilerOutput)
                .HasColumnType("text")
                .HasColumnName("compiler_output");
            entity.Property(e => e.Memory)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("memory");
            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.ResultState)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("result_state");
            entity.Property(e => e.Runtime)
                .HasColumnType("decimal(7, 4)")
                .HasColumnName("runtime");
            entity.Property(e => e.SubmitTime)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("submit_time");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Problem).WithMany(p => p.SubmissionRecords)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__submissio__probl__54CB950F");

            entity.HasOne(d => d.User).WithMany(p => p.SubmissionRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__submissio__user___55BFB948");
        });

        modelBuilder.Entity<TestDatum>(entity =>
        {
            entity.HasKey(e => e.TestDataId).HasName("PK__test_dat__C3AF6FF27FC55BE2");

            entity.ToTable("test_data");

            entity.Property(e => e.TestDataId).HasColumnName("test_data_id");
            entity.Property(e => e.InputData).HasColumnName("input_data");
            entity.Property(e => e.OutputData).HasColumnName("output_data");
            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.SequenceNumber).HasColumnName("sequence_number");

            entity.HasOne(d => d.Problem).WithMany(p => p.TestData)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_test_data_problem_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__CB9A1CFF97EC8EA4");

            entity.ToTable("users");

            entity.HasIndex(e => e.Account, "UQ_Account").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ_UserName").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.HasKey(e => e.UserCourseId).HasName("PK__user_cou__651E082692E5E6C8");

            entity.ToTable("user_courses");

            entity.Property(e => e.UserCourseId).HasColumnName("user_course_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.IsFavorite).HasColumnName("is_favorite");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Course).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_user_courses_course_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_user_courses_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
