using NaughtyAttributes;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LVL_number_1", menuName = "Create LVL", order = 0)]
public class LevelCreator : ScriptableObject
{
	[InfoBox(" нопка 'BuildScene' чисто гл€нуть все ли правильно спавнитс€ (не забудь почистить сцену)", EInfoBoxType.Warning)]
	public CustomTransform[] Buildings1Transforms;
	public CustomTransform[] Buildings2Transforms;
	public CustomTransform[] BuildingCubeTransforms;
	public CustomTransform[] SimpleEnemyTransforms;
	public CustomTransform[] ThrowingEnemyTransforms;
	public MovementPointForBuilder[] MovementPoints;


	[Button]
	public void PrepareArrays()
	{
		Buildings1Transforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings1)).Length];
		Buildings2Transforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings2)).Length];
		BuildingCubeTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingCube)).Length];
		SimpleEnemyTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.SimpleEnemy)).Length];
		ThrowingEnemyTransforms = new CustomTransform[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ThrowingEnemy)).Length];
		MovementPoints = new MovementPointForBuilder[GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.MovingPoint)).Length];
	}
	[Button]
	public void ScanScene()
	{
		FindBuildings1(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings1)));
		FindBuildings2(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.Buildings2)));
		FindBuildingCubes(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.BuildingCube)));
		FindSimpleEnemyes(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.SimpleEnemy)));
		FindThrowingEnemyes(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.ThrowingEnemy)));
		ScanMovingPoints(GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.MovingPoint)));
	}
	private void FindBuildings1(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			Buildings1Transforms[i].Position = gameObjects[i].transform.position;
			Buildings1Transforms[i].Rotation = gameObjects[i].transform.rotation;
			Buildings1Transforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindBuildings2(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			Buildings2Transforms[i].Position = gameObjects[i].transform.position;
			Buildings2Transforms[i].Rotation = gameObjects[i].transform.rotation;
			Buildings2Transforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindBuildingCubes(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			BuildingCubeTransforms[i].Position = gameObjects[i].transform.position;
			BuildingCubeTransforms[i].Rotation = gameObjects[i].transform.rotation;
			BuildingCubeTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindSimpleEnemyes(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			SimpleEnemyTransforms[i].Position = gameObjects[i].transform.position;
			SimpleEnemyTransforms[i].Rotation = gameObjects[i].transform.rotation;
			SimpleEnemyTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void FindThrowingEnemyes(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			ThrowingEnemyTransforms[i].Position = gameObjects[i].transform.position;
			ThrowingEnemyTransforms[i].Rotation = gameObjects[i].transform.rotation;
			ThrowingEnemyTransforms[i].Scale = gameObjects[i].transform.localScale;
		}
	}
	private void ScanMovingPoints(GameObject[] gameObjects)
	{
		for (int i = 0; i < gameObjects.Length; i++)
		{
			MovementPoints[i].Transform.Position = gameObjects[i].transform.position;
			MovementPoints[i].Transform.Rotation = gameObjects[i].transform.rotation;
			MovementPoints[i].Transform.Scale = gameObjects[i].transform.localScale;
			MovementPoints TempMovementPoint = gameObjects[i].GetComponent<MovementPoints>();
			MovementPoints[i].NeedToRotate = TempMovementPoint.NeedToRotate;
			MovementPoints[i].NeedToCountEnemy = TempMovementPoint.NeedToCountEnemy;
			MovementPoints[i].NeedToChangeSpeed = TempMovementPoint.NeedToChangeSpeed;
			MovementPoints[i].NewSpeed = TempMovementPoint.NewSpeed;
			MovementPoints[i].RotationVector = TempMovementPoint.RotationVector;
			MovementPoints[i].PointNum = TempMovementPoint.PointNum;
			MovementPoints[i].IsFinalPoint = TempMovementPoint.IsFinalPoint;
			MovementPoints[i].EnemyTransforms = new CustomTransform[TempMovementPoint.Enemyes.Length];

		}
	}
	[Button]
	public void ScanMovementPoints()
	{
		GameObject[] PointsGameObjects = GameObject.FindGameObjectsWithTag(TagManager.GetTag(TagType.MovingPoint));
		for (int i = 0; i < MovementPoints.Length; i++)
		{
			for (int j = 0; j < MovementPoints[i].EnemyTransforms.Length; j++)
			{
				MovementPoints TempMovementPoint = PointsGameObjects[i].GetComponent<MovementPoints>();
				MovementPoints[i].EnemyTransforms[j].Position = TempMovementPoint.Enemyes[j].transform.position;
				MovementPoints[i].EnemyTransforms[j].Rotation = TempMovementPoint.Enemyes[j].transform.rotation;
				MovementPoints[i].EnemyTransforms[j].Scale = TempMovementPoint.Enemyes[j].transform.localScale;
			}
		}
	}

	[Button]
	public void BuildScene()
	{
		for (int i = 0; i < Buildings1Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding1"]), Buildings1Transforms[i].Position, Buildings1Transforms[i].Rotation).transform.localScale = Buildings1Transforms[i].Scale;
		}
		for (int i = 0; i < Buildings2Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding2"]), Buildings2Transforms[i].Position, Buildings2Transforms[i].Rotation).transform.localScale = Buildings2Transforms[i].Scale;
		}
		for (int i = 0; i < BuildingCubeTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BuildingCube"]), BuildingCubeTransforms[i].Position, BuildingCubeTransforms[i].Rotation).transform.localScale = BuildingCubeTransforms[i].Scale;
		}
		MovementPoints[] TempMovementPoints = new MovementPoints[MovementPoints.Length];
		for (int i = 0; i < MovementPoints.Length; i++)
		{
			GameObject TempGameObject = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["MovingPoint"]), MovementPoints[i].Transform.Position, MovementPoints[i].Transform.Rotation);
			TempGameObject.transform.localScale = MovementPoints[i].Transform.Scale;
			TempMovementPoints[i] = TempGameObject.GetComponent<MovementPoints>();
			TempMovementPoints[i].RotationVector = MovementPoints[i].RotationVector;
			TempMovementPoints[i].NewSpeed = MovementPoints[i].NewSpeed;
			TempMovementPoints[i].PointNum = MovementPoints[i].PointNum;
			TempMovementPoints[i].NeedToRotate = MovementPoints[i].NeedToRotate;
			TempMovementPoints[i].NeedToCountEnemy = MovementPoints[i].NeedToCountEnemy;
			TempMovementPoints[i].NeedToChangeSpeed = MovementPoints[i].NeedToChangeSpeed;
			TempMovementPoints[i].IsFinalPoint = MovementPoints[i].IsFinalPoint;
			TempMovementPoints[i].Enemyes = new GameObject[MovementPoints[i].EnemyTransforms.Length];
		}


		for (int i = 0; i < SimpleEnemyTransforms.Length; i++)
		{
			for (int j = 0; j < MovementPoints.Length; j++)
			{
				for (int k = 0; k < MovementPoints[j].EnemyTransforms.Length; k++)
				{
					if(MovementPoints[j].EnemyTransforms[k].Position == SimpleEnemyTransforms[i].Position)
					{
						TempMovementPoints[j].Enemyes[k] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyPrefabWithRagdoll"]), SimpleEnemyTransforms[i].Position, SimpleEnemyTransforms[i].Rotation);
						TempMovementPoints[j].Enemyes[k].transform.localScale = SimpleEnemyTransforms[i].Scale;
					}
				}
			}
		}
		for (int i = 0; i < ThrowingEnemyTransforms.Length; i++)
		{
			//Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyThrowingBombs"]), ThrowingEnemyTransforms[i].Position, ThrowingEnemyTransforms[i].Rotation).transform.localScale = ThrowingEnemyTransforms[i].Scale;
			for (int j = 0; j < MovementPoints.Length; j++)
			{
				for (int k = 0; k < MovementPoints[j].EnemyTransforms.Length; k++)
				{
					if (MovementPoints[j].EnemyTransforms[k].Position == ThrowingEnemyTransforms[i].Position)
					{
						TempMovementPoints[j].Enemyes[k] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyThrowingBombs"]), ThrowingEnemyTransforms[i].Position, ThrowingEnemyTransforms[i].Rotation);
						TempMovementPoints[j].Enemyes[k].transform.localScale = ThrowingEnemyTransforms[i].Scale;
					}
				}
			}
		}
	}
}

[Serializable]
public class CustomTransform
{
	public Vector3 Position;
	public Quaternion Rotation;
	public Vector3 Scale;
}
[Serializable]
public class MovementPointForBuilder
{
	public CustomTransform Transform;
	public CustomTransform[] EnemyTransforms;
	public Vector3 RotationVector;
	public float NewSpeed;
	public int PointNum = 0;
	public bool NeedToRotate;
	public bool NeedToCountEnemy = true;
	public bool NeedToChangeSpeed = false;
	public bool IsFinalPoint = false;
}
