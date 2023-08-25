using UnityEngine;

[ExecuteInEditMode]
public class TilePresenceUpdateToNeighborsBroadcaster : MonoBehaviour
    , HierarchyMsg<NeighborRequest, ConnectionState>.IResponder
{
    private void BroadcastStateToNeighbours(bool connected)
    {
        var locationComp = transform.parent.GetComponent<LocationOnTilemapHelper>();
        locationComp.DoForNeighbors(
            neighbor => HierarchyMsg<NeighborRequest, ConnectionState>.Publish(
                new ConnectionState { connected = connected },
                new NeighborRequest { relation = locationComp.GetConverseRelation(neighbor.Key) },
                neighbor.Value));
    }

    private void Awake()
    {
        BroadcastStateToNeighbours(true);
    }

    private void OnDestroy()
    {
        BroadcastStateToNeighbours(false);
    }

    ConnectionState HierarchyMsg<NeighborRequest, ConnectionState>.IResponder.Respond(NeighborRequest request)
    {
        return new ConnectionState { connected = GetComponentInParent<LocationOnTilemapHelper>().GetNeighborsGameObjects().ContainsKey(request.relation) };
    }
}