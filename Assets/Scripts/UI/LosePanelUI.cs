using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LosePanelUI : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
        
        _restartButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });
    }
    
    private void Start()
    {
        Bird.Instance.OnGameOver += Bird_OnGameOver;
        
        Hide();
    }
    
    private void UpdateScoreText(int score)
    {
        _totalScoreText.text = $"Score: {score.ToString()}";
        _bestScoreText.text = $"Best: {PlayerPrefs.GetInt(SaveDataType.BestScore).ToString()}";
    }
    
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
    private void Bird_OnGameOver(object sender, Bird.OnGameOverEventArgs e)
    {
        UpdateScoreText(e.TotalScore);
        Show();
    }
}
