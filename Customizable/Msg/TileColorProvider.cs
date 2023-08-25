using UnityEngine;

[ExecuteAlways]
public class TileColorProvider : Poller<Color>
    , HierarchyMsg<TileColor>.IProvider
{
    protected override Color CurrentValue
    {
        get
        {
            var locationComponent = GetComponentInParent<LocationOnTilemapHelper>();
            return locationComponent.GetTilemap().GetColor(locationComponent.Location);
        }
    }

    protected override Color LastValue
    {
        set => HierarchyMsg<TileColor>.Publish(this);
    }

    TileColor HierarchyMsg<TileColor>.IProvider.Provide() => new TileColor { color = LastValue };
}
