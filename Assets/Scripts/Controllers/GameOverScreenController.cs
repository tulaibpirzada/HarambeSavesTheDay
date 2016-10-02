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
    }

    public void HideGameOverMenu()
    {
        GameController.Instance.IsUIOpen = false;
        gameOverScreenRef.gameObject.SetActive(false);
    }
}
