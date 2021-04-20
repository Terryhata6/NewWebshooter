using UnityEngine;

public class Bomb : MonoBehaviour
{
	public LayerMask _enemyLayer;

	private Rigidbody _rigidbody;
	private Vector3 RotationVector;
	private bool _needToMoveBomb = false;

	//movingBomb
	private AnimationCurve _curve;
	private Transform _destinationTransform;
	private Vector3 _destinationVector;
	private Vector3 _movingVector;
	private Vector3 _customDestinationVector;
	private float _bombMoveSpeed;
	private float _maxDistance;
	private float _maxHeight;
	private float _startingHeight;
	private ParticlesController _particlesController;

	public bool NeedToRotate = true;
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_particlesController = FindObjectOfType<ParticlesController>();
		int num1 = Random.Range(0, 3);
		int num2 = Random.Range(0, 3 - num1);
		int num3 = Random.Range(0, 3 - num1 - num2);
		_customDestinationVector = new Vector3(0, 0, 0.5f);
		RotationVector = new Vector3(num1, num2, num3);
	}
	private void FixedUpdate()
	{
		if (NeedToRotate)
			RotateObject();
		if (_needToMoveBomb)
			MoveBomb();
	}
	private void RotateObject()
	{
		_rigidbody.transform.Rotate(RotationVector);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
		{
			Destroy(collision.gameObject);
			DetonateBomb();
		}
	}
	private void DetonateBomb()
	{
		_particlesController.MakeSmallExplosion(transform.position);
		Collider[] sds = Physics.OverlapSphere(transform.position, 11f, _enemyLayer);
		for (int i = 0; i < sds.Length; i++)
		{
			try
			{
				sds[i].GetComponent<ThrowingEnemyController>().ThrowEnemy(transform.position);
			}
			catch { }
			try
			{
				sds[i].GetComponent<EnemyController>().ThrowEnemy(transform.position);
			}
			catch { }
		}
		Destroy(this.gameObject);
	}
	public void ThrowBomb(AnimationCurve curve, Transform destinationTransform, float speed, float maxHeight)
	{
		transform.parent = null;
		_curve = curve;
		_destinationTransform = destinationTransform;
		_destinationVector = _destinationTransform.position;
		_bombMoveSpeed = speed;
		_maxDistance = Vector3.Distance(transform.position, destinationTransform.position);
		_startingHeight = transform.position.y;
		_maxHeight = maxHeight;
		_needToMoveBomb = true;
		NeedToRotate = true;
	}
	public void MoveBomb()
	{
		_movingVector = Vector3.MoveTowards(transform.position, _destinationVector + _customDestinationVector, _bombMoveSpeed);
		if ((Vector3.Distance(transform.position, _destinationTransform.position + _customDestinationVector) / _maxDistance) > 0.3f)
		{
			_movingVector.y = _startingHeight + _curve.Evaluate((Vector3.Distance(transform.position, _destinationTransform.position + _customDestinationVector) / _maxDistance)) * _maxHeight;
		}
		if (Vector3.Distance(transform.position, _destinationTransform.position + _customDestinationVector) <= 1f)
		{
			_particlesController.MakeSmallExplosion(transform.position);
			Invoke("killPlayer", 0.1f);
		}
		transform.position = _movingVector;
	}
	private void killPlayer()
	{
		FindObjectOfType<UIController>().SetGradientsAlpha(1, 0);
		FindObjectOfType<MainGameController>().PlayerLose();
	}
}
