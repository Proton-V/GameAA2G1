using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; set; }

    public List<GameObject> TrayList = new List<GameObject>();
    public static int Score
    {
        get
        {
            return Instance._score;
        }
        set
        {
            Instance._score = value;
            Instance.ScoreChange.Invoke();
        }
    }
    private int _score { get; set; }

    public static int Time;
    private float _transformTrayPosX;

    public event Action ScoreChange;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ChangeState(State.Game);

        ScoreChange += ChangeScore;

        Score = 0;
        Time = GameManager.Instance.TimeSecToPlay;
        _transformTrayPosX = GameManager.Instance.TransformTrayPosX;

        StartCoroutine(StartGameTimer());
        SpawnNewTray();
    }

    private void ChangeScore()
    {
        GameManager.Instance.CanvasManager.ScoreText.text = $"Score: {Score}";
    }

    private IEnumerator StartGameTimer()
    {
        int timeSpawnClient = GameManager.Instance.TimeSpawnClient;
        Text timeText = GameManager.Instance.CanvasManager.TimeText;
        for(int s = Time; s >= 0; s--)
        {
            //SpawnNewClient
            if((Time-s) % timeSpawnClient == 0) SpawnNewClient();

            TimeSpan time = TimeSpan.FromSeconds(s);
            yield return new WaitForSeconds(1);
            timeText.text = string.Format("Time: {0:D2}:{1:D2}", time.Minutes, time.Seconds);
        }
        GameOver();
    }

    public void But_OK()
    {
        TrayController.Instance.TransformTrayPosX(_transformTrayPosX);
        SpawnNewTray();
    }
    public void PlaceObject(PlaceObject placeObject)
    {
        TrayController.Instance.TryPlace(placeObject);
    }
    public void SpawnNewTray()
    {
        GameManager gm = GameManager.Instance;
        GameObject tray = Instantiate(gm.TrayPrefab, gm.TrayHolder.transform);
        TrayList.Add(tray);
        tray.transform.position = gm.TrayHolder.transform.position + gm.TrayHolder.transform.up;
    }
    public void SpawnNewClient()
    {
        ClientPlacerController.Instance.TryPlace();
    }

    private void GameOver()
    {
        //Destroy all Clients && Trays
        ClientPlacerController.Instance.Places.FindAll(place => !place.IsFree).ForEach(place => 
        {
            place.IsFree = true;
            Destroy(place.Client.gameObject);
        });
        TrayList.ForEach(tray => Destroy(tray));

        ChangeState(State.GameOver);
        CanvasUI_Manager canvasManager = GameManager.Instance.CanvasManager;
        canvasManager.GameOverPanel.TextScore.text = canvasManager.ScoreText.text;
    }

    public void Restart()
    {
        Start();
    }


    private void ChangeState(State setState)
    {
        CanvasUI_Manager canvasManager = GameManager.Instance.CanvasManager;

        switch (setState)
        {
            case State.Game:
                canvasManager.GamePanel.SetActive(true);
                canvasManager.GameOverPanel.gameObject.SetActive(false);
                break;
            case State.GameOver:
                canvasManager.GamePanel.SetActive(false);
                canvasManager.GameOverPanel.gameObject.SetActive(true);
                break;
        }
    }
    public enum State
    {
        Game,
        GameOver
    }
}
