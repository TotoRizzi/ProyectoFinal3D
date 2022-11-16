using UnityEngine;
public class FRY_RavensUISignal : MonoBehaviour
{
    public static FRY_RavensUISignal Instance
    {
        get
        {
            return _instance;
        }
    }
    static FRY_RavensUISignal _instance;


    public RavensUISignal deadRavenParticlePrefab;
    public int particleStock = 2;

    public ObjectPool<RavensUISignal> pool;
    [SerializeField] Transform _parent;

    void Start()
    {
        _instance = this;
        pool = new ObjectPool<RavensUISignal>(ObjectCreator, RavensUISignal.TurnOn, RavensUISignal.TurnOff, particleStock);
    }

    public RavensUISignal ObjectCreator()
    {
        var newRavenUI = Instantiate(deadRavenParticlePrefab);
        newRavenUI.transform.SetParent(_parent, false);
        return newRavenUI;
    }

    public void ReturnObject(RavensUISignal b)
    {
        pool.ReturnObject(b);
    }
}
