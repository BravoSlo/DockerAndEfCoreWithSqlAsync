using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using DocekrEfCoreAndDockerSql.contexts;
using DocekrEfCoreAndDockerSql.models;

namespace DocekrEfCoreAndDockerSql {
	public class DemoMain {

		#region // locals //
		IServiceProvider serviceProvider;
		DataContext db;
		#endregion

		#region // ctor //
		public DemoMain(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
			this.db = serviceProvider.GetRequiredService<DataContext>();
			db.Database.EnsureCreated();
		}
		#endregion

		#region // public //
		public void Run() {
			demoRun();
		}
		public void RunAsync() {
			demoRunAsync().ConfigureAwait(false).GetAwaiter().GetResult();
		}
		#endregion

		#region // private //
		private void demoRun() {
			Console.WriteLine($"-> start");
			string jsonData = File.ReadAllText("data.json");
			UserWrapModel userWrap = JsonConvert.DeserializeObject<UserWrapModel>(jsonData);

			Console.WriteLine($"--> remove existing data start {db.users.Count()}");
			db.users.RemoveRange(db.users);
			db.SaveChanges();
			Console.WriteLine($"--> remove existing data end {db.users.Count()}");

			Console.WriteLine($"--> fill user data start {userWrap.users.Count}");
			db.users.AddRange(userWrap.users.ToArray());
			db.SaveChanges();
			Console.WriteLine($"--> fill user data end {db.users.Count()}");
		}
		private async Task demoRunAsync() {
			Console.WriteLine($"-> start");
			string jsonData = File.ReadAllText("data.json");
			UserWrapModel userWrap = JsonConvert.DeserializeObject<UserWrapModel>(jsonData);

			Console.WriteLine($"--> remove existing data start {db.users.Count()}");
			db.users.RemoveRange(db.users);
			await db.SaveChangesAsync();
			Console.WriteLine($"--> remove existing data end {db.users.Count()}");

			Console.WriteLine($"--> fill user data start {userWrap.users.Count}");
			db.users.AddRange(userWrap.users.ToArray());
			await db.SaveChangesAsync();
			Console.WriteLine($"--> fill user data end {db.users.Count()}");
		}
		#endregion

	}
}
