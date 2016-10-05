using UnityEngine;
using System.Collections;

public class KidMover : MonoBehaviour {
    
	GameReferences gameRef;
    private Vector3 targetPosition;
    private Vector3 kidTargetPosition;
    private bool isFalling;
    private bool isCrying;
    private Animator kidAnim;
    private float kidFallingTime;
    private float gorillaFetchingKidTime;
    private bool isFetched;
    private bool isGoingUp;

    void Start () {
        kidAnim = GetComponent<Animator>();
		targetPosition = new Vector3(-transform.position.x, transform.position.y, 0.0f);

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
		StartCoroutine (FallKid ());
	}

	IEnumerator FallKid()
	{
		yield return new WaitForSeconds (Random.Range (1.0f, 5.0f));
		isFalling = true;
        isCrying = false;
        kidAnim.SetBool("isFalling", true);
		GetComponent<Rigidbody2D>().isKinematic = false;
	}

	void Update () 
	{
        if (!isFalling && !isCrying)
        {
            float step = GameModel.Instance.Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
		else if(isCrying && !isFetched && GameModel.Instance.TimeLimitToFetchChild < (Time.time - kidFallingTime))
		{
			GameController.Instance.GameOver();

		}
        else if (isGoingUp & isCrying)
        {
            kidTargetPosition = new Vector3(transform.position.x, 1.1f, 0.0f);
            float kidStep = GameModel.Instance.Speed * (Time.deltaTime * 6.0f);
            transform.position = Vector3.MoveTowards(transform.position, kidTargetPosition, kidStep);
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

    }
    void OnCollisionEnter2D(Collision2D Collision)
    {
        isFalling = false;
        isCrying = true;
        kidAnim.SetBool("isFalling", false);
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
}
