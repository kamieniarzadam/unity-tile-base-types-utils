using UnityEngine;

[ExecuteInEditMode]
public class TileColorUpdateToNeighborsBroadcaster : MonoBehaviour
    , HierarchyMsg<TileColor>.IHandler
    , HierarchyMsg<NeighborRequest, TileColor>.IResponder
{
    void HierarchyMsg<TileColor>.IHandler.Handle(TileColor payload)
    {
        var locationComp = GetComponentInParent<LocationOnTilemapHelper>();
        locationComp.DoForNeighbors(
            neighbor => HierarchyMsg<NeighborRequest, TileColor>.Publish(
                payload,
                new NeighborRequest { relation = GetComponentInParent<LocationOnTilemapHelper>().GetConverseRelation(neighbor.Key) },
                neighbor.Value));
    }

    TileColor HierarchyMsg<NeighborRequest, TileColor>.IResponder.Respond(NeighborRequest request)
    {
        return HierarchyMsg<TileColor>.Get(GetComponentInParent<LocationOnTilemapHelper>().GetNeighborsGameObjects()[request.relation].gameObject.transform.GetChild(0).gameObject);
    }
}