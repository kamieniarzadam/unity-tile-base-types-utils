using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class LocationAwareTileBase : PrefabTileBase
{

    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
    {
        var ret = base.StartUp(location, tilemap, go);
        go.AddComponent<LocationOnTilemapHelper>().Location = location;
        go.SetActive(true);
        return ret;
    }
}
