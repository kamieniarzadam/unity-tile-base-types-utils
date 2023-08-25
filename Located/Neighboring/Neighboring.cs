using System.Collections.Generic;
using UnityEngine;

public abstract class Neighboring
{
    public class ConverseRelation
    {
        private readonly Dictionary<int, int> conversePairsLookup;

        public ConverseRelation(Dictionary<System.Enum, System.Enum> conversePairs)
        {
            conversePairsLookup = new Dictionary<int, int>();
            foreach (var pair in conversePairs)
            {
                conversePairsLookup.Add(System.Convert.ToInt32(pair.Key), System.Convert.ToInt32(pair.Value));
                conversePairsLookup.Add(System.Convert.ToInt32(pair.Value), System.Convert.ToInt32(pair.Key));
            }
        }

        public int Converse(int relation)
        {
            return conversePairsLookup[relation];
        }
    }

    public ConverseRelation converse;

    public int GetConverseRelation(int relation)
    {
        return converse.Converse(relation);
    }

    public abstract Dictionary<System.Enum, Vector3Int> GetNeighborsLocations(Vector3Int location);
}
