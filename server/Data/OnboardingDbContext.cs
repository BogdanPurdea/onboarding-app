using Microsoft.EntityFrameworkCore;
using OnboardingApp.Api.Models;

namespace OnboardingApp.Api.Data;

public class OnboardingDbContext(DbContextOptions<OnboardingDbContext> options) : DbContext(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentContact> DepartmentContacts { get; set; }
    public DbSet<OnboardingTask> OnboardingTasks { get; set; }
    public DbSet<TaskPrerequisite> TaskPrerequisites { get; set; }
    public DbSet<TaskInstruction> TaskInstructions { get; set; }
    public DbSet<OnboardingProgress> OnboardingProgresses { get; set; }

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

            entity.Property(d => d.RoleKey)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(d => d.Tagline)
                .HasMaxLength(200);

            entity.Property(d => d.WelcomeMessage)
                .HasMaxLength(500);

            entity.Property(d => d.MondaySchedule).HasMaxLength(10).HasDefaultValue("office");
            entity.Property(d => d.TuesdaySchedule).HasMaxLength(10).HasDefaultValue("office");
            entity.Property(d => d.WednesdaySchedule).HasMaxLength(10).HasDefaultValue("office");
            entity.Property(d => d.ThursdaySchedule).HasMaxLength(10).HasDefaultValue("remote");
            entity.Property(d => d.FridaySchedule).HasMaxLength(10).HasDefaultValue("remote");

            // Unique constraints
            entity.HasIndex(d => d.Name).IsUnique();
            entity.HasIndex(d => d.RoleKey).IsUnique();

            // Seed the 5 core departments with schedule and dashboard info
            entity.HasData(
                new Department
                {
                    Id = 1, Name = "Engineering", RoleKey = "engineering",
                    Tagline = "Build the future, one commit at a time",
                    WelcomeMessage = "Welcome to Engineering! We are excited to have you on board. Your first weeks will be all about getting your development environment ready and making your first contribution.",
                    MondaySchedule = "office", TuesdaySchedule = "office", WednesdaySchedule = "office",
                    ThursdaySchedule = "remote", FridaySchedule = "remote"
                },
                new Department
                {
                    Id = 2, Name = "Sales", RoleKey = "sales",
                    Tagline = "Turn every conversation into an opportunity",
                    WelcomeMessage = "Welcome to Sales! You'll be building relationships that drive Meridian's growth. Start by getting familiar with our CRM and shadowing top performers.",
                    MondaySchedule = "office", TuesdaySchedule = "remote", WednesdaySchedule = "office",
                    ThursdaySchedule = "remote", FridaySchedule = "office"
                },
                new Department
                {
                    Id = 3, Name = "Marketing", RoleKey = "marketing",
                    Tagline = "Tell the story that moves the market",
                    WelcomeMessage = "Welcome to Marketing! You'll shape how the world sees Meridian. Dive into the brand playbook and start exploring our content calendar.",
                    MondaySchedule = "remote", TuesdaySchedule = "office", WednesdaySchedule = "office",
                    ThursdaySchedule = "remote", FridaySchedule = "remote"
                },
                new Department
                {
                    Id = 4, Name = "HR", RoleKey = "hr",
                    Tagline = "People are our greatest product",
                    WelcomeMessage = "Welcome to HR! You'll play a key role in helping others succeed at Meridian. Start by completing your paperwork and meeting the team.",
                    MondaySchedule = "office", TuesdaySchedule = "office", WednesdaySchedule = "remote",
                    ThursdaySchedule = "office", FridaySchedule = "remote"
                },
                new Department
                {
                    Id = 5, Name = "Finance", RoleKey = "finance",
                    Tagline = "The numbers that keep us moving forward",
                    WelcomeMessage = "Welcome to Finance! Your work ensures Meridian's fiscal health. Get started by gaining system access and meeting the finance team.",
                    MondaySchedule = "office", TuesdaySchedule = "office", WednesdaySchedule = "remote",
                    ThursdaySchedule = "remote", FridaySchedule = "office"
                }
            );
        });

        // ── DepartmentContacts ───────────────────────────────────────────────
        modelBuilder.Entity<DepartmentContact>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id).UseIdentityColumn();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Role).IsRequired().HasMaxLength(100);
            entity.Property(c => c.AvatarInitials).HasMaxLength(3);
            entity.Property(c => c.Email).HasMaxLength(100);
            entity.Property(c => c.Bio).HasMaxLength(500);
            entity.Property(c => c.SlackMemberId).HasMaxLength(50);
            entity.Property(c => c.GoogleMeetUrl).HasMaxLength(150);
            entity.Property(c => c.PreferredCommTool).HasMaxLength(20).HasDefaultValue("slack");

            entity.HasIndex(c => c.DepartmentId)
                .HasDatabaseName("IX_DepartmentContacts_DepartmentId");

            entity.HasOne(c => c.Department)
                .WithMany(d => d.Contacts)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                // Engineering
                new DepartmentContact { Id = 1,  DepartmentId = 1, Name = "Priya Sharma",   Role = "Engineering Manager",    AvatarInitials = "PS", Email = "priya.sharma@meridian.io",   Bio = "Leads the backend platform team. Open-source enthusiast and conference speaker.", SlackMemberId = "U012345678A", GoogleMeetUrl = "https://meet.google.com/pri-ya-sharma", PreferredCommTool = "slack", DisplayOrder = 1 },
                new DepartmentContact { Id = 2,  DepartmentId = 1, Name = "Liam O'Brien",   Role = "Senior Engineer",         AvatarInitials = "LO", Email = "liam.obrien@meridian.io",    Bio = "Your onboarding buddy. Go-to for architecture questions and code reviews.", SlackMemberId = "U012345678B", GoogleMeetUrl = "https://meet.google.com/lia-m-obrien", PreferredCommTool = "meet", DisplayOrder = 2 },
                new DepartmentContact { Id = 3,  DepartmentId = 1, Name = "Yuki Tanaka",    Role = "DevOps Engineer",          AvatarInitials = "YT", Email = "yuki.tanaka@meridian.io",    Bio = "Owns CI/CD pipelines and cloud infrastructure. Ask her about the deployment process.", SlackMemberId = "U012345678C", GoogleMeetUrl = "https://meet.google.com/yuk-i-tanaka", PreferredCommTool = "slack", DisplayOrder = 3 },

                // Sales
                new DepartmentContact { Id = 4,  DepartmentId = 2, Name = "Marcus Webb",    Role = "Sales Manager",            AvatarInitials = "MW", Email = "marcus.webb@meridian.io",    Bio = "Leads the EMEA sales team. Passionate about consultative selling.", SlackMemberId = "U022345678A", GoogleMeetUrl = "https://meet.google.com/mar-c-us-webb", PreferredCommTool = "meet", DisplayOrder = 1 },
                new DepartmentContact { Id = 5,  DepartmentId = 2, Name = "Sofia Reyes",    Role = "Account Executive",        AvatarInitials = "SR", Email = "sofia.reyes@meridian.io",    Bio = "Your onboarding buddy. Top performer in enterprise deals.", SlackMemberId = "U022345678B", GoogleMeetUrl = "https://meet.google.com/sof-i-a-reyes", PreferredCommTool = "slack", DisplayOrder = 2 },
                new DepartmentContact { Id = 6,  DepartmentId = 2, Name = "James Okafor",   Role = "Sales Development Rep",    AvatarInitials = "JO", Email = "james.okafor@meridian.io",   Bio = "Manages inbound pipeline and prospect qualification.", SlackMemberId = "U022345678C", GoogleMeetUrl = "https://meet.google.com/jam-e-s-okafor", PreferredCommTool = "slack", DisplayOrder = 3 },

                // Marketing
                new DepartmentContact { Id = 7,  DepartmentId = 3, Name = "Amara Diallo",   Role = "Head of Marketing",       AvatarInitials = "AD", Email = "amara.diallo@meridian.io",   Bio = "Drives brand strategy and growth campaigns across all channels.", SlackMemberId = "U032345678A", GoogleMeetUrl = "https://meet.google.com/ama-r-a-diallo", PreferredCommTool = "meet", DisplayOrder = 1 },
                new DepartmentContact { Id = 8,  DepartmentId = 3, Name = "Chris Park",     Role = "Content Strategist",       AvatarInitials = "CP", Email = "chris.park@meridian.io",     Bio = "Your onboarding buddy. Owns the content calendar and editorial voice.", SlackMemberId = "U032345678B", GoogleMeetUrl = "https://meet.google.com/chr-i-s-park", PreferredCommTool = "slack", DisplayOrder = 2 },
                new DepartmentContact { Id = 9,  DepartmentId = 3, Name = "Elena Morel",    Role = "Performance Marketer",     AvatarInitials = "EM", Email = "elena.morel@meridian.io",    Bio = "Runs paid acquisition campaigns and analyses conversion metrics.", SlackMemberId = "U032345678C", GoogleMeetUrl = "https://meet.google.com/ele-n-a-morel", PreferredCommTool = "slack", DisplayOrder = 3 },

                // HR
                new DepartmentContact { Id = 10, DepartmentId = 4, Name = "David Kim",      Role = "HR Director",             AvatarInitials = "DK", Email = "david.kim@meridian.io",      Bio = "Oversees talent acquisition, L&D, and employee experience.", SlackMemberId = "U042345678A", GoogleMeetUrl = "https://meet.google.com/dav-i-d-kim", PreferredCommTool = "meet", DisplayOrder = 1 },
                new DepartmentContact { Id = 11, DepartmentId = 4, Name = "Fatima Al-Sayed", Role = "People Operations Lead",  AvatarInitials = "FA", Email = "fatima.alsayed@meridian.io", Bio = "Your onboarding buddy. Manages benefits, compliance, and HR ops.", SlackMemberId = "U042345678B", GoogleMeetUrl = "https://meet.google.com/fat-i-m-a", PreferredCommTool = "slack", DisplayOrder = 2 },
                new DepartmentContact { Id = 12, DepartmentId = 4, Name = "Tom Brennan",    Role = "Talent Acquisition Spec.", AvatarInitials = "TB", Email = "tom.brennan@meridian.io",    Bio = "Leads recruiting efforts across all departments.", SlackMemberId = "U042345678C", GoogleMeetUrl = "https://meet.google.com/tom-bren-nan", PreferredCommTool = "slack", DisplayOrder = 3 },

                // Finance
                new DepartmentContact { Id = 13, DepartmentId = 5, Name = "Nadia Petrov",   Role = "Finance Lead",            AvatarInitials = "NP", Email = "nadia.petrov@meridian.io",   Bio = "Owns financial reporting, forecasting, and budget cycles.", SlackMemberId = "U052345678A", GoogleMeetUrl = "https://meet.google.com/nad-i-a-petrov", PreferredCommTool = "meet", DisplayOrder = 1 },
                new DepartmentContact { Id = 14, DepartmentId = 5, Name = "Arjun Mehta",    Role = "Financial Analyst",       AvatarInitials = "AM", Email = "arjun.mehta@meridian.io",    Bio = "Your onboarding buddy. Specialises in expense reporting and financial modelling.", SlackMemberId = "U052345678B", GoogleMeetUrl = "https://meet.google.com/arj-u-n-mehta", PreferredCommTool = "slack", DisplayOrder = 2 },
                new DepartmentContact { Id = 15, DepartmentId = 5, Name = "Laura Bennett",  Role = "Payroll Specialist",      AvatarInitials = "LB", Email = "laura.bennett@meridian.io",  Bio = "Handles payroll processing and employee compensation queries.", SlackMemberId = "U052345678C", GoogleMeetUrl = "https://meet.google.com/lau-r-a-bennett", PreferredCommTool = "slack", DisplayOrder = 3 }
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
            new OnboardingTask { Id = 1,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",           Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 2,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 2, Title = "Set Up Your Development Environment",     Description = "Install the required tools for your workstation: VS Code (or your preferred IDE), Git, Node.js, and the .NET SDK. Follow the setup guide in the internal wiki." },
            new OnboardingTask { Id = 3,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 3, Title = "Get GitHub Organisation Access",           Description = "Request access to the Meridian GitHub organisation from your team lead. Accept the invitation email and confirm you can see the private repositories." },
            new OnboardingTask { Id = 4,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 4, Title = "Meet Your Team Lead & Onboarding Buddy",   Description = "Have a 30-minute intro call with your team lead and assigned buddy. Discuss your role, current projects, and how the team operates day-to-day." },
            new OnboardingTask { Id = 5,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 1, Title = "Install Docker",                           Description = "Install Docker Desktop and verify it is running correctly by executing `docker run hello-world` in your terminal. Docker is required to run the local development stack." },
            new OnboardingTask { Id = 6,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 2, Title = "Clone the Core API Repository",            Description = "Clone the main backend repository from GitHub and install its dependencies. Follow the README to confirm the project structure looks correct." },
            new OnboardingTask { Id = 7,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 3, Title = "Run the Application Locally",              Description = "Use `docker compose up` to spin up the full local stack (API + database). Verify the API responds at http://localhost:5000/healthz." },
            new OnboardingTask { Id = 8,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 4, Title = "Complete Security & Compliance Training",   Description = "Complete the mandatory security awareness module on the company LMS. This covers data handling policies, password management, and phishing awareness." },
            new OnboardingTask { Id = 9,  DepartmentId = 1, TimelinePhase = TimelinePhase.WeekThree, DisplayOrder = 1, Title = "Submit Your First Pull Request",            Description = "Pick up a good-first-issue ticket from the backlog, implement the fix or feature, and open a PR against the main branch following the contribution guidelines." },
            new OnboardingTask { Id = 10, DepartmentId = 1, TimelinePhase = TimelinePhase.WeekThree, DisplayOrder = 2, Title = "Complete Your First Code Review",           Description = "Review a colleague's open pull request. Leave at least one constructive comment or approval. This helps you understand the codebase and team coding standards." },
            new OnboardingTask { Id = 11, DepartmentId = 1, TimelinePhase = TimelinePhase.WeekFour,  DisplayOrder = 3, Title = "Attend Sprint Planning & Retrospective",    Description = "Participate in the full sprint ceremony cycle: planning, daily stand-ups, review, and retrospective. Share one observation or suggestion during the retro." },

            // ── Sales ────────────────────────────────────────────────────────
            new OnboardingTask { Id = 12, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",             Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 13, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 2, Title = "Set Up Your CRM Account",                   Description = "Log in to the company CRM (Salesforce) using the credentials sent to your work email. Complete your profile and familiarise yourself with the dashboard." },
            new OnboardingTask { Id = 14, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 3, Title = "Meet Your Sales Manager",                   Description = "Have a 30-minute intro call with your sales manager. Discuss your territory, targets, and the team's current pipeline." },
            new OnboardingTask { Id = 15, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 1, Title = "Complete Product & Service Training",        Description = "Work through the product training modules on the LMS. By the end you should be able to explain Meridian's core offering, pricing tiers, and key differentiators." },
            new OnboardingTask { Id = 16, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 2, Title = "Shadow a Senior Sales Rep on a Call",        Description = "Join a live discovery or demo call as a silent listener. Afterwards, debrief with the rep and note three things you would like to replicate." },
            new OnboardingTask { Id = 17, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekThree, DisplayOrder = 3, Title = "Set Up Your Sales Pipeline in the CRM",      Description = "Create your first opportunity records in Salesforce. Configure your personal pipeline view and set up task reminders for your first week's outreach." },
            new OnboardingTask { Id = 18, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekThree, DisplayOrder = 1, Title = "Make Your First Outbound Outreach",          Description = "Send personalised outreach to at least 10 prospects in your territory using the approved email templates. Log all activity in the CRM." },
            new OnboardingTask { Id = 19, DepartmentId = 2, TimelinePhase = TimelinePhase.WeekFour,  DisplayOrder = 2, Title = "Review Pipeline with Your Manager",          Description = "Present the current state of your pipeline in a 1:1 with your manager. Discuss deal health, blockers, and what support you need to hit your first target." },

            // ── Marketing ───────────────────────────────────────────────────
            new OnboardingTask { Id = 20, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",             Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 21, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 2, Title = "Get Access to Marketing Tools",             Description = "Request access to HubSpot, Canva, and the shared Google Drive marketing folder from your manager. Verify you can log in to each tool." },
            new OnboardingTask { Id = 22, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 3, Title = "Review Brand Guidelines",                   Description = "Read through the Meridian brand playbook (fonts, colours, tone of voice, logo usage). This is your reference for all content you produce." },
            new OnboardingTask { Id = 23, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 1, Title = "Get Familiar with the Content Calendar",     Description = "Review the current quarter's content calendar in Google Sheets. Identify the next three upcoming pieces you might contribute to." },
            new OnboardingTask { Id = 24, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 2, Title = "Complete Brand & Messaging Training",        Description = "Work through the internal brand messaging course on the LMS. Complete the short quiz at the end to confirm your understanding." },
            new OnboardingTask { Id = 25, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekThree, DisplayOrder = 1, Title = "Contribute to an Active Campaign",           Description = "Take ownership of at least one deliverable in an active marketing campaign — a social post, blog draft, or email copy. Get it reviewed and published." },
            new OnboardingTask { Id = 26, DepartmentId = 3, TimelinePhase = TimelinePhase.WeekFour,  DisplayOrder = 2, Title = "Present Your First Marketing Insights Report", Description = "Compile performance metrics for content you have worked on (impressions, clicks, conversions) and present a 5-minute summary to the marketing team." },

            // ── HR ──────────────────────────────────────────────────────────
            new OnboardingTask { Id = 27, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",             Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 28, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 2, Title = "Complete Employment Paperwork",             Description = "Sign and submit all required employment documents: contract, tax forms, and emergency contact details. Keep digital copies for your records." },
            new OnboardingTask { Id = 29, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 3, Title = "Get Your Employee ID & System Access",       Description = "Collect your employee ID badge from reception and confirm access to the HR information system (HRIS) and the payroll portal." },
            new OnboardingTask { Id = 30, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 1, Title = "Complete Mandatory Compliance Training",     Description = "Finish the mandatory compliance modules on the LMS: anti-harassment, GDPR data handling, and workplace safety. All three must be completed within week one." },
            new OnboardingTask { Id = 31, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 2, Title = "Enrol in Company Benefits",                 Description = "Log in to the benefits portal and select your health, dental, and pension options before the enrolment deadline. Contact the benefits provider with any questions." },
            new OnboardingTask { Id = 32, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekThree, DisplayOrder = 3, Title = "Review All Company HR Policies",            Description = "Read the employee handbook, leave policy, remote work policy, and code of conduct. Confirm you have reviewed them by signing the acknowledgement form." },
            new OnboardingTask { Id = 33, DepartmentId = 4, TimelinePhase = TimelinePhase.WeekFour,  DisplayOrder = 1, Title = "Complete 30-Day Check-in with Your Manager", Description = "Schedule and attend a structured 30-day review with your manager. Discuss your onboarding experience, early wins, and goals for the next two months." },

            // ── Finance ─────────────────────────────────────────────────────
            new OnboardingTask { Id = 34, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 1, Title = "Attend Day 1 Welcome Session",             Description = "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office." },
            new OnboardingTask { Id = 35, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 2, Title = "Get Access to Financial Systems",           Description = "Request access to the accounting software (QuickBooks) and the financial reporting suite from your manager. Verify login and confirm your permission level." },
            new OnboardingTask { Id = 36, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekOne,   DisplayOrder = 3, Title = "Meet the Finance Team Lead",                Description = "Have a 30-minute intro call with the Finance Lead. Discuss your responsibilities, the monthly close schedule, and how finance collaborates with other departments." },
            new OnboardingTask { Id = 37, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 1, Title = "Complete Financial Compliance Training",    Description = "Finish the mandatory financial compliance and anti-fraud training modules on the LMS. Take note of the expense submission deadlines covered in the course." },
            new OnboardingTask { Id = 38, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekTwo,   DisplayOrder = 2, Title = "Understand the Expense Reporting Process",  Description = "Review the expense policy and submit a test expense report in QuickBooks to verify your access and understand the approval workflow." },
            new OnboardingTask { Id = 39, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekThree, DisplayOrder = 3, Title = "Review Budget Reporting Templates",         Description = "Open the standard monthly and quarterly budget report templates in the shared Finance drive. Familiarise yourself with the structure before the next reporting cycle." },
            new OnboardingTask { Id = 40, DepartmentId = 5, TimelinePhase = TimelinePhase.WeekFour,  DisplayOrder = 1, Title = "Contribute to Your First Monthly Report",   Description = "Take ownership of one section of the monthly financial report. Prepare the data, validate figures with a colleague, and submit it ahead of the close deadline." }
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

        // ── TaskInstructions ─────────────────────────────────────────────────
        modelBuilder.Entity<TaskInstruction>(entity =>
        {
            entity.HasKey(ti => ti.Id);
            entity.Property(ti => ti.Id).UseIdentityColumn();
            entity.Property(ti => ti.Text).IsRequired().HasMaxLength(500);

            entity.HasOne(ti => ti.Task)
                .WithMany(t => t.Instructions)
                .HasForeignKey(ti => ti.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── OnboardingProgress ───────────────────────────────────────────────
        modelBuilder.Entity<OnboardingProgress>(entity =>
        {
            // Composite primary key: (SessionToken, TaskId)
            entity.HasKey(op => new { op.SessionToken, op.TaskId });

            entity.HasOne(op => op.Task)
                .WithMany()
                .HasForeignKey(op => op.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed TaskInstructions programmatically (120 records total, 3 per task)
        var instructions = new List<TaskInstruction>();
        for (int taskId = 1; taskId <= 40; taskId++)
        {
            instructions.Add(new TaskInstruction
            {
                Id = taskId * 3 - 2,
                TaskId = taskId,
                StepNumber = 1,
                Text = "Read and understand all requirements for this task."
            });
            instructions.Add(new TaskInstruction
            {
                Id = taskId * 3 - 1,
                TaskId = taskId,
                StepNumber = 2,
                Text = "Sync with your onboarding buddy or team lead if any blockers arise."
            });
            instructions.Add(new TaskInstruction
            {
                Id = taskId * 3,
                TaskId = taskId,
                StepNumber = 3,
                Text = "Verify and check off the item in your workspace dashboard once done."
            });
        }
        modelBuilder.Entity<TaskInstruction>().HasData(instructions);
    }
}
