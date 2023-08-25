using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GridLayout;

[ExecuteAlways]
public class LocationOnTilemapHelper : MonoBehaviour
{
    public Tilemap GetTilemap()
    {
        return transform.parent.GetComponentInParent<Tilemap>();
    }

    protected Dictionary<CellLayout, Neighboring> neighboringDictionary = new Dictionary<CellLayout, Neighboring>
    {
        { CellLayout.Hexagon, new HexagonNeighboring() },
        { CellLayout.Rectangle, new SquareNeighboring() }
    };

    protected Dictionary<CellLayout, int> numberOfCellSidesDictionary = new Dictionary<CellLayout, int>
    {
        { CellLayout.Hexagon, 6 },
        { CellLayout.Rectangle, 4 }
    };

    public virtual CellLayout CellLayout { get => GetTilemap().cellLayout; }
    public virtual Neighboring Neighboring { get => neighboringDictionary[CellLayout]; }
    public virtual int NumberOfCellSides { get => numberOfCellSidesDictionary[CellLayout]; }
    private Vector3Int _location;
    public virtual Vector3Int Location { get => _location; set => _location = value; }

    public Dictionary<int, GameObject> GetNeighborsGameObjects()
    {
        var ret = new Dictionary<int, GameObject>();
        var tm = GetTilemap();
        if (tm)
        {
            foreach (var location in Neighboring.GetNeighborsLocations(Location))
            {
                var go = tm.GetInstantiatedObject(location.Value);
                if (go)
                {
                    ret[System.Convert.ToInt32(location.Key)] = go;
                }
            }
        }
        return ret;
    }

    public delegate void OnNeighbor(KeyValuePair<int, GameObject> go);
    public void DoForNeighbors(OnNeighbor onNeighbor)
    {
        foreach (var neighborGameObject in GetComponent<LocationOnTilemapHelper>().GetNeighborsGameObjects())
        {
            onNeighbor(neighborGameObject);
        }
    }

    public delegate T Del<T>(Vector3Int location);
    public T ForThisLocation<T>(Del<T> del)
    {
        return del(Location);
    }

    public int GetConverseRelation(int relation)
    {
        return Neighboring.GetConverseRelation(relation);
    }
}
