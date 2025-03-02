using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelUI : MonoBehaviour
{
    public event EventHandler OnChangeBirdType;
    public event EventHandler OnBuyBird;
    
    [SerializeField] private Button _yellowBirdButton;
    [SerializeField] private Button _blueBirdButton;
    [SerializeField] private Button _redBirdButton;
    [SerializeField] private TextMeshProUGUI _blueBirdCostText;
    [SerializeField] private TextMeshProUGUI _redBirdCostText;
    [SerializeField] private Image _blueBirdCoinImage;
    [SerializeField] private Image _redBirdCoinImage;
    [SerializeField] private int _blueBirdCost;
    [SerializeField] private int _redBirdCost;
    
    private void Awake()
    {
        _blueBirdCostText.text = _blueBirdCost.ToString();
        _redBirdCostText.text = _redBirdCost.ToString();
        
        _yellowBirdButton.onClick.AddListener(() =>
        {
            PlayerData.BirdType = BirdType.YellowBird;
            OnChangeBirdType?.Invoke(this, EventArgs.Empty);
            Hide();
        });
        
        _blueBirdButton.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt(SaveDataType.IsBlueBirdOpened) == 0)
            {
                int coins = PlayerPrefs.GetInt(SaveDataType.Coins);
                
                if (coins >= _blueBirdCost)
                {
                    PlayerPrefs.SetInt(SaveDataType.Coins, coins - _blueBirdCost);
                    
                    OnBuyBird?.Invoke(this, EventArgs.Empty);
                    
                    _blueBirdCoinImage.gameObject.SetActive(false);
                    _blueBirdCostText.gameObject.SetActive(false);
                
                    PlayerPrefs.SetInt(SaveDataType.IsBlueBirdOpened, 1);
                }
            }

            if (PlayerPrefs.GetInt(SaveDataType.IsBlueBirdOpened) == 1)
            {
                PlayerData.BirdType = BirdType.BlueBird;
                OnChangeBirdType?.Invoke(this, EventArgs.Empty);
                Hide();
            }
        });
        
        _redBirdButton.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt(SaveDataType.IsRedBirdOpened) == 0)
            {
                int coins = PlayerPrefs.GetInt(SaveDataType.Coins);
                
                if (coins >= _redBirdCost)
                {
                    PlayerPrefs.SetInt(SaveDataType.Coins, coins - _redBirdCost);
                    
                    OnBuyBird?.Invoke(this, EventArgs.Empty);
                    
                    _redBirdCoinImage.gameObject.SetActive(false);
                    _redBirdCostText.gameObject.SetActive(false);
                
                    PlayerPrefs.SetInt(SaveDataType.IsRedBirdOpened, 1);
                }
            }

            if (PlayerPrefs.GetInt(SaveDataType.IsRedBirdOpened) == 1)
            {
                PlayerData.BirdType = BirdType.RedBird;
                OnChangeBirdType?.Invoke(this, EventArgs.Empty);
                Hide();
            }
        });

        if (PlayerPrefs.GetInt(SaveDataType.IsBlueBirdOpened) == 1)
        {
            _blueBirdCoinImage.gameObject.SetActive(false);
            _blueBirdCostText.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetInt(SaveDataType.IsRedBirdOpened) == 1)
        {
            _redBirdCoinImage.gameObject.SetActive(false);
            _redBirdCostText.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}