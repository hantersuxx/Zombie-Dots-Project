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


    public StateController Controller { get; private set; }
    public Camera Camera => Camera.allCameras[0];

    public float LastDragTime { get; private set; } = 0f;

    public float LastTapTime { get; private set; } = 0f;

    public GameObject DragObject { get; private set; } = null;

    public bool DropAllowed { get; private set; } = false;

    private void Update()
    {
        if (LevelStats.Instance.GameIsPaused || LevelStats.Instance.GameIsOver)
        {
            return;
        }

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
        if (hit.tag.Contains(Tags.Human) || hit.tag.Contains(Tags.Zombie))
        {
            Vibrator.Vibrate(Globals.TapDuration);
            HandleTaps(hit.gameObject);
            DragObject = hit.gameObject;
            Controller = DragObject.GetComponent<StateController>();
            LastDragTime = Time.time;
        }
    }

    private void OnTouchMoved(Vector3 touchPosition)
    {
        if (DragObject != null && Time.time - LastDragTime >= DragEnableTimeout && Controller.IsDraggable)
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
        if (Time.time - LastTapTime <= DoubleTapTimeout)
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
        if (DragObject.tag.Contains(Tags.Zombie))
        {
            var zombieController = DragObject.GetComponent<ZombieController>();
            zombieController.TakeDamage(LevelStats.Instance.TapDamage);
            Vibrator.Vibrate(Globals.TapDuration);
            if (zombieController.IsDead)
            {
                LevelManager.Instance.AddScore(Globals.KillZombieScore);
                LevelManager.Instance.KillZombie();
                if (zombieController.tag.Contains(Tags.BossZombie))
                {
                    LevelManager.Instance.AddScore(Globals.KillBossScore);
                    LevelManager.Instance.AchieveGoal();
                }
                Reset();
            }
        }
        else if (DragObject.tag.Contains(Tags.Human))
        {
            var humanController = DragObject.GetComponent<HumanController>();
            humanController.TakeDamage(LevelStats.Instance.TapDamage);
            Vibrator.Vibrate(Globals.TapDuration);
            if (humanController.IsDead)
            {
                LevelManager.Instance.AddScore(Globals.KillHumanScore);
                LevelManager.Instance.KillHuman();
                Reset();
            }
        }
    }

    private void Reset()
    {
        DropAllowed = false;
        LastTapTime = 0f;
        DragObject = null;
    }
}