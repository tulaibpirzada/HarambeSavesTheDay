using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverScreenEvents : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
        //GameController.Instance.StartGame();
        Debug.Log("Restart");
    }
}
