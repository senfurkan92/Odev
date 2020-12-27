using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
    public float Velocity { get; set; }
    public Vector3 StartPos { get; set; }
    public Vector3 EndPos { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > EndPos.x)
        {
            FindObjectOfType<AntiAircraft>().LossHealth();
            FindObjectOfType<GameVisual>().UpdateHealthBar();
            gameObject.SetActive(false);
        }
    }
}
