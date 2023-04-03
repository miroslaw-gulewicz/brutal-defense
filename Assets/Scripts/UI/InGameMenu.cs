using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
	[SerializeField] public Button _startWaveButton;
	
	[SerializeField] public WaveManager _waveManager;


	private void Awake()
	{
		_waveManager.OnEnemyCountChanged += onEnemyCountChanged;
		_startWaveButton.onClick.AddListener(HideButton);
	}

	private void onEnemyCountChanged()
	{
		_startWaveButton.gameObject.SetActive(_waveManager.CurrentEnemies == 0);
	}

	private void HideButton()
	{
		_startWaveButton.gameObject.SetActive(false);
	}
}
