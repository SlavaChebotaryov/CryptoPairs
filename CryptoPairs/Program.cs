using CryptoPairs.Intarfaces;
using CryptoPairs.Services;

namespace CryptoPairs
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();
			builder.Services.AddTransient<IPairsProvider, PairsProvider>();
			var cryptoCurrencyProvider = new CryptoCurrencyProvider();
			var cryptoCurrencies = cryptoCurrencyProvider.ReadCryptoCurrenciesFromFile("result-full-coins.txt");

			builder.Services.AddTransient<IPairsProvider>(provider => new PairsProvider(cryptoCurrencies));

			var app = builder.Build();

			app.UseStaticFiles();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=CryptoCurrency}/{action=Index}/{id?}");

			app.Run();
		}
	}
}