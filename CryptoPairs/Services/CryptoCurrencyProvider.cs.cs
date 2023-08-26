using CryptoPairs.Models;
using System.Collections.Generic;
using System.IO;

namespace CryptoPairs.Services
{
	public class CryptoCurrencyProvider
	{
		public List<CryptoCurrency> ReadCryptoCurrenciesFromFile(string filePath)
		{
			var lines = File.ReadAllLines(filePath);
			var cryptoCurrencies = lines.Select(line => new CryptoCurrency { Name = line }).ToList();

			var distinctCryptoCurrencies = cryptoCurrencies.GroupBy(c => c.Name)
														 .Select(group => group.First())
														 .OrderBy(c => c.Name)
														 .ToList();

			return distinctCryptoCurrencies;
		}

	}
}