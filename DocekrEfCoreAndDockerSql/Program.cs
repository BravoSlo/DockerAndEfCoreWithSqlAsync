using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using DocekrEfCoreAndDockerSql.contexts;
using DocekrEfCoreAndDockerSql.models;

namespace DocekrEfCoreAndDockerSql {
	class Program {
		static void Main(string[] args) {

			var configurationBuilder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("config.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();
			var configuration = configurationBuilder.Build();

			var serviceCollection = new ServiceCollection();
			serviceCollection.AddSingleton(typeof(IConfiguration), configuration);


			serviceCollection.AddDbContext<DataContext>(cfg => {
				cfg.UseSqlServer(
					configuration.GetConnectionString("DataContext"),
					opt => opt.MaxBatchSize(1000));
			});

			IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

			Console.WriteLine("Starting test ...");

			DemoMain demoMain = new DemoMain(serviceProvider);
			demoMain.Run();
			//demoMain.RunAsync();

			Console.WriteLine("Done");
			Console.ReadLine();
		}


	}
}
