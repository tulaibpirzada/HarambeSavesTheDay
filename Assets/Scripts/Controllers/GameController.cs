using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : Singleton<GameController> 
{
	GameReferences gameRef;
	GameObject gameContextObject;
    
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
		GorillaMovementController.Instance.Init (gameContextObject);
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
            GameObject kid = gameRef.kids[Random.Range(0, gameRef.kids.Length)];
            float[] x_values = { -5.1900f, 5.1900f };
            float x_value = x_values[Random.Range(0, x_values.Length)];
            Vector3 spawnPosition = new Vector3(
                x_value,
				1.1f,
                0.0f
                );
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(kid, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
        }
        
    }
    
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(4.0f);
        GameOverScreenController.Instance.ShowGameOverMenu(gameRef.gameOverScreenRef);
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
