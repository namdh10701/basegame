using System.Threading.Tasks;
using Online.Model;

namespace Online.Interface
{
    public interface IRankingService
	{
		public RankInfo RankInfo { get; }
		public UserRankInfo UserRankInfo { get; }
		public Task LoadUserRankInfoAsync();
	}
}