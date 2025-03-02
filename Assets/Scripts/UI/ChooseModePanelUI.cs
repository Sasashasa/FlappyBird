using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseModePanelUI : MonoBehaviour
{
	[SerializeField] private Button _normalButton;
	[SerializeField] private Button _reversedButton;
	[SerializeField] private Button _fastButton;
	
	private void Awake()
	{
		_normalButton.onClick.AddListener(() =>
		{
			PlayerData.Mode = Mode.Normal;
			SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
		});
        
		_reversedButton.onClick.AddListener(() =>
		{
			PlayerData.Mode = Mode.Reversed;
			SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
		});
        
		_fastButton.onClick.AddListener(() =>
		{
			PlayerData.Mode = Mode.Fast;
			SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
		});
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