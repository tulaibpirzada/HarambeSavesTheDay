using UnityEngine;
using System.Collections;

public class KidMover : MonoBehaviour {

    
    public float damagePerSecond;
    public int kidfetchEarning;
	public GameReferences gameRef;
    private Vector3 targetPosition;
    private Vector3 kidTargetPosition;
    private bool isFalling;
    private bool isCrying;
    private Animator kidAnim;
    private float kidFallingTime;
    private float gorillaFetchingKidTime;
    private bool isFetched;
    private bool isGoingUp;
    private bool isStanding;
    private int score;

    void Start () {
        score = 0;
        GameController.Instance.UpdateScore(score);
        kidAnim = GetComponent<Animator>();
		targetPosition = new Vector3(-transform.position.x, 0.62f, 0.0f);
        GameModel.Instance.CurrentTime = GameModel.Instance.TimeLimitToFetchChild;
        //ResetHealthBar();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameController.Instance.GetLeftHandControlCollider().GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameController.Instance.GetRightHandControlCollider().GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameController.Instance.GetGorillaCollider().GetComponent<Collider2D>());
        isFalling = false;
        isCrying = false;
        isFetched = false;
        isGoingUp = false;

        kidAnim.SetBool("isFalling", false);
        if(targetPosition.x > 0)
        {
            kidAnim.SetFloat("x", 1);
        }
        else if(targetPosition.x < 0)
        {
            kidAnim.SetFloat("x", -1);
        }
        StartCoroutine(KidStanding());
	}
    IEnumerator KidStanding()
    {
		yield return new WaitForSeconds(Random.Range (1.0f, 7.0f));
        isFalling = false;
        isStanding = true;
        kidAnim.SetBool("isStanding", true);
    }

	IEnumerator FallKid()
	{
		yield return new WaitForSeconds (Random.Range (2.0f, 5.0f));
		isFalling = true;
        isCrying = false;
        isStanding = false;
        kidAnim.SetBool("isStanding", false);
        kidAnim.SetBool("isFalling", true);
		GetComponent<Rigidbody2D>().isKinematic = false;
	}

	void Update () 
	{
        if (!isFalling && !isCrying && !isStanding)
        {
            float step = GameModel.Instance.Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
        else if (isCrying && !isFetched)
        {
            if (GameModel.Instance.TimeLimitToFetchChild < (Time.time - kidFallingTime))
            {
//                GameController.Instance.GameOver();
            }
            //else
            //{
            //    UpdateTimeBar();
            //}

        }
        else if (isGoingUp & isCrying)
        {
			kidTargetPosition = new Vector3(transform.position.x, 0.62f, 0.0f);
            float kidStep = GameModel.Instance.Speed * (Time.deltaTime * 6.0f);
            transform.position = Vector3.MoveTowards(transform.position, kidTargetPosition, kidStep);
            GetComponent<Rigidbody2D>().isKinematic = true;
            score += kidfetchEarning;
            GameController.Instance.UpdateScore(score);
            //ResetHealthBar();
            Debug.Log("Reset");
        }

    }
	void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.tag == "Ground")
		{
	        isFalling = false;
	        isCrying = true;
	        kidAnim.SetBool("isFalling", false);
	        kidAnim.SetBool("isStanding", false);
	        kidAnim.SetBool("isCrying", true);
	        kidFallingTime = Time.time;
	        Debug.Log("Time" + kidFallingTime);
	        //GetComponent<Rigidbody2D>().isKinematic = true;
		}
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isCrying && collider.tag == "Gorilla")
        {
            isFetched = true;
            isGoingUp = true;
            //GetComponent<Rigidbody2D>().velocity = transform.up * 25.0f;
            //GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
    //void OnTriggerExit2D(Collider2D collider)
    //{
    //    if (collider.tag == "Gorilla")
    //    {
    //        isFetched = false;
    //    }
    //}
    private void UpdateTimeBar()
    {
        gameRef.timeBar.transform.localScale = new Vector3(((GameModel.Instance.TimeLimitToFetchChild-(Time.time-kidFallingTime))/ GameModel.Instance.TimeLimitToFetchChild), 1.0f, 1.0f);
        Debug.Log("Update Time Bar"+ (GameModel.Instance.TimeLimitToFetchChild - (Time.time - kidFallingTime)) / GameModel.Instance.TimeLimitToFetchChild);
    }
    private void ResetHealthBar()
    {
        gameRef.timeBar.transform.localScale = new Vector3((GameModel.Instance.TimeLimitToFetchChild / GameModel.Instance.TimeLimitToFetchChild), 1.0f, 1.0f);
    }

        //gameRef.playerScoreLabel.text = "Score:\n" + score;
}
