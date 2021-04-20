using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public TextMeshProUGUI CoinsText;
	public GameObject InGamePanel;
	public GameObject WinPanel;
	public GameObject LosePanel;
	public Image[] Gradients;
	public TextMeshProUGUI CoinsTextInWinnerPanel;

	private CoinsController _coinsController;
	private Color _tempColor;
	private float _timeOfAddingCoins = 0.5f;
	private int _addingCoinsAmount;
	private void Awake()
	{
		InGamePanel.SetActive(true);
		_coinsController = FindObjectOfType<CoinsController>();
		CoinsText.text = _coinsController.GetCoinsAmount().ToString(); 
		SetGradientsAlpha(1, 1);
	}
	public void PauseGame()
	{
		Time.timeScale = 0f;
	}
	public void ContinueGame()
	{
		Time.timeScale = 1f;
	}
	public void NextLVL()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void LoseGame()
	{
		InGamePanel.SetActive(false);
		LosePanel.SetActive(true);
		Time.timeScale = 0f;
	}
	public void ActivateWinPanel(int coinsNum)
	{
		InGamePanel.SetActive(false);
		WinPanel.SetActive(true);
		CoinsTextInWinnerPanel.text = "+0";
		AddMoreCoinsInUI(coinsNum);
	}
	private void AddMoreCoinsInUI(int Amount)
	{
		for (int i = 0; i < Amount; i++)
		{
			Invoke("AddSingleCoin", 0.5f + _timeOfAddingCoins / Amount * i);
		}
	}
	public void SetGradientsAlpha(float maxHP, float currentHP)
	{
		for (int i = 0; i < Gradients.Length; i++)
		{
			_tempColor = Gradients[i].color;
			_tempColor.a = maxHP - currentHP;
			Gradients[i].color = _tempColor;
		}
	}
	private void AddSingleCoin()
	{
		_addingCoinsAmount++;
		CoinsTextInWinnerPanel.text = "+" + _addingCoinsAmount;
	}
}
