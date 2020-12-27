using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public List<Bullet> Bullets { get; set; }

    public List<Missile> Missiles { get; set; }

    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    public GameObject missilePrefab;

    private void Start()
    {
        Bullets = new List<Bullet>();
        LoadMissiles();
    }

    public Bullet GetBullet()
    {
        Bullet bullet = Bullets.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
        if (bullet == null)
        {
            var prefab = Instantiate(bulletPrefab);
            prefab.SetActive(false);
            Bullet addBullet = prefab.GetComponent<Bullet>();
            addBullet.Velocity = 0.8F;
            Bullets.Add(addBullet);
            return addBullet;
        }
        else
        {
            return bullet;
        }
    }

    public void GetMissile()
    {
        var missile =  Missiles.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
        if (missile != null)
        {
            missile.gameObject.transform.position = new Vector3(-3.56f, -4.07f, 0);
            missile.gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
            missile.gameObject.SetActive(true);
            missile.StartGuide();
            FindObjectOfType<AntiAircraft>().LastMissileSecond = FindObjectOfType<Timer>().TotalElapsedSecond;
        }
    }

    public void LoadMissiles()
    {
        Missiles = new List<Missile>();
        for (int i = 0; i < 5; i++)
        {
            GameObject prefab = Instantiate(missilePrefab);
            prefab.SetActive(false);
            Missile missile = prefab.GetComponent<Missile>();
            missile.Velocity = 1.2f;
            Missiles.Add(missile);
        }       
    }
}
