using Mirror;
using UnityEngine.Events;

public class SendEventIfNetworkObjIsMine : NetworkBehaviour
{
    public UnityEvent IsMine;
    public UnityEvent IsNotMine;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            IsMine?.Invoke();
        }
        else
        {
            IsNotMine?.Invoke();
        }
    }

}
