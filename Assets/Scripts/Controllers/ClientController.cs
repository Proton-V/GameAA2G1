using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;
public class Order
{
    public List<string> objectsName { get; set; } = new List<string>();

}

public class ClientController : MonoBehaviour
{
    [SerializeField]
    private OrderCanvasManager _orderCanvasManager;
    [SerializeField]
    private GameObject _trayPlace;

    [HideInInspector]
    public ClientPlaceZone PlaceZone;
    [HideInInspector]
    public bool IsReached = false;

    private float _timerTime = 0f;
    private float _timerStep = 0.1f;

    private Order _order = new Order();

    private NavMeshAgent _navMesh;
    public void Init(ClientPlaceZone placeZone, float timeTimer, List<string> order)
    {
        PlaceZone = placeZone;
        _timerTime = timeTimer;
        placeZone.SetClient(this);
        SetOrder(order);
        SetTryPlaceController(_trayPlace);
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.SetDestination(placeZone.transform.position);

    }

    public void PlayerInPlace()
    {
        IsReached = true;
        _navMesh.enabled = false;
        StartCoroutine(StartTimer());
        transform.rotation = PlaceZone.transform.rotation;
        transform.position = PlaceZone.transform.position;
    }
    private IEnumerator StartTimer()
    {
        Image fillableImage = _orderCanvasManager.Timer.FillableImage;
        float fillAmountStep = _timerStep / _timerTime;

        for (float i = _timerTime; i > 0; i -= _timerStep)
        {
            yield return new WaitForSeconds(_timerStep);
            fillableImage.fillAmount -= fillAmountStep;
        }
        SadClient();
    }

    private void SadClient()
    {
        _navMesh.enabled = true;
        _navMesh.SetDestination(GameManager.Instance.ClientExitPoint.position);
        PlaceZone.IsFree = true;
        GameController.Score--;
    }
    private void GladClient()
    {
        _navMesh.enabled = true;
        _navMesh.SetDestination(GameManager.Instance.ClientExitPoint.position);
        PlaceZone.IsFree = true;
        GameController.Score++;
    }

    private void SetOrder(List<string> order)
    {
        _order.objectsName = order;
        foreach (string name in _order.objectsName)
        {
            GameObject objUIprefab = Resources.Load<GameObject>($"OrdersUI/{name}");
            Instantiate(objUIprefab, _orderCanvasManager.OrderPanel.transform);
        }
    }
    private void SetTryPlaceController(GameObject obj)
    {
        obj.AddComponent<ClientTrayPlaceController>()._clientController = this;
    }


    public bool TryGoodOrder(Order order)
    {
        if (order.objectsName.Count != _order.objectsName.Count) return false;
        //Check eqals order && _order
        bool result = true;
        foreach (string i in _order.objectsName)
        {
            if (order.objectsName.Contains(i))
                order.objectsName.Remove(i);
            else
            {
                result = false;
                break;
            }
        }

        if (result)
        {
            //Good
            GladClient();
            return true;
        }
        else
            return false;
    }
}
