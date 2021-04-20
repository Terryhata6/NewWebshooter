using System.Collections.Generic;

public static class TagManager
{
	private static readonly Dictionary<TagType, string> _tags;

	static TagManager()
	{
		_tags = new Dictionary<TagType, string>
			{
				{TagType.Player, "Player"},
				{TagType.EnemyPart, "EnemyPart"},
				{TagType.Wall, "Wall"},
				{TagType.Web, "Web"},
				{TagType.MovingPoint, "MovingPoint"},
				{TagType.Object, "Object"},
				{TagType.Buildings1, "Buildings1"},
				{TagType.Buildings2, "Buildings2"},
				{TagType.SimpleEnemy, "SimpleEnemy"},
				{TagType.ThrowingEnemy, "ThrowingEnemy"},
				{TagType.BuildingCube, "BuildingCube"},
				{TagType.Bottom, "Bottom"}
			};
	}

	public static string GetTag(TagType tagType)
	{
		return _tags[tagType];
	}
}
