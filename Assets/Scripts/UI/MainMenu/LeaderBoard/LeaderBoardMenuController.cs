using PlayFab.ClientModels;
using UnityEngine;
using Utilities;

public class LeaderBoardMenuController : MonoBehaviour, IMenu
{
    [SerializeField]
    private Canvas _leaderBoardCanvas;

    [SerializeField]
    private LeaderBoardRow[] _leaderBoardRows;

    [SerializeField]
    private LeaderBoardManager _leaderBoardManager;

    public string BETTER = "Better than You";
    public string YOU = "(You)";
    public string LOOSER = "Looser";

    public void Close()
    {
        _leaderBoardCanvas.gameObject.SetActive(false);
    }

    public void Display()
    {
        RefreshLeaderboard();
        _leaderBoardCanvas.gameObject.SetActive(true);
    }

    public void RefreshLeaderboard()
    {
        
        _leaderBoardManager.Login(() => _leaderBoardManager.UpdateLeaderBoard(PlayerProgressMonitor.Instance.BodyCount, () => _leaderBoardManager.GetLeaderBoard(OnLeaderBoardResult)));
    }

    private void OnLeaderBoardResult(GetLeaderboardResult obj)
    {
        int i = 0;
        string desc = BETTER;
        foreach(var item in obj.Leaderboard)
        {
            if(item.PlayFabId == _leaderBoardManager.PlayerId)
            {
                desc = YOU;
            }

            _leaderBoardRows[i++].UpdateData(item.Position + 1, desc, item.StatValue);
        }

       for (; i < _leaderBoardRows.Length; i++)
        {
            _leaderBoardRows[i].gameObject.SetActive(false);
        }
    }
}
