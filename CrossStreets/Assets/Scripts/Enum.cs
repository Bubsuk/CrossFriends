
public class LayerName
{
    public const int TILE_LINE = 3;
    public const int OBSTACLE = 6;
    public const int MOVE_OBSTACLE = 7;
    public const int FLOATING_LOG = 8;
}

enum PlayerState
{
    Idle,
    Die,
    ReadyJump,
}

enum PlayerDir
{
    Forward = 0,
    Back = 180,
    Right = 90,
    Left = -90,
}

public enum TileType
{
    Grass = 0,
    Road = 1,
    Water = 2,
    Rail = 3,
    DarkGrass = 4,
    RoadLine = 5,

}

public enum ObstacleType
{

    ShortTree = 6,
    Tree = 7,

    Dragon1 = 8,
    Dragon2 = 9,
    FloatingLog = 10,
    Train = 11,

}
