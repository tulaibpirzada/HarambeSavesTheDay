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

    void Start () {
        kidAnim = GetComponent<Animator>();
		targetPosition = new Vector3(-transform.position.x, transform.position.y, 0.0f);

		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameController.Instance.GetLeftHandControlCollider().GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameController.Instance.GetRightHandControlCollider().GetComponent<Collider2D>());
        isFalling = false;
        isCrying = false;
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
        if(GameModel.Instance.TimeLimitToFetchChild >= (Time.time - kidFallingTime))
        {
            GameController.Instance.GameOver();

        }
        else
        {
            if (!isFalling && !isCrying)
            {
                float step = GameModel.Instance.Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            }
            else
            {
                Debug.Log("Velocity: " + GetComponent<Rigidbody2D>().velocity);
            }
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
}
