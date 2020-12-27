using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AntiAircraft : MonoBehaviour
{
    public Magazine Magazine { get; set; }

    public Coroutine FireCoroutine { get; set; }

    public float LastFireSecond { get; set; }

    public float LastMissileSecond { get; set; }

    public float FireShift { get; set; }

    public int Health { get; set; }

    [SerializeField]
    public AudioClip fireClip;
    [SerializeField]
    public TextMeshProUGUI missileText;

    private void Start()
    {
        Magazine = gameObject.transform.GetChild(0).gameObject.GetComponent<Magazine>();
        Magazine.gameObject.transform.position = gameObject.transform.position;
        LastFireSecond = 0;
        LastMissileSecond = 0;
        FireShift = 2f;
        Health = 5;
    }

    private void Update()
    {
        // missile text
        if (FindObjectOfType<Timer>().TotalElapsedSecond - LastMissileSecond > 7)
        {
            missileText.text = "Ready";
        }
        else
        {
            missileText.text = "Wait";
        }

        // fire missile
        if (Input.GetKeyDown(KeyCode.M) && FindObjectOfType<Timer>().TotalElapsedSecond - LastMissileSecond > 7)
        {
            Magazine.GetMissile();
        }
    }

    private void FixedUpdate()
    {
        // rotate 
        float z = gameObject.transform.eulerAngles.z;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if(z>=45)
            gameObject.transform.Rotate(new Vector3(0, 0, -0.1f), 1);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(z<=135)
            gameObject.transform.Rotate(new Vector3(0, 0, 0.1f), 1);

        }

        // fire anti-aircraft gun
        if (Input.GetKey(KeyCode.Space))
        {
            if (FireCoroutine == null && FindObjectOfType<Timer>().TotalElapsedSecond - LastFireSecond >= FireShift  )
            {
                FireCoroutine = StartCoroutine(Fire());
            }
        }
        else
        {
            if (FireCoroutine != null)
            {
                StopCoroutine(FireCoroutine);
                FireCoroutine = null;
            }
        }     
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            float z = gameObject.transform.eulerAngles.z;
            Bullet bullet = Magazine.GetComponent<Magazine>().GetBullet();
            float posX = gameObject.transform.position.x + gameObject.transform.localScale.y * Mathf.Cos(z * Mathf.Deg2Rad);
            float posY = gameObject.transform.position.y + gameObject.transform.localScale.y * Mathf.Sin(z * Mathf.Deg2Rad);
            bullet.gameObject.transform.position = new Vector3(posX, posY, 3);
            bullet.gameObject.transform.eulerAngles = new Vector3(0, 0, z);
            float velocityX = bullet.Velocity * Mathf.Cos(z * Mathf.Deg2Rad);
            float velocityY = bullet.Velocity * Mathf.Sin(z * Mathf.Deg2Rad);
            bullet.gameObject.SetActive(true);
            AudioSource.PlayClipAtPoint(fireClip, bullet.gameObject.transform.position, 100);
            bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocityX, velocityY, 0);
            LastFireSecond = FindObjectOfType<Timer>().TotalElapsedSecond;
            yield return new WaitForSeconds(FireShift);
        }
    }

    public void LossHealth()
    {
        if (Health > 0)
        {
            Health--;
        }
        if(Health <=0)
        {
            FindObjectOfType<GameInitiliazer>().Pause();
            FindObjectOfType<GameVisual>().ShowEndPanel();
        }
    }
}
