using UnityEngine;
using System.Collections;

public class GameEvents : MonoBehaviour 
{

	public void ThrowbackButtonTapped() 
	{
		GameController.Instance.ThrowBackKid ();
	}
}
