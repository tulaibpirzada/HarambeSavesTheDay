using UnityEngine;
using System.Collections;

public class KidMover : MonoBehaviour {

    private Vector3 targetPosition;
    private bool isFalling;

	void Start () {
        targetPosition = new Vector3(-transform.position.x, 0.0f, 0.0f);
        isFalling = false;
		StartCoroutine (FallKid ());
	}

	IEnumerator FallKid()
	{
		yield return new WaitForSeconds (Random.Range (1.0f, 7.0f));
		isFalling = true;
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
