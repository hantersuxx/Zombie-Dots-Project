using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public StateController Controller => GetComponent<StateController>();

    private Camera Camera => Camera.allCameras[0];

    private Vector3 ScreenPoint { get; set; }
    private Vector3 Offset { get; set; }
    private float DoubleClickTimeout => 0.25f;
    private bool ClickEnable { get; set; } = true;
    private bool DoubleClick { get; set; } = false;

    void OnMouseDown()
    {
        Controller.SetupAI(false);

        ScreenPoint = Camera.WorldToScreenPoint(gameObject.transform.position);
        Offset = gameObject.transform.position - Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z);
        Vector3 curPosition = Camera.ScreenToWorldPoint(curScreenPoint) + Offset;
        transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
    }

    void OnMouseUp()
    {
        if (ClickEnable)
        {
            transform.position = Extensions.GetClosestTile(transform.position, BoardManager.Instance.Tiles).Key;
            Controller.SetupAI(true);

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
        yield return 0;
    }

    protected virtual void OnSingleClick()
    {

    }

    protected virtual void OnDoubleClick()
    {
        Destroy(gameObject);
    }
}
