using UnityEngine;
using NaughtyAttributes;

public class BonusLvlController : MonoBehaviour
{
	public GameObject Coin;
	public GameObject Wall;
	public Vector3 StartThrownigPosition;
	public int MaxCoins;
	public float TimeBeforeThrownig = 0.4f;

	private MainGameController _mainGameController;
	private GameObject _throwedObject;
	private ThrowingObject _throwedObjectScript;
	private Vector2 _minScreenPosition, _maxScreenPosition;
	private float _timeBeforeThrowing;
	private float _widthOfScreen;
	private int _numberOfCoinsCaught = 0;

	private void Awake()
	{
		_mainGameController = FindObjectOfType<MainGameController>();
		Camera.main.orthographic = true;
		_minScreenPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		_maxScreenPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		_widthOfScreen = _maxScreenPosition.x * 2;
		Camera.main.orthographic = false;
	}
	public void CaughtCoin()
	{
		_numberOfCoinsCaught++;
	}
	public void StartSpawner()
	{
		_timeBeforeThrowing = 0f;
		for (int i = 0; i < MaxCoins; i++)
		{
			_timeBeforeThrowing += TimeBeforeThrownig;
			Invoke("SpawnCoin", _timeBeforeThrowing);
			if (i == MaxCoins - 1)
			{
				_timeBeforeThrowing += 5;
				Invoke("EndLvl", _timeBeforeThrowing);
			}
		}
	}
	private void SpawnCoin()
	{
		StartThrownigPosition.x = _minScreenPosition.x + _widthOfScreen * Random.Range(0f, 1f);
		_throwedObject = Instantiate(Coin, StartThrownigPosition, Quaternion.identity);
		_throwedObjectScript = _throwedObject.GetComponent<ThrowingObject>();
		_throwedObjectScript.SetForce(Random.Range(3000f, 3501f));
		_throwedObjectScript.TrowObjectUp();
		_throwedObjectScript.bonusLvlController = this;
	}
	public void StartBonusPart()
	{
		Wall.SetActive(true);
		StartSpawner();
	}
	private void EndLvl()
	{
		_mainGameController.EndBonusLvl(_numberOfCoinsCaught);
	}
}
