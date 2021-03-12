using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public static List<string> RandomOrder
    {
        get
        {
            List<string> newList = new List<string>();
            for(int i = 0; i < Random.Range(1,3); i++)
            {
                newList.Add(RandomObjectName.ToString());
            }
            return newList;
        }
    }

    public static ObjectNames RandomObjectName
    {
        get
        {
            ObjectNames obj = ObjectNames.Cube;
            switch (Random.Range(0, 3))
            {
                case 0:
                    obj = ObjectNames.Cube;
                    break;
                case 1:
                    obj = ObjectNames.Sphere;
                    break;
                case 2:
                    obj = ObjectNames.Cube2;
                    break;
            }
            return obj;
        }
    }

    public GameObject TrayHolder;
    public GameObject TrayPrefab;
    public GameObject ClientPrefab;
    public List<Transform> ClientSpawnPoints;
    public Transform ClientExitPoint;
    public CanvasUI_Manager CanvasManager;
    [Range(0, 300)]
    public int TimeSecToPlay;
    [Range(2, 10)]
    public int TimeSpawnClient = 5;
    [Range(0f,20f)]
    public float MinTimeTimer;
    [Range(0f, 20f)]
    public float MaxTimeTimer;
    
    [Range(10f, 100f)]
    public float TrayForceSpeed = 50f;
    [Range(1f, 2f)]
    public float TransformTrayPosX = 1.0f;


    private void OnValidate()
    {
        if (MaxTimeTimer < MinTimeTimer) MaxTimeTimer = MinTimeTimer;
    }

    private void Awake()
    {
        Instance = this;
    }

    public enum ObjectNames
    {
        Sphere,
        Cube,
        Cube2
    }
}
