using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public static LevelUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private Image _getReadyImage;

    private static readonly int StartGame = Animator.StringToHash("StartGame");

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Bird.Instance.OnScoreChanged += Bird_OnScoreChanged;
        Bird.Instance.OnGameOver += Bird_OnGameOver;
    }

    public void ShowGetReadyImageAnimation()
    {
        _getReadyImage.gameObject.GetComponent<Animator>().SetTrigger(StartGame);
    }

    private void UpdateCurrentScore(int score)
    {
        _currentScoreText.text = score.ToString();
    }

    private void HideLevelUIElements()
    {
        _currentScoreText.gameObject.SetActive(false);
        _getReadyImage.gameObject.SetActive(false);
    }

    private void Bird_OnScoreChanged(object sender, Bird.OnScoreChangedEventArgs e)
    {
        UpdateCurrentScore(e.Score);
    }

    private void Bird_OnGameOver(object sender, EventArgs e)
    {
        HideLevelUIElements();
    }
}