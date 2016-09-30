using UnityEngine;
using System.Collections;

public class GameStartScreenEvents : MonoBehaviour 
{
	public void StartGame()
	{
		GameController.Instance.StartGame ();
	}
}
