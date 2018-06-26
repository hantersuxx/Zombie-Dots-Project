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
    //public int TotalCirclesCount { get; private set; }

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
        //TotalCirclesCount = Transforms.Count;
        MakeRotation();
    }

    private void Update()
    {
        LevelManager.UpdateText(HP, $"{LevelVariables.Instance.CurrentHealth}/{LevelVariables.Instance.BaseHealth} HP");
    }

    private void MakeRotation()
    {
        StopAllCoroutines();
        for (int i = 0; i < Transforms.Count; i++)
        {
            float multiplier = Random.Range(1, 20);
            float angle = (i % 2 == 0) ? Time.deltaTime : Time.deltaTime * -1f;
            StartCoroutine(RotationCoroutine(Transforms[i], multiplier * angle));
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

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    var obj = collision.gameObject;
    //    if (obj.tag == Tags.Zombie)
    //    {
    //        OnZombieStay();
    //    }
    //}

    //private void OnZombieStay()
    //{
    //    var collider = GetComponent<CircleCollider2D>();
    //    float diff = LevelManager.Instance.BaseHealth / TotalCirclesCount;
    //    if ((Transforms.Count - 1) * diff > LevelManager.Instance.CurrentHealth)
    //    {
    //        collider.radius -= collider.radius / 2 / Transforms.Count;
    //        var transform = Transforms.First();
    //        Transforms.Remove(transform);
    //        Destroy(transform.gameObject);
    //        MakeRotation();
    //        for (int i = 0; i < 50; i++)
    //        {
    //            ObjectPooler.Instance.SpawnFromPool(Tags.CreatureParticle, transform.position);
    //        }
    //    }
    //}

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
