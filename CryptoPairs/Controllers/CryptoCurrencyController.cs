
using CryptoPairs.Intarfaces;
using CryptoPairs.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPairs.Controllers
{

	public class CryptoCurrencyController : Controller
	{
		private readonly IPairsProvider _pairsProvider;

		public CryptoCurrencyController(IPairsProvider pairsProvider) => _pairsProvider = pairsProvider;


		public IActionResult Index(int page = 1)
		{
			var pairs = _pairsProvider.GetPairs(page);
			var totalCount = _pairsProvider.Count();

			var viewModel = new CryptoCurrencyViewModel
			{
				Pairs = pairs,
				TotalCount = totalCount,
				CurrentPage = page
			};

			var visiblePages = _pairsProvider.GetPaginationRange(page, (int)Math.Ceiling((double)totalCount / 20));
			ViewData["VisiblePages"] = visiblePages;

			return View(viewModel);
		}

	}
}