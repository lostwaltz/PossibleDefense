using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour  //only speed about
{
    private float baseSpeed;    
    private float currentSpeed;
    private Coroutine ForceCoroutine;

    public void Initialize(float speed)
    {
        baseSpeed = speed;
        currentSpeed = speed;
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }

    public void SpeedBuff(float percentage, float duration, bool up)
    {
        if(ForceCoroutine != null)
        {
            StopCoroutine(ForceCoroutine);
        }
        ForceCoroutine = StartCoroutine(ChangeSpeed(percentage, duration, up));
    }

    IEnumerator ChangeSpeed(float percentage, float duration, bool SpeedUp)
    {
        float originSpeed = currentSpeed;

        if (SpeedUp)
            currentSpeed = currentSpeed + (currentSpeed * percentage * 0.01f);  //speed up
        else
            currentSpeed = Mathf.Clamp(currentSpeed - (currentSpeed * percentage * 0.01f), 0, currentSpeed);  //speed down

        yield return new WaitForSeconds(duration);

        currentSpeed = originSpeed;
    }
}
