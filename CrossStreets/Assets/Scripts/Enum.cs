
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

}

public enum ObstacleType
{

    Tree = 5,
    ShortTree = 6,
    Dragon1 = 7,
    Dragon2 = 8,

}
