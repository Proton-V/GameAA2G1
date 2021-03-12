using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class ClientPlacerController : MonoBehaviour
{
    public static ClientPlacerController Instance { get; set; }
    public List<ClientPlaceZone> Places;

    private void Awake()
    {
        Instance = this;
    }

    public void TryPlace()
    {
        
        ClientPlaceZone zone = Places.FirstOrDefault((place) => place.IsFree);
        if (zone != null)
        {
            GameManager gm = GameManager.Instance;
            Vector3 spawnPos = gm.ClientSpawnPoints[Random.Range(0, gm.ClientSpawnPoints.Count)].position;
            ClientController client = Instantiate(gm.ClientPrefab, spawnPos, transform.rotation).GetComponent<ClientController>();
            List<string> order = GameManager.RandomOrder;
            client.Init(zone, Random.Range(gm.MinTimeTimer, gm.MaxTimeTimer), order);
        }
    }
}
