using UnityEngine;
using System.Collections;

public class KidMover : MonoBehaviour {
    private GameReferences gameRef;
    private Vector3 targetPosition;
    private bool isFalling;
    private Animator kidAnim;

	void Start () {
        kidAnim = GetComponent<Animator>();
        targetPosition = new Vector3(-transform.position.x, 0.0f, 0.0f);
        isFalling = false;
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
		yield return new WaitForSeconds (Random.Range (1.0f, 7.0f));
		isFalling = true;
        kidAnim.SetBool("isFalling", true);
		GetComponent<Rigidbody2D>().isKinematic = false;
			
	}
	
	void Update () 
	{
        if(!isFalling)
        {
            float step = GameModel.Instance.Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }

    }
}
