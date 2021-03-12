using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrayController : MonoBehaviour
{
    public static TrayController Instance { get; set; }
    public List<PlaceZone> Places;

    [HideInInspector]
    public Order TrayObjects = new Order();

    private Rigidbody _rb;

    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        GameController.Instance.TrayList.Remove(gameObject);
    }

    public void TryPlace(PlaceObject placeObject)
    {
        PlaceZone zone = Places.FirstOrDefault((place) => place.IsFree);
        zone?.SetObject(placeObject);
        //Variant for check contains this product
        //        if(Places.Where((place)=> !place.IsFree && place.PlaceObject.Name == placeObject.Name).Count() == 0)
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Transporter")
            _rb.AddForce(transform.right * GameManager.Instance.TrayForceSpeed);
        if (collision.gameObject.tag == "RubbishBin")
        {
            Destroy(gameObject);
            GameController.Score--;
        }
    }

    public void TransformTrayPosX(float x)
    {
        //Set Trayobject before Force Tray
        TrayObjects.objectsName = Places.Where((place) => !place.IsFree).Select((place) => place.PlaceObject.Name).ToList();
        transform.position += new Vector3(x, 0, 0);
    }
}
