using System;
using UnityEngine;
using UnityEngine.UI;
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
    private bool isDead = false;

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

        isDead = false;
        UpdateUI();
        HPCanvas.transform.rotation = Quaternion.LookRotation(-cam.transform.forward, Vector3.up);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        //쉴드데미지 처리 => 회피 => 체력데미지 처리
        float remainingDamage = Shield.AbsorbDamage(damage);

        float evasionPercentage = Random.Range(0f, 100f);
        if (evasionPercentage < Evasion) return;

        if (remainingDamage > 0)
        {
            Health.TakeDamage(remainingDamage);
            HPBar.fillAmount = Health.CurrentHealth / Health.MaxHealth;
            isDamaging = true;

            if (Health.IsDead)
            {
                OnDead?.Invoke();
                isDead = true;
            }
            else
            {
                SoundManager.Instance.PlayClip("Hit", transform.position);
                OnDamage?.Invoke();
            }
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
        HPBar.fillAmount = Health.GetHealthPercentage;
        shieldImage.fillAmount = Shield.GetShieldPercentage;
    }
}

