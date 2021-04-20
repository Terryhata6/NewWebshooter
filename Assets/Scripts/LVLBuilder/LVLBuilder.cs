using UnityEngine;

public class LVLBuilder : MonoBehaviour
{
	//TODO
	private MovementPoints[] PlayerMovementPoints;
	public MovementPoints[] BuildLvlAndReturnMovementPoints(LevelCreator lvl)
	{
		for (int i = 0; i < lvl.Buildings1Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding1"]), lvl.Buildings1Transforms[i].Position, lvl.Buildings1Transforms[i].Rotation).transform.localScale = lvl.Buildings1Transforms[i].Scale;
		}
		for (int i = 0; i < lvl.Buildings2Transforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["PrepearedBuilding2"]), lvl.Buildings2Transforms[i].Position, lvl.Buildings2Transforms[i].Rotation).transform.localScale = lvl.Buildings2Transforms[i].Scale;
		}
		for (int i = 0; i < lvl.BuildingCubeTransforms.Length; i++)
		{
			Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["BuildingCube"]), lvl.BuildingCubeTransforms[i].Position, lvl.BuildingCubeTransforms[i].Rotation).transform.localScale = lvl.BuildingCubeTransforms[i].Scale;
		}
		MovementPoints[] TempMovementPoints = new MovementPoints[lvl.MovementPoints.Length];
		for (int i = 0; i < lvl.MovementPoints.Length; i++)
		{
			GameObject TempGameObject = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["MovingPoint"]), lvl.MovementPoints[i].Transform.Position, lvl.MovementPoints[i].Transform.Rotation);
			TempGameObject.transform.localScale = lvl.MovementPoints[i].Transform.Scale;
			TempMovementPoints[i] = TempGameObject.GetComponent<MovementPoints>();
			TempMovementPoints[i].RotationVector = lvl.MovementPoints[i].RotationVector;
			TempMovementPoints[i].NewSpeed = lvl.MovementPoints[i].NewSpeed;
			TempMovementPoints[i].PointNum = lvl.MovementPoints[i].PointNum;
			TempMovementPoints[i].NeedToRotate = lvl.MovementPoints[i].NeedToRotate;
			TempMovementPoints[i].NeedToCountEnemy = lvl.MovementPoints[i].NeedToCountEnemy;
			TempMovementPoints[i].NeedToChangeSpeed = lvl.MovementPoints[i].NeedToChangeSpeed;
			TempMovementPoints[i].IsFinalPoint = lvl.MovementPoints[i].IsFinalPoint;
			TempMovementPoints[i].Enemyes = new GameObject[lvl.MovementPoints[i].EnemyTransforms.Length];
		}


		for (int i = 0; i < lvl.SimpleEnemyTransforms.Length; i++)
		{
			for (int j = 0; j < lvl.MovementPoints.Length; j++)
			{
				for (int k = 0; k < lvl.MovementPoints[j].EnemyTransforms.Length; k++)
				{
					if (lvl.MovementPoints[j].EnemyTransforms[k].Position == lvl.SimpleEnemyTransforms[i].Position)
					{
						TempMovementPoints[j].Enemyes[k] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyPrefabWithRagdoll"]), lvl.SimpleEnemyTransforms[i].Position, lvl.SimpleEnemyTransforms[i].Rotation);
						TempMovementPoints[j].Enemyes[k].transform.localScale = lvl.SimpleEnemyTransforms[i].Scale;
					}
				}
			}
		}
		for (int i = 0; i < lvl.ThrowingEnemyTransforms.Length; i++)
		{
			//Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyThrowingBombs"]), ThrowingEnemyTransforms[i].Position, ThrowingEnemyTransforms[i].Rotation).transform.localScale = ThrowingEnemyTransforms[i].Scale;
			for (int j = 0; j < lvl.MovementPoints.Length; j++)
			{
				for (int k = 0; k < lvl.MovementPoints[j].EnemyTransforms.Length; k++)
				{
					if (lvl.MovementPoints[j].EnemyTransforms[k].Position == lvl.ThrowingEnemyTransforms[i].Position)
					{
						TempMovementPoints[j].Enemyes[k] = Instantiate(Resources.Load<GameObject>(PrefabAssetPath.LevelParts["EnemyThrowingBombs"]), lvl.ThrowingEnemyTransforms[i].Position, lvl.ThrowingEnemyTransforms[i].Rotation);
						TempMovementPoints[j].Enemyes[k].transform.localScale = lvl.ThrowingEnemyTransforms[i].Scale;
					}
				}
			}
		}
		PlayerMovementPoints = new MovementPoints[TempMovementPoints.Length];
		for (int i = 0; i < PlayerMovementPoints.Length; i++)
		{
			for (int j = 0; j < TempMovementPoints.Length; j++)
			{
				if(TempMovementPoints[j].PointNum == i)
				{
					PlayerMovementPoints[i] = TempMovementPoints[j];
				}
			}
		}
		return PlayerMovementPoints;
	}
}
