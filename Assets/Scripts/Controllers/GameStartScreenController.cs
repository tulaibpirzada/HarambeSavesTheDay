using UnityEngine;
using System.Collections;

public class GameStartScreenController : Singleton<GameStartScreenController>
{
	GameStartScreenReferences gameStartScreenRef;

	public void ShowGameStartMenu(GameStartScreenReferences gameStartScreenReference)
	{
		GameController.Instance.IsUIOpen = true;
		gameStartScreenRef = gameStartScreenReference;
		gameStartScreenRef.gameObject.SetActive (true);
	}

	public void HideGameStartMenu()
	{
		GameController.Instance.IsUIOpen = false;
		gameStartScreenRef.gameObject.SetActive (false);
	}


}
