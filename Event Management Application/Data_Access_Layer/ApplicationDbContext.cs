using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<GuestList> GuestLists { get; set; }
    public DbSet<Budget> Budgets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Event to Organizer relationship (1-to-many)
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Organizer)
            .WithMany(o => o.Events)
            .HasForeignKey(e => e.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Event to Attendees relationship (many-to-many)
        modelBuilder.Entity<Event>()
            .HasMany(e => e.Attendees)
            .WithMany(a => a.RegisteredEvents)
            .UsingEntity(j => j.ToTable("EventAttendees"));

        // GuestList relationships (many-to-one)
        modelBuilder.Entity<GuestList>()
            .HasOne(gl => gl.Event)
            .WithMany()
            .HasForeignKey(gl => gl.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GuestList>()
            .HasOne(gl => gl.Attendee)
            .WithMany()
            .HasForeignKey(gl => gl.AttendeeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Budget to Event relationship (one-to-one or one-to-many depending on your use case)
        modelBuilder.Entity<Budget>()
            .HasOne(b => b.Event)
            .WithMany()
            .HasForeignKey(b => b.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
