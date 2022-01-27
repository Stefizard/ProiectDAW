using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Models;
using ProiectDAW.Models.Base;

namespace ProiectDAW.Data
{
    public class Context:DbContext
    {
        public DbSet<Comanda> Comenzi { get; set; }
        public DbSet<Credentiale> Credentiale { get; set; }
        public DbSet<ListaProduse> ListeProduse { get; set; }
        public DbSet<Produs> Produse { get; set; }
        public DbSet<User> Useri { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comanda>()
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Comanda>()
                .Property(e => e.DateModified)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Credentiale>()
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Credentiale>()
                .Property(e => e.DateModified)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ListaProduse>()
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<ListaProduse>()
                .Property(e => e.DateModified)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Produs>()
                .Property(e => e.DateCreated)
                 .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Produs>()
                .Property(e => e.DateModified)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<User>()
                .Property(e => e.DateModified)
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
