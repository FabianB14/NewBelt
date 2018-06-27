﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using NewBelt.Models;
using System;

namespace NewBelt.Migrations
{
    [DbContext(typeof(BeltContext))]
    [Migration("20180626203835_Users")]
    partial class Users
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("NewBelt.Models.Likers", b =>
                {
                    b.Property<int>("LikersId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PostId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UsersId");

                    b.HasKey("LikersId");

                    b.HasIndex("PostId");

                    b.HasIndex("UsersId");

                    b.ToTable("likers");
                });

            modelBuilder.Entity("NewBelt.Models.Users", b =>
                {
                    b.Property<int>("UsersId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("UsersId");

                    b.ToTable("newusers");
                });

            modelBuilder.Entity("Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AliasName");

                    b.Property<int>("Likes");

                    b.Property<string>("Posts")
                        .IsRequired();

                    b.Property<int>("UsersId");

                    b.Property<int>("numParticipants");

                    b.HasKey("PostId");

                    b.HasIndex("UsersId");

                    b.ToTable("post");
                });

            modelBuilder.Entity("NewBelt.Models.Likers", b =>
                {
                    b.HasOne("Post", "Post")
                        .WithMany("PostThatAreLiked")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewBelt.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Post", b =>
                {
                    b.HasOne("NewBelt.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}