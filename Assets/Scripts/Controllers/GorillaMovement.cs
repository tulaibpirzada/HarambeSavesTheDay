using UnityEngine;
using System.Collections;

public class GorillaMovement : MonoBehaviour {
    private bool isWalking;
    private Animator gorillaAnim;

	// Use this for initialization
	void Start () {
        gorillaAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float input_x = Input.GetAxisRaw("Horizontal");

        isWalking = (Mathf.Abs(input_x)) > 0;
        gorillaAnim.SetBool("isWalking",isWalking);
        if (isWalking)
        {
            gorillaAnim.SetFloat("x", input_x);
        }

		transform.position += new Vector3(input_x, 0, 0).normalized*Time.deltaTime/2.0f;
        transform.position = new Vector3(
                                Mathf.Clamp(transform.position.x, -5.1900f, 5.1900f), transform.position.y, transform.position.z);

    }
}
