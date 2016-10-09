using UnityEngine;
using System.Collections;

public class GameOverScreenController : Singleton<GameOverScreenController>
{
    GameOverScreenReferences gameOverScreenRef;
    public void ShowGameOverMenu(GameOverScreenReferences gameOverScreenReference)
    {
        GameController.Instance.IsUIOpen = true;
        gameOverScreenRef = gameOverScreenReference;
        gameOverScreenRef.gameObject.SetActive(true);
		gameOverScreenRef.score.text = GameModel.Instance.Score.ToString ();
		gameOverScreenRef.highestScore.text = GameModel.Instance.Score.ToString ();
    }

    public void HideGameOverMenu()
    {
        GameController.Instance.IsUIOpen = false;
        gameOverScreenRef.gameObject.SetActive(false);
    }
}
