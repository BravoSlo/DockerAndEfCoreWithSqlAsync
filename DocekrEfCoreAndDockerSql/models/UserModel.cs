using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace DocekrEfCoreAndDockerSql.models {
	[Table("Users")]
	public class UserModel {

		#region // properties //
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long id { get; set; }
		[Required]
		public DateTime? createdDate { get; set; } = DateTime.UtcNow;
		[Required]
		public DateTime? modifiedDate { get; set; } = DateTime.UtcNow;
		[Timestamp, Required]
		public byte[] timestamp { get; set; }
		public string fname { get; set; } = "";
		public string name { get; set; } = "";
		public string country { get; set; } = "";
		public string callsign { get; set; } = "";
		public string city { get; set; } = "";
		public string surname { get; set; } = "";
		public int radio_id { get; set; } = 0;
		public string remarks { get; set; } = "";
		public string state { get; set; } = "";
		#endregion

		#region // model builder //
		public virtual void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<UserModel>().Property(i => i.createdDate).HasDefaultValueSql("getutcdate()");
			modelBuilder.Entity<UserModel>().Property(i => i.modifiedDate).HasDefaultValueSql("getutcdate()");
			modelBuilder.Entity<UserModel>().HasIndex(i => new { i.id, i.timestamp });
		}
		#endregion


	}
}
