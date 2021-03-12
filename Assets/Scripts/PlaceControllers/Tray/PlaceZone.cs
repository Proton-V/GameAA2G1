using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceZone : MonoBehaviour
{
    public bool IsFree = true;
    [HideInInspector]
    public PlaceObject PlaceObject;

    public void SetObject(PlaceObject placeObject)
    {
        IsFree = false;
        GameObject obj = Instantiate(placeObject.gameObject);
        obj.transform.SetParent(gameObject.transform);
        obj.transform.position = transform.position;
        PlaceObject = placeObject;
    }
}
