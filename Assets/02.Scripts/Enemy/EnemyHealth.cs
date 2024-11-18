using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    public Health Health { get; private set; } = new();
    public Shield Shield { get; private set; } = new();
    public float Evasion { get; private set; }
    private EnemySO enemyData;

    [SerializeField] private Canvas HPCanvas;
    [SerializeField] private Image HPBar;
    [SerializeField] private Image backHP;
    [SerializeField] private Image shieldImage;

    private float decreaseSpeed = 2;
    private bool isDamaging = false;
    private Camera cam;

    public event Action OnDamage;
    public event Action OnDead;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (isDamaging)
        {
            HPDecrease();
        }
    }

    public void SetUp(EnemySO enemy)
    {
        enemyData = enemy;
        Health.Initialize(enemy.baseMaxHP * enemy.maxHPModifier);
        Shield.Initialize(enemy.shield);
        Evasion = enemy.evasion;

        UpdateUI();
        HPCanvas.transform.LookAt(cam.transform);
    }

    public void TakeDamage(float damage)
    {
        float evasionPercentage = Random.Range(0f, 100f);
        if (evasionPercentage < Evasion) return;

        float remainingDamage = Shield.AbsorbDamage(damage);

        if (remainingDamage > 0)
        {
            Health.TakeDamage(remainingDamage);
            HPBar.fillAmount = Health.CurrentHealth / Health.MaxHealth;
            isDamaging = true;

            if (Health.IsDead)
                OnDead?.Invoke();
            else
                OnDamage?.Invoke();
        }
        UpdateUI();
    }

    private void HPDecrease()
    {
        backHP.fillAmount = Mathf.Lerp(backHP.fillAmount, HPBar.fillAmount, Time.deltaTime * decreaseSpeed);
        if (Mathf.Approximately(HPBar.fillAmount, backHP.fillAmount))
        {
            isDamaging = false;
        }
    }

    public void Heal(float amount)
    {
        Health.Heal(amount);
        UpdateUI();
    }

    private void UpdateUI()
    {
        HPBar.fillAmount = Health.CurrentHealth / Health.MaxHealth;
        shieldImage.fillAmount = Shield.CurrentShield / Shield.MaxShield;
    }
}

