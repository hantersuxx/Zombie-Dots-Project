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

    public float MinSpeed => minSpeed;
    public float MaxSpeed => maxSpeed;
    public int MinX => minX;
    public int MaxX => maxX;
    public int MinY => minY;
    public int MaxY => maxY;

    public void OnObjectSpawn(object transfer)
    {
        gameObject.GetComponent<MovementAgent>().Speed = Random.Range(MinSpeed, MaxSpeed);
        gameObject.GetComponent<MovementAgent>().MoveTo(new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)));
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
}

//zomb #f44242
//hum #00ffb9