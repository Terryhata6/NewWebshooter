using UnityEngine;

public class InputController : MonoBehaviour
{
	//следит за пальцем на экране
	private WebShooter _webShooter;
	[HideInInspector] public Vector3 TouchPosition;
	[HideInInspector] public bool DragingStarted = false;
	public bool UseMouse = false;

	private void Start()
	{
		_webShooter = FindObjectOfType<WebShooter>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			DragingStarted = true;
			TouchPosition = Input.mousePosition;
			_webShooter.ShootWeb(TouchPosition);
		}
		else
		{
			DragingStarted = false;
		}
	}
}
