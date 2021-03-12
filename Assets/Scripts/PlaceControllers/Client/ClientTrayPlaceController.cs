using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClientTrayPlaceController : MonoBehaviour
{
    [HideInInspector]
    public ClientController _clientController;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Tray")
        {
            //foreach(string i in other.GetComponent<TrayController>().TrayObjects.objectsName)
            bool tryOrder = _clientController.TryGoodOrder(other.GetComponent<TrayController>().TrayObjects);
            if (tryOrder) Destroy(other.gameObject);
        }
    }
}
