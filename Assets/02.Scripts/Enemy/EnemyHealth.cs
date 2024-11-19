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
    private Vector3 camDirection;

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
            BackHPDecrease();
        }
    }

    public void SetUp(EnemySO enemy)
    {
        enemyData = enemy;
        Health.Initialize(enemy.baseMaxHP * enemy.maxHPModifier);
        Shield.Initialize(enemy.shield);
        Evasion = enemy.evasion;

        UpdateUI();
        HPLookAtCamera();
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

    private void BackHPDecrease()
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

    public void ShieldRecharge(float amount)
    {
        Shield.Recharge(amount);
        UpdateUI();
    }

    private void UpdateUI()
    {
        HPBar.fillAmount = Health.CurrentHealth / Health.MaxHealth;
        shieldImage.fillAmount = Shield.CurrentShield / Shield.MaxShield;
    }

    private void HPLookAtCamera()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            HPCanvas.transform.LookAt((hit.point - transform.position).normalized);
        }
    }
}

