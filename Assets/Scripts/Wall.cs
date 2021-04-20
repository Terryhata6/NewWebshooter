using UnityEngine;

public class Wall : MonoBehaviour
{
	public GameObject Web;
	public bool IsRightWall;
	public bool IsLeftWall;
	private Vector3 _movingVector;

	private float _magicNumber = 0.05f;
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(TagManager.GetTag(TagType.Web)))
		{
			SpawnWeb(collision);
		}
	}
	//спавнит паутину и поворачивает ее нужным образом
	private void SpawnWeb(Collision collision)
	{
		SphereCollider webBulletCollider = collision.gameObject.GetComponent<SphereCollider>();
		webBulletCollider.isTrigger = true;
		GameObject WebObject = Instantiate(Web, collision.gameObject.transform.position + new Vector3(0, 0, -0.4f), Quaternion.Euler(0, 180f, Random.Range(0, 360)));
		WebObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		if (collision.transform.position.y - collision.GetContact(0).point.y <= _magicNumber)
		{
			if (collision.GetContact(0).point.z - collision.transform.position.z <= _magicNumber)
			{
				if (collision.GetContact(0).point.x > collision.transform.position.x)
				{
					WebObject.transform.rotation = Quaternion.Euler(WebObject.transform.rotation.eulerAngles + new Vector3(0, 90f, 0));
				}
				else
				{
					WebObject.transform.rotation = Quaternion.Euler(WebObject.transform.rotation.eulerAngles + new Vector3(0, -90f, 0));
				}
			}
		}
		else
		{
			WebObject.transform.rotation = Quaternion.Euler(WebObject.transform.rotation.eulerAngles + new Vector3(-90f, 0, 0));
			_movingVector = WebObject.transform.position;
			_movingVector.y -= 0.2f;
			_movingVector.z += 0.4f;
			WebObject.transform.position = _movingVector;
			WebObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		}
	}
}
