using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleGameMenu : MonoBehaviour
{
    public static SimpleGameMenu Instance;

    [SerializeField] Button play_button;
    [SerializeField] Button restart_button;
    [SerializeField] Button Lobby_button;
    [SerializeField] TextMeshProUGUI score_text;
    [SerializeField] GameObject Lobby_Panel;
    [SerializeField] GameObject game_Panel;
    [SerializeField] GameObject endGame_Panel;

    #region Accesores
    public Button Play_button { get => play_button; set => play_button = value; }
    public Button Restart_button { get => restart_button; set => restart_button = value; }
    public Button Lobby_button1 { get => Lobby_button; set => Lobby_button = value; }
    public TextMeshProUGUI Score_text { get => score_text; set => score_text = value; }
    public GameObject Lobby_Panel1 { get => Lobby_Panel; set => Lobby_Panel = value; }
    public GameObject Game_Panel { get => game_Panel; set => game_Panel = value; }
    public GameObject EndGame_Panel { get => endGame_Panel; set => endGame_Panel = value; } 
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        game_Panel.SetActive(false);
        Lobby_Panel.SetActive(true);
        endGame_Panel.SetActive(false);
        play_button.onClick.AddListener(() =>{
            Lobby_Panel.SetActive(false);
            game_Panel.SetActive(true);
            endGame_Panel.SetActive(false);
            DoButtonSound();
        });
        restart_button.onClick.AddListener(() => {
            endGame_Panel.SetActive(false);
            game_Panel.SetActive(false);
            DoButtonSound();

        });
        Lobby_button.onClick.AddListener(() => {
            Lobby_Panel.SetActive(true);
            endGame_Panel.SetActive(false);
            game_Panel.SetActive(false);
            DoButtonSound();
        });

    }

    void DoButtonSound()
    {
        SoundManager.instance.Play("Button");
    }

    private void Start()
    {
        DeathLimit.OnDeathTrigger += () => { endGame_Panel.SetActive(true); game_Panel.SetActive(true); };
        Race.Instance.OnScoreChange += () => SetScore(Race.Instance.Score);
        Race.Instance.OnStartRace += () => game_Panel.SetActive(true); 
    }

    public void SetScore(int s)
    {
        score_text.text = s.ToString();
    }

}
