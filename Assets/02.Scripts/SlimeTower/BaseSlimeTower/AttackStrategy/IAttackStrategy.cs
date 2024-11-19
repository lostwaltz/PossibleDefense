using UnityEngine;

public interface IAttackStrategy
{
     
    //target이 여러 명인 경우도 대응 하도록
    void Execute(Transform  target);
    void Setting(Transform firePos , Transform  targetPos , float damage);
}