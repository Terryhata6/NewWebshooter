using UnityEngine;

public class MainGameController : MonoBehaviour
{
	public LevelCreator[] AvailableLevels;

	private PlayerMovement _playerMovement;
	private ArtController _artController;
	private UIController _uiController;
	private SaveController _saveController;
	private LVLBuilder _lvlBuilder;
	private CoinsController _coinsController;
	private HealthController _healthController;
	private BonusLvlController _bonusLvlController;
	private int AmountOfEnemyes;
	private float _timeBeforeContinueMoving = 0.5f;
	private bool _isFinal;
	private bool _lvlComplete;
	private void Awake()
	{
		Time.timeScale = 1f;
		_bonusLvlController = FindObjectOfType<BonusLvlController>();
		_playerMovement = FindObjectOfType<PlayerMovement>();
		_artController = FindObjectOfType<ArtController>();
		_uiController = FindObjectOfType<UIController>();
		_lvlBuilder = FindObjectOfType<LVLBuilder>();
		_saveController = FindObjectOfType<SaveController>();
		_coinsController = FindObjectOfType<CoinsController>();
		_healthController = FindObjectOfType<HealthController>();
		StartLvl();
	}
	public void StartLvl()
	{
		_artController.SetMaterials();
		if (_saveController.IsItTimeForBonusLvl())
		{
			_bonusLvlController.StartBonusPart();
			_artController.ChangeMaterials();
			Debug.Log("BonusLVL");
		}
		else
		{
			_playerMovement.SetMovementPoints(_lvlBuilder.BuildLvlAndReturnMovementPoints(AvailableLevels[_saveController.GetNextLvlNum()]));
			_playerMovement.StartMoving();
		}
	}
	public bool CanMove()
	{
		if (AmountOfEnemyes > 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	public void LevelIsEnded()
	{
		_isFinal = true;
	}
	public void LevelIsEnded(bool noEnemyes)
	{
		_isFinal = true;
		if (noEnemyes)
		{
			PlayerWin();
		}
	}
	public void ActivateEnemyes(GameObject[] enemyes, bool needToCountEnemyes)
	{
		if (needToCountEnemyes)
		{
			AmountOfEnemyes += enemyes.Length;
		}
		for (int i = 0; i < enemyes.Length; i++)
		{
			if (enemyes[i].GetComponent<EnemyController>() != null)
			{
				enemyes[i].GetComponent<EnemyController>().ActivateEnemy();
				_healthController.AddEnemyToList(enemyes[i].GetComponent<EnemyController>());
			}
			else
			{
				enemyes[i].GetComponent<ThrowingEnemyController>().ActivateEnemy();
				_healthController.AddEnemyToList(enemyes[i].GetComponent<ThrowingEnemyController>());
			}
		}
	}
	public void EnemyBeenDefeated()
	{
		if (AmountOfEnemyes > 0)
		{
			AmountOfEnemyes--;
			if (CanMove())
			{
				Invoke("ContinueMoving", _timeBeforeContinueMoving);
				if (_isFinal)
				{
					Invoke("PlayerWin", _timeBeforeContinueMoving);
				}
			}
		}
	}
	public void PlayerLose()
	{
		if (!_lvlComplete)
			_uiController.LoseGame();
	}
	private void ContinueMoving()
	{
		_playerMovement.ContinueMoving();
	}
	private void PlayerWin()
	{
		_lvlComplete = true;
		int CoinsNum = Random.Range(40, 71);
		_saveController.SaveCurrentLvl(false);
		_uiController.ActivateWinPanel(CoinsNum);
		_coinsController.AddCoins(CoinsNum);
	}
	public void EndBonusLvl(int amountOfCoinsCaught)
	{
		_lvlComplete = true;
		int CoinsNum = amountOfCoinsCaught;
		_saveController.SaveCurrentLvl(true);
		_uiController.ActivateWinPanel(CoinsNum);
		_coinsController.AddCoins(CoinsNum);
	}
}
