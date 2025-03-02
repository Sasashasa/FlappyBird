using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Image _shopButtonBirdImage;
    [SerializeField] private Sprite _yellowBirdSprite;
    [SerializeField] private Sprite _blueBirdSprite;
    [SerializeField] private Sprite _redBirdSprite;
    [SerializeField] private ChooseModePanelUI _chooseModePanelUI;
    [SerializeField] private ShopPanelUI _shopPanelUI;
    [SerializeField] private TextMeshProUGUI _coinsText;
    
    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            _chooseModePanelUI.Show();
        });

        _shopButton.onClick.AddListener(() =>
        {
            _shopPanelUI.Show();
        });
        
        SetPlayerPrefsKeys();
        SetShopButtonBirdImage();
        
        _coinsText.text = PlayerPrefs.GetInt(SaveDataType.Coins).ToString();
        
        _shopPanelUI.OnChangeBirdType += ShopPanelUI_OnChangeBirdType;
        _shopPanelUI.OnBuyBird += ShopPanelUI_OnBuyBird;
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
    }
    
    private void SetPlayerPrefsKeys()
    {
        if (!PlayerPrefs.HasKey(SaveDataType.IsBlueBirdOpened))
            PlayerPrefs.SetInt(SaveDataType.IsBlueBirdOpened, 0);

        if (!PlayerPrefs.HasKey(SaveDataType.IsRedBirdOpened))
            PlayerPrefs.SetInt(SaveDataType.IsRedBirdOpened, 0);
        
        if (!PlayerPrefs.HasKey(SaveDataType.Coins))
            PlayerPrefs.SetInt(SaveDataType.Coins, 0);
        
        if (!PlayerPrefs.HasKey(SaveDataType.BestScore))
            PlayerPrefs.SetInt(SaveDataType.BestScore, 0);
    }

    private void SetShopButtonBirdImage()
    {
        switch (PlayerData.BirdType)
        {
            case BirdType.YellowBird:
                _shopButtonBirdImage.sprite = _yellowBirdSprite;
                break;
            case BirdType.BlueBird:
                _shopButtonBirdImage.sprite = _blueBirdSprite;
                break;
            case BirdType.RedBird:
                _shopButtonBirdImage.sprite = _redBirdSprite;
                break;
        }
    }
    
    private void ShopPanelUI_OnChangeBirdType(object sender, EventArgs e)
    {
        SetShopButtonBirdImage();
    }
    
    private void ShopPanelUI_OnBuyBird(object sender, EventArgs e)
    {
        _coinsText.text = PlayerPrefs.GetInt(SaveDataType.Coins).ToString();
    }
}