using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SarayaAdmin.Entity.Model {
    public partial class AppDbContext : DbContext {
        public AppDbContext () { }

        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        public virtual DbSet<Credentials> Credentials { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuRoleMap> MenuRoleMap { get; set; }
        public virtual DbSet<Parameter> Parameter { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleMap> RoleMap { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.EnableDetailedErrors ();
            optionsBuilder.EnableSensitiveDataLogging ();

            if (!optionsBuilder.IsConfigured) {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql ("Host=localhost;Database=saraya;Username=postgres;Password=root");
            }
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation ("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Credentials> (entity => {
                entity.ToTable ("credentials");

                entity.Property (e => e.Id)
                    .HasColumnName ("id")
                    .UseNpgsqlIdentityByDefaultColumn ();

                entity.Property (e => e.CreatedBy)
                    .HasColumnName ("created_by")
                    .HasMaxLength (50);

                entity.Property (e => e.CreatedHost)
                    .HasColumnName ("created_host")
                    .HasMaxLength (20);

                entity.Property (e => e.CreatedOn).HasColumnName ("created_on");

                entity.Property (e => e.Email)
                    .IsRequired ()
                    .HasColumnName ("email")
                    .HasMaxLength (100);

                entity.Property (e => e.ModifiedBy)
                    .HasColumnName ("modified_by")
                    .HasMaxLength (50);

                entity.Property (e => e.ModifiedHost)
                    .HasColumnName ("modified_host")
                    .HasMaxLength (20);

                entity.Property (e => e.ModifiedOn).HasColumnName ("modified_on");

                entity.Property (e => e.Password)
                    .IsRequired ()
                    .HasColumnName ("password")
                    .HasMaxLength (255);

                entity.Property (e => e.PublicUser).HasColumnName ("public_user");

                entity.Property (e => e.Salt)
                    .HasColumnName ("salt")
                    .HasMaxLength (255)
                    .HasDefaultValueSql ("NULL::character varying");

                entity.Property (e => e.Status).HasColumnName ("status");

                entity.Property (e => e.Username)
                    .IsRequired ()
                    .HasColumnName ("username")
                    .HasMaxLength (50);

                entity.Property (e => e.VerificationCode)
                    .HasColumnName ("verification_code")
                    .HasColumnType ("character varying");
            });

            modelBuilder.Entity<Menu> (entity => {
                entity.ToTable ("menu");

                entity.Property (e => e.Id)
                    .HasColumnName ("id")
                    .UseNpgsqlIdentityByDefaultColumn ();

                entity.Property (e => e.Parent).HasColumnName ("parent");

                entity.Property (e => e.Status).HasColumnName ("status");

                entity.Property (e => e.Title)
                    .HasColumnName ("title")
                    .HasMaxLength (50);

                entity.Property (e => e.Url)
                    .HasColumnName ("url")
                    .HasMaxLength (100);

                entity.HasOne (d => d.ParentNavigation)
                    .WithMany (p => p.InverseParentNavigation)
                    .HasForeignKey (d => d.Parent)
                    .HasConstraintName ("fk_parent");
            });

            modelBuilder.Entity<MenuRoleMap> (entity => {
                entity.ToTable ("menu_role_map");

                entity.Property (e => e.Id)
                    .HasColumnName ("id")
                    .UseNpgsqlIdentityByDefaultColumn ();

                entity.Property (e => e.MenuId).HasColumnName ("menu_id");

                entity.Property (e => e.RoleId).HasColumnName ("role_id");

                entity.HasOne (d => d.Menu)
                    .WithMany (p => p.MenuRoleMap)
                    .HasForeignKey (d => d.MenuId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("menu_role_map_fk");

                entity.HasOne (d => d.Role)
                    .WithMany (p => p.MenuRoleMap)
                    .HasForeignKey (d => d.RoleId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("menu_role_map_fk_1");
            });

            modelBuilder.Entity<Parameter> (entity => {
                entity.ToTable ("parameter");

                entity.Property (e => e.Id)
                    .HasColumnName ("id")
                    .UseNpgsqlIdentityByDefaultColumn ();

                entity.Property (e => e.Code)
                    .IsRequired ()
                    .HasColumnName ("code")
                    .HasMaxLength (5);

                entity.Property (e => e.CreatedBy)
                    .HasColumnName ("created_by")
                    .HasMaxLength (50);

                entity.Property (e => e.CreatedHost)
                    .HasColumnName ("created_host")
                    .HasMaxLength (20);

                entity.Property (e => e.CreatedOn).HasColumnName ("created_on");

                entity.Property (e => e.Description)
                    .IsRequired ()
                    .HasColumnName ("description")
                    .HasMaxLength (100);

                entity.Property (e => e.ModifiedBy)
                    .HasColumnName ("modified_by")
                    .HasMaxLength (50);

                entity.Property (e => e.ModifiedHost)
                    .HasColumnName ("modified_host")
                    .HasColumnType ("character varying");

                entity.Property (e => e.ModifiedOn).HasColumnName ("modified_on");

                entity.Property (e => e.Name)
                    .IsRequired ()
                    .HasColumnName ("name")
                    .HasMaxLength (100);
            });

            modelBuilder.Entity<Role> (entity => {
                entity.ToTable ("role");

                entity.Property (e => e.Id)
                    .HasColumnName ("id")
                    .UseNpgsqlIdentityByDefaultColumn ();

                entity.Property (e => e.Name)
                    .IsRequired ()
                    .HasColumnName ("name")
                    .HasMaxLength (50);

                entity.Property (e => e.Status).HasColumnName ("status");
            });

            modelBuilder.Entity<RoleMap> (entity => {
                entity.ToTable ("role_map");

                entity.Property (e => e.Id)
                    .HasColumnName ("id")
                    .UseNpgsqlIdentityByDefaultColumn ();

                entity.Property (e => e.CredentialId).HasColumnName ("credential_id");

                entity.Property (e => e.RoleId).HasColumnName ("role_id");

                entity.HasOne (d => d.Credential)
                    .WithMany (p => p.RoleMap)
                    .HasForeignKey (d => d.CredentialId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("role_map_credentials_fk");

                entity.HasOne (d => d.Role)
                    .WithMany (p => p.RoleMap)
                    .HasForeignKey (d => d.RoleId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("role_map_role_fk");
            });

            modelBuilder.Entity<User> (entity => {
                entity.ToTable ("user");

                entity.Property (e => e.Id)
                    .HasColumnName ("id")
                    .UseNpgsqlIdentityByDefaultColumn ();

                entity.Property (e => e.CreatedBy)
                    .HasColumnName ("created_by")
                    .HasMaxLength (50);

                entity.Property (e => e.CreatedHost)
                    .HasColumnName ("created_host")
                    .HasMaxLength (20);

                entity.Property (e => e.CreatedOn).HasColumnName ("created_on");

                entity.Property (e => e.CredentialId).HasColumnName ("credential_id");

                entity.Property (e => e.FirstName)
                    .HasColumnName ("first_name")
                    .HasMaxLength (50);

                entity.Property (e => e.LastName)
                    .HasColumnName ("last_name")
                    .HasMaxLength (50);

                entity.Property (e => e.ModifiedBy)
                    .HasColumnName ("modified_by")
                    .HasMaxLength (50);

                entity.Property (e => e.ModifiedHost)
                    .HasColumnName ("modified_host")
                    .HasMaxLength (20);

                entity.Property (e => e.ModifiedOn).HasColumnName ("modified_on");

                entity.Property (e => e.Phone)
                    .HasColumnName ("phone")
                    .HasColumnType ("character varying");

                entity.HasOne (d => d.Credential)
                    .WithMany (p => p.User)
                    .HasForeignKey (d => d.CredentialId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("user_fk");
            });

            modelBuilder.Entity<Credentials> ().HasData (
                new Credentials {
                    Id = 1,
                        Email = "admin@admin.com",
                        Status = true,
                        Password = "CiIzQaA1vmGP2Pkrcp2CDVelxx2w4Nw3qgvkgwcrf1c=",
                        Username = "administrator",
                        Salt = "Ef+5WLoTHpQ3nGH+uDko+w==",
                        CreatedBy = "System",
                        CreatedOn = DateTime.Now
                }
            );

            modelBuilder.Entity<Role> ().HasData (
                new Role {
                    Id = 1,
                        Name = "System Admin",
                        Status = true
                }
            );

            modelBuilder.Entity<Menu> ().HasData (
                new Menu {
                    Id = 1,
                        Title = "Master Data",
                        Url = "#",
                        Parent = null,
                        Status = true
                },
                new Menu {
                    Id = 2,
                        Title = "User",
                        Url = "/master/user",
                        Parent = 1,
                        Status = true
                },
                new Menu {
                    Id = 3,
                        Title = "Menu",
                        Url = "/master/menu",
                        Parent = 1,
                        Status = true
                },
                new Menu {
                    Id = 4,
                        Title = "Role",
                        Url = "/master/role",
                        Parent = 1,
                        Status = true
                },
                new Menu {
                    Id = 5,
                        Title = "User Role Map",
                        Url = "/master/user-role-map",
                        Parent = 1,
                        Status = true
                },
                new Menu {
                    Id = 6,
                        Title = "Menu Role Map",
                        Url = "/master/menu-role-map",
                        Parent = 1,
                        Status = true
                }
            );

            modelBuilder.Entity<RoleMap> ().HasData (
                new RoleMap {
                    Id = 1,
                        RoleId = 1,
                        CredentialId = 1
                }
            );

            modelBuilder.Entity<MenuRoleMap> ().HasData (
                new MenuRoleMap {
                    Id = 1,
                        RoleId = 1,
                        MenuId = 1
                },
                new MenuRoleMap {
                    Id = 2,
                        RoleId = 1,
                        MenuId = 2
                },
                new MenuRoleMap {
                    Id = 3,
                        RoleId = 1,
                        MenuId = 3
                },
                new MenuRoleMap {
                    Id = 4,
                        RoleId = 1,
                        MenuId = 4
                },
                new MenuRoleMap {
                    Id = 5,
                        RoleId = 1,
                        MenuId = 5
                },
                new MenuRoleMap {
                    Id = 6,
                        RoleId = 1,
                        MenuId = 6
                }
            );
        }
    }
}