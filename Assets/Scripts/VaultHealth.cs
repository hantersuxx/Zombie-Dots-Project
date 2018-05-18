using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaultHealth : MonoBehaviour
{
    [SerializeField]
    private int baseHealth = 10;
    [SerializeField]
    private Text healthPoints;
    [SerializeField]
    private Image damageImage;
    [SerializeField]
    private float flashSpeed = 5f;
    [SerializeField]
    private Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    public int BaseHealth => baseHealth;
    public int CurrentHealth { get; private set; }
    public Text HealthPoints => healthPoints;
    public Image DamageImage => damageImage;
    public float FlashSpeed => flashSpeed;
    public Color FlashColor => flashColor;
    public bool IsDead { get; private set; }
    public bool Damaged { get; private set; }

    private void Awake()
    {
        CurrentHealth = BaseHealth;
        HealthPoints.text = CurrentHealth.ToString();
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
    }

    public void TakeDamage(int amount)
    {
        Damaged = true;
        CurrentHealth -= amount;
        HealthPoints.text = CurrentHealth.ToString();

        if (CurrentHealth <= 0 && !IsDead)
        {
            Death();
        }
    }

    private void Death()
    {
        IsDead = true;
    }
}
