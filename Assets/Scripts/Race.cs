using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour
{
    public static Race Instance;
    private bool endRace = false;
    private int score;

    [SerializeField] TrackSpawner spawner;
    [SerializeField] Track StartPrefab;
    [SerializeField] Vector3 startPosition;

    public delegate void Action();
    public event Action OnStartRace;
    public event Action OnDificultiUp;
    public event Action OnEndRace;
    public event Action OnScoreChange;

    [SerializeField]Player player;

    #region Accesores
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            if (OnScoreChange!=null)
            {
                OnScoreChange();
            }
        }
    }
    public bool EndRace1
    {
        get
        {
            return endRace;
        }

        set
        {
            endRace = value;
        }
    }
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        DeathLimit.OnDeathTrigger += EndRace;
    }
    private void Start()
    {
        SimpleGameMenu.Instance.Restart_button.onClick.AddListener(() => { Restart(); StartRace(); });


    }

    public void StartRace()
    {
        if (OnStartRace!=null)
        {
            OnStartRace();
        }
        StartPrefab.Go(startPosition);
        Score = 0;
        StartCoroutine(AumentarDificultad());
        StartCoroutine(UpScorePerSecond());

    }
    public void QuitRace()
    {
        EndRace();
        GameManager.Instance.GoLobby();
    }
    public void EndRace()
    {
        StartCoroutine(EndRaceCorrutine());
    }
    IEnumerator UpScorePerSecond()
    {
        while (!endRace)
        {
            Score++;
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator AumentarDificultad()
    {
        while (!endRace)
        {
            yield return new WaitForSeconds(15);
            Score += 100;
            if(OnDificultiUp!=null)
            {
                OnDificultiUp();
            }
        }
    }
    IEnumerator EndRaceCorrutine()
    {
        if (OnEndRace != null)
        {
            OnEndRace();
        }
        endRace = true;
        GameManager.Instance.SetBestScore(Score);
        spawner.StartSpawn = false;
        yield return new WaitForSeconds(5);
        GameManager.Instance.GoLobby();
        Restart();
    }
    private void Restart()
    {
        StopAllCoroutines();
        Score = 0;
        
        spawner.StartSpawn = false;
        for (int i = 0; i < spawner.Tracks.Length; i++)
        {
            spawner.Tracks[i].transform.position = new Vector3(500, 0, 0);
        }
        player.transform.position = Vector3.up*0.5f;
        endRace = false;
        SoundManager.instance.SetPitch("Clock", 1);

    }


}
