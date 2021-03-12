using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientExitPointController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Client")
        {
            Destroy(other.gameObject);
        }
    }
}
