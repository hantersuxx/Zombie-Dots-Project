using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSave : MonoBehaviour
{
    public VaultHealth VaultHealth => GameObject.FindGameObjectWithTag(Tags.Vault).GetComponent<VaultHealth>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == Tags.Vault)
        {
            GameManager.Instance.AddScore(1);
            ObjectPooler.Instance.Destroy(Tags.Human, gameObject);
        }
    }
}
