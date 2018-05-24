using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private float dragRadius = 0.6f;
    [SerializeField]
    private float doubleTapTimeout = 0.3f;
    [SerializeField]
    private int particleCount = 20;

    public StateController Controller { get; private set; }
    public Camera Camera => Camera.allCameras[0];
    public float DragRadius => dragRadius;
    public float DoubleTapTimeout => doubleTapTimeout;
    public int ParticleCount => particleCount;
    public float LastTapTime { get; private set; } = 0f;
    public GameObject DragObject { get; private set; } = null;
    public bool DropAllowed { get; private set; } = false;

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

        //TODO: delete
        if (Input.GetKey(KeyCode.Space))
        {
            var instance = ObjectPooler.Instance.SpawnFromPool(Tags.CreatureParticle, new Vector3(2, 2));
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
        }
    }

    private void OnTouchMoved(Vector3 touchPosition)
    {
        if (DragObject != null)
        {
            Controller.SetupAI(false);
            DragObject.transform.position = new Vector3(touchPosition.x, touchPosition.y);
            DropAllowed = true;
        }
    }

    private void OnTouchEnded()
    {
        if (DragObject != null && DropAllowed)
        {
            DragObject.transform.position = BoardManager.Instance.GridDictionary.GetClosestPosition(DragObject.transform.position).Key;
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
        if (DragObject.tag == Tags.Zombie)
        {
            DestroyCreature(Tags.Zombie, "#ff0000");
        }
        else if (DragObject.tag == Tags.Human)
        {
            DestroyCreature(Tags.Human, "#00ff00");
        }
    }

    private void DestroyCreature(string tag, string hexColor)
    {
        ObjectPooler.Instance.Destroy(tag, DragObject);
        for (int i = 0; i < ParticleCount; i++)
        {
            ObjectPooler.Instance.SpawnFromPool(Tags.CreatureParticle, DragObject.transform.position, hexColor);
        }
    }
}


//zomb #f44242
//hum #00ffb9


//zomb #ff0000
//hum #00ff00
