using UnityEngine;
using System.Collections;

public class GameLauncher : MonoBehaviour 
{
	void Start()
	{
		GameModel.Instance.SetUpGameVariables ();
		GameController.Instance.LoadGame (gameObject);
	}

}
