using UnityEngine;
public enum EmenyType
{
    Small,Medium,Large
}
[CreateAssetMenu]

public class EnemyFactory : GameObjectFactory
{
    [System.Serializable]
    class EnemyConfig
    {
        public Enemy prefab = default;
    
        [FloatRangeSlider(0.5f, 2f)] 
        public FloatRange scale = new FloatRange(1f);

        [FloatRangeSlider(0.2f, 5f)] 
        public FloatRange speed = new FloatRange(1f);

        [FloatRangeSlider(-0.4f, 0.4f)] 
        public FloatRange pathOffset = new FloatRange(0f);

        [FloatRangeSlider(10f, 1000f)] 
        public FloatRange health = new FloatRange(100f);
    }
    [SerializeField] 
    EnemyConfig small = default, medium = default, large = default;

    EnemyConfig GetConfig(EmenyType type)
    {
        switch (type)
        {
            case EmenyType.Small: return small;
            case EmenyType.Medium: return medium;
            case EmenyType.Large: return large;
        }
        Debug.Assert(false,"Unsupported enemy type!");
        return null;
    } 
    public Enemy Get(EmenyType type=EmenyType.Medium)
    {
        EnemyConfig config = GetConfig(type);
        Enemy instance = CreateGameObjectInstance(config.prefab);
        instance.OriginFactory = this;
        instance.Initialize(
            config.scale.RandomValueInRange,
            config.speed.RandomValueInRange,
            config.pathOffset.RandomValueInRange,
            config.health.RandomValueInRange
        );
        return instance;
    }
    public void Reclaim(Enemy enemy)
    {
        Debug.Assert(enemy.OriginFactory==this,"Wrong factory reclaimed!");
        Destroy(enemy.gameObject);
    }
}
