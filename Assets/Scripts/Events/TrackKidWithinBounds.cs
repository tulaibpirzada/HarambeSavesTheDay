using UnityEngine;
using System.Collections;

public class TrackKidWithinBounds : MonoBehaviour {

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Kid")
        {
            GameModel.Instance.KidInside = true;
        }
    }

}
