using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RC_Charter2.Models
{
	class RC_Charter2Context : DbContext
	{
		public virtual DbSet<Aircraft> Aircraft { get; set; }
		public virtual DbSet<AircraftProperties> AircraftProperties { get; set; }
		public virtual DbSet<BalanceHistory> BalanceHistories { get; set; }
		public virtual DbSet<Cash> Cash { get; set; }
		public virtual DbSet<CharterFlightCharge> CharterFlightCharges { get; set; }
		public virtual DbSet<CharterTrip> CharterTrips { get; set; }
		public virtual DbSet<Check> Checks { get; set; }
		public virtual DbSet<CrewAssignment> CrewAssignments { get; set; }
		public virtual DbSet<CrewRequirement> CrewRequirements { get; set; }
		public virtual DbSet<Customer> Customers { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Flight> Flights { get; set; }
		public virtual DbSet<License> Licenses { get; set; }
		public virtual DbSet<Licensure> Licensures { get; set; }
		public virtual DbSet<PaymentMode> PaymentModes { get; set; }
		public virtual DbSet<Result> Results { get; set; }
		public virtual DbSet<Test> Tests { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer(
					"Server =.\\SQLExpress; Database = RCCharter2; Trusted_Connection = True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Aircraft>(entity =>
			{
				entity.ToTable("Aircraft");

				entity.HasKey(e => e.AircraftNumber);
				entity.Property(e => e.AircraftNumber).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.AircraftPropertiesId).IsRequired(false);

				entity.Property(e => e.ChargePerMile).HasColumnType("money").IsRequired(false);

				entity.Property(e => e.HourlyWaitingCharge).HasColumnType("money").IsRequired(false);

				entity.Property(e => e.Model).IsRequired(false);

				entity.Property(e => e.NumberOfSeats).IsRequired(false);

				entity.Property(e => e.Weight).HasColumnType("float").IsRequired(false);

				entity.Property(e => e.YearManufactured).IsRequired(false);

				entity.HasOne(d => d.AircraftProperties)
					.WithMany(p => p.Aircraft)
					.HasForeignKey(d => d.AircraftPropertiesId);
			});

			modelBuilder.Entity<AircraftProperties>(entity =>
			{
				entity.ToTable("AircraftProperty");

				entity.HasKey(e => e.AircraftPropertiesId);
				entity.Property(e => e.AircraftPropertiesId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.AircraftType).IsRequired(false);

				entity.Property(e => e.AircraftCategory).IsRequired(false);

				entity.Property(e => e.EngineType).IsRequired(false);

				entity.Property(e => e.AircraftWeight).IsRequired(false);

			});

			modelBuilder.Entity<BalanceHistory>(entity =>
			{
				entity.ToTable("BalanceHistory");

				entity.HasKey(e => e.BalanceHistoryId);
				entity.Property(e => e.BalanceHistoryId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.CharterTripId).IsRequired(false);

				entity.Property(e => e.RemainingBalance).HasColumnType("money").IsRequired(false);

				entity.Property(e => e.DatePaid).HasColumnType("date").IsRequired(false);

				entity.Property(e => e.ModeOfPayment).IsRequired(false);

				entity.HasOne(d => d.CharterTrip)
					.WithMany(p => p.BalanceHistories)
					.HasForeignKey(d => d.CharterTripId);
			});

			modelBuilder.Entity<Cash>(entity =>
			{
				entity.ToTable("Cash");

				entity.HasKey(e => e.CashId);
				entity.Property(e => e.CashId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.PaymentModeId).IsRequired(false);

				entity.Property(e => e.Amount).HasColumnType("money").IsRequired(false);

				entity.Property(e => e.Date).HasColumnType("date").IsRequired(false);
			});

			modelBuilder.Entity<CharterFlightCharge>(entity =>
			{
				entity.ToTable("CharterFlightCharge");

				entity.HasKey(e => e.CharterFlightChargeId);
				entity.Property(e => e.CharterFlightChargeId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.CharterTripId).IsRequired(false);


				entity.Property(e => e.EmployeeId).IsRequired(false);

				entity.Property(e => e.ChargeType).IsRequired(false);

				entity.Property(e => e.Amount).HasColumnType("money").IsRequired(false);

				entity.Property(e => e.Quantity).HasColumnType("float").IsRequired(false);

				entity.HasOne(d => d.CharterTrip)
					.WithMany(p => p.CharterFlightCharges)
					.HasForeignKey(d => d.CharterTripId);


				entity.HasOne(d => d.Employee)
					.WithMany(p => p.CharterFlightCharges)
					.HasForeignKey(d => d.EmployeeId).IsRequired(false);
			});

			modelBuilder.Entity<CharterTrip>(entity =>
			{
				entity.ToTable("CharterTrip");

				entity.HasKey(e => e.CharterTripId);
				entity.Property(e => e.CharterTripId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.AircraftNumber).IsRequired(false);

				entity.Property(e => e.CustomerId).IsRequired(false);

				entity.Property(e => e.TotalDistanceFlown).HasColumnType("float").IsRequired(false);

				entity.Property(e => e.TotalWaitingTime).IsRequired(false);

				entity.Property(e => e.TotalFuelUsage).HasColumnType("float").IsRequired(false);

				entity.Property(e => e.Origin).IsRequired(false);

				entity.Property(e => e.FinalDestination).IsRequired(false);

				entity.Property(e => e.Purpose).IsRequired(false);

				entity.Property(e => e.TotalCharge).HasColumnType("money").IsRequired(false);

				entity.Property(e => e.RemainingBalance).HasColumnType("money").IsRequired(false);

				entity.Property(e => e.DateLastPaid).HasColumnType("date").IsRequired(false);

				entity.HasOne(d => d.Aircraft)
					.WithMany(p => p.CharterTrips)
					.HasForeignKey(d => d.AircraftNumber);

				entity.HasOne(d => d.Customer)
					.WithMany(p => p.CharterTrips)
					.HasForeignKey(d => d.CustomerId);
			});

			modelBuilder.Entity<Check>(entity =>
			{
				entity.ToTable("Check");

				entity.HasKey(e => e.CheckId);
				entity.Property(e => e.CheckId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.PaymentModeId).IsRequired(false);

				entity.Property(e => e.CheckNumber).IsRequired(false);

				entity.Property(e => e.CheckDate).HasColumnType("date").IsRequired(false);

				entity.Property(e => e.Amount).HasColumnType("money").IsRequired(false);
			});

			modelBuilder.Entity<CrewAssignment>(entity =>
			{
				entity.ToTable("CrewAssignment");

				entity.HasKey(e => e.CrewAssignmentId);
				entity.Property(e => e.CrewAssignmentId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.EmployeeId).IsRequired(false);

				entity.Property(e => e.CharterTripId).IsRequired(false);

				entity.Property(e => e.DateAssigned).HasColumnType("date").IsRequired(false);

				entity.Property(e => e.Role).IsRequired(false);

				entity.Property(e => e.WorkHours).HasColumnType("float").IsRequired(false);

				entity.HasOne(d => d.Employee)
					.WithMany(p => p.CrewAssignments)
					.HasForeignKey(d => d.EmployeeId);

				entity.HasOne(d => d.CharterTrip)
					.WithMany(p => p.CrewAssignments)
					.HasForeignKey(d => d.CharterTripId);
			});

			modelBuilder.Entity<CrewRequirement>(entity =>
			{
				entity.ToTable("CrewRequirement");

				entity.HasKey(e => e.CrewRequirementId);
				entity.Property(e => e.CrewRequirementId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.LicenseType).IsRequired(false);

				entity.Property(e => e.AircraftPropertiesId).IsRequired(false);

				entity.Property(e => e.RequiredNumber).IsRequired(false);

				entity.HasOne(d => d.License)
					.WithMany(p => p.CrewRequirements)
					.HasForeignKey(d => d.LicenseType);

				entity.HasOne(d => d.AircraftProperties)
					.WithMany(p => p.CrewRequirements)
					.HasForeignKey(d => d.AircraftPropertiesId);
			});

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.ToTable("Customer");

				entity.HasKey(e => e.CustomerId);
				entity.Property(e => e.CustomerId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.Name).IsRequired(false);

				entity.Property(e => e.Address).IsRequired(false);

				entity.Property(e => e.AvailableCredits).HasColumnType("money").IsRequired(false);

				entity.Property(e => e.UsedCredits).HasColumnType("money").IsRequired(false);
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.ToTable("Employee");

				entity.HasKey(e => e.EmployeeId);
				entity.Property(e => e.EmployeeId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.Name).IsRequired(false);

				entity.Property(e => e.Address).IsRequired(false);
			});

			modelBuilder.Entity<Flight>(entity =>
			{
				entity.ToTable("Flight");

				entity.HasKey(e => e.FlightId);
				entity.Property(e => e.FlightId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.CharterTripId).IsRequired(false);

				entity.Property(e => e.Date).HasColumnType("date").IsRequired(false);

				entity.Property(e => e.TimeDeparture).IsRequired(false);

				entity.Property(e => e.TimeArrival).IsRequired(false);

				entity.Property(e => e.Origin).IsRequired(false);

				entity.Property(e => e.Destination).IsRequired(false);

				entity.Property(e => e.DistanceFlown).HasColumnType("float").IsRequired(false);

				entity.Property(e => e.WaitingTime).IsRequired(false);

				entity.Property(e => e.Order).IsRequired(false);

				entity.Property(e => e.FuelUsage).HasColumnType("float").IsRequired(false);

				entity.HasOne(d => d.CharterTrip)
					.WithMany(p => p.Flights)
					.HasForeignKey(d => d.CharterTripId);
			});

			modelBuilder.Entity<License>(entity =>
			{
				entity.ToTable("License");

				entity.HasKey(e => e.LicenseType);
				entity.Property(e => e.LicenseType).IsRequired();

				entity.Property(e => e.Description).IsRequired(false);
			});

			modelBuilder.Entity<Licensure>(entity =>
			{
				entity.ToTable("Licensure");

				entity.HasKey(e => e.LicensureId);
				entity.Property(e => e.LicensureId).IsRequired();

				entity.Property(e => e.LicenseType).IsRequired(false);

				entity.Property(e => e.EmployeeId).IsRequired(false);

				entity.Property(e => e.DateEarned).HasColumnType("date").IsRequired(false);

				entity.HasOne(d => d.License)
					.WithMany(p => p.Licensures)
					.HasForeignKey(d => d.LicenseType);

				entity.HasOne(d => d.Employee)
					.WithMany(p => p.Licensures)
					.HasForeignKey(d => d.EmployeeId);
			});

			modelBuilder.Entity<PaymentMode>(entity =>
			{
				entity.ToTable("PaymentMode");

				entity.HasKey(e => e.PaymentModeId);
				entity.Property(e => e.PaymentModeId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.CustomerId).IsRequired(false);

				entity.Property(e => e.CharterTripId).IsRequired(false);

				entity.Property(e => e.ModeOfPayment).IsRequired(false);

				entity.HasOne(d => d.Customer)
					.WithMany(p => p.PaymentModes)
					.HasForeignKey(d => d.CustomerId).IsRequired(false);

				entity.HasOne(d => d.CharterTrip)
					.WithMany(p => p.PaymentModes)
					.HasForeignKey(d => d.CharterTripId);

				entity.HasOne(d => d.Check)
					.WithOne(p => p.PaymentMode)
					.HasForeignKey<PaymentMode>(d => d.CheckId).IsRequired(false);

				entity.HasOne(d => d.Cash)
					.WithOne(p => p.PaymentMode)
					.HasForeignKey<PaymentMode>(d => d.CashId).IsRequired(false);
			});

			modelBuilder.Entity<Result>(entity =>
			{
				entity.ToTable("Result");

				entity.HasKey(e => e.ResultId);
				entity.Property(e => e.ResultId).ValueGeneratedOnAdd().IsRequired();

				entity.Property(e => e.TestCode).IsRequired(false);

				entity.Property(e => e.EmployeeId).IsRequired(false);

				entity.Property(e => e.TestDate).HasColumnType("date").IsRequired(false);

				entity.Property(e => e.TestResult).IsRequired(false);

				entity.Property(e => e.Expiration).HasColumnType("date").IsRequired(false);

				entity.HasOne(d => d.Employee)
					.WithMany(p => p.Results)
					.HasForeignKey(d => d.EmployeeId);

				entity.HasOne(d => d.Test)
					.WithMany(p => p.Results)
					.HasForeignKey(d => d.TestCode);
			});

			modelBuilder.Entity<Test>(entity =>
			{
				entity.ToTable("Test");

				entity.HasKey(e => e.TestCode);
				entity.Property(e => e.TestCode).IsRequired();

				entity.Property(e => e.TestDescription).IsRequired(false);

				entity.Property(e => e.TestFrequency).IsRequired(false);
			});
		}
	}
}
