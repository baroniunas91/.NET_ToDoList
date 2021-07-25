﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.RestfulAPI.Data;

namespace ToDoList.RestfulAPI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20210725171517_AddUserIdToTodoModel")]
    partial class AddUserIdToTodoModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ToDoList.RestfulAPI.Models.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsDone")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("ToDoList.RestfulAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmailAddress = "admin@test.lt",
                            Password = "labasrytas123",
                            Role = "admin"
                        },
                        new
                        {
                            Id = 2,
                            EmailAddress = "user1@test.lt",
                            Password = "labasrytas123",
                            Role = "user"
                        },
                        new
                        {
                            Id = 3,
                            EmailAddress = "user2@test.lt",
                            Password = "labasrytas123",
                            Role = "user"
                        });
                });

            modelBuilder.Entity("ToDoList.RestfulAPI.Models.Todo", b =>
                {
                    b.HasOne("ToDoList.RestfulAPI.Models.User", "User")
                        .WithMany("Todos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.RestfulAPI.Models.User", b =>
                {
                    b.Navigation("Todos");
                });
#pragma warning restore 612, 618
        }
    }
}