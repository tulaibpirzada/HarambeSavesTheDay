using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel: Singleton <GameModel>
{
    private float timeLimitToFetchChild;
    private float speed;
    private float currentTime;

    public bool KidInside
    {
        get;
        set;
    }

    public float CurrentTime
    {
        get { return currentTime; }
        set { currentTime = value; }
    }
    public float TimeLimitToFetchChild
    {
        get { return timeLimitToFetchChild; }
    }
    public float Speed
    {
        get { return speed; }
    }

	public int Score
	{
		get;
		set;
	}

	public int HighScore
	{
		get;
		set;
	}


    public void SetUpGameVariables()
	{
        speed = 1.0f;
        timeLimitToFetchChild = 5.0f;
		this.Score = 0;
		this.HighScore = 0;
        this.KidInside = false;
    }
}
