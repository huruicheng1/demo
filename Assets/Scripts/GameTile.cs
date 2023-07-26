using UnityEngine;
using UnityEngine.UI;

public class GameTile : MonoBehaviour
{
    [SerializeField] 
    Transform arrow = default;
    GameTile north, east, south, west,nextOnPath;
    int distance;
    public bool HasPath => distance != int.MaxValue;
    public Vector3 ExitPoint { get; private set; }
    public bool IsAlternative { get; set; }
    public GameTile GrowPathNorth() => GrowPathTo(north,Direction.South);
    public GameTile GrowPathEast() => GrowPathTo(east,Direction.West);
    public GameTile GrowPathSouth() => GrowPathTo(south,Direction.North);
    public GameTile GrowPathWest() => GrowPathTo(west,Direction.East);
   
    static Quaternion
        northRotation=Quaternion.Euler(90f,0f,0f),
        eastRotation=Quaternion.Euler(90f,90f,0f),
        southRotation=Quaternion.Euler(90f,180f,0f),
        westRotation=Quaternion.Euler(90f, 270f, 0f);

    GameTileContent contect;
    
    public Direction PathDirection { get; private set; }

    public GameTile NextTileOnPath => nextOnPath;
    public enum DirectionChange
    {
        None,TurnRight,TurnLeft,TurnAround
    }

    public GameTileContent Content
    {
        get => contect;
        set
        {
            Debug.Assert(value!=null,"Null assigned to content!");
            if (contect != null)
            {
                contect.Recycle();
            }

            contect = value;
            contect.transform.localPosition = transform.localPosition;
        }
    }
    
    public void HidePath()
    {
        arrow.gameObject.SetActive(false);
    }
    public void ShowPath()
    {
        if (distance == 0)
        {
            arrow.gameObject.SetActive(false);
            return;
        }

        arrow.gameObject.SetActive(true);
        arrow.localRotation =
            nextOnPath == north ? northRotation :
            nextOnPath == east ? eastRotation :
            nextOnPath == south ? southRotation :
            westRotation;
    }
    public static void MakeEastWestNeighbors(GameTile east, GameTile west)
    {
        Debug.Assert(
            west.east==null&&east.west==null,"Redefined neighbors!"
        );
        west.east = east;
        east.west = west;
    }

    public static void MakeNorthSouthNeighbors(GameTile north, GameTile south)
    {
        Debug.Assert(
            south.north==null&&north.south==null,"Redefined neighbors!"
        );
        south.north = north;
        north.south = south;
    }

    public void ClearPath()
    {
        distance = int.MaxValue;
        nextOnPath = null;
    }

    public void BecomeDestination()
    {
        distance = 0;
        nextOnPath = null;
        ExitPoint = transform.localPosition;
    }

    GameTile GrowPathTo(GameTile neighbor,Direction direction)
    {
        Debug.Assert(HasPath,"No path");
        if (!HasPath || neighbor == null || neighbor.HasPath)
        {
            return null;
        }
        neighbor.distance = distance + 1;
        neighbor.nextOnPath = this;
        neighbor.ExitPoint = 
            neighbor.transform.localPosition + direction.GetHalfVector();
        neighbor.PathDirection = direction;
        return
            neighbor.Content.Type != GameTileContentType.Wall ? neighbor : null;
    }
}
