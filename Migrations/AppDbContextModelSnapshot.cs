﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PISWF;

#nullable disable

namespace PISWF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PISWF.domain.registermc.model.entity.FileDocument", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_type");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long?>("RegisterMCId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RegisterMCId");

                    b.ToTable("file_document");
                });

            modelBuilder.Entity("PISWF.domain.registermc.model.entity.RegisterMC", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("AmountMoney")
                        .HasColumnType("double precision")
                        .HasColumnName("amount_money");

                    b.Property<long>("MunicipalityId")
                        .HasColumnType("bigint");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<double>("ShareFundsSubvention")
                        .HasColumnType("double precision")
                        .HasColumnName("share_funds_subvention");

                    b.Property<double>("SubventionShare")
                        .HasColumnType("double precision")
                        .HasColumnName("subvention_share");

                    b.Property<DateTime>("ValidDate")
                        .HasColumnType("Date")
                        .HasColumnName("valid_date");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("register-m-c");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.auth.model.entity.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long?>("VisibilityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("VisibilityId");

                    b.ToTable("role");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.auth.model.entity.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<long?>("MunicipalityId")
                        .HasColumnType("bigint");

                    b.Property<long?>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<long>("Password")
                        .HasColumnType("bigint")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.HasKey("Login");

                    b.HasIndex("MunicipalityId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.auth.model.entity.Visibility", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Rate")
                        .HasColumnType("text")
                        .HasColumnName("rate");

                    b.HasKey("Id");

                    b.ToTable("visibility");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.logger.model.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AuthorLogin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JsonEntity")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("json_entity");

                    b.Property<DateTime>("LogDate")
                        .HasColumnType("Date")
                        .HasColumnName("log_date");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("method_name");

                    b.HasKey("Id");

                    b.HasIndex("AuthorLogin");

                    b.ToTable("log");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.muni_org.model.entity.Municipality", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("municipality");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.muni_org.model.entity.Organization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("organization");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<long>("RolesId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsersLogin")
                        .HasColumnType("text");

                    b.HasKey("RolesId", "UsersLogin");

                    b.HasIndex("UsersLogin");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("PISWF.domain.registermc.model.entity.FileDocument", b =>
                {
                    b.HasOne("PISWF.domain.registermc.model.entity.RegisterMC", null)
                        .WithMany("Documents")
                        .HasForeignKey("RegisterMCId");
                });

            modelBuilder.Entity("PISWF.domain.registermc.model.entity.RegisterMC", b =>
                {
                    b.HasOne("PISWF.infrasrtucture.muni_org.model.entity.Municipality", "Municipality")
                        .WithMany()
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PISWF.infrasrtucture.muni_org.model.entity.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipality");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.auth.model.entity.Role", b =>
                {
                    b.HasOne("PISWF.infrasrtucture.auth.model.entity.Visibility", "Visibility")
                        .WithMany()
                        .HasForeignKey("VisibilityId");

                    b.Navigation("Visibility");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.auth.model.entity.User", b =>
                {
                    b.HasOne("PISWF.infrasrtucture.muni_org.model.entity.Municipality", "Municipality")
                        .WithMany()
                        .HasForeignKey("MunicipalityId");

                    b.HasOne("PISWF.infrasrtucture.muni_org.model.entity.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.Navigation("Municipality");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("PISWF.infrasrtucture.logger.model.Log", b =>
                {
                    b.HasOne("PISWF.infrasrtucture.auth.model.entity.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("PISWF.infrasrtucture.auth.model.entity.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PISWF.infrasrtucture.auth.model.entity.User", null)
                        .WithMany()
                        .HasForeignKey("UsersLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PISWF.domain.registermc.model.entity.RegisterMC", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
