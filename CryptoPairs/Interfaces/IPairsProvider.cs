namespace CryptoPairs.Intarfaces
{
    public interface IPairsProvider
    {
        public uint Count();
        public IEnumerable<string> GetPairs(int page);

        public IEnumerable<int> GetPaginationRange(int currentPage, int totalPages);
    }
}