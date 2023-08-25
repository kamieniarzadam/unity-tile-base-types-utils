using UnityEngine;

[ExecuteAlways]
public class TestManualTileScaleProvider : Poller<float>
    , HierarchyMsg<TileScale>.IProvider
{
    [Range(0.0f, 1.0f)]
    public float tileScale = 0.7f;

    protected override float CurrentValue
    {
        get => tileScale;
    }

    protected override float LastValue
    {
        set => HierarchyMsg<TileScale>.Publish(this);
    }

    TileScale HierarchyMsg<TileScale>.IProvider.Provide()
    {
        return new TileScale { scale = LastValue };
    }
}
