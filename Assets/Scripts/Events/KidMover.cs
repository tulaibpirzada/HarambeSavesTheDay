using UnityEngine;
using System.Collections;

public class KidMover : MonoBehaviour {

    
    public float damagePerSecond;
    public int fetchEarning;
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

    void Start () {
        kidAnim = GetComponent<Animator>();
		targetPosition = new Vector3(-transform.position.x, 0.47f, 0.0f);
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
		StartCoroutine (FallKid ());
	}
    IEnumerator KidStanding()
    {
        yield return new WaitForSeconds(3.0f);
        isFalling = false;
        isStanding = true;
        kidAnim.SetBool("isFalling", false);
        kidAnim.SetBool("isStanding", true);
    }
	IEnumerator FallKid()
	{
		yield return new WaitForSeconds (Random.Range (5.0f, 10.0f));
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
                GameController.Instance.GameOver();
            }
            //else
            //{
            //    UpdateTimeBar();
            //}

        }
        else if (isGoingUp & isCrying)
        {
            kidTargetPosition = new Vector3(transform.position.x, 1.1f, 0.0f);
            float kidStep = GameModel.Instance.Speed * (Time.deltaTime * 6.0f);
            transform.position = Vector3.MoveTowards(transform.position, kidTargetPosition, kidStep);
            GetComponent<Rigidbody2D>().isKinematic = true;
            //ResetHealthBar();
            Debug.Log("Reset");
        }

    }
    void OnCollisionEnter2D(Collision2D Collision)
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
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Gorilla")
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
        gameRef.timeBar.transform.localScale = new Vector3((GameModel.Instance.TimeLimitToFetchChild/GameModel.Instance.TimeLimitToFetchChild), 1.0f, 1.0f);
    }
}
