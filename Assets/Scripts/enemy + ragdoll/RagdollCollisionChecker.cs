using UnityEngine;

public class RagdollCollisionChecker : MonoBehaviour
{
	private EnemyController _stickmanscript;
	private ThrowingEnemyController _throwingStickmanScript;
	private bool _isThrowingStickman;
	public void SetParametrs(EnemyController stickmanscript)
	{
		_isThrowingStickman = false;
		_stickmanscript = stickmanscript;
	}
	public void SetParametrs(ThrowingEnemyController stickmanscript)
	{
		_isThrowingStickman = true;
		_throwingStickmanScript = stickmanscript;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Wall)))
		{
			if (_isThrowingStickman)
			{
				_throwingStickmanScript.GetStickmanStucked(collision, transform.position, gameObject.name);
			}
			else
			{
				_stickmanscript.GetStickmanStucked(collision, transform.position, gameObject.name);
			}
		}
	}
}
