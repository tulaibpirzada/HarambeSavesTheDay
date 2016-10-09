using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : Singleton<GameController> 
{
	GameReferences gameRef;
	GameObject gameContextObject;
	public KidMover fetchedKid;
    
    bool isUIOpen;

	public bool IsUIOpen 
	{
		get; 
		set;
	}

	public void LoadGame(GameObject gameObject)
	{
		gameContextObject = gameObject;
		gameRef = gameContextObject.GetComponent<GameReferences> ();
		GameStartScreenController.Instance.ShowGameStartMenu (gameRef.gameStartScreenRef);

	}
	public void StartGame()
	{
		GameStartScreenController.Instance.HideGameStartMenu ();
		UpdateScore (GameModel.Instance.Score);
		GorillaMovementController.Instance.Init (gameContextObject);
//		ResetHealthBar();
        StartCoroutine(Spawn());
    }
    public void GameOver()
    {
        GorillaMovementController.Instance.GorillaKilled();
        StartCoroutine(Dead());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);
        while(true)
        {
			GameObject kid = gameRef.kids [Random.Range(0, gameRef.kids.Length)];
            float[] x_values = { -5.1900f, 5.1900f };
			float x_value = x_values [Random.Range(0, x_values.Length)];
            Vector3 spawnPosition = new Vector3(
                x_value,
				0.62f,
                0.0f
                );
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(kid, spawnPosition, spawnRotation);
            KidMover kidMover = kid.gameObject.GetComponent<KidMover>();
            kidMover.gameRef = gameRef;
            Debug.Log("Kid coming");
            yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
            
        }
        
    }
    public void UpdateScore(int score)
    {
		gameRef.playerScoreLabel.text = "Score: " + score.ToString();
        Debug.Log("Score: " + score);
    } 
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2.0f);
        GameOverScreenController.Instance.ShowGameOverMenu(gameRef.gameOverScreenRef);
    }

	public void ActivateThrowBackButtons(KidMover kidMover)
	{
		foreach (GameObject throwBackButton in gameRef.throwBackButtons)
		{
			throwBackButton.SetActive (true);
		}
		fetchedKid = kidMover;
	}

	public void DeactivateThrowbackButtons(int addedScore)
	{
		foreach (GameObject throwBackButton in gameRef.throwBackButtons)
		{
			throwBackButton.SetActive (false);
		}
		GameModel.Instance.Score += addedScore;
		UpdateScore(GameModel.Instance.Score);
	}

	public void ThrowBackKid()
	{
		fetchedKid.ThrowbackKid ();
	}

	public void ResetHealthBar()
	{
		gameRef.timeBar.transform.localScale = new Vector3((GameModel.Instance.TimeLimitToFetchChild/GameModel.Instance.TimeLimitToFetchChild), 1.0f, 1.0f);
	}

	public GameObject GetLeftHandControlCollider ()
	{
		return gameRef.leftMovementControl;
	}

	public GameObject GetRightHandControlCollider ()
	{
		return gameRef.rightMovementControl;
	}

    public GameObject GetGorillaCollider()
    {
        return gameRef.gorilla;
    }
}
