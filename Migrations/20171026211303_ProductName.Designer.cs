using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ECommerce.Models;

namespace ECommerce.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20171026211303_ProductName")]
    partial class ProductName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.3");

            modelBuilder.Entity("ECommerce.Models.Customer", b =>
                {
                    b.Property<int>("customer_id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("name");

                    b.HasKey("customer_id");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("ECommerce.Models.Order", b =>
                {
                    b.Property<int>("order_id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<int>("customer_id");

                    b.Property<int>("product_id");

                    b.Property<int>("quantity");

                    b.HasKey("order_id");

                    b.HasIndex("customer_id");

                    b.HasIndex("product_id");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("ECommerce.Models.Product", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<string>("image_link");

                    b.Property<string>("name");

                    b.Property<int>("quantity");

                    b.HasKey("product_id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("ECommerce.Models.Order", b =>
                {
                    b.HasOne("ECommerce.Models.Customer", "Recipient")
                        .WithMany("Purchases")
                        .HasForeignKey("customer_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECommerce.Models.Product", "Item")
                        .WithMany("Orders")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
