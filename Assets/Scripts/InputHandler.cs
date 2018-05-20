using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private float doubleTapTimeout = 0.25f;
    [SerializeField]
    private float dragRadius = 0.75f;

    public StateController Controller { get; private set; }
    public Camera Camera => Camera.allCameras[0];
    //public Vector3 ScreenPoint { get; private set; }
    public Vector3 Offset { get; private set; }
    public float DragRadius => dragRadius;
    public bool DropAllowed { get; private set; } = false;
    public float DoubleTapTimeout => doubleTapTimeout;
    public float LastTapTime { get; private set; } = 0f;
    public GameObject DragObject { get; private set; } = null;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan(touchPosition);
                    break;
                case TouchPhase.Moved:
                    OnTouchMoved(touchPosition);
                    break;
                case TouchPhase.Ended:
                    OnTouchEnded();
                    break;
            }
        }
    }

    private void OnTouchBegan(Vector3 touchPosition)
    {
        var hit = Physics2D.OverlapCircle(touchPosition, DragRadius);
        if (hit.tag == Tags.Human || hit.tag == Tags.Zombie)
        {
            HandleTaps(hit.gameObject);
            DragObject = hit.gameObject;
            Controller = DragObject.GetComponent<StateController>();
            //ScreenPoint = Camera.WorldToScreenPoint(DragObject.transform.position);
            Offset = DragObject.transform.position - Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        }
    }

    private void OnTouchMoved(Vector3 touchPosition)
    {
        if (DragObject != null)
        {
            Controller.SetupAI(false);
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 curPosition = Camera.ScreenToWorldPoint(curScreenPoint) + Offset;
            DragObject.transform.position = new Vector3(touchPosition.x, touchPosition.y, DragObject.transform.position.z);
            DropAllowed = true;
        }
    }

    private void OnTouchEnded()
    {
        if (DragObject != null && DropAllowed)
        {
            DragObject.transform.position = Extensions.GetClosestPosition(DragObject.transform.position, BoardManager.Instance.GridDictionary).Key;
            Controller.SetupAI(true);
            DropAllowed = false;
            DragObject = null;
        }
    }

    private void HandleTaps(GameObject currentGameObject)
    {
        if (Time.time - LastTapTime < DoubleTapTimeout)
        {
            if (DragObject == currentGameObject)
            {
                OnDoubleTap();
            }
        }
        LastTapTime = Time.time;
    }

    protected virtual void OnSingleTap()
    {

    }

    protected virtual void OnDoubleTap()
    {
        StateController.DestroyInstance(DragObject);
    }
}
