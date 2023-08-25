using System.Collections.Generic;
using UnityEngine;

public class HexagonNeighboring : Neighboring
{
    public enum Relation
    {
        East,
        SouthEast,
        SouthWest,
        West,
        NorthWest,
        NorthEast
    }

    public HexagonNeighboring()
    {
        converse = new ConverseRelation(
            new Dictionary<System.Enum, System.Enum>
            {
                    { Relation.East, Relation.West },
                    { Relation.SouthEast, Relation.NorthWest },
                    { Relation.SouthWest, Relation.NorthEast }
            }
        );
    }

    public override Dictionary<System.Enum, Vector3Int> GetNeighborsLocations(Vector3Int location)
    {
        int oddYOffset = System.Math.Abs(location.y) % 2;
        return new Dictionary<System.Enum, Vector3Int>
        {
            { Relation.East, location + new Vector3Int(1, 0, 0) },
            { Relation.SouthEast, location + new Vector3Int(0+oddYOffset, -1, 0) },
            { Relation.SouthWest, location + new Vector3Int(-1+oddYOffset, -1, 0) },
            { Relation.West, location + new Vector3Int(-1, 0, 0) },
            { Relation.NorthWest, location + new Vector3Int(-1+oddYOffset, 1, 0) },
            { Relation.NorthEast, location + new Vector3Int(0+oddYOffset, 1, 0) }
        };
    }
}
