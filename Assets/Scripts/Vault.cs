using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vault : MonoBehaviour
{
    [SerializeField]
    [Range(10, 100)]
    private int baseHealth = 10;
    [SerializeField]
    private Text healthPoints;
    [SerializeField]
    private Image damageImage;
    [SerializeField]
    private float flashSpeed = 120f;
    [SerializeField]
    private Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    private int currentHealth;

    public int BaseHealth => baseHealth;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        private set
        {
            currentHealth = (value <= 0) ? 0 : value;
        }
    }
    public Text HealthPoints => healthPoints;
    public Image DamageImage => damageImage;
    public float FlashSpeed => flashSpeed;
    public Color FlashColor => flashColor;
    public bool IsDead { get; private set; }
    public bool Damaged { get; private set; }

    private void Awake()
    {
        CurrentHealth = BaseHealth;
    }

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

    private void Update()
    {
        if (Damaged)
        {
            DamageImage.color = FlashColor;
        }
        else
        {
            DamageImage.color = Color.Lerp(DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
        }
        Damaged = false;
        LevelManager.UpdateText(HealthPoints, $"{CurrentHealth} HP");
    }

    private IEnumerator RotationCoroutine(Transform transform, float angle)
    {
        while (true)
        {
            transform.Rotate(Vector3.forward, angle);
            yield return null;
        }
    }

    public void TakeDamage(int amount)
    {
        Damaged = true;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0 && !IsDead)
        {
            IsDead = true;
        }
    }
}
