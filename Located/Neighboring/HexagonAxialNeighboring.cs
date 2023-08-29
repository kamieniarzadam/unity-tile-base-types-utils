using System.Collections.Generic;
using UnityEngine;

public class HexagonAxialNeighboring : Neighboring
{
  
    public static Dictionary<System.Enum, Vector3Int> relativeVectors;
    public static Dictionary<Vector3Int, System.Enum> relativeVectorsInverse;
    

    public enum Relation
    {
        East,
        SouthEast,
        SouthWest,
        West,
        NorthWest,
        NorthEast,

        Center
    }

    public static Dictionary<System.Enum, Vector3Int> neighboursRelativeVectors = new Dictionary<System.Enum, Vector3Int>
        {
            { Relation.East, new Vector3Int(1, 0, 0) },
            { Relation.SouthEast, new Vector3Int(1, -1, 0) },
            { Relation.SouthWest, new Vector3Int(0, -1, 0) },
            { Relation.West, new Vector3Int(-1, 0, 0) },
            { Relation.NorthWest, new Vector3Int(-1, 1, 0) },
            { Relation.NorthEast, new Vector3Int(0, 1, 0) },
            { Relation.Center, new Vector3Int(0, 0, 0) }
        };

    static HexagonAxialNeighboring()
    {
        converse = new ConverseRelation(
            new Dictionary<System.Enum, System.Enum>
            {
                    { Relation.East, Relation.West },
                    { Relation.SouthEast, Relation.NorthWest },
                    { Relation.SouthWest, Relation.NorthEast }
            }
        );
        relativeVectors = new Dictionary<System.Enum, Vector3Int> ();

        foreach (var pair in new Dictionary<System.Enum, Vector3Int> {
            { Relation.East, new Vector3Int(1, 0, 0) },
            { Relation.SouthEast, new Vector3Int(1, -1, 0) },
            { Relation.SouthWest, new Vector3Int(0, -1, 0) }
        })
        {
            relativeVectors.Add(pair.Key, pair.Value);
            relativeVectors.Add((Relation)converse.Converse(System.Convert.ToInt32(pair.Key)), -pair.Value);
        }
        relativeVectors.Add(Relation.Center, new Vector3Int(0, 0, 0) );

        relativeVectorsInverse = new Dictionary<Vector3Int, System.Enum>();
        foreach (var pair in relativeVectors)
        {
            relativeVectorsInverse.Add(pair.Value, pair.Key);
        }
    }

    public override Dictionary<System.Enum, Vector3Int> GetNeighborsLocations(Vector3Int location)
    {
        return new Dictionary<System.Enum, Vector3Int>
        {
            { Relation.East, location + new Vector3Int(1, 0, 0) },
            { Relation.SouthEast, location + new Vector3Int(1, -1, 0) },
            { Relation.SouthWest, location + new Vector3Int(0, -1, 0) },
            { Relation.West, location + new Vector3Int(-1, 0, 0) },
            { Relation.NorthWest, location + new Vector3Int(-1, 1, 0) },
            { Relation.NorthEast, location + new Vector3Int(0, 1, 0) }
        };
    }
}
