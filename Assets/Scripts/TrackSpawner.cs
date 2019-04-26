using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackSpawner : MonoBehaviour
{
    bool startSpawn = false;
    private static float trackSizes=4;
    [SerializeField]
    Track[] tracks;
    float time = 0f;
    private static float spawnTime = 2.5f;

    #region Accesores
    public static float SpawnTime
    {
        get
        {
            return spawnTime;
        }

        set
        {
            spawnTime = value;
        }
    }
    public bool StartSpawn
    {
        get
        {
            return startSpawn;
        }

        set
        {
            startSpawn = value;
            if (value)
            {
                Spawn();
            }
            else
                CancelInvoke("Spawn");
        }
    }
    public Track[] Tracks
    {
        get
        {
            return tracks;
        }

        set
        {
            tracks = value;
        }
    }
    #endregion

    void Start()
    {
        trackSizes = 4;
        time = 3f;
        Track.OnVelMovChange += FitSpawnTime;
        Race.Instance.OnStartRace += () => StartSpawn = true;
        Race.Instance.OnEndRace += () => StartSpawn = false;
        Race.Instance.OnDificultiUp += ShuffleTraks;
        ShuffleTraks();
    }
    void Update()
    {
        if (startSpawn)
        {
            time += Time.deltaTime;

            if (time >= spawnTime)
            {
                
                time = 0f;
            }
        }
        Debug.Log("spawneado cada:"+ spawnTime);
    }
    void ShuffleTraks()
    {
        System.Random r = new System.Random();
        Tracks = Tracks.OrderBy(x => r.Next()).ToArray();
    }
    void Spawn()
    {
        int index = Random.Range(0, tracks.Length);
        Track toSpawn = Tracks.Where(x => !x.isMoving).FirstOrDefault();
        if(toSpawn!=null)
            toSpawn.Go(transform.position);

        FitSpawnTime();
        Invoke("Spawn", spawnTime);


        //while (true)
        //{
        //    if (tracks[index].isMoving)
        //    {
        //        index = Random.Range(0, tracks.Length);
        //    }
        //    else
        //    {
        //        tracks[index].Go(transform.position);
        //        break;
        //    }
        //}
    }
    public static void FitSpawnTime()
    {
        SpawnTime =  trackSizes/Track.VelMov;
    }

}
