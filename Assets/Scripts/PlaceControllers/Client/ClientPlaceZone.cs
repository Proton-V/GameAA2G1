using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClientPlaceZone : MonoBehaviour
{
    public bool IsFree = true;
    public ClientController Client;

    public void SetClient(ClientController client)
    {
        IsFree = false;
        Client = client;
        client.transform.SetParent(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Client")
        {
            ClientController client = other.GetComponent<ClientController>();
            if (client.PlaceZone == this && !client.IsReached)
            {
                client.GetComponent<Rigidbody>().isKinematic = true;
                client.PlayerInPlace();
            }
        }
    }

}
