using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Vault : MonoBehaviour
{
    [SerializeField]
    private Text hp;

    public Text HP => hp;
    public List<Transform> Transforms { get; private set; }

    private IEnumerator Coroutine { get; set; }

    private void Start()
    {
        Transforms = new List<Transform>();
        var bottom = new Vector3(BoardManager.Instance.MaxX / 2, BoardManager.Instance.MinY - 0.5f);
        gameObject.transform.position = bottom;
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].gameObject.tag == Tags.Circle)
            {
                Transforms.Add(transforms[i]);
            }
        }
        MakeRotation();
    }

    private void Update()
    {
        LevelManager.UpdateText(HP, $"{LevelVariables.Instance.CurrentHealth}/{LevelVariables.Instance.BaseHealth} HP");
    }

    private void MakeRotation()
    {
        for (int i = 0; i < Transforms.Count; i++)
        {
            float multiplier = Random.Range(1, 20);
            float angle = (i % 2 == 0) ? Time.deltaTime : Time.deltaTime * -1f;
            StartRotation(i, multiplier, angle);
        }
    }

    private void StartRotation(int i, float multiplier, float angle)
    {
        Coroutine = RotationCoroutine(Transforms[i], multiplier * angle);
        StartCoroutine(Coroutine);
    }

    private void StopRotation()
    {
        StopCoroutine(Coroutine);
    }

    private IEnumerator RotationCoroutine(Transform transform, float angle)
    {
        while (true)
        {
            transform.Rotate(Vector3.forward, angle);
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopRotation();
    }
}
