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

class GameManager
{
    private int width, height, score1, score2;
    private char up1 = 'w', down1 = 's', up2 = 'i', down2 = 'k';
    private bool quit;
    private Ball ball;
    private Paddle player1, player2;

    public GameManager(int w, int h)
    {
        width = w;
        height = h;
        score1 = score2 = 0;
        quit = false;
        ball = new Ball(w / 2, h / 2);
        player1 = new Paddle(1, h / 2 - 3);
        player2 = new Paddle(w - 2, h / 2 - 3);
    }
    
    public void Draw()
    {
        Console.Clear();
        for (int i = 0; i < width + 2; i++) Console.Write("#");
        Console.WriteLine();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (j == 0 || j == width - 1)
                    Console.Write("#");
                else if (ball.X == j && ball.Y == i)
                    Console.Write("O");
                else if (player1.X == j && (i >= player1.Y && i < player1.Y + 4))
                    Console.Write("|");
                else if (player2.X == j && (i >= player2.Y && i < player2.Y + 4))
                    Console.Write("|");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }

        for (int i = 0; i < width + 2; i++) Console.Write("#");
        Console.WriteLine();
        Console.WriteLine($"Score 1: {score1} | Score 2: {score2}");
    }
    
    public void Input()
    {
        if (!Console.KeyAvailable) return;
        ConsoleKeyInfo key = Console.ReadKey(true);
        
        if (key.KeyChar == up1 && player1.Y > 0) player1.MoveUp();
        if (key.KeyChar == down1 && player1.Y < height - 4) player1.MoveDown();
        if (key.KeyChar == up2 && player2.Y > 0) player2.MoveUp();
        if (key.KeyChar == down2 && player2.Y < height - 4) player2.MoveDown();
        if (ball.Dir == Direction.STOP) ball.RandomDirection();
        if (key.KeyChar == 'q') quit = true;
    }
    
    public void Logic()
    {
        ball.Move();
        if (ball.Y <= 0 || ball.Y >= height - 1)
            ball.ChangeDirection(ball.Dir == Direction.UPLEFT || ball.Dir == Direction.UPRIGHT ? Direction.DOWNLEFT : Direction.UPLEFT);
        if (ball.X == 0) { score2++; ball.Reset(); }
        if (ball.X == width - 1) { score1++; ball.Reset(); }
    }
    
    public void Run()
    {
        while (!quit)
        {
            Draw();
            Input();
            Logic();
            Thread.Sleep(50);
        }
    }
}

class Paddle
{
    public int X { get; private set; }
    public int Y { get; private set; }
    private int originalX, originalY;

    public Paddle(int posX, int posY)
    {
        originalX = posX;
        originalY = posY;
        X = posX;
        Y = posY;
    }
    public void Reset() { X = originalX; Y = originalY; }
    public void MoveUp() { Y--; }
    public void MoveDown() { Y++; }
}

class Program
{
    static void Main()
    {
        GameManager game = new GameManager(50, 25);
        game.Run();
    }
}