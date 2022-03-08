
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
    Road,
    Water,
    Rail,
    Grass,
    DarkGrass,
}