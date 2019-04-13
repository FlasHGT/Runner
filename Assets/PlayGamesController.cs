using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGamesController : MonoBehaviour
{
	public static PlayGamesController Instance = null;

	private bool loggedIn = false;

	public void AuthenticateUser()
	{
		if(!loggedIn)
		{
			PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
			PlayGamesPlatform.InitializeInstance(config);
			PlayGamesPlatform.Activate();

			Social.localUser.Authenticate((bool success) =>
			{
				loggedIn = true;
			});
		}
	}

	public void PostToLeaderboard(long newScore)
	{
		Social.ReportScore(newScore, GPGSIds.leaderboard_high_score, (bool success) =>
		{
			
		});
	}

	public void ShowLeaderboardUI ()
	{
		if(loggedIn)
		{
			PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_score);
		}
	}

	private void Awake()
	{
		if(!Instance)
		{
			Instance = this;
			DontDestroyOnLoad(this);
			return;
		}
		Destroy(this);
	}
}
