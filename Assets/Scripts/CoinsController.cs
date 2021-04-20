using UnityEngine;

public class CoinsController : MonoBehaviour
{
	private string _coinsString = "Coins";
	private int _curentCoinsAmount;
	public int GetCoinsAmount()
	{
		_curentCoinsAmount = PlayerPrefs.GetInt(_coinsString);
		return _curentCoinsAmount;
	}

	public void AddCoins(int coinsAmount)
	{
		_curentCoinsAmount += coinsAmount;
		PlayerPrefs.SetInt(_coinsString, _curentCoinsAmount);
	}
}
