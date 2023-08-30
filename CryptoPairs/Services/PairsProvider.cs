using CryptoPairs.Intarfaces;
using CryptoPairs.Models;

namespace CryptoPairs.Services
{
	public class PairsProvider : IPairsProvider
	{
		private const int QuantityPerPage = 20;
		private readonly IEnumerable<CryptoCurrency> _cryptoCurrencies;
		private readonly int cryptoCurrenciesCount;
		private readonly uint numberOfPairs;
		public PairsProvider(IEnumerable<CryptoCurrency> cryptoCurrencies)
		{
			_cryptoCurrencies = cryptoCurrencies;
			cryptoCurrenciesCount = cryptoCurrencies.Count();
			numberOfPairs = this.Count();
		}
		public uint Count() => ((uint)cryptoCurrenciesCount * ((uint)cryptoCurrenciesCount - 1)) / 2;

		public IEnumerable<string> GetPairs(int page)
		{
			var (ExternalIndex, InternalIndex) = SerchIndexes(page);
			int itemsPerPage = QuantityPerPage;
			int currenciesCount = cryptoCurrenciesCount;
			var pairs = new List<string>();

			for (; ExternalIndex < currenciesCount - 1; ExternalIndex++)
			{
				for (; InternalIndex < currenciesCount; InternalIndex++, itemsPerPage--)
				{
					var pair = $"{_cryptoCurrencies.ElementAt(ExternalIndex).Name}-{_cryptoCurrencies.ElementAt(InternalIndex).Name}";
					pairs.Add(pair);
					if (itemsPerPage - 1 == 0)
					{
						return pairs;
					}
				}
				InternalIndex = ExternalIndex + 2;
			}

			return pairs;
		}
		private (int, int) SerchIndexes(int page)
		{
			var elementsToCurrentPage = (uint)(page * QuantityPerPage - QuantityPerPage);
			var currenciesCount = cryptoCurrenciesCount;

			if (numberOfPairs / 2 > elementsToCurrentPage)
			{
				for (int ExternalIndex = 0; ExternalIndex < currenciesCount; ExternalIndex++)
				{

					for (int InternalIndex = ExternalIndex + 1; InternalIndex < currenciesCount; InternalIndex++, elementsToCurrentPage--)
					{
						if (elementsToCurrentPage == 0) return (ExternalIndex, InternalIndex);
					}
				}
			}
			if (elementsToCurrentPage > numberOfPairs) elementsToCurrentPage = numberOfPairs;

			for (int ExternalIndex = currenciesCount - 1; ExternalIndex >= 0; ExternalIndex--)
			{

				for (int InternalIndex = currenciesCount - 1; InternalIndex > ExternalIndex; InternalIndex--, elementsToCurrentPage++)
				{
					if (elementsToCurrentPage == numberOfPairs) return (ExternalIndex, InternalIndex);
				}
			}

			return (0, 0);

		}
		public IEnumerable<int> GetPaginationRange(int currentPage, int totalPages)
		{
			List<int> visiblePages = new();

			for (int i = 1; i <= Math.Min(3, totalPages); i++)
			{
				visiblePages.Add(i);
			}

			for (int i = Math.Max(4, currentPage - 1); i <= Math.Min(totalPages - 3, currentPage + 1); i++)
			{
				visiblePages.Add(i);
			}

			for (int i = totalPages - 2; i <= totalPages; i++)
			{
				visiblePages.Add(i);
			}

			return visiblePages;
		}
	}

}
