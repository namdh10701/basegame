///////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Welcome to your first Cloud Script revision!
//
// Cloud Script runs in the PlayFab cloud and has full access to the PlayFab Game Server API 
// (https://api.playfab.com/Documentation/Server), and it runs in the context of a securely
// authenticated player, so you can use it to implement logic for your game that is safe from
// client-side exploits. 
//
// Cloud Script functions can also make web requests to external HTTP
// endpoints, such as a database or private API for your title, which makes them a flexible
// way to integrate with your existing backend systems.
//
// There are several different options for calling Cloud Script functions:
//
// 1) Your game client calls them directly using the "ExecuteCloudScript" API,
// passing in the function name and arguments in the request and receiving the 
// function return result in the response.
// (https://api.playfab.com/Documentation/Client/method/ExecuteCloudScript)
// 
// 2) You create PlayStream event actions that call them when a particular 
// event occurs, passing in the event and associated player profile data.
// (https://api.playfab.com/playstream/docs)
// 
// 3) For titles using the Photon Add-on (https://playfab.com/marketplace/photon/),
// Photon room events trigger webhooks which call corresponding Cloud Script functions.
// 
// The following examples demonstrate all three options.
//
///////////////////////////////////////////////////////////////////////////////////////////////////////

const ProfileField = Object.freeze({
    Level: 'Level',
    Exp: 'Exp',
    Rank: 'Rank',
    RankScore: 'RankScore',
    CurrentRankID: 'CurrentRankID',
    LimitPackages: 'LimitPackages',
    Gachas: 'Gachas'
});

const TitleReadOnlyData = Object.freeze({
    RankInfo: 'RankInfo',
    RankUnrank: 'RankUnrank',
    RankRookie: 'RankRookie',
    RankGunner: 'RankGunner',
    RankHunter: 'RankHunter',
    RankCaptain: 'RankCaptain',
    RankConquer: 'RankConquer'
});

const EItemType = Object.freeze({
    Ship: 'ship', // 0
    Crew: 'crew', // 
    Cannon: 'cannon', Ammo: 'ammo', Blueprint: 'blueprint',
});

const EDatabase = Object.freeze({
    ShipsDB: 'ShipsDB',
    CrewsDB: 'CrewsDB',
    CannonsDB: 'CannonsDB',
    CannonFeversDB: 'CannonsDB',
    AmmosDB: 'AmmosDB',
    CannonUpgradeDB: 'CannonUpgradeDB',
    AmmoUpgradeDB: 'AmmoUpgradeDB',
    ShipUpgradeDB: 'ShipUpgradeDB',
    GachasDB: 'GachasDB',
    GachaItemsDB: 'GachaItemsDB',
});

const ERarity = Object.freeze({
    Common: 0, Good: 1, Rare: 2, Epic: 3, Legendary: 4
});

const EVirtualCurrency = Object.freeze({
    Gold: 'GO', Gem: 'GE', Energy: 'EN', Ticket: 'TI', Diamond: 'DI', Key: 'KE'
});

const ERank = Object.freeze({
    Unrank: 'Unrank', Rookie: 'Rookie', Gunner: 'Gunner', Hunter: 'Hunter', Captain: 'Captain', Conquer: 'Conquer'
});

const EErrorCode = Object.freeze({
    NotEnoughItem: 1000,
    ItemNotFound: 1001,
    ItemNotMatch: 1002,
    NotEnoughGold: 1003,
    NotEnoughBlueprint: 1004,
    NotEnoughEnergy: 1005,
    NotEnoughTicket: 1006,
    PackageLimited: 1007,
    NotEnoughGem: 1008,
    NotEnoughKey: 1009
});

const Total_Player_Per_Rank_Group = 50;

const GetItemUpgradeDB = function (itemType) {
    switch (itemType) {
        case EItemType.Ship:
            return EDatabase.ShipUpgradeDB;

        case EItemType.Cannon:
            return EDatabase.CannonUpgradeDB;

        case EItemType.Ammo:
            return EDatabase.AmmoUpgradeDB;
    }
    return "";
};

const GetItemDB = function (itemType) {
    switch (itemType) {
        case EItemType.Ship:
            return EDatabase.ShipsDB;

        case EItemType.Cannon:
            return EDatabase.CannonsDB;

        case EItemType.Ammo:
            return EDatabase.AmmosDB;

        case EItemType.Crew:
            return EDatabase.CrewsDB;
    }
    return "";
};

// Profile Script
handlers.RequestNewProfile = function (args, context) {
    // Default Profile
    var userData = {};
    userData[ProfileField.Level] = 1;
    userData[ProfileField.Exp] = 0;
    userData[ProfileField.Rank] = ERank.Unrank;
    userData[ProfileField.RankScore] = 0;
    userData[ProfileField.CurrentRankID] = "";
    userData[ProfileField.LimitPackages] = [];
    userData[ProfileField.Gachas] = [];

    var reqReadOnlyData = {
        PlayFabId: currentPlayerId, Data: userData
    };
    var resultData = server.UpdateUserReadOnlyData(reqReadOnlyData);
};

handlers.CombineItems = function (args, context) {
    if (args.ItemInstanceIds.length < 2) {
        return {
            Result: false, Error: EErrorCode.NotEnoughItem
        };
    }

    var reqInventory = {
        PlayFabId: currentPlayerId
    };
    var resInventory = server.GetUserInventory(reqInventory);

    let CombineItems = [];
    for (let i = 0; i < args.ItemInstanceIds.length; i++) {
        var item = resInventory.Inventory.find(val => val.ItemInstanceId == args.ItemInstanceIds[i]);
        if (item != null) {
            CombineItems.push(item);
        } else {
            return {
                Result: false, Error: EErrorCode.ItemNotFound
            }
        }
    }

    const parts = CombineItems[0].ItemId.split('_');
    var itemLevel = CombineItems[0].CustomData?.Level ?? 0;
    const itemType = parts[0];
    var configId = CombineItems[0].ItemId;

    for (let i = 1; i < CombineItems.length; i++) {
        if (CombineItems[i].ItemId != configId) {
            return {
                Result: false, Error: EErrorCode.ItemNotMatch
            }
        }
    }

    // Refund Blueprints
    var blueprints = RefundBlueprints(itemType, CombineItems);

    configId = parts[1];
    return CombineItem(configId, itemType, itemLevel, blueprints);
};

const RefundBlueprints = function (itemType, combineItems) {
    var keyDB = GetItemUpgradeDB(itemType);
    var resUpgrade = titleDataCache.GetTitleData({Keys: keyDB});
    var itemUpgrade = JSON.parse(resUpgrade.Data[keyDB]);

    let grantBlueprints = [];
    let revokeItems = [];
    combineItems.forEach(item => {
        const level = parseInt(item.CustomData?.Level ?? 0) + 1;
        for (let lv = 1; lv <= level; lv++) {
            var upgradeConfig = itemUpgrade.find(val => val.level === lv);
            for (let j = 0; j < upgradeConfig.blueprint; j++) grantBlueprints.push('blueprint_' + itemType);
        }
        revokeItems.push({
            PlayFabId: currentPlayerId, ItemInstanceId: item.ItemInstanceId,
        });
    });

    var resRevokeItems = server.RevokeInventoryItems({
        Items: revokeItems
    });

    return grantBlueprints;
}

const CombineItem = function (configId, itemType, itemLevel, blueprints) {
    var keyDB = GetItemDB(itemType);
    let resConfig = titleDataCache.GetTitleData({Keys: keyDB});
    let itemConfig = JSON.parse(resConfig.Data[keyDB]);

    let curConfig = itemConfig.find(val => val.id == configId);
    let newCannonConfig = itemConfig.find(val => val.id == curConfig.upgrade_id);
    log.debug("id", newCannonConfig.id);
    let itemId = itemType + '_' + newCannonConfig.id;

    blueprints.push(itemId);
    let resGrantItems = server.GrantItemsToUser({
        PlayFabId: currentPlayerId, ItemIds: blueprints,
    });

    let craftItem = resGrantItems.ItemGrantResults.find(val => val.ItemId == itemId);
    let refundBlueprints = resGrantItems.ItemGrantResults.filter(val => val.ItemId != itemId);
    craftItem.CustomData = {
        Level: itemLevel
    };

    var updateItem = server.UpdateUserInventoryItemCustomData({
        PlayFabId: currentPlayerId, ItemInstanceId: craftItem.ItemInstanceId, Data: craftItem.CustomData
    });

    return {
        Result: true, Item: craftItem, RefundBlueprints: refundBlueprints
    };
};

handlers.UpgradeItem = function (args, context) {
    var reqInventory = {
        PlayFabId: currentPlayerId
    };
    var resultInventory = server.GetUserInventory(reqInventory);

    var upgradeItem = resultInventory.Inventory.find(val => val.ItemInstanceId == args.ItemInstanceId);
    if (upgradeItem != null) {
        const nextLevel = parseInt(upgradeItem.CustomData?.Level ?? 0) + 1;
        const parts = upgradeItem.ItemId.split('_');
        const itemType = parts[0];

        let blueprintId = "";
        switch (itemType) {
            case EItemType.Ship:
                blueprintId = EItemType.Blueprint + '_' + EItemType.Ship;
                break;
            case EItemType.Cannon:
                blueprintId = EItemType.Blueprint + '_' + EItemType.Cannon;
                break;
            case EItemType.Ammo:
                blueprintId = EItemType.Blueprint + '_' + EItemType.Ammo;
                break;
        }

        // Get Upgrade Config
        var keyUpgradeDB = GetItemUpgradeDB(itemType);
        var resConfig = titleDataCache.GetTitleData({
            Keys: [keyUpgradeDB]
        });
        var nextLevelConfig = JSON.parse(resConfig.Data[keyUpgradeDB]).find(val => val.level === nextLevel);

        // Get Player Virtual Currency
        var resInventory = server.GetUserInventory({PlayFabId: currentPlayerId});
        if (resInventory.VirtualCurrency[EVirtualCurrency.Gold] < nextLevelConfig.gold) {
            return {
                Result: false, Error: EErrorCode.NotEnoughGold
            };
        }

        // Get Player Blueprints
        var blueprints = resultInventory.Inventory.filter(val => val.ItemId == blueprintId);
        if (blueprints.length < nextLevelConfig.blueprint) {
            return {
                Result: false, Error: EErrorCode.NotEnoughBlueprint
            };
        }

        // Increase Level Item
        upgradeItem.CustomData = {
            Level: nextLevel,
        };
        var reqUpgrade = {
            PlayFabId: currentPlayerId, ItemInstanceId: args.ItemInstanceId, Data: {
                Level: upgradeItem.CustomData.Level
            }
        };
        var resUpgrade = server.UpdateUserInventoryItemCustomData(reqUpgrade);

        // Substract Gold
        var resSubGold = server.SubtractUserVirtualCurrency({
            PlayFabId: currentPlayerId, VirtualCurrency: EVirtualCurrency.Gold, Amount: nextLevelConfig.gold
        });

        // Substract Blueprint
        var revokeIDs = [];
        for (let i = 0; i < nextLevelConfig.blueprint; i++) {
            var reqConsume = {
                PlayFabId: currentPlayerId, ItemInstanceId: blueprints[i].ItemInstanceId, ConsumeCount: 1
            };
            var resSubBlueprint = server.ConsumeItem(reqConsume);
            revokeIDs.push(resSubBlueprint.ItemInstanceId);
        }

        var resultResponse = {
            Result: true, ItemUpgrade: upgradeItem, VirtualCurrency: {}, RevokeBlueprintIDs: revokeIDs
        };

        resultResponse.VirtualCurrency[EVirtualCurrency.Gold] = resSubGold.Balance;

        return resultResponse;
    }

    return {
        Result: false, Error: EErrorCode.ItemNotFound
    }
};

handlers.GetRankInfo = function (args, context) {
    let reqReadOnlyData = {
        PlayFabId: currentPlayerId,
        Keys: [ProfileField.Rank, ProfileField.CurrentRankID]
    };
    let resData = server.GetUserReadOnlyData(reqReadOnlyData);

    let userRank = ERank.Unrank;
    let userRankScore = 0;
    let userRankId = "";
    if (resData.Data.hasOwnProperty(ProfileField.Rank)) {
        userRank = resData.Data[ProfileField.Rank].Value;
    }
    if (resData.Data.hasOwnProperty(ProfileField.CurrentRankID)) {
        userRankId = resData.Data[ProfileField.CurrentRankID].Value;
    }

    // Rank Info
    let userRankDB = 'Rank' + userRank;
    let keys = [TitleReadOnlyData.RankInfo, userRankDB];
    let resTitleData = server.GetTitleInternalData({Keys: keys});
    let userRankData = JSON.parse(resTitleData.Data[userRankDB]);
    let userRankInfo = userRankData.find(val => val.Id == userRankId);
    SortArrayByKey(userRankInfo.Players, 'Score', 'desc');

    return {
        Result: true,
        RankInfo: JSON.parse(resTitleData.Data[TitleReadOnlyData.RankInfo]),
        UserRankInfo: userRankInfo
    };
};

handlers.CreateRankTicket = function (args, context) {
    // TODO move to config
    let energyPerMatch = 1;
    let ticketPerMatch = 1;
    
    var resInventory = server.GetUserInventory({PlayFabId: currentPlayerId});
    if (resInventory.VirtualCurrency[EVirtualCurrency.Energy] < energyPerMatch) {
        return {
            Result: false, Error: EErrorCode.NotEnoughEnergy
        };
    }

    if (resInventory.VirtualCurrency[EVirtualCurrency.Ticket] <= 0) {
        return {
            Result: false, Error: EErrorCode.NotEnoughTicket
        };
    }

    let result = {Result: true, VirtualCurrency: {}};
    var resSubEnergy = server.SubtractUserVirtualCurrency({
        PlayFabId: currentPlayerId, VirtualCurrency: EVirtualCurrency.Energy, Amount: energyPerMatch
    });
    result.VirtualCurrency[EVirtualCurrency.Energy] = resSubEnergy.Balance

    var resSubTicket = server.SubtractUserVirtualCurrency({
        PlayFabId: currentPlayerId, VirtualCurrency: EVirtualCurrency.Ticket, Amount: ticketPerMatch
    });
    result.VirtualCurrency[EVirtualCurrency.Ticket] = resSubTicket.Balance

    CheckJoinRank(currentPlayerId);

    return result;
};

const CheckJoinRank = function (playfabId) {
    let reqReadOnlyData = {
        PlayFabId: currentPlayerId,
        Keys: [ProfileField.Rank, ProfileField.RankScore, ProfileField.CurrentRankID]
    };
    let resData = server.GetUserReadOnlyData(reqReadOnlyData);

    let currentRankId = resData.Data[ProfileField.CurrentRankID].Value;
    let userRank = resData.Data[ProfileField.Rank].Value;

    if (currentRankId == "") {
        var resProfile = server.GetPlayerProfile({PlayFabId: currentPlayerId});

        let rankGroup = 'Rank' + userRank;
        let resTitleData = server.GetTitleInternalData({Keys: [rankGroup]});
        let rankData = JSON.parse(resTitleData.Data[rankGroup]);
        let userRankInfo = null;

        var userRankProfile = {
            Id: currentPlayerId,
            Name: resProfile.PlayerProfile.DisplayName,
            Score: 0
        };

        for (let i = 0; i < rankData.length; i++) {
            if (rankData[i].Count < Total_Player_Per_Rank_Group) {
                currentRankId = rankData[i].Id;
                rankData[i].Count++;
                rankData[i].Players.push(userRankProfile);
                userRankInfo = rankData[i];
                break;
            }
        }

        if (currentRankId == "") {
            currentRankId = GenerateGUID();
            userRankInfo = {
                Id: currentRankId,
                Count: 1,
                Players: [userRankProfile]
            };
            rankData.push(userRankInfo);
        }

        var resUpdate = server.SetTitleInternalData({
            Key: 'Rank' + userRank,
            Value: JSON.stringify(rankData)
        });

        var userData = {};
        userData[ProfileField.CurrentRankID] = currentRankId;
        var resUserData = server.UpdateUserReadOnlyData({
            PlayFabId: currentPlayerId, Data: userData
        });

        return {
            Result: true,
            UserRankInfo: userRankInfo
        }
    }
    return {
        Result: false
    };
};

handlers.SubmitRankingMatchAsync = function (args, context) {
    var resReadOnlyData = server.GetUserReadOnlyData({
        PlayFabId: currentPlayerId,
        Keys: [ProfileField.Rank, ProfileField.RankScore, ProfileField.CurrentRankID]
    });

    let newData = {};
    newData[ProfileField.RankScore] = parseInt(resReadOnlyData.Data[ProfileField.RankScore].Value, 10) + args.Score;
    server.UpdateUserReadOnlyData({
        PlayFabId: currentPlayerId,
        Data: newData
    });

    let userRankDB = 'Rank' + resReadOnlyData.Data[ProfileField.Rank].Value;
    let resTitleData = server.GetTitleInternalData({Keys: [userRankDB]});
    let rankData = JSON.parse(resTitleData.Data[userRankDB]);
    let userRankInfo = rankData.find(val => val.Id == resReadOnlyData.Data[ProfileField.CurrentRankID].Value);

    userRankInfo.Players.find(val => val.Id == currentPlayerId).Score = newData[ProfileField.RankScore];
    var resUpdate = server.SetTitleInternalData({
        Key: userRankDB,
        Value: JSON.stringify(rankData)
    });

    return {
        Result: true,
        UserRankInfo: userRankInfo
    };
};

handlers.ReportLimitPackage = function (args, context) {
    let reqReadOnlyData = {
        PlayFabId: currentPlayerId,
        Keys: [ProfileField.LimitPackages]
    };
    let resData = server.GetUserReadOnlyData(reqReadOnlyData);

    let unixTimestamp = Math.floor(Date.now() / 1000);

    var limitPackages = [];
    if (resData.Data.hasOwnProperty(ProfileField.LimitPackages)) {
        limitPackages = JSON.parse(resData.Data[ProfileField.LimitPackages].Value);
    }

    let limitPackage = limitPackages.find(val => val.Id == args.AdUnitId);
    if (limitPackage != null) {
        limitPackage.LastTime = unixTimestamp;
        limitPackage.Count += 1;
    } else {
        let newLimitPackage = {
            Id: args.AdUnitId,
            LastTime: unixTimestamp,
            Count: 1
        };
        limitPackages.push(newLimitPackage);
    }

    var newLimitPackages = {};
    newLimitPackages[ProfileField.LimitPackages] = JSON.stringify(limitPackages);
    server.UpdateUserReadOnlyData({
        PlayFabId: currentPlayerId, Data: newLimitPackages
    });

    return {
        Result: true,
        LimitPackages: limitPackages
    };
};

handlers.RequestGacha = function (args, context) {
    let resTitleData = server.GetTitleInternalData({Keys: [EDatabase.GachasDB, EDatabase.GachaItemsDB]});
    let gachaPackages = JSON.parse(resTitleData.Data[EDatabase.GachasDB]);
    let gachaPack = gachaPackages.find(val => val.id == args.GachaID);

    if (gachaPack == null) {
        return {
            Result: false, Error: EErrorCode.ItemNotFound
        };
    }

    // Get Player Virtual Currency
    var resInventory = server.GetUserInventory({PlayFabId: currentPlayerId});
    switch (gachaPack.virtual_currency) {
        case EVirtualCurrency.Gem:
            if (resInventory.VirtualCurrency[EVirtualCurrency.Gem] < gachaPack.price) {
                return {
                    Result: false, Error: EErrorCode.NotEnoughGem
                };
            }
            break;

        case EVirtualCurrency.Key:
            if (resInventory.VirtualCurrency[EVirtualCurrency.Key] < gachaPack.price) {
                return {
                    Result: false, Error: EErrorCode.NotEnoughKey
                };
            }
            break;

        default: {
            return {
                Result: false, Error: EErrorCode.ItemNotFound
            };
        }
    }

    var result = {Result: true, VirtualCurrency: {}};
    switch (gachaPack.virtual_currency) {
        case EVirtualCurrency.Gem:
            var resSubGem = server.SubtractUserVirtualCurrency({
                PlayFabId: currentPlayerId, VirtualCurrency: EVirtualCurrency.Gem, Amount: gachaPack.price
            });
            result.VirtualCurrency[EVirtualCurrency.Gem] = resSubGem.Balance
            break;

        case EVirtualCurrency.Key:
            var resSubKey = server.SubtractUserVirtualCurrency({
                PlayFabId: currentPlayerId, VirtualCurrency: EVirtualCurrency.Key, Amount: gachaPack.price
            });
            result.VirtualCurrency[EVirtualCurrency.Key] = resSubKey.Balance
            break;
    }

    // User Data
    let resUserData = server.GetUserReadOnlyData({
        PlayFabId: currentPlayerId,
        Keys: [ProfileField.Gachas]
    });

    let userGachas = [];
    let userGacha = null;
    if (resUserData.Data.hasOwnProperty(ProfileField.Gachas)) {
        userGachas = JSON.parse(resUserData.Data[ProfileField.Gachas].Value);
        userGacha = userGachas.find(val => val.Id == args.GachaID);
    }

    if (userGacha == null) {
        userGacha = {Id: args.GachaID, CountRare: 0, CountEpic: 0};
        userGachas.push(userGacha);
    }

    let gachaItemsDB = JSON.parse(resTitleData.Data[EDatabase.GachaItemsDB]);
    let gachaItems = [];

    let amount = 0;
    for (let i = 0; i < gachaPack.amount; i++) {
        let itemRarity = RandomItemRarity(gachaPack);

        if (itemRarity == ERarity.Epic || userGacha.CountEpic >= 100) {
            gachaItems.push(RandomItem(gachaItemsDB, gachaPack.type, ERarity.Epic));
            userGacha.CountEpic = 0;
        }
        if (itemRarity == ERarity.Rare || userGacha.CountRare >= 10) {
            gachaItems.push(RandomItem(gachaItemsDB, gachaPack.type, ERarity.Rare));
            userGacha.CountRare = 0;
        } else {
            gachaItems.push(RandomItem(gachaItemsDB, gachaPack.type, itemRarity));
            userGacha.CountRare += 1;
            userGacha.CountEpic += 1;
        }
    }

    let resGrantItem = server.GrantItemsToUser({
        PlayFabId: currentPlayerId, ItemIds: gachaItems,
    });

    let readOnlyData = {};
    readOnlyData[ProfileField.Gachas] = JSON.stringify(userGachas);
    let resultData = server.UpdateUserReadOnlyData({
        PlayFabId: currentPlayerId, Data: readOnlyData
    });

    result['Items'] = resGrantItem.ItemGrantResults;
    return result;
};

const RandomItemRarity = function (gachaPack) {
    const totalWeight = gachaPack.weight_epic + gachaPack.weight_rare + gachaPack.weight_good + gachaPack.weight_common;
    let randomVal = Math.floor(Math.random() * totalWeight);
    let itemRarity = ERarity.Common;

    if (randomVal <= gachaPack.weight_epic) {
        itemRarity = ERarity.Epic;
    } else if (randomVal <= gachaPack.weight_epic + gachaPack.weight_rare) {
        itemRarity = ERarity.Rare;
    } else if (randomVal <= gachaPack.weight_epic + gachaPack.weight_rare + gachaPack.weight_good) {
        itemRarity = ERarity.Good;
    } else {
        itemRarity = ERarity.Common;
    }

    return itemRarity;
}

const RandomItem = function (gachaItemsDB, type, rarity) {
    let items = gachaItemsDB.filter(val => val.rarity == rarity && val.type == type);
    const totalWeight = items.reduce((sum, item) => sum + item.weight, 0);
    let randomValue = Math.random() * totalWeight;

    for (const item of items) {
        if (randomValue < item.weight) {
            return type + "_" + item.id;
        }
        randomValue -= item.weight;
    }
};

handlers.CampaignComplete = async function (args, context) {

};

// This is a simple example of making a PlayFab server API call
handlers.makeAPICall = function (args, context) {
    var request = {
        PlayFabId: currentPlayerId, Statistics: [{
            StatisticName: "Level", Value: 2
        }]
    };
    // The pre-defined "server" object has functions corresponding to each PlayFab server API 
    // (https://api.playfab.com/Documentation/Server). It is automatically 
    // authenticated as your title and handles all communication with 
    // the PlayFab API, so you don't have to write extra code to issue HTTP requests. 
    var playerStatResult = server.UpdatePlayerStatistics(request);
};

// This an example of a function that calls a PlayFab Entity API. The function is called using the 
// 'ExecuteEntityCloudScript' API (https://api.playfab.com/documentation/CloudScript/method/ExecuteEntityCloudScript).
handlers.makeEntityAPICall = function (args, context) {

    // The profile of the entity specified in the 'ExecuteEntityCloudScript' request.
    // Defaults to the authenticated entity in the X-EntityToken header.
    var entityProfile = context.currentEntity;

    // The pre-defined 'entity' object has functions corresponding to each PlayFab Entity API,
    // including 'SetObjects' (https://api.playfab.com/documentation/Data/method/SetObjects).
    var apiResult = entity.SetObjects({
        Entity: entityProfile.Entity, Objects: [{
            ObjectName: "obj1", DataObject: {
                foo: "some server computed value", prop1: args.prop1
            }
        }]
    });

    return {
        profile: entityProfile, setResult: apiResult.SetResults[0].SetResult
    };
};

// This is a simple example of making a web request to an external HTTP API.
handlers.makeHTTPRequest = function (args, context) {
    var headers = {
        "X-MyCustomHeader": "Some Value"
    };

    var body = {
        input: args, userId: currentPlayerId, mode: "foobar"
    };

    var url = "http://httpbin.org/status/200";
    var content = JSON.stringify(body);
    var httpMethod = "post";
    var contentType = "application/json";

    // The pre-defined http object makes synchronous HTTP requests
    var response = http.request(url, httpMethod, content, contentType, headers);
    return {responseContent: response};
};

// This is a simple example of a function that is called from a
// PlayStream event action. (https://playfab.com/introducing-playstream/)
handlers.handlePlayStreamEventAndProfile = function (args, context) {

    // The event that triggered the action 
    // (https://api.playfab.com/playstream/docs/PlayStreamEventModels)
    var psEvent = context.playStreamEvent;

    // The profile data of the player associated with the event
    // (https://api.playfab.com/playstream/docs/PlayStreamProfileModels)
    var profile = context.playerProfile;

    // Post data about the event to an external API
    var content = JSON.stringify({user: profile.PlayerId, event: psEvent.EventName});
    var response = http.request('https://httpbin.org/status/200', 'post', content, 'application/json', null);

    return {externalAPIResponse: response};
};


// Below are some examples of using Cloud Script in slightly more realistic scenarios

// This is a function that the game client would call whenever a player completes
// a level. It updates a setting in the player's data that only game server
// code can write - it is read-only on the client - and it updates a player
// statistic that can be used for leaderboards. 
//
// A funtion like this could be extended to perform validation on the 
// level completion data to detect cheating. It could also do things like 
// award the player items from the game catalog based on their performance.
handlers.completedLevel = function (args, context) {
    var level = args.levelName;
    var monstersKilled = args.monstersKilled;

    var updateUserDataResult = server.UpdateUserInternalData({
        PlayFabId: currentPlayerId, Data: {
            lastLevelCompleted: level
        }
    });

    log.debug("Set lastLevelCompleted for player " + currentPlayerId + " to " + level);
    var request = {
        PlayFabId: currentPlayerId, Statistics: [{
            StatisticName: "level_monster_kills", Value: monstersKilled
        }]
    };
    server.UpdatePlayerStatistics(request);
    log.debug("Updated level_monster_kills stat for player " + currentPlayerId + " to " + monstersKilled);
};


// In addition to the Cloud Script handlers, you can define your own functions and call them from your handlers. 
// This makes it possible to share code between multiple handlers and to improve code organization.
handlers.updatePlayerMove = function (args) {
    var validMove = processPlayerMove(args);
    return {validMove: validMove};
};


// This is a helper function that verifies that the player's move wasn't made
// too quickly following their previous move, according to the rules of the game.
// If the move is valid, then it updates the player's statistics and profile data.
// This function is called from the "UpdatePlayerMove" handler above and also is 
// triggered by the "RoomEventRaised" Photon room event in the Webhook handler
// below. 
//
// For this example, the script defines the cooldown period (playerMoveCooldownInSeconds)
// as 15 seconds. A recommended approach for values like this would be to create them in Title
// Data, so that they can be queries in the script with a call to GetTitleData
// (https://api.playfab.com/Documentation/Server/method/GetTitleData). This would allow you to
// make adjustments to these values over time, without having to edit, test, and roll out an
// updated script.
function processPlayerMove(playerMove) {
    var now = Date.now();
    var playerMoveCooldownInSeconds = 15;

    var playerData = server.GetUserInternalData({
        PlayFabId: currentPlayerId, Keys: ["last_move_timestamp"]
    });

    var lastMoveTimestampSetting = playerData.Data["last_move_timestamp"];

    if (lastMoveTimestampSetting) {
        var lastMoveTime = Date.parse(lastMoveTimestampSetting.Value);
        var timeSinceLastMoveInSeconds = (now - lastMoveTime) / 1000;
        log.debug("lastMoveTime: " + lastMoveTime + " now: " + now + " timeSinceLastMoveInSeconds: " + timeSinceLastMoveInSeconds);

        if (timeSinceLastMoveInSeconds < playerMoveCooldownInSeconds) {
            log.error("Invalid move - time since last move: " + timeSinceLastMoveInSeconds + "s less than minimum of " + playerMoveCooldownInSeconds + "s.");
            return false;
        }
    }

    var playerStats = server.GetPlayerStatistics({
        PlayFabId: currentPlayerId
    }).Statistics;
    var movesMade = 0;
    for (var i = 0; i < playerStats.length; i++) if (playerStats[i].StatisticName === "") movesMade = playerStats[i].Value;
    movesMade += 1;
    var request = {
        PlayFabId: currentPlayerId, Statistics: [{
            StatisticName: "movesMade", Value: movesMade
        }]
    };
    server.UpdatePlayerStatistics(request);
    server.UpdateUserInternalData({
        PlayFabId: currentPlayerId, Data: {
            last_move_timestamp: new Date(now).toUTCString(), last_move: JSON.stringify(playerMove)
        }
    });

    return true;
}

// This is an example of using PlayStream real-time segmentation to trigger
// game logic based on player behavior. (https://playfab.com/introducing-playstream/)
// The function is called when a player_statistic_changed PlayStream event causes a player 
// to enter a segment defined for high skill players. It sets a key value in
// the player's internal data which unlocks some new content for the player.
handlers.unlockHighSkillContent = function (args, context) {
    var playerStatUpdatedEvent = context.playStreamEvent;
    var request = {
        PlayFabId: currentPlayerId, Data: {
            "HighSkillContent": "true", "XPAtHighSkillUnlock": playerStatUpdatedEvent.StatisticValue.toString()
        }
    };
    var playerInternalData = server.UpdateUserInternalData(request);
    log.info('Unlocked HighSkillContent for ' + context.playerProfile.DisplayName);
    return {profile: context.playerProfile};
};

// Photon Webhooks Integration
//
// The following functions are examples of Photon Cloud Webhook handlers. 
// When you enable the Photon Add-on (https://playfab.com/marketplace/photon/)
// in the Game Manager, your Photon applications are automatically configured
// to authenticate players using their PlayFab accounts and to fire events that 
// trigger your Cloud Script Webhook handlers, if defined. 
// This makes it easier than ever to incorporate multiplayer server logic into your game.


// Triggered automatically when a Photon room is first created
handlers.RoomCreated = function (args) {
    log.debug("Room Created - Game: " + args.GameId + " MaxPlayers: " + args.CreateOptions.MaxPlayers);
};

// Triggered automatically when a player joins a Photon room
handlers.RoomJoined = function (args) {
    log.debug("Room Joined - Game: " + args.GameId + " PlayFabId: " + args.UserId);
};

// Triggered automatically when a player leaves a Photon room
handlers.RoomLeft = function (args) {
    log.debug("Room Left - Game: " + args.GameId + " PlayFabId: " + args.UserId);
};

// Triggered automatically when a Photon room closes
// Note: currentPlayerId is undefined in this function
handlers.RoomClosed = function (args) {
    log.debug("Room Closed - Game: " + args.GameId);
};

// Triggered automatically when a Photon room game property is updated.
// Note: currentPlayerId is undefined in this function
handlers.RoomPropertyUpdated = function (args) {
    log.debug("Room Property Updated - Game: " + args.GameId);
};

// Triggered by calling "OpRaiseEvent" on the Photon client. The "args.Data" property is 
// set to the value of the "customEventContent" HashTable parameter, so you can use
// it to pass in arbitrary data.
handlers.RoomEventRaised = function (args) {
    var eventData = args.Data;
    log.debug("Event Raised - Game: " + args.GameId + " Event Type: " + eventData.eventType);

    switch (eventData.eventType) {
        case "playerMove":
            processPlayerMove(eventData);
            break;

        default:
            break;
    }
};

handlers.IsLimitWeeklyPackage = function (args, context) {
    let reqReadOnlyData = {
        PlayFabId: currentPlayerId,
        Keys: [ProfileField.LimitPackages]
    };
    let resData = server.GetUserReadOnlyData(reqReadOnlyData);

    let limitPackages = JSON.parse(resData[ProfileField.LimitPackages].Value);
    let limitPack = limitPackages.find(val => val.id == args.WeeklyPackID);
    if (limitPack == null || limitPack.RemainingTime > 0) {
        return {
            Result: false
        };
    }
    return {
        Result: false,
        Error: EErrorCode.PackageLimited
    }
};

handlers.BonusGold = function (args, context) {
    let catelogItems = server.GetCatalogItems({});

    let catelogItem = catelogItems.Catalog.find(val => val.ItemId == args.ItemId);
    if (catelogItem.Bundle != null) {
        let goldAmount = 0;
        if (catelogItem.Bundle.BundledVirtualCurrencies.hasOwnProperty(EVirtualCurrency.Gold)) {
            goldAmount = parseInt(catelogItem.Bundle.BundledVirtualCurrencies[EVirtualCurrency.Gold]);
        }

        if (goldAmount > 0) {
            // Get Level
            let resData = server.GetUserReadOnlyData({
                PlayFabId: currentPlayerId,
                Keys: [ProfileField.Level]
            });
            let level = parseInt(resData.Data[ProfileField.Level].Value);

            var grantResult = server.AddUserVirtualCurrency({
                PlayFabId: currentPlayerId, VirtualCurrency: EVirtualCurrency.Gold, Amount: goldAmount * level
            });

            var virtualCurrency = {};
            virtualCurrency[EVirtualCurrency.Gold] = grantResult.Balance;
            return {
                Result: true, VirtualCurrency: virtualCurrency
            };
        }
        return {
            Result: false,
            Error: EErrorCode.NotEnoughGold
        }
    }
    return {
        Result: false,
        Error: EErrorCode.ItemNotFound
    }
};

///
/// Helper functions
///

function GenerateGUID() {
    const randomPart = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        const r = Math.random() * 16 | 0;
        const v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
    return `${randomPart}`;
}

/**
 * Sort array of object by key name
 * @param array target array
 * @param key object key name
 * @param order asc or desc
 * @returns {*}
 * @constructor
 */
function SortArrayByKey(array, key, order = 'asc') {
    return array.sort((a, b) => {
        if (!a.hasOwnProperty(key) || !b.hasOwnProperty(key)) {
            // Property doesn't exist on either object
            return 0;
        }

        const varA = (typeof a[key] === 'string') ? a[key].toUpperCase() : a[key];
        const varB = (typeof b[key] === 'string') ? b[key].toUpperCase() : b[key];

        let comparison = 0;
        if (varA > varB) {
            comparison = 1;
        } else if (varA < varB) {
            comparison = -1;
        }
        return (order === 'desc') ? (comparison * -1) : comparison;
    });
}

///
/// TileData cache solution
///
class TitleDataCache {
    constructor() {
        this.cache = {
            titleData: null
        };
    }

    _fetchTitleDataFromServer() {
        var request = {
            Keys: Object.values(EDatabase)
        };
        
        var result = server.GetTitleData(request);

        if (result.error) {
            throw new Error(result.error);
        }

        this.cache.titleData = result.Data;
        return result.Data;
    }

    GetTitleData(keys) {
        if (this.cache.titleData) {
            var result = {};
            keys.forEach(key => {
                if (this.cache.titleData.hasOwnProperty(key)) {
                    result[key] = this.cache.titleData[key];
                }
            });
            return result;
        } else {
            var data = this._fetchTitleDataFromServer();
            var result = {};
            keys.forEach(key => {
                if (data.hasOwnProperty(key)) {
                    result[key] = data[key];
                }
            });
            return result;
        }
    }

    clearCache() {
        this.cache.titleData = null;
    }
}

var titleDataCache = new TitleDataCache();

handlers.GetTitleData = function (args, context) {
    var keys = args.Keys || [];
    try {
        var data = titleDataCache.GetTitleData(keys);
        return { Data: data };
    } catch (error) {
        return { error: error.message };
    }
};

handlers.ClearTitleDataCache = function (args, context) {
    titleDataCache.clearCache();
    return { message: "Cache cleared successfully" };
};