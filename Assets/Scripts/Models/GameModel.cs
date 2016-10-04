using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel: Singleton <GameModel>
{
    private float timeLimitToFetchChild;
    private float speed;
    public float TimeLimitToFetchChild
    {
        get { return timeLimitToFetchChild; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public void SetUpGameVariables()
	{
        speed = 1.0f;
        timeLimitToFetchChild = 3.0f;
    }
}
