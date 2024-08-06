using System.Threading.Tasks;
using Online.Model;

namespace Online.Interface
{
    public interface IRankingService
	{
		public SeasonInfo SeasonInfo { get; }
		public RankInfo RankInfo { get; }
		public Task LoadUserRankInfoAsync();
	}
}