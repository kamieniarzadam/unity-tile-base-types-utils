using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class SectorConnectionStateHandler
    : MonoBehaviour
    , HierarchyMsg<ConnectionState>.IHandler
{
    public UnityEvent OnConnect;
    public UnityEvent OnDisconnect;

    void Start() => HierarchyMsg<ConnectionState>.Request(this);

    private void SetConnected(bool c)
    {
        if (c)
        {
            OnConnect.Invoke();
        }
        else
        {
            OnDisconnect.Invoke();
        }
    }

    void HierarchyMsg<ConnectionState>.IHandler.Handle(ConnectionState payload) => SetConnected(payload.connected);
}
