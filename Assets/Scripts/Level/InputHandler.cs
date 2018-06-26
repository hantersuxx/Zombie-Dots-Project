using System;
using System.Collections;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private float dragRadius = 0.6f;
    public float DragRadius { get => dragRadius; private set => dragRadius = value; }

    [SerializeField]
    private float doubleTapTimeout = 0.3f;
    public float DoubleTapTimeout { get => doubleTapTimeout; private set => doubleTapTimeout = value; }

    [SerializeField]
    private float dragEnableTimeout = 0.1f;
    public float DragEnableTimeout { get => dragEnableTimeout; private set => dragEnableTimeout = value; }

    //[SerializeField]
    //private float dropEnableTimeout = 0.1f;
    //public float DropEnableTimeout { get => dropEnableTimeout; private set => dropEnableTimeout = value; }


    public StateController Controller { get; private set; }
    public Camera Camera => Camera.allCameras[0];

    //public float LastDragTime { get; private set; } = 0f;

    public float LastTouchTime { get; private set; } = 0f;

    public GameObject DragObject { get; private set; } = null;

    public bool DropAllowed { get; private set; } = false;

    private void Update()
    {
        if (LevelVariables.Instance.GameIsPaused || LevelVariables.Instance.GameIsOver)
        {
            return;
        }

        // Handle native touch events
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            HandleTouch(touch.fingerId, Camera.ScreenToWorldPoint(touch.position), touch.phase);
        }
        //foreach (Touch touch in Input.touches)
        //{
        //    HandleTouch(touch.fingerId, Camera.ScreenToWorldPoint(touch.position), touch.phase);
        //}

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(10, Camera.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(10, Camera.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(10, Camera.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }
    }

    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
    {
        switch (touchPhase)
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

    private void OnTouchBegan(Vector3 touchPosition)
    {
        var hit = Physics2D.OverlapCircle(touchPosition, DragRadius);
        if (hit.tag.Contains(Tags.Human) || hit.tag.Contains(Tags.Zombie))
        {
            Vibrator.Vibrate(Globals.TapDuration);
            HandleTaps(hit.gameObject);
            DragObject = hit.gameObject;
            Controller = DragObject.GetComponent<StateController>();
        }
    }

    private void OnTouchMoved(Vector3 touchPosition)
    {
        if (Time.time - LastTouchTime >= DragEnableTimeout && DragObject != null && Controller.Stats.IsDraggable)
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
        if (Time.time - LastTouchTime <= DoubleTapTimeout)
        {
            if (DragObject == currentGameObject)
            {
                OnDoubleTap();
            }
        }
        LastTouchTime = Time.time;
    }

    protected virtual void OnSingleTap()
    {

    }

    protected virtual void OnDoubleTap()
    {
        if (DragObject.tag.Contains(Tags.Zombie))
        {
            HandleDoubleTap<ZombieController>(HandleZombieDeath);
        }
        else if (DragObject.tag.Contains(Tags.Human))
        {
            HandleDoubleTap<HumanController>(HandleHumanDeath);
        }
    }

    private void HandleDoubleTap<T>(System.Action action) where T : StateController
    {
        var controller = DragObject.GetComponent<T>();
        controller.TakeDamage(LevelVariables.Instance.TapDamage);
        Vibrator.Vibrate(Globals.TapDuration);
        if (controller.IsDead)
        {
            action.Invoke();
            Reset();
        }
    }

    private void HandleZombieDeath()
    {
        LevelManager.Instance.AddScore(Globals.KillZombieScore);
        LevelManager.Instance.KillZombie();
        if (DragObject.tag == Tags.BossZombie)
        {
            LevelManager.Instance.AddScore(Globals.KillBossScore);
            LevelManager.Instance.AchieveGoal();
        }
    }

    private void HandleHumanDeath()
    {
        LevelManager.Instance.AddScore(Globals.KillHumanScore);
        LevelManager.Instance.KillHuman();
    }

    private void Reset()
    {
        DropAllowed = false;
        LastTouchTime = 0f;
        DragObject = null;
        Controller = null;
    }
}