namespace CryptoPairs.Models
{
    public class CryptoCurrencyViewModel
    {
        public IEnumerable<string> Pairs { get; set; }
        public uint TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / 20);

    }
}
