using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DirtSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Vector3> availablePlaces;
    public int dirtToSpawn;
    public GameObject dirt;
    public int minHealth, maxHealth;
    bool dirtSpawned;
    public List<GameObject> dirts;
    public Awake managerScript;

    // Start is called before the first frame update
    void Start()
    {
        dirtSpawned = false;
        tilemap = gameObject.GetComponent<Tilemap>();
        availablePlaces = new List<Vector3>();

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3 location = tilemap.CellToWorld(position);
            if (!tilemap.HasTile(position))
            {
                continue;
            }

            availablePlaces.Add(location);
        }
    }

    private void FixedUpdate()
    {
        if(dirts.Count == 0 && dirtSpawned)
        {
            dirtSpawned = false;
            Debug.Log("Win");
            managerScript.cleaned = true;
        }
    }

    // Update is called once per frame
    public void SpawnDirt()
    {
        List<Vector3> currentAvailablePlaces = new List<Vector3>();

        for (int x = 0; x < dirtToSpawn;)
        {
            Vector3 currentPosition = availablePlaces[Random.Range(0, availablePlaces.Count - 1)];
            if (!(currentAvailablePlaces.Contains(currentPosition)))
            {
                currentAvailablePlaces.Add(currentPosition);
                x++;
            }
        }

        foreach (Vector3 location in currentAvailablePlaces)
        {
            GameObject currentDirt = GameObject.Instantiate(dirt);
            currentDirt.transform.position = location;
            currentDirt.transform.parent = transform;
            currentDirt.GetComponent<ToothDirt>().health = Random.Range(minHealth, maxHealth);
            dirts.Add(currentDirt);
        }
        
        dirtSpawned = true;
    }
}
