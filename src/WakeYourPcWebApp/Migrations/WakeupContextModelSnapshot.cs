using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WakeYourPcWebApp.Models;

namespace WakeYourPcWebApp.Migrations
{
    [DbContext(typeof(WakeupContext))]
    partial class WakeupContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WakeYourPcWebApp.Models.Machine", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HostName");

                    b.Property<string>("MacAddress");

                    b.Property<string>("MachineName");

                    b.Property<bool?>("ShouldWakeup");

                    b.Property<int>("State");

                    b.Property<string>("Username");

                    b.HasKey("Guid");

                    b.HasIndex("Username");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("WakeYourPcWebApp.Models.User", b =>
                {
                    b.Property<string>("Username");

                    b.Property<string>("Password");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WakeYourPcWebApp.Models.Machine", b =>
                {
                    b.HasOne("WakeYourPcWebApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username");
                });
        }
    }
}
