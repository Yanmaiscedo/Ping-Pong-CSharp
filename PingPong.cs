using System;
using System.Threading;

enum Direction
{
    STOP = 0, LEFT, UPLEFT, DOWNLEFT, RIGHT, UPRIGHT, DOWNRIGHT
}

class Ball
{
    public int X { get; private set; }
    public int Y { get; private set; }
    private int originalX, originalY;
    public Direction Dir { get; private set; }

    public Ball(int posX, int posY)
    {
        originalX = posX;
        originalY = posY;
        X = posX;
        Y = posY;
        Dir = Direction.STOP;
    }
    public void Reset() { X = originalX; Y = originalY; Dir = Direction.STOP; }
    public void ChangeDirection(Direction d) { Dir = d; }
    public void RandomDirection() { Dir = (Direction)(new Random().Next(1, 7)); }
    public void Move()
    {
        switch (Dir)
        {
            case Direction.LEFT: X--; break;
            case Direction.RIGHT: X++; break;
            case Direction.UPLEFT: X--; Y--; break;
            case Direction.DOWNLEFT: X--; Y++; break;
            case Direction.UPRIGHT: X++; Y--; break;
            case Direction.DOWNRIGHT: X++; Y++; break;
        }
    }
}

class Program
{

}