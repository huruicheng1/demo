using UnityEngine;
public enum GameTileContentType
{
    Empty,Destination,Wall,SpawnPoint,Tower
}
[SelectionBase]
public class GameTileContent : MonoBehaviour
{
    public bool BlocksPath =>
        Type == GameTileContentType.Wall || Type == GameTileContentType.Tower;
    [SerializeField] 
    GameTileContentType type = default;
    public GameTileContentType Type => type;
    
    GameTileContentFactory originFactory;

    public virtual void GameUpdate()
    {
        
    }
    
    public GameTileContentFactory OriginFactory
    {
        get => originFactory;
        set
        {
            Debug.Assert(originFactory==null,"Redefined origin factory!");
            originFactory = value;
        }
    }

    public void Recycle()
    {
        originFactory.Reclaim(this);
    }
    
}
