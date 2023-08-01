using UnityEngine;

[CreateAssetMenu]
public class WarFactory : GameObjectFactory
{
    [SerializeField] 
    Explosion explonsionPrefab = default;
    
    [SerializeField] 
    Shell shellPrefab = default;

    public Explosion Explosion => Get(explonsionPrefab);
    public Shell Shell => Get(shellPrefab);

    T Get<T>(T prefab) where T : WarEntity
    {
        T instance = CreateGameObjectInstance(prefab);
        instance.OriginFactory = this;
        return instance;
    }

    public void Reclaim(WarEntity entity)
    {
        Debug.Assert(entity.OriginFactory==this,"Wrong factory reclaimed!");
        Destroy(entity.gameObject);
    }
    
}
