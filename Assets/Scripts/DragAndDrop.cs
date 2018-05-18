using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public StateController Controller { get; private set; }
    public Camera Camera => Camera.allCameras[0];
    public Vector3 ScreenPoint { get; private set; }
    public Vector3 Offset { get; private set; }
    public float DoubleClickTimeout => 0.25f;
    public bool ClickEnable { get; private set; } = true;
    public bool DoubleClick { get; private set; } = false;
    public bool DragAllowed { get; private set; } = false;
    public GameObject GameObject { get; private set; } = null;

    private void Update()
    {
        if (Input.touchCount > 0)
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
        var hit = Physics2D.OverlapCircle(touchPosition, 1f);
        if (hit.tag == Tags.Human || hit.tag == Tags.Zombie)
        {
            GameObject = hit.gameObject;
            Controller = GameObject.GetComponent<StateController>();
            Controller.SetupAI(false);
            ScreenPoint = Camera.WorldToScreenPoint(GameObject.transform.position);
            Offset = GameObject.transform.position - Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z));
            DragAllowed = true;
        }
    }

    private void OnTouchMoved(Vector3 touchPosition)
    {
        if (DragAllowed)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z);
            Vector3 curPosition = Camera.ScreenToWorldPoint(curScreenPoint) + Offset;
            GameObject.transform.position = new Vector3(touchPosition.x, touchPosition.y, GameObject.transform.position.z);
        }
    }

    private void OnTouchEnded()
    {
        if (DragAllowed)
        {
            GameObject.transform.position = Extensions.GetClosestPosition(GameObject.transform.position, BoardManager.Instance.GridDictionary).Key;
            Controller.SetupAI(true);
            DragAllowed = false;
        }
        if (ClickEnable)
        {
            ClickEnable = false;
            StartCoroutine(TrapDoubleClicks(DoubleClickTimeout));
        }
    }

    private IEnumerator TrapDoubleClicks(float timer)
    {
        float endTime = Time.time + timer;
        while (Time.time < endTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yield return new WaitForSeconds(0.25f);
                ClickEnable = true;
                DoubleClick = true;
            }
            yield return 0;
        }

        if (DoubleClick)
        {
            OnDoubleClick();
            DoubleClick = false;
        }
        else
        {
            OnSingleClick();
        }

        ClickEnable = true;
        GameObject = null;
        yield return 0;
    }

    protected virtual void OnSingleClick()
    {

    }

    protected virtual void OnDoubleClick()
    {
        StateController.DestroyInstance(GameObject);
    }
}
