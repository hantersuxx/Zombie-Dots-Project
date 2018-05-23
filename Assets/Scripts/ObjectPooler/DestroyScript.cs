using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 10f;
    public float TimeToDestroy => timeToDestroy;

    private void OnEnable()
    {
        Invoke(nameof(Destroy), timeToDestroy);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
