using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public BoardManager boardManager;

    protected IEnumerable<Vector3> Positions => boardManager.Tiles.Select(t => t.Position);
    protected Camera Camera => Camera.allCameras[0];

    protected Vector3 ScreenPoint { get; set; }
    protected Vector3 Offset { get; set; }
    protected float DoubleClickTimeout => 0.25f;
    protected bool ClickEnable { get; set; } = true;
    protected bool DoubleClick { get; set; } = false;

    void OnMouseDown()
    {
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

    }
}
