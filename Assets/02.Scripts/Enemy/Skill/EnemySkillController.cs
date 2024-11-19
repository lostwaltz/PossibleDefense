using System.Collections;
using UnityEngine;

public class EnemySkillController : MonoBehaviour
{
    [field: SerializeField] public BaseSkillSO[] Skills {  get; private set; }
    private WaitForSeconds cooldown;
    private Enemy enemy;
    private Coroutine SkillCoroutine;
    bool isAlive = true;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        isAlive = true;
        if(SkillCoroutine != null)
        {
            StopCoroutine(SkillCoroutine);
        }
        SkillCoroutine = StartCoroutine(UseSkills());
    }

    private void OnDisable()
    {
        if(SkillCoroutine != null)
        {
            StopCoroutine(SkillCoroutine);
            SkillCoroutine = null;
        }
    }

    IEnumerator UseSkills()
    {
        int index = 0;
        while (isAlive)
        {
            if (Skills.Length > 0)
            {
                Skills[index].Execute(enemy);
                cooldown = new WaitForSeconds(Skills[index].cooldown);

                yield return cooldown;

                index = (index + 1) % Skills.Length;
            }
            else
                yield break;    //coroutine finish
        }
    }

    public void OnDead()
    {
        isAlive = false;
        if (SkillCoroutine != null)
        {
            StopCoroutine(SkillCoroutine);
            SkillCoroutine = null;
        }
    }
}
