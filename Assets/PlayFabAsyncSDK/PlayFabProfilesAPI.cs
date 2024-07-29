using System.Threading.Tasks;
using PlayFab.ProfilesModels;

namespace PlayFabAsync
{
    public class PlayFabProfilesAPI
    {
        public static Task<PlayFabAsyncResult<GetGlobalPolicyResponse>> GetGlobalPolicyAsync(GetGlobalPolicyRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetGlobalPolicyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetGlobalPolicyResponse>>();

            PlayFab.PlayFabProfilesAPI.GetGlobalPolicy(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetGlobalPolicyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetGlobalPolicyResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetEntityProfileResponse>> GetProfileAsync(GetEntityProfileRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetEntityProfileResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetEntityProfileResponse>>();

            PlayFab.PlayFabProfilesAPI.GetProfile(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetEntityProfileResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetEntityProfileResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetEntityProfilesResponse>> GetProfilesAsync(GetEntityProfilesRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetEntityProfilesResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetEntityProfilesResponse>>();

            PlayFab.PlayFabProfilesAPI.GetProfiles(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetEntityProfilesResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetEntityProfilesResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetTitlePlayersFromMasterPlayerAccountIdsResponse>> GetTitlePlayersFromMasterPlayerAccountIdsAsync(GetTitlePlayersFromMasterPlayerAccountIdsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetTitlePlayersFromMasterPlayerAccountIdsResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetTitlePlayersFromMasterPlayerAccountIdsResponse>>();

            PlayFab.PlayFabProfilesAPI.GetTitlePlayersFromMasterPlayerAccountIds(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitlePlayersFromMasterPlayerAccountIdsResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitlePlayersFromMasterPlayerAccountIdsResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetTitlePlayersFromProviderIDsResponse>> GetTitlePlayersFromXboxLiveIDsAsync(GetTitlePlayersFromXboxLiveIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetTitlePlayersFromProviderIDsResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetTitlePlayersFromProviderIDsResponse>>();

            PlayFab.PlayFabProfilesAPI.GetTitlePlayersFromXboxLiveIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitlePlayersFromProviderIDsResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitlePlayersFromProviderIDsResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<SetGlobalPolicyResponse>> SetGlobalPolicyAsync(SetGlobalPolicyRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<SetGlobalPolicyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<SetGlobalPolicyResponse>>();

            PlayFab.PlayFabProfilesAPI.SetGlobalPolicy(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetGlobalPolicyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetGlobalPolicyResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<SetProfileLanguageResponse>> SetProfileLanguageAsync(SetProfileLanguageRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<SetProfileLanguageResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<SetProfileLanguageResponse>>();

            PlayFab.PlayFabProfilesAPI.SetProfileLanguage(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetProfileLanguageResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetProfileLanguageResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<SetEntityProfilePolicyResponse>> SetProfilePolicyAsync(SetEntityProfilePolicyRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<SetEntityProfilePolicyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<SetEntityProfilePolicyResponse>>();

            PlayFab.PlayFabProfilesAPI.SetProfilePolicy(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetEntityProfilePolicyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetEntityProfilePolicyResponse>(null, error));
            });

            return tcs.Task;
        }
    }
}
