using UnityEngine;

public interface IAttackStrategy
{
     
    //target�� ���� ���� ��쵵 ���� �ϵ���
    void Execute(Transform  target);
    void Setting(Transform firePos , Transform  targetPos , float damage);
}