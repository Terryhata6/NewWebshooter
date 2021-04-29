using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float MaxSpeed;
	public float MaxRotationSpeed;

	private MovementPoints[] _movementPoints;
	private MainGameController _mainGameController;
	private float RotationSideX;
	private float RotationSideY;
	private float RotationSideZ;
	private float _speed;
	private float _rotationSpeed;
	private int _currentPointNum;
	private int TimesOfRotateX;
	private int TimesOfRotateY;
	private int TimesOfRotateZ;
	private bool _needToMove = false;

	private void Start()
	{
		_speed = MaxSpeed;
		_mainGameController = FindObjectOfType<MainGameController>();
		_rotationSpeed = FindObjectOfType<PlayerMovement>().MaxRotationSpeed;
	}
	private void FixedUpdate()
	{
		if (_needToMove && _currentPointNum < _movementPoints.Length)
		{
			transform.position = Vector3.MoveTowards(transform.position, _movementPoints[_currentPointNum].transform.position, _speed);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(TagManager.GetTag(TagType.MovingPoint)))
		{
			if (_movementPoints[_currentPointNum].NeedToRotate)
			{
				RotatePlayer(_movementPoints[_currentPointNum].RotationVector);
			}
			if (_movementPoints[_currentPointNum].NeedToChangeSpeed)
			{
				_speed = _movementPoints[_currentPointNum].NewSpeed;
			}
			if (_movementPoints[_currentPointNum].Enemyes.Length == 0)
			{
				ChangePoint();
			}
			else
			{
				_mainGameController.ActivateEnemyes(_movementPoints[_currentPointNum].Enemyes, _movementPoints[_currentPointNum].NeedToCountEnemy);
				if (_movementPoints[_currentPointNum].NeedToCountEnemy)
				{
					_needToMove = false;
				}
				else
				{
					ChangePoint();
				}
				if ((_movementPoints[_currentPointNum].IsFinalPoint))
				{
					_mainGameController.LevelIsEnded();
				}
			}

			if (_currentPointNum < _movementPoints.Length && (_movementPoints[_currentPointNum].IsFinalPoint) && _movementPoints[_currentPointNum].Enemyes.Length == 0)
			{
				_mainGameController.LevelIsEnded(true);
			}
			Destroy(other.gameObject);//что бы лишних колайдеров на сцене не было 
		}
	}
	public void SetMovementPoints(MovementPoints[] movementPoints)
	{
		_movementPoints = movementPoints;
	}
	public void StartMoving()
	{
		_needToMove = true;
	}
	public void ContinueMoving()
	{
		ChangePoint();
		_needToMove = true;
	}
	public void ChangePoint()
	{
		_currentPointNum++;
	}

	#region Rotation
	public void RotatePlayer(Vector3 RotatingVector)
	{
		if (RotatingVector.x != 0)
		{
			if (RotatingVector.x > 0)
				RotationSideX = 1;
			else
				RotationSideX = -1;
			TimesOfRotateX = (int)(ConvertToPositive(RotatingVector.x) / _rotationSpeed);
		}
		else
		{
			RotationSideX = 0;
		}
		if (RotatingVector.y != 0)
		{
			if (RotatingVector.y > 0)
				RotationSideY = 1;
			else
				RotationSideY = -1;
			TimesOfRotateY = (int)(ConvertToPositive(RotatingVector.y) / _rotationSpeed);
		}
		else
		{
			RotationSideY = 0;
		}
		if (RotatingVector.z != 0)
		{
			if (RotatingVector.z > 0)
				RotationSideZ = 1;
			else
				RotationSideZ = -1;
			TimesOfRotateZ = (int)(ConvertToPositive(RotatingVector.z) / _rotationSpeed);
		}
		else
		{
			RotationSideZ = 0;
		}


		int maxnum = 0;
		if (TimesOfRotateX > TimesOfRotateY && TimesOfRotateX > TimesOfRotateY)
		{
			maxnum = TimesOfRotateX;
		}
		if (TimesOfRotateY > TimesOfRotateX && TimesOfRotateY > TimesOfRotateZ)
		{
			maxnum = TimesOfRotateY;
		}
		if (TimesOfRotateZ > TimesOfRotateY && TimesOfRotateZ > TimesOfRotateX)
		{
			maxnum = TimesOfRotateZ;
		}
		for (int i = 0; i < maxnum; i++)
		{
			if (i <= TimesOfRotateX)
			{
				Invoke("RotateALittleX", 0.01f * i);
			}
			if (i <= TimesOfRotateY)
			{
				Invoke("RotateALittleY", 0.01f * i);
			}
			if (i <= TimesOfRotateZ)
			{
				Invoke("RotateALittleZ", 0.01f * i);
			}

		}
		//transform.Rotate(RotatingVector);
	}
	private void RotateALittleX()
	{
		transform.Rotate(new Vector3(_rotationSpeed * RotationSideX, 0, 0));
	}
	private void RotateALittleY()
	{
		transform.Rotate(new Vector3(0, _rotationSpeed * RotationSideY, 0));
	}
	private void RotateALittleZ()
	{
		transform.Rotate(new Vector3(0, 0, _rotationSpeed * RotationSideZ));
	}
	private float ConvertToPositive(float num)
	{
		if (num > 0)
			return num;
		if (num < 0)
			return -num;
		return 0;
	}
	#endregion
}
