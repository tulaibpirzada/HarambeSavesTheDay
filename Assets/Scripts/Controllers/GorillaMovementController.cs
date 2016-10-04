using UnityEngine;
using System.Collections;

public class GorillaMovementController : Singleton<GorillaMovementController>  {

	GameReferences gameRef;
	private bool shouldAllowGorillaMovement;
	private bool leftControlTapped;
	private bool rightControlTapped;
    private bool isKilled;
	private Animator gorillaAnim;

	public void Init(GameObject gameObject)
	{
		gameRef = gameObject.GetComponent<GameReferences> ();
		gorillaAnim = gameRef.gorilla.GetComponent<Animator> ();
		shouldAllowGorillaMovement = true;
        isKilled = false;
	}

	void FixedUpdate()
	{
		if (shouldAllowGorillaMovement)
		{
			if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
					
				Vector3 worldPoint = Vector3.zero;
				#if UNITY_EDITOR
				worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//for touch device
				#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
				worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				#endif
				GameObject leftMovementControl = gameRef.leftMovementControl;
				GameObject rightMovementControl = gameRef.rightMovementControl;
				if (leftMovementControl.GetComponent<Collider2D> ().OverlapPoint (worldPoint)) {
					
					leftControlTapped = true;
					rightControlTapped = false;	
					Debug.Log("Left Collidor tapped");
					
				} else if (rightMovementControl.GetComponent<Collider2D> ().OverlapPoint (worldPoint)) {
					
					leftControlTapped = false;
					rightControlTapped = true;	
					Debug.Log("Right Collidor tapped");
				}

			}

			if ((leftControlTapped || rightControlTapped) && ((Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Moved || Input.GetTouch (0).phase == TouchPhase.Stationary))  || (Input.GetAxisRaw ("Mouse X") == 0 || Input.GetAxisRaw ("Mouse Y") == 0))) {

				Debug.Log("Touch Moved");
				if (leftControlTapped) {

					gorillaAnim.SetBool("isWalking", true);
					gorillaAnim.SetFloat("x", -1);
					gameRef.gorilla.transform.position += new Vector3(-1, 0, 0).normalized*Time.deltaTime/2.0f;
					Debug.Log("Incremented value: " + gameRef.gorilla.transform.position);
					gameRef.gorilla.transform.position = new Vector3(
						Mathf.Clamp(gameRef.gorilla.transform.position.x, -5.1900f, 5.1900f), gameRef.gorilla.transform.position.y, gameRef.gorilla.transform.position.z);
					
				} else if (rightControlTapped) {

					gorillaAnim.SetBool("isWalking", true);
					gorillaAnim.SetFloat("x", 1);
					gameRef.gorilla.transform.position += new Vector3(1, 0, 0).normalized*Time.deltaTime/2.0f;
					gameRef.gorilla.transform.position = new Vector3(
						Mathf.Clamp(gameRef.gorilla.transform.position.x, -5.1900f, 5.1900f), gameRef.gorilla.transform.position.y, gameRef.gorilla.transform.position.z);
				}
			}

			if ((leftControlTapped || rightControlTapped) && ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp (0)))) {

				Debug.Log("Touch Ended");

				if (leftControlTapped) {

					leftControlTapped = false;
					gorillaAnim.SetBool("isWalking", false);

				} else if (rightControlTapped) {

					rightControlTapped = false;
					gorillaAnim.SetBool("isWalking", false);
				}
			}
		}
	}
    public void GorillaKilled()
    {
        gorillaAnim.SetBool("isWalking", false);
        gorillaAnim.SetBool("isKilled", true);
    }
}
