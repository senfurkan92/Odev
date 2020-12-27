using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AircraftManager : MonoBehaviour
{
    [SerializeField] public GameObject aircraftPrefab;

    public List<Aircraft> Aircrafts { get; set; }  
    private Coroutine AircraftCoroutine { get; set; }
    private Vector2 Offset { get; set; }


    private void Start()
    {
        Aircrafts = new List<Aircraft>();
        Offset = new Vector2(2, 5);
        AircraftCoroutine = StartCoroutine(LeadAircraft());
    }

    private IEnumerator LeadAircraft()
    {
        while (true)
        {
            int countOfActiveAircraft = FindObjectsOfType<Aircraft>().Length;
            if (countOfActiveAircraft < 10)
            {
                Aircraft currentAircraft = Aircrafts.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
                if (currentAircraft == null)
                {
                    var prefab = Instantiate(aircraftPrefab);
                    Aircraft addAircraft = prefab.gameObject.GetComponent<Aircraft>();
                    Aircrafts.Add(addAircraft);
                    AssignPropsOfAircraft(addAircraft);
                    MoveAircraft(addAircraft);
                }
                else
                {
                    AssignPropsOfAircraft(currentAircraft);
                    currentAircraft.gameObject.SetActive(true);
                    MoveAircraft(currentAircraft);
                }
            }
            yield return new WaitForSeconds(Random.Range(2.75f, 4f));
        }
    }

    private void AssignPropsOfAircraft(Aircraft aircraft)
    {
        // aircraft start position
        float startX = SceneLimits.MinX - Offset.x;
        float startRandomY = Random.Range(SceneLimits.MaxY - Offset.y, SceneLimits.MaxY);
        aircraft.StartPos = new Vector2(startX, startRandomY);
        // aircraft end position
        float endX = SceneLimits.MaxX + Offset.x;
        float endRandomY = Random.Range(SceneLimits.MaxY - Offset.y, SceneLimits.MaxY);
        aircraft.EndPos = new Vector2(endX, endRandomY);
        // aircraft velocity 
        aircraft.Velocity = Random.Range(0.2f, 1f);
        // deploy aircraft
        aircraft.gameObject.transform.position = aircraft.StartPos;
    }
    private void MoveAircraft(Aircraft aircraft)
    {
        float angle = Mathf.Atan2(aircraft.EndPos.y-aircraft.StartPos.y, aircraft.EndPos.x-aircraft.StartPos.x);
        float velocityX = aircraft.Velocity * Mathf.Cos(angle);
        float velocityY = aircraft.Velocity * Mathf.Sin(angle);
        aircraft.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, velocityY);
    }
}
