using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameReferences : MonoBehaviour 
{
	public GameStartScreenReferences gameStartScreenRef;
    public GameOverScreenReferences gameOverScreenRef;
	public Text playerScoreLabel;
    public Text highScore;
	public GameObject leftMovementControl;
	public GameObject rightMovementControl;
    //public GameObject upMovementControl;
	public GameObject gorilla;
    public GameObject[] kids;
    public SpriteRenderer timeBar;
	public GameObject[] throwBackButtons;
}
