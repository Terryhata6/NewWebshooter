using System;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class MovementPoints : MonoBehaviour
{

	public int PointNum = 0;
	public GameObject[] Enemyes;
	public bool NeedToCountEnemy = true;
	public bool IsFinalPoint = false;

	[Foldout("Extras")]
	public bool NeedToRotate;

	[Foldout("Extras")]
	[ShowIf("NeedToRotate")]
	public Vector3 RotationVector;

	[Foldout("Extras")]
	public bool NeedToChangeSpeed = false;

	[Foldout("Extras")]
	[ShowIf("NeedToChangeSpeed")]
	public float NewSpeed;
}
