using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Blog.Data.Migrations;
using Blog.Domain.Models;

namespace Blog.Data
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
        Justification = "Inherited from IDbContext interface, which exists to support using.")]
    public partial class BlogContext : DbContext, IUnitOfWork
    {
        public BlogContext() : base(nameOrConnectionString: "BlogDb") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); //Todo WillCascadeOnDelete(false)

            SetupCategoryEntity(modelBuilder);
            SetupContactEntity(modelBuilder);
            SetupMembershipEntity(modelBuilder);
            SetupPostEntity(modelBuilder);
            SetupRoleEntity(modelBuilder);
            SetupTagEntity(modelBuilder);
            SetupUserEntity(modelBuilder);

            // EF (4.3 and up) auto-migrations initializer
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, Configuration>());
        }

        private static void SetupCategoryEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.CategoryId)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Category>().Property(c => c.UrlSlug).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Category>().Property(c => c.Description).IsMaxLength();
            modelBuilder.Entity<Category>().HasMany(c => c.Posts);
        }

        private static void SetupContactEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasKey(c => c.ContactId);
            modelBuilder.Entity<Contact>().Property(c => c.ContactId)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Contact>().Property(c => c.Name).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Contact>().Property(c => c.Email).IsRequired().HasMaxLength(250);
            modelBuilder.Entity<Contact>().Property(c => c.Website).IsOptional().HasMaxLength(500);
            modelBuilder.Entity<Contact>().Property(c => c.Body).IsRequired().IsMaxLength();
        }

        private static void SetupMembershipEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>().HasKey(m => m.UserId);
            modelBuilder.Entity<Membership>().Property(m => m.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Membership>().Property(m => m.CreateDate).IsOptional();
            modelBuilder.Entity<Membership>().Property(m => m.ConfirmationToken).IsOptional().HasMaxLength(128);
            modelBuilder.Entity<Membership>().Property(m => m.IsConfirmed).IsOptional();
            modelBuilder.Entity<Membership>().Property(m => m.LastPasswordFailureDate).IsOptional();
            modelBuilder.Entity<Membership>().Property(m => m.PasswordFailuresSinceLastSuccess).IsOptional();
            modelBuilder.Entity<Membership>().Property(m => m.Password).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Membership>().Property(m => m.PasswordChangedDate).IsOptional();
            modelBuilder.Entity<Membership>().Property(m => m.PasswordSalt).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Membership>().Property(m => m.PasswordVerificationToken).IsOptional().HasMaxLength(128);
            modelBuilder.Entity<Membership>().Property(m => m.PasswordVerificationTokenExpirationDate).IsOptional();
            modelBuilder.Entity<Membership>().ToTable("webpages_Membership");
        }

        private static void SetupPostEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasKey(p => p.PostId);
            modelBuilder.Entity<Post>().Property(p => p.PostId)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Post>().Property(p => p.ShortDescription).IsRequired().IsMaxLength();
            modelBuilder.Entity<Post>().Property(p => p.Description).IsRequired().IsMaxLength();
            modelBuilder.Entity<Post>().Property(p => p.Meta).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<Post>().Property(p => p.UrlSlug).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<Post>().Property(p => p.Published);
            modelBuilder.Entity<Post>().Property(p => p.PostedOn).IsRequired();
            modelBuilder.Entity<Post>().Property(p => p.Modified).IsOptional();
            modelBuilder.Entity<Post>().HasRequired(p => p.Category);
            modelBuilder.Entity<Post>().HasMany(p => p.Tags);
        }

        private static void SetupRoleEntity(DbModelBuilder modelBuilder)
        {
            //Role mappings
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Role>().Property(r => r.RoleName).IsOptional().HasMaxLength(256);
            modelBuilder.Entity<Role>().HasMany(r => r.Users).WithOptional();
            modelBuilder.Entity<Role>().ToTable("webpages_Roles");
        }

        private static void SetupTagEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasKey(t => t.TagId);
            modelBuilder.Entity<Tag>().Property(t => t.TagId)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Tag>().Property(t => t.Name).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Tag>().Property(t => t.UrlSlug).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Tag>().Property(t => t.Description).IsMaxLength();
            modelBuilder.Entity<Tag>().HasMany(t => t.Posts).WithMany(p => p.Tags).Map(pt =>
            {
                pt.ToTable("PostTag");
                pt.MapLeftKey("TagId");
                pt.MapRightKey("PostId");
            }); 
        }

        private static void SetupUserEntity(DbModelBuilder modelBuilder)
        {
            //User mappings
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<User>().HasMany(u => u.Roles);
            modelBuilder.Entity<User>().ToTable("UserProfile");

            modelBuilder.Entity<User>()
            .HasMany<Role>(r => r.Roles)
            .WithMany(u => u.Users)
            .Map(m =>
            {
                m.ToTable("webpages_UsersInRoles");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            //Example usage
            var type = typeof(T);
            //if (type == typeof(AssignmentContent))
            //{
            //    return (IQueryable<T>)AssignmentContents.Include(ac => ac.Language);
            //}
            //if (type == typeof(CourseProgram))
            //{
            //    return
            //        (IQueryable<T>)
            //        CoursePrograms.Include(cp => cp.Schedules.Select(s => s.Students.Select(st => st.Person)
            //                                                               .Select(p => p.Address)
            //                                                               .Select(a => a.Country)));
            //}
            //if (type == typeof(DiscussionContent))
            //{
            //    return (IQueryable<T>)DiscussionContents.Include(dc => dc.Language);
            //}
            return (IQueryable<T>)Set(type);
        }

        #region IUnitOfWork Members

        void IUnitOfWork.SaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb, ex
                ); // Add the original exception as the innerException
            }
        }

        #endregion
    }
}
