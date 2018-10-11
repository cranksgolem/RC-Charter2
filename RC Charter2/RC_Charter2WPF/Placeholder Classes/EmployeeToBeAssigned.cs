using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using RC_Charter2.Models;

namespace RC_Charter2WPF.Placeholder_Classes
{
	public class EmployeeToBeAssigned : ObservableObject
	{
		public EmployeeToBeAssigned()
		{
			CrewAssignments = new HashSet<CrewAssignment>();
			Results = new HashSet<Result>();
			Licensures = new HashSet<Licensure>();
			CharterFlightCharges = new HashSet<CharterFlightCharge>();
			Licenses = new HashSet<License>();
			SelectedLicense = new License {Description = "default"};
		}

		public int? EmployeeId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public License SelectedLicense { get; set; }
		public bool? IsExtraCrew { get; set; }
		private string _status;

		public ICollection<CrewAssignment> CrewAssignments { get; set; }
		public ICollection<Result> Results { get; set; }
		public ICollection<Licensure> Licensures { get; set; }
		public ICollection<CharterFlightCharge> CharterFlightCharges { get; set; }
		public ICollection<License> Licenses { get; set; }

		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				RaisePropertyChanged(nameof(Status));
			}
		}

	}
}
