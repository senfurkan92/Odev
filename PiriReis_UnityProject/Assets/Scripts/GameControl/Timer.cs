using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float TotalElapsedSecond { get; set; }

    private void FixedUpdate()
    {
        TotalElapsedSecond += Time.fixedDeltaTime;
        FindObjectOfType<GameVisual>().ShowTime(TotalElapsedSecond);
    }
}
