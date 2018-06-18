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
    private int minX = -25;
    [SerializeField]
    private int maxX = 25;
    [SerializeField]
    private int minY = -25;
    [SerializeField]
    private int maxY = 25;
    [SerializeField]
    private float timeToDestroy = 10f;

    public float MinSpeed => minSpeed;
    public float MaxSpeed => maxSpeed;
    public int MinX => minX;
    public int MaxX => maxX;
    public int MinY => minY;
    public int MaxY => maxY;
    public float TimeToDestroy => timeToDestroy;
    public MovementAgent MovementAgent => GetComponent<MovementAgent>();

    private void Colorize(string hexColor)
    {
        Color outColor;
        ColorUtility.TryParseHtmlString(hexColor, out outColor);
        gameObject.GetComponent<SpriteRenderer>().color = outColor;
    }

    private IEnumerator ExecuteAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Code to execute after the delay
        ObjectPooler.Instance.Destroy(tag, gameObject);
    }

    public void HandleObjectSpawn(object value)
    {
        MovementAgent.Speed = Random.Range(MinSpeed, MaxSpeed);
        MovementAgent.MoveTo(new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)));
        string val = value?.ToString();
        if (!string.IsNullOrEmpty(val))
        {
            Colorize(val);
        }
        StartCoroutine(ExecuteAfterTime(TimeToDestroy));
    }

    public void HandleObjectDestroy()
    {
        MovementAgent.StopMovement();
        gameObject.SetActive(false);
    }
}