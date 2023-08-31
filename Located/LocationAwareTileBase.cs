using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class LocationAwareTileBase : PrefabTileBase
{
    GameObject wrapper;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        if (!wrapper)
        {
            wrapper = new GameObject("Tile Wrapper");
            wrapper.SetActive(false);
            wrapper.transform.parent = tilemap.GetComponent<TilemapRenderer>().gameObject.transform;
            wrapper.hideFlags = HideFlags.DontSave;
            wrapper.AddComponent<LocationOnTilemapHelper>();
            Instantiate(prefab, wrapper.transform);
        }
        wrapper.SetActive(false);
        tileData.gameObject = wrapper;
    }

    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
    {
        var ret = base.StartUp(location, tilemap, go);
        go.GetComponent<LocationOnTilemapHelper>().Location = location;
        go.SetActive(true);
        return ret;
    }
}
