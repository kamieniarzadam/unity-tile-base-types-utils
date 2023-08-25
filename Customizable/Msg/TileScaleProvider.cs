using UnityEngine;

[ExecuteAlways]
public class TileScaleProvider : Poller<float>
    , HierarchyMsg<TileScale>.IProvider
{
    protected override float CurrentValue
    {
        get
        {
            var locationComponent = GetComponentInParent<LocationOnTilemapHelper>();
            return locationComponent.ForThisLocation(locationComponent.GetTilemap().GetTransformMatrix).MultiplyPoint(Vector3.forward).z;
        }
    }

    protected override float LastValue
    {
        set => HierarchyMsg<TileScale>.Publish(this);
    }

    TileScale HierarchyMsg<TileScale>.IProvider.Provide() => new TileScale { scale = LastValue };
}
