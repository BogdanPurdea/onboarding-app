using Microsoft.EntityFrameworkCore;
using OnboardingApp.Api.Models;

namespace OnboardingApp.Api.Data;

public class OnboardingDbContext(DbContextOptions<OnboardingDbContext> options) : DbContext(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<OnboardingTask> OnboardingTasks { get; set; }
    public DbSet<TaskPrerequisite> TaskPrerequisites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── Departments ──────────────────────────────────────────────────────
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Id)
                .UseIdentityColumn(); // Serial / auto-increment in PostgreSQL

            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Unique constraint on Name
            entity.HasIndex(d => d.Name)
                .IsUnique();

            // Check constraint – only the 5 core departments are allowed
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Departments_Name",
                "\"Name\" IN ('Engineering', 'Sales', 'Marketing', 'HR', 'Finance')"));

            // Seed the 5 core departments
            entity.HasData(
                new Department { Id = 1, Name = "Engineering" },
                new Department { Id = 2, Name = "Sales" },
                new Department { Id = 3, Name = "Marketing" },
                new Department { Id = 4, Name = "HR" },
                new Department { Id = 5, Name = "Finance" }
            );
        });

        // ── OnboardingTasks ──────────────────────────────────────────────────
        modelBuilder.Entity<OnboardingTask>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id)
                .UseIdentityColumn();

            entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(t => t.Description)
                .IsRequired();

            // Store the TimelinePhase enum as an integer
            entity.Property(t => t.TimelinePhase)
                .HasConversion<int>();

            // Index on DepartmentId for fast role-specific queries
            // e.g. SELECT * FROM "OnboardingTasks" WHERE "DepartmentId" = 1
            entity.HasIndex(t => t.DepartmentId)
                .HasDatabaseName("IX_OnboardingTasks_DepartmentId");

            entity.HasOne(t => t.Department)
                .WithMany(d => d.OnboardingTasks)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── TaskPrerequisites ────────────────────────────────────────────────
        modelBuilder.Entity<TaskPrerequisite>(entity =>
        {
            // Composite primary key
            entity.HasKey(tp => new { tp.PreDependentTaskId, tp.PostDependentTaskId });

            // PreDependentTaskId → the task that must be completed first
            entity.HasOne(tp => tp.PreDependentTask)
                .WithMany(t => t.Dependents)
                .HasForeignKey(tp => tp.PreDependentTaskId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cyclical cascades

            // PostDependentTaskId → the task that is unlocked after
            entity.HasOne(tp => tp.PostDependentTask)
                .WithMany(t => t.Prerequisites)
                .HasForeignKey(tp => tp.PostDependentTaskId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ── Seed: OnboardingTasks ────────────────────────────────────────────
        // IDs 1–11  → Engineering (DepartmentId = 1)
        // IDs 12–19 → Sales       (DepartmentId = 2)
        // IDs 20–26 → Marketing   (DepartmentId = 3)
        // IDs 27–33 → HR          (DepartmentId = 4)
        // IDs 34–40 → Finance     (DepartmentId = 5)
        modelBuilder.Entity<OnboardingTask>().HasData(

            // ── Engineering ─────────────────────────────────────────────────
            new OnboardingTask { Id = 1,  DepartmentId = 1, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",           Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 2,  DepartmentId = 1, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 2, Title = "Set Up Your Development Environment",     Description = "Install the required tools for your workstation: VS Code (or your preferred IDE), Git, Node.js, and the .NET SDK. Follow the setup guide in the internal wiki." },
            new OnboardingTask { Id = 3,  DepartmentId = 1, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 3, Title = "Get GitHub Organisation Access",           Description = "Request access to the Meridian GitHub organisation from your team lead. Accept the invitation email and confirm you can see the private repositories." },
            new OnboardingTask { Id = 4,  DepartmentId = 1, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 4, Title = "Meet Your Team Lead & Onboarding Buddy",   Description = "Have a 30-minute intro call with your team lead and assigned buddy. Discuss your role, current projects, and how the team operates day-to-day." },
            new OnboardingTask { Id = 5,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 1, Title = "Install Docker",                           Description = "Install Docker Desktop and verify it is running correctly by executing `docker run hello-world` in your terminal. Docker is required to run the local development stack." },
            new OnboardingTask { Id = 6,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 2, Title = "Clone the Core API Repository",            Description = "Clone the main backend repository from GitHub and install its dependencies. Follow the README to confirm the project structure looks correct." },
            new OnboardingTask { Id = 7,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 3, Title = "Run the Application Locally",              Description = "Use `docker compose up` to spin up the full local stack (API + database). Verify the API responds at http://localhost:5000/healthz." },
            new OnboardingTask { Id = 8,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 4, Title = "Complete Security & Compliance Training",   Description = "Complete the mandatory security awareness module on the company LMS. This covers data handling policies, password management, and phishing awareness." },
            new OnboardingTask { Id = 9,  DepartmentId = 1, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 1, Title = "Submit Your First Pull Request",            Description = "Pick up a good-first-issue ticket from the backlog, implement the fix or feature, and open a PR against the main branch following the contribution guidelines." },
            new OnboardingTask { Id = 10, DepartmentId = 1, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 2, Title = "Complete Your First Code Review",           Description = "Review a colleague's open pull request. Leave at least one constructive comment or approval. This helps you understand the codebase and team coding standards." },
            new OnboardingTask { Id = 11, DepartmentId = 1, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 3, Title = "Attend Sprint Planning & Retrospective",    Description = "Participate in the full sprint ceremony cycle: planning, daily stand-ups, review, and retrospective. Share one observation or suggestion during the retro." },

            // ── Sales ────────────────────────────────────────────────────────
            new OnboardingTask { Id = 12, DepartmentId = 2, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",             Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 13, DepartmentId = 2, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 2, Title = "Set Up Your CRM Account",                   Description = "Log in to the company CRM (Salesforce) using the credentials sent to your work email. Complete your profile and familiarise yourself with the dashboard." },
            new OnboardingTask { Id = 14, DepartmentId = 2, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 3, Title = "Meet Your Sales Manager",                   Description = "Have a 30-minute intro call with your sales manager. Discuss your territory, targets, and the team's current pipeline." },
            new OnboardingTask { Id = 15, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 1, Title = "Complete Product & Service Training",        Description = "Work through the product training modules on the LMS. By the end you should be able to explain Meridian's core offering, pricing tiers, and key differentiators." },
            new OnboardingTask { Id = 16, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 2, Title = "Shadow a Senior Sales Rep on a Call",        Description = "Join a live discovery or demo call as a silent listener. Afterwards, debrief with the rep and note three things you would like to replicate." },
            new OnboardingTask { Id = 17, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 3, Title = "Set Up Your Sales Pipeline in the CRM",      Description = "Create your first opportunity records in Salesforce. Configure your personal pipeline view and set up task reminders for your first week's outreach." },
            new OnboardingTask { Id = 18, DepartmentId = 2, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 1, Title = "Make Your First Outbound Outreach",          Description = "Send personalised outreach to at least 10 prospects in your territory using the approved email templates. Log all activity in the CRM." },
            new OnboardingTask { Id = 19, DepartmentId = 2, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 2, Title = "Review Pipeline with Your Manager",          Description = "Present the current state of your pipeline in a 1:1 with your manager. Discuss deal health, blockers, and what support you need to hit your first target." },

            // ── Marketing ───────────────────────────────────────────────────
            new OnboardingTask { Id = 20, DepartmentId = 3, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",             Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 21, DepartmentId = 3, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 2, Title = "Get Access to Marketing Tools",             Description = "Request access to HubSpot, Canva, and the shared Google Drive marketing folder from your manager. Verify you can log in to each tool." },
            new OnboardingTask { Id = 22, DepartmentId = 3, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 3, Title = "Review Brand Guidelines",                   Description = "Read through the Meridian brand playbook (fonts, colours, tone of voice, logo usage). This is your reference for all content you produce." },
            new OnboardingTask { Id = 23, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 1, Title = "Get Familiar with the Content Calendar",     Description = "Review the current quarter's content calendar in Google Sheets. Identify the next three upcoming pieces you might contribute to." },
            new OnboardingTask { Id = 24, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 2, Title = "Complete Brand & Messaging Training",        Description = "Work through the internal brand messaging course on the LMS. Complete the short quiz at the end to confirm your understanding." },
            new OnboardingTask { Id = 25, DepartmentId = 3, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 1, Title = "Contribute to an Active Campaign",           Description = "Take ownership of at least one deliverable in an active marketing campaign — a social post, blog draft, or email copy. Get it reviewed and published." },
            new OnboardingTask { Id = 26, DepartmentId = 3, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 2, Title = "Present Your First Marketing Insights Report", Description = "Compile performance metrics for content you have worked on (impressions, clicks, conversions) and present a 5-minute summary to the marketing team." },

            // ── HR ──────────────────────────────────────────────────────────
            new OnboardingTask { Id = 27, DepartmentId = 4, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",             Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 28, DepartmentId = 4, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 2, Title = "Complete Employment Paperwork",             Description = "Sign and submit all required employment documents: contract, tax forms, and emergency contact details. Keep digital copies for your records." },
            new OnboardingTask { Id = 29, DepartmentId = 4, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 3, Title = "Get Your Employee ID & System Access",       Description = "Collect your employee ID badge from reception and confirm access to the HR information system (HRIS) and the payroll portal." },
            new OnboardingTask { Id = 30, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 1, Title = "Complete Mandatory Compliance Training",     Description = "Finish the mandatory compliance modules on the LMS: anti-harassment, GDPR data handling, and workplace safety. All three must be completed within week one." },
            new OnboardingTask { Id = 31, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 2, Title = "Enrol in Company Benefits",                 Description = "Log in to the benefits portal and select your health, dental, and pension options before the enrolment deadline. Contact the benefits provider with any questions." },
            new OnboardingTask { Id = 32, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 3, Title = "Review All Company HR Policies",            Description = "Read the employee handbook, leave policy, remote work policy, and code of conduct. Confirm you have reviewed them by signing the acknowledgement form." },
            new OnboardingTask { Id = 33, DepartmentId = 4, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 1, Title = "Complete 30-Day Check-in with Your Manager", Description = "Schedule and attend a structured 30-day review with your manager. Discuss your onboarding experience, early wins, and goals for the next two months." },

            // ── Finance ─────────────────────────────────────────────────────
            new OnboardingTask { Id = 34, DepartmentId = 5, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",             Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 35, DepartmentId = 5, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 2, Title = "Get Access to Financial Systems",           Description = "Request access to the accounting software (QuickBooks) and the financial reporting suite from your manager. Verify login and confirm your permission level." },
            new OnboardingTask { Id = 36, DepartmentId = 5, TimelinePhase = TimelinePhase.DayOne,   DisplayOrder = 3, Title = "Meet the Finance Team Lead",                Description = "Have a 30-minute intro call with the Finance Lead. Discuss your responsibilities, the monthly close schedule, and how finance collaborates with other departments." },
            new OnboardingTask { Id = 37, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 1, Title = "Complete Financial Compliance Training",    Description = "Finish the mandatory financial compliance and anti-fraud training modules on the LMS. Take note of the expense submission deadlines covered in the course." },
            new OnboardingTask { Id = 38, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 2, Title = "Understand the Expense Reporting Process",  Description = "Review the expense policy and submit a test expense report in QuickBooks to verify your access and understand the approval workflow." },
            new OnboardingTask { Id = 39, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekOne,  DisplayOrder = 3, Title = "Review Budget Reporting Templates",         Description = "Open the standard monthly and quarterly budget report templates in the shared Finance drive. Familiarise yourself with the structure before the next reporting cycle." },
            new OnboardingTask { Id = 40, DepartmentId = 5, TimelinePhase = TimelinePhase.MonthOne, DisplayOrder = 1, Title = "Contribute to Your First Monthly Report",   Description = "Take ownership of one section of the monthly financial report. Prepare the data, validate figures with a colleague, and submit it ahead of the close deadline." }
        );

        // ── Seed: TaskPrerequisites ──────────────────────────────────────────
        // Engineering: Install Docker → Clone API → Run Locally → First PR → First Code Review
        // Sales:       Product Training → Shadow Call → First Outreach
        // Marketing:   Brand Training → Contribute to Campaign
        // HR:          Complete Paperwork → Enrol in Benefits | Compliance Training → 30-Day Check-in
        // Finance:     Understand Expenses → Contribute to Monthly Report
        modelBuilder.Entity<TaskPrerequisite>().HasData(
            // Engineering chain
            new TaskPrerequisite { PreDependentTaskId = 5,  PostDependentTaskId = 6  }, // Install Docker → Clone Core API
            new TaskPrerequisite { PreDependentTaskId = 6,  PostDependentTaskId = 7  }, // Clone Core API → Run Locally
            new TaskPrerequisite { PreDependentTaskId = 7,  PostDependentTaskId = 9  }, // Run Locally → First PR
            new TaskPrerequisite { PreDependentTaskId = 9,  PostDependentTaskId = 10 }, // First PR → First Code Review

            // Sales chain
            new TaskPrerequisite { PreDependentTaskId = 15, PostDependentTaskId = 16 }, // Product Training → Shadow Call
            new TaskPrerequisite { PreDependentTaskId = 16, PostDependentTaskId = 18 }, // Shadow Call → First Outreach

            // Marketing chain
            new TaskPrerequisite { PreDependentTaskId = 24, PostDependentTaskId = 25 }, // Brand Training → Contribute to Campaign

            // HR chains
            new TaskPrerequisite { PreDependentTaskId = 28, PostDependentTaskId = 31 }, // Complete Paperwork → Enrol in Benefits
            new TaskPrerequisite { PreDependentTaskId = 30, PostDependentTaskId = 33 }, // Compliance Training → 30-Day Check-in

            // Finance chain
            new TaskPrerequisite { PreDependentTaskId = 38, PostDependentTaskId = 40 }  // Understand Expenses → Monthly Report
        );
    }
}
