using UnityEngine;
public class HierarchyMsg
{
    public interface ISender
    {
    }

    public interface IReceiver
    {
    }

    public delegate void Send<T>(T sender) where T : ISender;
    public delegate void Receive<T>(T receiver) where T : IReceiver;

    public static void Publish<T>(GameObject root, Receive<T> receive) where T : IReceiver
    {
        foreach (var receiver in root.GetComponentsInChildren<T>(false))
        {
            if ((receiver as Behaviour).enabled)
            {
                receive(receiver);
            }
        }
    }

    public static void Request<T>(GameObject root, Send<T> send) where T : ISender
    {
        send(root.GetComponentInParent<T>());
    }
}

public class HierarchyMsg<TMessage> : HierarchyMsg
{
    public interface IProvider : ISender
    {
        TMessage Provide();
    }

    public interface IHandler : IReceiver
    {
        void Handle(TMessage message);
    }

    public static void Publish(IProvider provider)
    {
        Publish(provider, (provider as MonoBehaviour).gameObject);
    }

    public static void Publish(IProvider provider, GameObject root)
    {
        Publish(provider.Provide(), root);
    }

    public static void Publish(TMessage msg, GameObject root)
    {
        Publish<IHandler>(root, handler => handler.Handle(msg));
    }

    public static void Request(IHandler handler)
    {
        Request(handler, (handler as MonoBehaviour).gameObject);
    }

    public static void Request(IHandler handler, GameObject root)
    {
        Request<IProvider>(root, provider => handler.Handle(provider.Provide()));
    }

    public static TMessage Get(MonoBehaviour mono)
    {
        return Get(mono.gameObject);
    }

    public static TMessage Get(GameObject go)
    {
        TMessage ret = default;
        Request<IProvider>(go, provider =>
        {
            ret = provider.Provide();
        });
        return ret;
    }
}

public class HierarchyMsg<TRequest, TResponse> : HierarchyMsg<TResponse>
{
    public interface IResponder : ISender
    {
        TResponse Respond(TRequest request);
    }

    public interface IRequestor : IReceiver
    {
        TRequest FormRequest();
        void Handle(TRequest request, TResponse response);
    }

    public static void Publish(IResponder responder, TRequest request)
    {
        Publish(responder, request, (responder as MonoBehaviour).gameObject);
    }

    public static void Publish(IResponder responder, TRequest request, GameObject go)
    {
        Publish(responder.Respond(request), request, go);
    }

    public static void Publish(TResponse response, TRequest request, GameObject go)
    {
        Publish<IRequestor>(go, requestor => requestor.Handle(request, response));
    }

    public static void Request(IRequestor requestor)
    {
        var request = requestor.FormRequest();
        Request<IResponder>((requestor as MonoBehaviour).gameObject, responder => requestor.Handle(request, responder.Respond(request)));
    }

    public static TResponse Get(IRequestor requestor)
    {
        return Get(requestor.FormRequest(), (requestor as MonoBehaviour).gameObject);
    }

    public static TResponse Get(TRequest request, GameObject go)
    {
        TResponse ret = default;
        Request<IResponder>(go, responder =>
        {
            ret = responder.Respond(request);
        });
        return ret;
    }
}
