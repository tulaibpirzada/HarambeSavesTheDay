using UnityEngine;
using System.Collections;

public class KidMover : MonoBehaviour {
    
	GameReferences gameRef;
    private Vector3 targetPosition;
    private bool isFalling;
    private bool isCrying;
    private Animator kidAnim;
    private float kidFallingTime;
    private float gorillaFetchingKidTime;
    private bool isFetched;

    void Start () {
        kidAnim = GetComponent<Animator>();
		targetPosition = new Vector3(-transform.position.x, transform.position.y, 0.0f);

		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameController.Instance.GetLeftHandControlCollider().GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameController.Instance.GetRightHandControlCollider().GetComponent<Collider2D>());
        isFalling = false;
        isCrying = false;
        isFetched = false;
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
		
    }
    void OnCollisionEnter2D(Collision2D Collision)
    {
        isFalling = false;
        isCrying = true;
        kidAnim.SetBool("isFalling", false);
        kidAnim.SetBool("isCrying", true);
        kidFallingTime = Time.time;
        Debug.Log("Time" + kidFallingTime);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Gorilla")
        {
            isFetched = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Gorilla")
        {
            isFetched = false;
        }
    }
}
