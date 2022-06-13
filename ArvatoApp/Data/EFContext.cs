using ArvatoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ArvatoApp.Data {
	public interface IEFContext {
		public DbSet<CustomerTicket> CustomerTickets { get; set; }
		public DbSet<AppErrorLog> AppErrorLogs { get; set; }
	}
	public class EFContext : DbContext, IEFContext {
		public DbSet<CustomerTicket> CustomerTickets { get; set; }
		public DbSet<AppErrorLog> AppErrorLogs { get; set; }
		public EFContext(DbContextOptions<EFContext> options) : base(options) {
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<CustomerTicket>().ToTable("CustomerTickets");
			modelBuilder.Entity<AppErrorLog>().ToTable("AppErrorLogs");
		}
	}
}
