using System.Threading.Tasks;
using PlayFab.ClientModels;

namespace PlayFabAsync
{
    public static class PlayFabClientAPI
    {
        public static Task<PlayFabAsyncResult<AcceptTradeResponse>> AcceptTradeAsync(AcceptTradeRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AcceptTradeResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AcceptTradeResponse>>();

            PlayFab.PlayFabClientAPI.AcceptTrade(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AcceptTradeResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AcceptTradeResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<AddFriendResult>> AddFriendAsync(AddFriendRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AddFriendResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AddFriendResult>>();

            PlayFab.PlayFabClientAPI.AddFriend(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddFriendResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddFriendResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<AddGenericIDResult>> AddGenericIDAsync(AddGenericIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AddGenericIDResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AddGenericIDResult>>();

            PlayFab.PlayFabClientAPI.AddGenericID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddGenericIDResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddGenericIDResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<AddOrUpdateContactEmailResult>> AddOrUpdateContactEmailAsync(AddOrUpdateContactEmailRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AddOrUpdateContactEmailResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AddOrUpdateContactEmailResult>>();

            PlayFab.PlayFabClientAPI.AddOrUpdateContactEmail(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddOrUpdateContactEmailResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddOrUpdateContactEmailResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<AddSharedGroupMembersResult>> AddSharedGroupMembersAsync(AddSharedGroupMembersRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AddSharedGroupMembersResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AddSharedGroupMembersResult>>();

            PlayFab.PlayFabClientAPI.AddSharedGroupMembers(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddSharedGroupMembersResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddSharedGroupMembersResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<AddUsernamePasswordResult>> AddUsernamePasswordAsync(AddUsernamePasswordRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AddUsernamePasswordResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AddUsernamePasswordResult>>();

            PlayFab.PlayFabClientAPI.AddUsernamePassword(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddUsernamePasswordResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AddUsernamePasswordResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>> AddUserVirtualCurrencyAsync(AddUserVirtualCurrencyRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>>();

            PlayFab.PlayFabClientAPI.AddUserVirtualCurrency(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<AndroidDevicePushNotificationRegistrationResult>> AndroidDevicePushNotificationRegistrationAsync(AndroidDevicePushNotificationRegistrationRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AndroidDevicePushNotificationRegistrationResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AndroidDevicePushNotificationRegistrationResult>>();

            PlayFab.PlayFabClientAPI.AndroidDevicePushNotificationRegistration(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AndroidDevicePushNotificationRegistrationResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AndroidDevicePushNotificationRegistrationResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<AttributeInstallResult>> AttributeInstallAsync(AttributeInstallRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<AttributeInstallResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<AttributeInstallResult>>();

            PlayFab.PlayFabClientAPI.AttributeInstall(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AttributeInstallResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<AttributeInstallResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<CancelTradeResponse>> CancelTradeAsync(CancelTradeRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<CancelTradeResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<CancelTradeResponse>>();

            PlayFab.PlayFabClientAPI.CancelTrade(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<CancelTradeResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<CancelTradeResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ConfirmPurchaseResult>> ConfirmPurchaseAsync(ConfirmPurchaseRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ConfirmPurchaseResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ConfirmPurchaseResult>>();

            PlayFab.PlayFabClientAPI.ConfirmPurchase(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConfirmPurchaseResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConfirmPurchaseResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ConsumeItemResult>> ConsumeItemAsync(ConsumeItemRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ConsumeItemResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ConsumeItemResult>>();

            PlayFab.PlayFabClientAPI.ConsumeItem(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumeItemResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumeItemResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ConsumeMicrosoftStoreEntitlementsResponse>> ConsumeMicrosoftStoreEntitlementsAsync(ConsumeMicrosoftStoreEntitlementsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ConsumeMicrosoftStoreEntitlementsResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ConsumeMicrosoftStoreEntitlementsResponse>>();

            PlayFab.PlayFabClientAPI.ConsumeMicrosoftStoreEntitlements(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumeMicrosoftStoreEntitlementsResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumeMicrosoftStoreEntitlementsResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ConsumePS5EntitlementsResult>> ConsumePS5EntitlementsAsync(ConsumePS5EntitlementsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ConsumePS5EntitlementsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ConsumePS5EntitlementsResult>>();

            PlayFab.PlayFabClientAPI.ConsumePS5Entitlements(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumePS5EntitlementsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumePS5EntitlementsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ConsumePSNEntitlementsResult>> ConsumePSNEntitlementsAsync(ConsumePSNEntitlementsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ConsumePSNEntitlementsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ConsumePSNEntitlementsResult>>();

            PlayFab.PlayFabClientAPI.ConsumePSNEntitlements(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumePSNEntitlementsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumePSNEntitlementsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ConsumeXboxEntitlementsResult>> ConsumeXboxEntitlementsAsync(ConsumeXboxEntitlementsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ConsumeXboxEntitlementsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ConsumeXboxEntitlementsResult>>();

            PlayFab.PlayFabClientAPI.ConsumeXboxEntitlements(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumeXboxEntitlementsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ConsumeXboxEntitlementsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<CreateSharedGroupResult>> CreateSharedGroupAsync(CreateSharedGroupRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<CreateSharedGroupResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<CreateSharedGroupResult>>();

            PlayFab.PlayFabClientAPI.CreateSharedGroup(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<CreateSharedGroupResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<CreateSharedGroupResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ExecuteCloudScriptResult>> ExecuteCloudScriptAsync(ExecuteCloudScriptRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ExecuteCloudScriptResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ExecuteCloudScriptResult>>();

            PlayFab.PlayFabClientAPI.ExecuteCloudScript(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ExecuteCloudScriptResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ExecuteCloudScriptResult>(null, error));
            });

            return tcs.Task;
        }
        /*
    public static Task<PlayFabAsyncResult<TOut>> ExecuteCloudScriptAsync<TOut>(ExecuteCloudScriptRequest request)
    {
        TaskCompletionSource<PlayFabAsyncResult<TOut>> tcs = new TaskCompletionSource<PlayFabAsyncResult<TOut>>();

        PlayFabClientAPI.ExecuteCloudScript<TOut>(request, result =>
        {
            tcs.TrySetResult(new PlayFabAsyncResult<TOut>(result, null));
        }, error =>
        {
            tcs.TrySetResult(new PlayFabAsyncResult<TOut>(null, error));
        });

        return tcs.Task;
    }
    */
        public static Task<PlayFabAsyncResult<GetAccountInfoResult>> GetAccountInfoAsync(GetAccountInfoRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetAccountInfoResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetAccountInfoResult>>();

            PlayFab.PlayFabClientAPI.GetAccountInfo(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetAccountInfoResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetAccountInfoResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetAdPlacementsResult>> GetAdPlacementsAsync(GetAdPlacementsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetAdPlacementsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetAdPlacementsResult>>();

            PlayFab.PlayFabClientAPI.GetAdPlacements(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetAdPlacementsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetAdPlacementsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ListUsersCharactersResult>> GetAllUsersCharactersAsync(ListUsersCharactersRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ListUsersCharactersResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ListUsersCharactersResult>>();

            PlayFab.PlayFabClientAPI.GetAllUsersCharacters(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ListUsersCharactersResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ListUsersCharactersResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetCatalogItemsResult>> GetCatalogItemsAsync(GetCatalogItemsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetCatalogItemsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetCatalogItemsResult>>();

            PlayFab.PlayFabClientAPI.GetCatalogItems(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCatalogItemsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCatalogItemsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetCharacterDataResult>> GetCharacterDataAsync(GetCharacterDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetCharacterDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetCharacterDataResult>>();

            PlayFab.PlayFabClientAPI.GetCharacterData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetCharacterInventoryResult>> GetCharacterInventoryAsync(GetCharacterInventoryRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetCharacterInventoryResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetCharacterInventoryResult>>();

            PlayFab.PlayFabClientAPI.GetCharacterInventory(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterInventoryResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterInventoryResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetCharacterLeaderboardResult>> GetCharacterLeaderboardAsync(GetCharacterLeaderboardRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetCharacterLeaderboardResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetCharacterLeaderboardResult>>();

            PlayFab.PlayFabClientAPI.GetCharacterLeaderboard(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterLeaderboardResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterLeaderboardResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetCharacterDataResult>> GetCharacterReadOnlyDataAsync(GetCharacterDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetCharacterDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetCharacterDataResult>>();

            PlayFab.PlayFabClientAPI.GetCharacterReadOnlyData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetCharacterStatisticsResult>> GetCharacterStatisticsAsync(GetCharacterStatisticsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetCharacterStatisticsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetCharacterStatisticsResult>>();

            PlayFab.PlayFabClientAPI.GetCharacterStatistics(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterStatisticsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetCharacterStatisticsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetContentDownloadUrlResult>> GetContentDownloadUrlAsync(GetContentDownloadUrlRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetContentDownloadUrlResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetContentDownloadUrlResult>>();

            PlayFab.PlayFabClientAPI.GetContentDownloadUrl(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetContentDownloadUrlResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetContentDownloadUrlResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetLeaderboardResult>> GetFriendLeaderboardAsync(GetFriendLeaderboardRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardResult>>();

            PlayFab.PlayFabClientAPI.GetFriendLeaderboard(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetFriendLeaderboardAroundPlayerResult>> GetFriendLeaderboardAroundPlayerAsync(GetFriendLeaderboardAroundPlayerRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetFriendLeaderboardAroundPlayerResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetFriendLeaderboardAroundPlayerResult>>();

            PlayFab.PlayFabClientAPI.GetFriendLeaderboardAroundPlayer(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetFriendLeaderboardAroundPlayerResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetFriendLeaderboardAroundPlayerResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetFriendsListResult>> GetFriendsListAsync(GetFriendsListRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetFriendsListResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetFriendsListResult>>();

            PlayFab.PlayFabClientAPI.GetFriendsList(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetFriendsListResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetFriendsListResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetLeaderboardResult>> GetLeaderboardAsync(GetLeaderboardRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardResult>>();

            PlayFab.PlayFabClientAPI.GetLeaderboard(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetLeaderboardAroundCharacterResult>> GetLeaderboardAroundCharacterAsync(GetLeaderboardAroundCharacterRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardAroundCharacterResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardAroundCharacterResult>>();

            PlayFab.PlayFabClientAPI.GetLeaderboardAroundCharacter(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardAroundCharacterResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardAroundCharacterResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetLeaderboardAroundPlayerResult>> GetLeaderboardAroundPlayerAsync(GetLeaderboardAroundPlayerRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardAroundPlayerResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardAroundPlayerResult>>();

            PlayFab.PlayFabClientAPI.GetLeaderboardAroundPlayer(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardAroundPlayerResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardAroundPlayerResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetLeaderboardForUsersCharactersResult>> GetLeaderboardForUserCharactersAsync(GetLeaderboardForUsersCharactersRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardForUsersCharactersResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetLeaderboardForUsersCharactersResult>>();

            PlayFab.PlayFabClientAPI.GetLeaderboardForUserCharacters(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardForUsersCharactersResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetLeaderboardForUsersCharactersResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPaymentTokenResult>> GetPaymentTokenAsync(GetPaymentTokenRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPaymentTokenResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPaymentTokenResult>>();

            PlayFab.PlayFabClientAPI.GetPaymentToken(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPaymentTokenResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPaymentTokenResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPhotonAuthenticationTokenResult>> GetPhotonAuthenticationTokenAsync(GetPhotonAuthenticationTokenRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPhotonAuthenticationTokenResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPhotonAuthenticationTokenResult>>();

            PlayFab.PlayFabClientAPI.GetPhotonAuthenticationToken(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPhotonAuthenticationTokenResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPhotonAuthenticationTokenResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayerCombinedInfoResult>> GetPlayerCombinedInfoAsync(GetPlayerCombinedInfoRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayerCombinedInfoResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayerCombinedInfoResult>>();

            PlayFab.PlayFabClientAPI.GetPlayerCombinedInfo(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerCombinedInfoResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerCombinedInfoResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayerProfileResult>> GetPlayerProfileAsync(GetPlayerProfileRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayerProfileResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayerProfileResult>>();

            PlayFab.PlayFabClientAPI.GetPlayerProfile(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerProfileResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerProfileResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayerSegmentsResult>> GetPlayerSegmentsAsync(GetPlayerSegmentsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayerSegmentsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayerSegmentsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayerSegments(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerSegmentsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerSegmentsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayerStatisticsResult>> GetPlayerStatisticsAsync(GetPlayerStatisticsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayerStatisticsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayerStatisticsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayerStatistics(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerStatisticsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerStatisticsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayerStatisticVersionsResult>> GetPlayerStatisticVersionsAsync(GetPlayerStatisticVersionsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayerStatisticVersionsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayerStatisticVersionsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayerStatisticVersions(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerStatisticVersionsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerStatisticVersionsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayerTagsResult>> GetPlayerTagsAsync(GetPlayerTagsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayerTagsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayerTagsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayerTags(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerTagsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerTagsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayerTradesResponse>> GetPlayerTradesAsync(GetPlayerTradesRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayerTradesResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayerTradesResponse>>();

            PlayFab.PlayFabClientAPI.GetPlayerTrades(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerTradesResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayerTradesResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromFacebookIDsResult>> GetPlayFabIDsFromFacebookIDsAsync(GetPlayFabIDsFromFacebookIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromFacebookIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromFacebookIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromFacebookIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromFacebookIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromFacebookIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromFacebookInstantGamesIdsResult>> GetPlayFabIDsFromFacebookInstantGamesIdsAsync(GetPlayFabIDsFromFacebookInstantGamesIdsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromFacebookInstantGamesIdsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromFacebookInstantGamesIdsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromFacebookInstantGamesIds(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromFacebookInstantGamesIdsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromFacebookInstantGamesIdsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromGameCenterIDsResult>> GetPlayFabIDsFromGameCenterIDsAsync(GetPlayFabIDsFromGameCenterIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromGameCenterIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromGameCenterIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromGameCenterIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromGameCenterIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromGameCenterIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromGenericIDsResult>> GetPlayFabIDsFromGenericIDsAsync(GetPlayFabIDsFromGenericIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromGenericIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromGenericIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromGenericIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromGenericIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromGenericIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromGoogleIDsResult>> GetPlayFabIDsFromGoogleIDsAsync(GetPlayFabIDsFromGoogleIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromGoogleIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromGoogleIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromGoogleIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromGoogleIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromGoogleIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromGooglePlayGamesPlayerIDsResult>> GetPlayFabIDsFromGooglePlayGamesPlayerIDsAsync(GetPlayFabIDsFromGooglePlayGamesPlayerIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromGooglePlayGamesPlayerIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromGooglePlayGamesPlayerIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromGooglePlayGamesPlayerIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromGooglePlayGamesPlayerIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromGooglePlayGamesPlayerIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromKongregateIDsResult>> GetPlayFabIDsFromKongregateIDsAsync(GetPlayFabIDsFromKongregateIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromKongregateIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromKongregateIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromKongregateIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromKongregateIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromKongregateIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromNintendoServiceAccountIdsResult>> GetPlayFabIDsFromNintendoServiceAccountIdsAsync(GetPlayFabIDsFromNintendoServiceAccountIdsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromNintendoServiceAccountIdsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromNintendoServiceAccountIdsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromNintendoServiceAccountIds(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromNintendoServiceAccountIdsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromNintendoServiceAccountIdsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>> GetPlayFabIDsFromNintendoSwitchDeviceIdsAsync(GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromNintendoSwitchDeviceIds(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromPSNAccountIDsResult>> GetPlayFabIDsFromPSNAccountIDsAsync(GetPlayFabIDsFromPSNAccountIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromPSNAccountIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromPSNAccountIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromPSNAccountIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromPSNAccountIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromPSNAccountIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromSteamIDsResult>> GetPlayFabIDsFromSteamIDsAsync(GetPlayFabIDsFromSteamIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromSteamIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromSteamIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromSteamIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromSteamIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromSteamIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromTwitchIDsResult>> GetPlayFabIDsFromTwitchIDsAsync(GetPlayFabIDsFromTwitchIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromTwitchIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromTwitchIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromTwitchIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromTwitchIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromTwitchIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPlayFabIDsFromXboxLiveIDsResult>> GetPlayFabIDsFromXboxLiveIDsAsync(GetPlayFabIDsFromXboxLiveIDsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromXboxLiveIDsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPlayFabIDsFromXboxLiveIDsResult>>();

            PlayFab.PlayFabClientAPI.GetPlayFabIDsFromXboxLiveIDs(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromXboxLiveIDsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPlayFabIDsFromXboxLiveIDsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPublisherDataResult>> GetPublisherDataAsync(GetPublisherDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPublisherDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPublisherDataResult>>();

            PlayFab.PlayFabClientAPI.GetPublisherData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPublisherDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPublisherDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetPurchaseResult>> GetPurchaseAsync(GetPurchaseRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetPurchaseResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetPurchaseResult>>();

            PlayFab.PlayFabClientAPI.GetPurchase(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPurchaseResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetPurchaseResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetSharedGroupDataResult>> GetSharedGroupDataAsync(GetSharedGroupDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetSharedGroupDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetSharedGroupDataResult>>();

            PlayFab.PlayFabClientAPI.GetSharedGroupData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetSharedGroupDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetSharedGroupDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetStoreItemsResult>> GetStoreItemsAsync(GetStoreItemsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetStoreItemsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetStoreItemsResult>>();

            PlayFab.PlayFabClientAPI.GetStoreItems(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetStoreItemsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetStoreItemsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetTimeResult>> GetTimeAsync(GetTimeRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetTimeResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetTimeResult>>();

            PlayFab.PlayFabClientAPI.GetTime(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTimeResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTimeResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetTitleDataResult>> GetTitleDataAsync(GetTitleDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetTitleDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetTitleDataResult>>();

            PlayFab.PlayFabClientAPI.GetTitleData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitleDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitleDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetTitleNewsResult>> GetTitleNewsAsync(GetTitleNewsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetTitleNewsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetTitleNewsResult>>();

            PlayFab.PlayFabClientAPI.GetTitleNews(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitleNewsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitleNewsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetTitlePublicKeyResult>> GetTitlePublicKeyAsync(GetTitlePublicKeyRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetTitlePublicKeyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetTitlePublicKeyResult>>();

            PlayFab.PlayFabClientAPI.GetTitlePublicKey(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitlePublicKeyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTitlePublicKeyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetTradeStatusResponse>> GetTradeStatusAsync(GetTradeStatusRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetTradeStatusResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetTradeStatusResponse>>();

            PlayFab.PlayFabClientAPI.GetTradeStatus(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTradeStatusResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetTradeStatusResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetUserDataResult>> GetUserDataAsync(GetUserDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetUserDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetUserDataResult>>();

            PlayFab.PlayFabClientAPI.GetUserData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetUserInventoryResult>> GetUserInventoryAsync(GetUserInventoryRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetUserInventoryResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetUserInventoryResult>>();

            PlayFab.PlayFabClientAPI.GetUserInventory(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserInventoryResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserInventoryResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetUserDataResult>> GetUserPublisherDataAsync(GetUserDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetUserDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetUserDataResult>>();

            PlayFab.PlayFabClientAPI.GetUserPublisherData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetUserDataResult>> GetUserPublisherReadOnlyDataAsync(GetUserDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetUserDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetUserDataResult>>();

            PlayFab.PlayFabClientAPI.GetUserPublisherReadOnlyData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GetUserDataResult>> GetUserReadOnlyDataAsync(GetUserDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GetUserDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GetUserDataResult>>();

            PlayFab.PlayFabClientAPI.GetUserReadOnlyData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GetUserDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<GrantCharacterToUserResult>> GrantCharacterToUserAsync(GrantCharacterToUserRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<GrantCharacterToUserResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<GrantCharacterToUserResult>>();

            PlayFab.PlayFabClientAPI.GrantCharacterToUser(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GrantCharacterToUserResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<GrantCharacterToUserResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkAndroidDeviceIDResult>> LinkAndroidDeviceIDAsync(LinkAndroidDeviceIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkAndroidDeviceIDResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkAndroidDeviceIDResult>>();

            PlayFab.PlayFabClientAPI.LinkAndroidDeviceID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkAndroidDeviceIDResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkAndroidDeviceIDResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> LinkAppleAsync(LinkAppleRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabClientAPI.LinkApple(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkCustomIDResult>> LinkCustomIDAsync(LinkCustomIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkCustomIDResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkCustomIDResult>>();

            PlayFab.PlayFabClientAPI.LinkCustomID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkCustomIDResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkCustomIDResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkFacebookAccountResult>> LinkFacebookAccountAsync(LinkFacebookAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkFacebookAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkFacebookAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkFacebookAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkFacebookAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkFacebookAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkFacebookInstantGamesIdResult>> LinkFacebookInstantGamesIdAsync(LinkFacebookInstantGamesIdRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkFacebookInstantGamesIdResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkFacebookInstantGamesIdResult>>();

            PlayFab.PlayFabClientAPI.LinkFacebookInstantGamesId(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkFacebookInstantGamesIdResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkFacebookInstantGamesIdResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkGameCenterAccountResult>> LinkGameCenterAccountAsync(LinkGameCenterAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkGameCenterAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkGameCenterAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkGameCenterAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkGameCenterAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkGameCenterAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkGoogleAccountResult>> LinkGoogleAccountAsync(LinkGoogleAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkGoogleAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkGoogleAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkGoogleAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkGoogleAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkGoogleAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkGooglePlayGamesServicesAccountResult>> LinkGooglePlayGamesServicesAccountAsync(LinkGooglePlayGamesServicesAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkGooglePlayGamesServicesAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkGooglePlayGamesServicesAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkGooglePlayGamesServicesAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkGooglePlayGamesServicesAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkGooglePlayGamesServicesAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkIOSDeviceIDResult>> LinkIOSDeviceIDAsync(LinkIOSDeviceIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkIOSDeviceIDResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkIOSDeviceIDResult>>();

            PlayFab.PlayFabClientAPI.LinkIOSDeviceID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkIOSDeviceIDResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkIOSDeviceIDResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkKongregateAccountResult>> LinkKongregateAsync(LinkKongregateAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkKongregateAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkKongregateAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkKongregate(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkKongregateAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkKongregateAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> LinkNintendoServiceAccountAsync(LinkNintendoServiceAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabClientAPI.LinkNintendoServiceAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkNintendoSwitchDeviceIdResult>> LinkNintendoSwitchDeviceIdAsync(LinkNintendoSwitchDeviceIdRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkNintendoSwitchDeviceIdResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkNintendoSwitchDeviceIdResult>>();

            PlayFab.PlayFabClientAPI.LinkNintendoSwitchDeviceId(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkNintendoSwitchDeviceIdResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkNintendoSwitchDeviceIdResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResult>> LinkOpenIdConnectAsync(LinkOpenIdConnectRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResult>>();

            PlayFab.PlayFabClientAPI.LinkOpenIdConnect(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkPSNAccountResult>> LinkPSNAccountAsync(LinkPSNAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkPSNAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkPSNAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkPSNAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkPSNAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkPSNAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkSteamAccountResult>> LinkSteamAccountAsync(LinkSteamAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkSteamAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkSteamAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkSteamAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkSteamAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkSteamAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkTwitchAccountResult>> LinkTwitchAsync(LinkTwitchAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkTwitchAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkTwitchAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkTwitch(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkTwitchAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkTwitchAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LinkXboxAccountResult>> LinkXboxAccountAsync(LinkXboxAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LinkXboxAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LinkXboxAccountResult>>();

            PlayFab.PlayFabClientAPI.LinkXboxAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkXboxAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LinkXboxAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithAndroidDeviceIDAsync(LoginWithAndroidDeviceIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithAndroidDeviceID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithAppleAsync(LoginWithAppleRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithApple(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithCustomIDAsync(LoginWithCustomIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithCustomID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithEmailAddressAsync(LoginWithEmailAddressRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithEmailAddress(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithFacebookAsync(LoginWithFacebookRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithFacebook(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithFacebookInstantGamesIdAsync(LoginWithFacebookInstantGamesIdRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithFacebookInstantGamesId(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithGameCenterAsync(LoginWithGameCenterRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithGameCenter(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithGoogleAccountAsync(LoginWithGoogleAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithGoogleAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithGooglePlayGamesServicesAsync(LoginWithGooglePlayGamesServicesRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithGooglePlayGamesServices(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithIOSDeviceIDAsync(LoginWithIOSDeviceIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithIOSDeviceID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithKongregateAsync(LoginWithKongregateRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithKongregate(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithNintendoServiceAccountAsync(LoginWithNintendoServiceAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithNintendoServiceAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithNintendoSwitchDeviceIdAsync(LoginWithNintendoSwitchDeviceIdRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithNintendoSwitchDeviceId(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithOpenIdConnectAsync(LoginWithOpenIdConnectRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithOpenIdConnect(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithPlayFabAsync(LoginWithPlayFabRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithPlayFab(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithPSNAsync(LoginWithPSNRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithPSN(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithSteamAsync(LoginWithSteamRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithSteam(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithTwitchAsync(LoginWithTwitchRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithTwitch(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<LoginResult>> LoginWithXboxAsync(LoginWithXboxRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<LoginResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<LoginResult>>();

            PlayFab.PlayFabClientAPI.LoginWithXbox(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<LoginResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<OpenTradeResponse>> OpenTradeAsync(OpenTradeRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<OpenTradeResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<OpenTradeResponse>>();

            PlayFab.PlayFabClientAPI.OpenTrade(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<OpenTradeResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<OpenTradeResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<PayForPurchaseResult>> PayForPurchaseAsync(PayForPurchaseRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<PayForPurchaseResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<PayForPurchaseResult>>();

            PlayFab.PlayFabClientAPI.PayForPurchase(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<PayForPurchaseResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<PayForPurchaseResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<PurchaseItemResult>> PurchaseItemAsync(PurchaseItemRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<PurchaseItemResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<PurchaseItemResult>>();

            PlayFab.PlayFabClientAPI.PurchaseItem(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<PurchaseItemResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<PurchaseItemResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RedeemCouponResult>> RedeemCouponAsync(RedeemCouponRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RedeemCouponResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RedeemCouponResult>>();

            PlayFab.PlayFabClientAPI.RedeemCoupon(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RedeemCouponResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RedeemCouponResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResponse>> RefreshPSNAuthTokenAsync(RefreshPSNAuthTokenRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>>();

            PlayFab.PlayFabClientAPI.RefreshPSNAuthToken(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RegisterForIOSPushNotificationResult>> RegisterForIOSPushNotificationAsync(RegisterForIOSPushNotificationRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RegisterForIOSPushNotificationResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RegisterForIOSPushNotificationResult>>();

            PlayFab.PlayFabClientAPI.RegisterForIOSPushNotification(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RegisterForIOSPushNotificationResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RegisterForIOSPushNotificationResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RegisterPlayFabUserResult>> RegisterPlayFabUserAsync(RegisterPlayFabUserRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RegisterPlayFabUserResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RegisterPlayFabUserResult>>();

            PlayFab.PlayFabClientAPI.RegisterPlayFabUser(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RegisterPlayFabUserResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RegisterPlayFabUserResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RemoveContactEmailResult>> RemoveContactEmailAsync(RemoveContactEmailRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RemoveContactEmailResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RemoveContactEmailResult>>();

            PlayFab.PlayFabClientAPI.RemoveContactEmail(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RemoveContactEmailResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RemoveContactEmailResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RemoveFriendResult>> RemoveFriendAsync(RemoveFriendRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RemoveFriendResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RemoveFriendResult>>();

            PlayFab.PlayFabClientAPI.RemoveFriend(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RemoveFriendResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RemoveFriendResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RemoveGenericIDResult>> RemoveGenericIDAsync(RemoveGenericIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RemoveGenericIDResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RemoveGenericIDResult>>();

            PlayFab.PlayFabClientAPI.RemoveGenericID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RemoveGenericIDResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RemoveGenericIDResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RemoveSharedGroupMembersResult>> RemoveSharedGroupMembersAsync(RemoveSharedGroupMembersRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RemoveSharedGroupMembersResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RemoveSharedGroupMembersResult>>();

            PlayFab.PlayFabClientAPI.RemoveSharedGroupMembers(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RemoveSharedGroupMembersResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RemoveSharedGroupMembersResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ReportAdActivityResult>> ReportAdActivityAsync(ReportAdActivityRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ReportAdActivityResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ReportAdActivityResult>>();

            PlayFab.PlayFabClientAPI.ReportAdActivity(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ReportAdActivityResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ReportAdActivityResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResponse>> ReportDeviceInfoAsync(DeviceInfoRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>>();

            PlayFab.PlayFabClientAPI.ReportDeviceInfo(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ReportPlayerClientResult>> ReportPlayerAsync(ReportPlayerClientRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ReportPlayerClientResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ReportPlayerClientResult>>();

            PlayFab.PlayFabClientAPI.ReportPlayer(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ReportPlayerClientResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ReportPlayerClientResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RestoreIOSPurchasesResult>> RestoreIOSPurchasesAsync(RestoreIOSPurchasesRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RestoreIOSPurchasesResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RestoreIOSPurchasesResult>>();

            PlayFab.PlayFabClientAPI.RestoreIOSPurchases(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RestoreIOSPurchasesResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RestoreIOSPurchasesResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<RewardAdActivityResult>> RewardAdActivityAsync(RewardAdActivityRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<RewardAdActivityResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<RewardAdActivityResult>>();

            PlayFab.PlayFabClientAPI.RewardAdActivity(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RewardAdActivityResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<RewardAdActivityResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<SendAccountRecoveryEmailResult>> SendAccountRecoveryEmailAsync(SendAccountRecoveryEmailRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<SendAccountRecoveryEmailResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<SendAccountRecoveryEmailResult>>();

            PlayFab.PlayFabClientAPI.SendAccountRecoveryEmail(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SendAccountRecoveryEmailResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SendAccountRecoveryEmailResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<SetFriendTagsResult>> SetFriendTagsAsync(SetFriendTagsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<SetFriendTagsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<SetFriendTagsResult>>();

            PlayFab.PlayFabClientAPI.SetFriendTags(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetFriendTagsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetFriendTagsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<SetPlayerSecretResult>> SetPlayerSecretAsync(SetPlayerSecretRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<SetPlayerSecretResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<SetPlayerSecretResult>>();

            PlayFab.PlayFabClientAPI.SetPlayerSecret(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetPlayerSecretResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<SetPlayerSecretResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<StartPurchaseResult>> StartPurchaseAsync(StartPurchaseRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<StartPurchaseResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<StartPurchaseResult>>();

            PlayFab.PlayFabClientAPI.StartPurchase(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<StartPurchaseResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<StartPurchaseResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>> SubtractUserVirtualCurrencyAsync(SubtractUserVirtualCurrencyRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>>();

            PlayFab.PlayFabClientAPI.SubtractUserVirtualCurrency(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ModifyUserVirtualCurrencyResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkAndroidDeviceIDResult>> UnlinkAndroidDeviceIDAsync(UnlinkAndroidDeviceIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkAndroidDeviceIDResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkAndroidDeviceIDResult>>();

            PlayFab.PlayFabClientAPI.UnlinkAndroidDeviceID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkAndroidDeviceIDResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkAndroidDeviceIDResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResponse>> UnlinkAppleAsync(UnlinkAppleRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>>();

            PlayFab.PlayFabClientAPI.UnlinkApple(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkCustomIDResult>> UnlinkCustomIDAsync(UnlinkCustomIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkCustomIDResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkCustomIDResult>>();

            PlayFab.PlayFabClientAPI.UnlinkCustomID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkCustomIDResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkCustomIDResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkFacebookAccountResult>> UnlinkFacebookAccountAsync(UnlinkFacebookAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkFacebookAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkFacebookAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkFacebookAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkFacebookAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkFacebookAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkFacebookInstantGamesIdResult>> UnlinkFacebookInstantGamesIdAsync(UnlinkFacebookInstantGamesIdRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkFacebookInstantGamesIdResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkFacebookInstantGamesIdResult>>();

            PlayFab.PlayFabClientAPI.UnlinkFacebookInstantGamesId(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkFacebookInstantGamesIdResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkFacebookInstantGamesIdResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkGameCenterAccountResult>> UnlinkGameCenterAccountAsync(UnlinkGameCenterAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkGameCenterAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkGameCenterAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkGameCenterAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkGameCenterAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkGameCenterAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkGoogleAccountResult>> UnlinkGoogleAccountAsync(UnlinkGoogleAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkGoogleAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkGoogleAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkGoogleAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkGoogleAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkGoogleAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkGooglePlayGamesServicesAccountResult>> UnlinkGooglePlayGamesServicesAccountAsync(UnlinkGooglePlayGamesServicesAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkGooglePlayGamesServicesAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkGooglePlayGamesServicesAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkGooglePlayGamesServicesAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkGooglePlayGamesServicesAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkGooglePlayGamesServicesAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkIOSDeviceIDResult>> UnlinkIOSDeviceIDAsync(UnlinkIOSDeviceIDRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkIOSDeviceIDResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkIOSDeviceIDResult>>();

            PlayFab.PlayFabClientAPI.UnlinkIOSDeviceID(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkIOSDeviceIDResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkIOSDeviceIDResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkKongregateAccountResult>> UnlinkKongregateAsync(UnlinkKongregateAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkKongregateAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkKongregateAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkKongregate(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkKongregateAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkKongregateAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResponse>> UnlinkNintendoServiceAccountAsync(UnlinkNintendoServiceAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>>();

            PlayFab.PlayFabClientAPI.UnlinkNintendoServiceAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkNintendoSwitchDeviceIdResult>> UnlinkNintendoSwitchDeviceIdAsync(UnlinkNintendoSwitchDeviceIdRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkNintendoSwitchDeviceIdResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkNintendoSwitchDeviceIdResult>>();

            PlayFab.PlayFabClientAPI.UnlinkNintendoSwitchDeviceId(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkNintendoSwitchDeviceIdResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkNintendoSwitchDeviceIdResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResponse>> UnlinkOpenIdConnectAsync(UnlinkOpenIdConnectRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>>();

            PlayFab.PlayFabClientAPI.UnlinkOpenIdConnect(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkPSNAccountResult>> UnlinkPSNAccountAsync(UnlinkPSNAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkPSNAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkPSNAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkPSNAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkPSNAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkPSNAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkSteamAccountResult>> UnlinkSteamAccountAsync(UnlinkSteamAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkSteamAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkSteamAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkSteamAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkSteamAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkSteamAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkTwitchAccountResult>> UnlinkTwitchAsync(UnlinkTwitchAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkTwitchAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkTwitchAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkTwitch(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkTwitchAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkTwitchAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlinkXboxAccountResult>> UnlinkXboxAccountAsync(UnlinkXboxAccountRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlinkXboxAccountResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlinkXboxAccountResult>>();

            PlayFab.PlayFabClientAPI.UnlinkXboxAccount(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkXboxAccountResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlinkXboxAccountResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlockContainerItemResult>> UnlockContainerInstanceAsync(UnlockContainerInstanceRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlockContainerItemResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlockContainerItemResult>>();

            PlayFab.PlayFabClientAPI.UnlockContainerInstance(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlockContainerItemResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlockContainerItemResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UnlockContainerItemResult>> UnlockContainerItemAsync(UnlockContainerItemRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UnlockContainerItemResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UnlockContainerItemResult>>();

            PlayFab.PlayFabClientAPI.UnlockContainerItem(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlockContainerItemResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UnlockContainerItemResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<EmptyResponse>> UpdateAvatarUrlAsync(UpdateAvatarUrlRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<EmptyResponse>>();

            PlayFab.PlayFabClientAPI.UpdateAvatarUrl(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<EmptyResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UpdateCharacterDataResult>> UpdateCharacterDataAsync(UpdateCharacterDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UpdateCharacterDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UpdateCharacterDataResult>>();

            PlayFab.PlayFabClientAPI.UpdateCharacterData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateCharacterDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateCharacterDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UpdateCharacterStatisticsResult>> UpdateCharacterStatisticsAsync(UpdateCharacterStatisticsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UpdateCharacterStatisticsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UpdateCharacterStatisticsResult>>();

            PlayFab.PlayFabClientAPI.UpdateCharacterStatistics(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateCharacterStatisticsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateCharacterStatisticsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UpdatePlayerStatisticsResult>> UpdatePlayerStatisticsAsync(UpdatePlayerStatisticsRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UpdatePlayerStatisticsResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UpdatePlayerStatisticsResult>>();

            PlayFab.PlayFabClientAPI.UpdatePlayerStatistics(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdatePlayerStatisticsResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdatePlayerStatisticsResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UpdateSharedGroupDataResult>> UpdateSharedGroupDataAsync(UpdateSharedGroupDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UpdateSharedGroupDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UpdateSharedGroupDataResult>>();

            PlayFab.PlayFabClientAPI.UpdateSharedGroupData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateSharedGroupDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateSharedGroupDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UpdateUserDataResult>> UpdateUserDataAsync(UpdateUserDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UpdateUserDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UpdateUserDataResult>>();

            PlayFab.PlayFabClientAPI.UpdateUserData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateUserDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateUserDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UpdateUserDataResult>> UpdateUserPublisherDataAsync(UpdateUserDataRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UpdateUserDataResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UpdateUserDataResult>>();

            PlayFab.PlayFabClientAPI.UpdateUserPublisherData(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateUserDataResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateUserDataResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<UpdateUserTitleDisplayNameResult>> UpdateUserTitleDisplayNameAsync(UpdateUserTitleDisplayNameRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<UpdateUserTitleDisplayNameResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<UpdateUserTitleDisplayNameResult>>();

            PlayFab.PlayFabClientAPI.UpdateUserTitleDisplayName(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateUserTitleDisplayNameResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<UpdateUserTitleDisplayNameResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ValidateAmazonReceiptResult>> ValidateAmazonIAPReceiptAsync(ValidateAmazonReceiptRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ValidateAmazonReceiptResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ValidateAmazonReceiptResult>>();

            PlayFab.PlayFabClientAPI.ValidateAmazonIAPReceipt(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ValidateAmazonReceiptResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ValidateAmazonReceiptResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ValidateGooglePlayPurchaseResult>> ValidateGooglePlayPurchaseAsync(ValidateGooglePlayPurchaseRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ValidateGooglePlayPurchaseResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ValidateGooglePlayPurchaseResult>>();

            PlayFab.PlayFabClientAPI.ValidateGooglePlayPurchase(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ValidateGooglePlayPurchaseResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ValidateGooglePlayPurchaseResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ValidateIOSReceiptResult>> ValidateIOSReceiptAsync(ValidateIOSReceiptRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ValidateIOSReceiptResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ValidateIOSReceiptResult>>();

            PlayFab.PlayFabClientAPI.ValidateIOSReceipt(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ValidateIOSReceiptResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ValidateIOSReceiptResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<ValidateWindowsReceiptResult>> ValidateWindowsStoreReceiptAsync(ValidateWindowsReceiptRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<ValidateWindowsReceiptResult>> tcs = new TaskCompletionSource<PlayFabAsyncResult<ValidateWindowsReceiptResult>>();

            PlayFab.PlayFabClientAPI.ValidateWindowsStoreReceipt(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ValidateWindowsReceiptResult>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<ValidateWindowsReceiptResult>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<WriteEventResponse>> WriteCharacterEventAsync(WriteClientCharacterEventRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<WriteEventResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<WriteEventResponse>>();

            PlayFab.PlayFabClientAPI.WriteCharacterEvent(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<WriteEventResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<WriteEventResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<WriteEventResponse>> WritePlayerEventAsync(WriteClientPlayerEventRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<WriteEventResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<WriteEventResponse>>();

            PlayFab.PlayFabClientAPI.WritePlayerEvent(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<WriteEventResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<WriteEventResponse>(null, error));
            });

            return tcs.Task;
        }
        public static Task<PlayFabAsyncResult<WriteEventResponse>> WriteTitleEventAsync(WriteTitleEventRequest request)
        {
            TaskCompletionSource<PlayFabAsyncResult<WriteEventResponse>> tcs = new TaskCompletionSource<PlayFabAsyncResult<WriteEventResponse>>();

            PlayFab.PlayFabClientAPI.WriteTitleEvent(request, result =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<WriteEventResponse>(result, null));
            }, error =>
            {
                tcs.TrySetResult(new PlayFabAsyncResult<WriteEventResponse>(null, error));
            });

            return tcs.Task;
        }
    }
}
