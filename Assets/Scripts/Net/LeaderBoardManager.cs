using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class LeaderBoardManager : MonoBehaviour
{
    PlayFabAuthenticationContext authenticationContext;

    public const string SLAUGHTER_BOARD_NAME = "SloughterBoard";

    public string PlayerId => authenticationContext.PlayFabId;

    private void Start()
    {
        
    }

    public void Login(Action OnSuccessCallback)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, (result) =>
        {
            OnSuccess(result);
            OnSuccessCallback();
        }, OnError);
    }

    public void UpdateLeaderBoard(int score, Action successCallBack)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = SLAUGHTER_BOARD_NAME,
                    Value = score
                }
            },
            AuthenticationContext = authenticationContext
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, (obj) => { OnSuccesUpdate(obj); successCallBack(); }, OnError);
    }

    private void OnSuccesUpdate(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log("Leader Board updated ");
    }

    public void GetLeaderBoard(Action<GetLeaderboardResult> OnLeaderBoardResultSuccessfull)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = SLAUGHTER_BOARD_NAME,
            StartPosition = 0,
            MaxResultsCount = 5,
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardResultSuccessfull, OnError);
    }


    private void OnError(PlayFabError obj)
    {
        Debug.Log("Account not created " + obj.Error);
    }

    private void OnSuccess(LoginResult obj)
    {
       authenticationContext = obj.AuthenticationContext;
        Debug.Log("Account created");
    }
}
