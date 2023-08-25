using UnityEngine;

[ExecuteInEditMode]
public class NeighborTilePresenceUpdateRepeaterToSector : MonoBehaviour
    , HierarchyMsg<ConnectionState>.IProvider
    , HierarchyMsg<NeighborRequest, ConnectionState>.IRequestor
{
    NeighborRequest HierarchyMsg<NeighborRequest, ConnectionState>.IRequestor.FormRequest() =>
        new NeighborRequest { relation = transform.GetSiblingIndex() };

    void HierarchyMsg<NeighborRequest, ConnectionState>.IRequestor.Handle(NeighborRequest request, ConnectionState response)
    {
        if (transform.GetSiblingIndex() == request.relation)
        {
            HierarchyMsg<ConnectionState>.Publish(response, gameObject);
        }
    }

    ConnectionState HierarchyMsg<ConnectionState>.IProvider.Provide() =>
        HierarchyMsg<NeighborRequest, ConnectionState>.Get(this);
}