using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public virtual float Velocity { get; set; }

    [SerializeField]
    public GameObject explosionPrefab;

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "AircraftTag")
        {
            FindObjectOfType<GameVisual>().AddPoint();
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
