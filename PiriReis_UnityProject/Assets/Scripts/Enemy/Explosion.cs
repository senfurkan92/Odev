using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    public AudioClip explosionClip;

    public void Start()
    {
        AudioSource.PlayClipAtPoint(explosionClip, gameObject.transform.position);
    }

    public void DestroyYourself()
    {
        Destroy(gameObject);
    }
}
