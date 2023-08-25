using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class PrefabTileBase : TileBase
{
    public GameObject prefab;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0.0f, 0.0f, 0.0f, 0.0f), new Vector2(0.0f, 0.0f));
        tileData.gameObject = prefab;
    }
}
