using Microsoft.EntityFrameworkCore;

using DocekrEfCoreAndDockerSql.models;

namespace DocekrEfCoreAndDockerSql.contexts {
	public class DataContext : DbContext {

		#region // ctor //
		public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions) { }
		#endregion

		#region // model builder //
		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			new UserModel().OnModelCreating(modelBuilder);
		}
		#endregion

		#region // dbset //
		public virtual DbSet<UserModel> users { get; set; }
		#endregion

	}
}
