using UnityEngine;

[ExecuteInEditMode]
public class NeighborTileColorUpdateRepeaterToSector : MonoBehaviour
    , HierarchyMsg<TileNeighborColor>.IProvider
    , HierarchyMsg<NeighborRequest, TileColor>.IRequestor
{
    NeighborRequest HierarchyMsg<NeighborRequest, TileColor>.IRequestor.FormRequest() =>
        new NeighborRequest { relation = transform.GetSiblingIndex() };

    void HierarchyMsg<NeighborRequest, TileColor>.IRequestor.Handle(NeighborRequest request, TileColor response)
    {
        if (transform.GetSiblingIndex() == request.relation)
        {
            HierarchyMsg<TileNeighborColor>.Publish(new TileNeighborColor { color = response.color }, gameObject);
        }
    }

    TileNeighborColor HierarchyMsg<TileNeighborColor>.IProvider.Provide() =>
        new TileNeighborColor { color = HierarchyMsg<NeighborRequest, TileColor>.Get(this).color };
}