using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel: Singleton <GameModel>
{
    private float speed;
    public float Speed
    {
        get { return speed; }
    }
    public void SetUpGameVariables()
	{
        speed = 1.0f;
    }
}
