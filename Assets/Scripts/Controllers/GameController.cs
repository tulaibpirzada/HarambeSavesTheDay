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
		StartGame ();
//		GameStartScreenController.Instance.ShowGameStartMenu (gameRef.gameStartScreenRef);

	}
	public void StartGame()
	{
//		GameStartScreenController.Instance.HideGameStartMenu ();
		GorillaMovementController.Instance.Init (gameContextObject);
	}

}
