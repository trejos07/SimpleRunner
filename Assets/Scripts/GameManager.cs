using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    int bestScore = 0;
    [SerializeField]
    Cinemachine.CinemachineVirtualCamera lobbyCamera, gameCamera;
    [SerializeField]
    Race race;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        SimpleGameMenu.Instance.Play_button.onClick.AddListener(() => StartGame());
        SimpleGameMenu.Instance.Lobby_button1.onClick.AddListener(() => GoLobby());
        SimpleGameMenu.Instance.Restart_button.onClick.AddListener(() => StartGame());
    }

    public void SetBestScore(int s)
    {
        bestScore= s > bestScore ? s : bestScore;
    }
    public void StartGame()
    {
        lobbyCamera.enabled = false;
        gameCamera.enabled = true;
        race.StartRace();
        
    }
    public void GoLobby()
    {
        lobbyCamera.enabled = true;
        gameCamera.enabled = false;
        SimpleGameMenu.Instance.Lobby_Panel1.SetActive(true);
    }

}
