using UnityEngine;
using System.Collections;

public class GorillaMovement : MonoBehaviour {

    private Animator gorillaAnim;

	// Use this for initialization
	void Start () {
        gorillaAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float input_x = Input.GetAxisRaw("Horizontal");

        bool isWalking = (Mathf.Abs(input_x)) > 0;
        gorillaAnim.SetBool("isWalking",isWalking);
        if (isWalking)
        {
            gorillaAnim.SetFloat("x", input_x);
        }

		transform.position += new Vector3(input_x, 0, 0).normalized*Time.deltaTime/2.0f;

    }
}
