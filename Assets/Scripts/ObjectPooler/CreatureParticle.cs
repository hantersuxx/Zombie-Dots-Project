using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureParticle : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float minSpeed = 5f;
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private int minX = -100;
    [SerializeField]
    private int maxX = 100;
    [SerializeField]
    private int minY = -100;
    [SerializeField]
    private int maxY = 100;
    [SerializeField]
    private float timeToDestroy = 10f;

    public float MinSpeed => minSpeed;
    public float MaxSpeed => maxSpeed;
    public int MinX => minX;
    public int MaxX => maxX;
    public int MinY => minY;
    public int MaxY => maxY;
    public float TimeToDestroy => timeToDestroy;
    public MovementAgent MovementAgent { get; private set; }

    private void Awake()
    {
        MovementAgent = gameObject.GetComponent<MovementAgent>();
    }

    public void OnObjectSpawn(object transfer)
    {
        MovementAgent.Speed = Random.Range(MinSpeed, MaxSpeed);
        MovementAgent.MoveTo(new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)));
        if (!string.IsNullOrEmpty(transfer?.ToString()))
        {
            Colorize(transfer.ToString());
        }
    }

    private void Colorize(string hexColor)
    {
        Color outColor;
        ColorUtility.TryParseHtmlString(hexColor, out outColor);
        gameObject.GetComponent<SpriteRenderer>().color = outColor;
    }

    private void OnEnable()
    {
        Invoke(nameof(Destroy), timeToDestroy);
    }

    private void OnDisable()
    {
        MovementAgent.StopMovement();
        CancelInvoke();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}