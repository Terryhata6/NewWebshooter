using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

	public float MaxHealth;
	public float EnemyHitPower;
	public float HealingPower;

	private List<EnemyController> ActiveEnemycontrollers;
	private List<ThrowingEnemyController> ActiveThrowingEnemycontrollers;
	private UIController _uiController;
	private MainGameController _mainGameController;
	private float CurrentHealth;
	private int AmountOfEnemyesAttacking;

	private void Awake()
	{
		CurrentHealth = MaxHealth;
		_uiController = FindObjectOfType<UIController>();
		_mainGameController = FindObjectOfType<MainGameController>();
		ActiveEnemycontrollers = new List<EnemyController>();
		ActiveThrowingEnemycontrollers = new List<ThrowingEnemyController>();
	}
	public void AddEnemyToList(EnemyController enemy)
	{
		ActiveEnemycontrollers.Add(enemy);
	}
	public void AddEnemyToList(ThrowingEnemyController enemy)
	{
		ActiveThrowingEnemycontrollers.Add(enemy);
	}
	private void FixedUpdate()
	{
		AmountOfEnemyesAttacking = 0;
		foreach (EnemyController enemy in ActiveEnemycontrollers)
		{
			if (enemy.IsEnemyActive && enemy.IsEnemyAttacking)
				AmountOfEnemyesAttacking++;
		}
		foreach (ThrowingEnemyController enemy in ActiveThrowingEnemycontrollers)
		{
			if (enemy.IsEnemyActive && enemy.IsEnemyAttacking)
				AmountOfEnemyesAttacking++;
		}
		if (AmountOfEnemyesAttacking > 0)
		{
			DecreaseHelth();
			_uiController.SetGradientsAlpha(MaxHealth, CurrentHealth);
		}
		if (AmountOfEnemyesAttacking == 0 && CurrentHealth < MaxHealth)
		{
			IncreaseHelth(); 
			_uiController.SetGradientsAlpha(MaxHealth, CurrentHealth);
		}
	}
	private void IncreaseHelth()
	{
		CurrentHealth += HealingPower;
	}
	private void DecreaseHelth()
	{
		CurrentHealth -= AmountOfEnemyesAttacking * EnemyHitPower;
		if (CurrentHealth <= 0)
		{
			_mainGameController.PlayerLose();
		}
	}
}
