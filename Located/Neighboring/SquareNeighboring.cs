using System.Collections.Generic;
using UnityEngine;

public class SquareNeighboring : Neighboring
{
    public enum Relation
    {
        East,
        South,
        West,
        North
    }

    public SquareNeighboring()
    {
        converse = new ConverseRelation(
            new Dictionary<System.Enum, System.Enum>
            {
                    { Relation.East, Relation.West },
                    { Relation.South, Relation.North }
            }
        );
    }

    public override Dictionary<System.Enum, Vector3Int> GetNeighborsLocations(Vector3Int location)
    {
        int oddYOffset = System.Math.Abs(location.y) % 2;
        return new Dictionary<System.Enum, Vector3Int>
        {
            { Relation.East, location + new Vector3Int(1, 0, 0) },
            { Relation.South, location + new Vector3Int(0, -1, 0) },
            { Relation.West, location + new Vector3Int(-1, 0, 0) },
            { Relation.North, location + new Vector3Int(0, 1, 0) }
        };
    }
}
