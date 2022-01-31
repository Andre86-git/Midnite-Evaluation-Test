using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameVector2Event touchEvent;
    public GameVector2Event swipeEvent;

    [Space()]
    public bool exitOnBackButton = true;

    private Touch touch;
    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                OnTouchStart(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                OnTouchEnd(touch.position);
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            OnTouchStart(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnTouchEnd(Input.mousePosition);
        }
#endif

        if (exitOnBackButton && Input.GetKeyDown(KeyCode.Escape)) 
        { 
            Application.Quit(); 
        }
    }

    private void OnTouchStart(Vector2 touchPosition)
    {
        touchStartPosition = touchPosition;

        if (touchEvent != null)
        {
            touchEvent.Raise(touchPosition);
        }
    }

    private void OnTouchEnd(Vector2 touchPosition)
    {
        touchEndPosition = touchPosition;

        float dx = touchEndPosition.x - touchStartPosition.x;
        float dy = touchEndPosition.y - touchStartPosition.y;
        float absDX = Mathf.Abs(dx);
        float absDY = Mathf.Abs(dy);

        if ((absDX > 0) && (absDY > 0))
        {
            Vector2 swipeDir;
            if (absDX > absDY)
            {
                swipeDir = (dx > 0) ? new Vector3(1, 0) : new Vector3(-1, 0);
            }
            else
            {
                swipeDir = (dy > 0) ? new Vector3(0, 1) : new Vector3(0, -1);
            }

            if (swipeEvent != null)
            {
                swipeEvent.Raise(swipeDir);
            }
        }
    }
}
