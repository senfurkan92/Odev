using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Missile : Ammo
{

    public void StartGuide()
    {
        StartCoroutine(Guide());
    }

    public IEnumerator Guide()
    {
        while (true)
        {
            List<Aircraft> aircrafts = FindObjectsOfType<Aircraft>().ToList();
            if (aircrafts.Count == 0)
            {
                Deactive();
            }
            else
            {
                Vector2 targetPos = DetectTarget(aircrafts);
                float radian = Mathf.Atan2(targetPos.y - gameObject.transform.position.y, targetPos.x - gameObject.transform.position.x);
                var angles = gameObject.transform.eulerAngles;
                gameObject.transform.eulerAngles = new Vector3(angles.x, angles.y, radian * Mathf.Rad2Deg);
                var velocityX = Velocity * Mathf.Cos(radian);
                var velocityY = Velocity * Mathf.Sin(radian);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocityX, velocityY, 0);
            }
            yield return new WaitForSeconds(0.75f);
        }
    }

    public Vector2 DetectTarget(List<Aircraft> aircrafts)
    {
        List<Vector2> positions = aircrafts.Select(x => (Vector2)x.transform.position).ToList();
        Vector2 targetPos = positions[0];
        foreach (Vector2 pos in positions)
        {
            if (Vector2.Distance(gameObject.transform.position, targetPos) > Vector2.Distance(gameObject.transform.position, pos))
            {
                targetPos = pos;
            }
        }
        return targetPos;
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
}
