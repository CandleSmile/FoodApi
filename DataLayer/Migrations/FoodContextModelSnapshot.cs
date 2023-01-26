﻿// <auto-generated />
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(FoodContext))]
    partial class FoodContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataLayer.Items.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = new byte[] { 74, 12, 236, 106, 16, 39, 237, 202, 101, 103, 155, 17, 154, 105, 202, 222, 40, 195, 252, 207, 21, 51, 97, 252, 219, 62, 77, 208, 72, 226, 184, 20, 106, 49, 168, 131, 182, 213, 71, 224, 204, 220, 70, 16, 56, 179, 96, 186, 210, 126, 159, 218, 76, 8, 175, 104, 143, 238, 229, 47, 236, 149, 49, 121 },
                            PasswordSalt = new byte[] { 238, 242, 120, 212, 79, 197, 92, 142, 63, 199, 176, 100, 159, 11, 146, 23, 231, 86, 153, 142, 90, 114, 67, 237, 122, 129, 223, 255, 69, 78, 142, 76, 171, 76, 35, 57, 108, 58, 249, 103, 100, 227, 190, 59, 134, 181, 141, 83, 196, 202, 222, 147, 216, 71, 246, 81, 65, 92, 210, 2, 130, 150, 185, 184, 248, 63, 63, 99, 208, 91, 0, 254, 200, 32, 229, 205, 44, 209, 221, 71, 168, 232, 16, 64, 220, 210, 7, 168, 231, 46, 146, 101, 149, 190, 210, 166, 7, 242, 161, 150, 62, 82, 49, 15, 34, 134, 126, 206, 27, 124, 248, 38, 17, 142, 13, 72, 97, 64, 238, 72, 218, 52, 236, 50, 167, 179, 39, 189 },
                            Username = "lizaveta.razumovich@gmail.com"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
