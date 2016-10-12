using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class KidMovementController : Singleton<KidMovementController> {

    public KidMover kid;
    private bool kidTapped;
    public bool ShouldAllowKidDragging
    {
        get;
        set;
    }
    public bool ThrowUp
    {
        get;
        set;
    }
    public void Init(GameObject gameObject)
    {
        ShouldAllowKidDragging = false;
        ThrowUp = false;
    }
    private void FixedUpdate()
    {
        if (ShouldAllowKidDragging)
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
            {
                bool isUITapped = false;

                Vector3 worldPoint = Vector3.zero;
#if UNITY_EDITOR
                worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isUITapped = EventSystem.current.IsPointerOverGameObject();
                //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
				worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				isUITapped = EventSystem.current.IsPointerOverGameObject(Input.GetTouch (0).fingerId);
#endif
                if (!isUITapped)
                {
                    if (kid.GetComponent<Collider2D>().OverlapPoint(worldPoint))
                    {
                        kidTapped = true;
                        Debug.Log("Kid tapped");
                    }
                }
            }
            if (kidTapped && ((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)) || (Input.GetAxisRaw("Mouse X") == 0 || Input.GetAxisRaw("Mouse Y") == 0)))
            {
            }
            if (kidTapped && ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0))))
            {
                if (GameModel.Instance.KidInside)
                {
                    this.ThrowUp = true;
                    kid.IsFetched = true;
                    kid.IsGoingUp = this.ThrowUp;
                    GameController.Instance.UpdateScore(kid.kidfetchEarning);
                    Debug.Log("Touch Ended");
                    GameModel.Instance.KidInside = false;
                }
            }
        }
    }
}
