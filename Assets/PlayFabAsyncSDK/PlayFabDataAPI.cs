using System.Threading.Tasks;
using PlayFab.DataModels;

namespace PlayFabAsync
{
    public class PlayFabDataAPI
    {
        public static Task<PlayFabAsyncResult<AbortFileUploadsResponse>> AbortFileUploadsAsync(AbortFileUploadsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AbortFileUploadsResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AbortFileUploadsResponse>>();

            PlayFab.PlayFabDataAPI.AbortFileUploads(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AbortFileUploadsResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AbortFileUploadsResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<DeleteFilesResponse>> DeleteFilesAsync(DeleteFilesRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<DeleteFilesResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<DeleteFilesResponse>>();

            PlayFab.PlayFabDataAPI.DeleteFiles(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<DeleteFilesResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<DeleteFilesResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<FinalizeFileUploadsResponse>> FinalizeFileUploadsAsync(FinalizeFileUploadsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<FinalizeFileUploadsResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<FinalizeFileUploadsResponse>>();

            PlayFab.PlayFabDataAPI.FinalizeFileUploads(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<FinalizeFileUploadsResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<FinalizeFileUploadsResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetFilesResponse>> GetFilesAsync(GetFilesRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetFilesResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetFilesResponse>>();

            PlayFab.PlayFabDataAPI.GetFiles(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetFilesResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetFilesResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetObjectsResponse>> GetObjectsAsync(GetObjectsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetObjectsResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetObjectsResponse>>();

            PlayFab.PlayFabDataAPI.GetObjects(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetObjectsResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetObjectsResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<InitiateFileUploadsResponse>> InitiateFileUploadsAsync(InitiateFileUploadsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<InitiateFileUploadsResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<InitiateFileUploadsResponse>>();

            PlayFab.PlayFabDataAPI.InitiateFileUploads(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<InitiateFileUploadsResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<InitiateFileUploadsResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<SetObjectsResponse>> SetObjectsAsync(SetObjectsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<SetObjectsResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<SetObjectsResponse>>();

            PlayFab.PlayFabDataAPI.SetObjects(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetObjectsResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetObjectsResponse>(null, error));
            });

            return tcs.Task;
        }
    }
}
