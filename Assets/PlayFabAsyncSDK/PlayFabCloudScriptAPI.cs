using System.Threading.Tasks;
using PlayFab.CloudScriptModels;

namespace PlayFabAsync
{
    public class PlayFabCloudScriptAPI
    {
        public static Task<PlayFabAsyncResult<ExecuteCloudScriptResult>> ExecuteEntityCloudScriptAsync(ExecuteEntityCloudScriptRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ExecuteCloudScriptResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ExecuteCloudScriptResult>>();

            PlayFab.PlayFabCloudScriptAPI.ExecuteEntityCloudScript(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ExecuteCloudScriptResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ExecuteCloudScriptResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ExecuteFunctionResult>> ExecuteFunctionAsync(ExecuteFunctionRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ExecuteFunctionResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ExecuteFunctionResult>>();

            PlayFab.PlayFabCloudScriptAPI.ExecuteFunction(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ExecuteFunctionResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ExecuteFunctionResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetFunctionResult>> GetFunctionAsync(GetFunctionRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetFunctionResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetFunctionResult>>();

            PlayFab.PlayFabCloudScriptAPI.GetFunction(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetFunctionResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetFunctionResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ListFunctionsResult>> ListFunctionsAsync(ListFunctionsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ListFunctionsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ListFunctionsResult>>();

            PlayFab.PlayFabCloudScriptAPI.ListFunctions(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ListFunctionsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ListFunctionsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ListHttpFunctionsResult>> ListHttpFunctionsAsync(ListFunctionsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ListHttpFunctionsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ListHttpFunctionsResult>>();

            PlayFab.PlayFabCloudScriptAPI.ListHttpFunctions(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ListHttpFunctionsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ListHttpFunctionsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ListQueuedFunctionsResult>> ListQueuedFunctionsAsync(ListFunctionsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ListQueuedFunctionsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ListQueuedFunctionsResult>>();

            PlayFab.PlayFabCloudScriptAPI.ListQueuedFunctions(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ListQueuedFunctionsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ListQueuedFunctionsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> PostFunctionResultForEntityTriggeredActionAsync(PostFunctionResultForEntityTriggeredActionRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabCloudScriptAPI.PostFunctionResultForEntityTriggeredAction(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> PostFunctionResultForFunctionExecutionAsync(PostFunctionResultForFunctionExecutionRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabCloudScriptAPI.PostFunctionResultForFunctionExecution(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> PostFunctionResultForPlayerTriggeredActionAsync(PostFunctionResultForPlayerTriggeredActionRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabCloudScriptAPI.PostFunctionResultForPlayerTriggeredAction(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> PostFunctionResultForScheduledTaskAsync(PostFunctionResultForScheduledTaskRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabCloudScriptAPI.PostFunctionResultForScheduledTask(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> RegisterHttpFunctionAsync(RegisterHttpFunctionRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabCloudScriptAPI.RegisterHttpFunction(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> RegisterQueuedFunctionAsync(RegisterQueuedFunctionRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabCloudScriptAPI.RegisterQueuedFunction(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> UnregisterFunctionAsync(UnregisterFunctionRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabCloudScriptAPI.UnregisterFunction(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
    }
}
