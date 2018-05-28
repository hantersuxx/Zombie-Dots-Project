using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    private void Start()
    {
        var bottom = new Vector3(BoardManager.Instance.MaxX / 2, BoardManager.Instance.MinY - 0.5f);
        gameObject.transform.position = bottom;
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i] == transform) { continue; }
            float multiplier = Random.Range(1, 20);
            float angle = (i % 2 == 0) ? Time.deltaTime : Time.deltaTime * -1f;
            StartCoroutine(RotationCoroutine(transforms[i], multiplier * angle));
        }
    }

    private IEnumerator RotationCoroutine(Transform transform, float angle)
    {
        while (true)
        {
            transform.Rotate(Vector3.forward, angle);
            yield return null;
        }
    }
}
