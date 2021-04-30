using System;
using Godot;
using MachineLearning;

public class GameInstance : Control
{
    [Export] public int width = 16;
    [Export] public int height = 32;
    [Export] public float squareSize = 4f;
    [Export] public float fallDelay = .5f;

    public float score { get; set; }
    public float fitness { get; set; }
    public ReferenceRect gameBorder { get; private set; }
    public Inputer inputer { get; private set; }
    public AI ai { get; set; }

    int nxtColor = 0;
    int nxt = 0;
    float currentFallDelay = 0f;
    float timeHoldingPiece = 0;
    bool gameOver;
    bool draw;
    bool tetrominoDraw;
    Random rng;

    public bool manualUpdate { get; set; }
    public int[,] grid { get; private set; }
    public Tetronimo currentTetronimo { get; private set; }

    [Signal] delegate void GameOver();

    public override void _Ready()
    {
        gameBorder = GetNode<ReferenceRect>("GameBorder");
        inputer = GetNode<Inputer>("Inputer");

        Reset();
        SpawnTetronimo();
    }

    public override void _PhysicsProcess(float delta)
    {
        gameBorder.RectPosition = -Vector2.One * gameBorder.BorderWidth * .5f;
        gameBorder.RectSize = new Vector2(width, height) * squareSize + Vector2.One * gameBorder.BorderWidth * .5f;
        RectMinSize = new Vector2(width, height) * squareSize;

        if (!manualUpdate) Tick(delta);
    }

    public override void _Process(float delta)
    {
        if (draw)
        {
            Update();
        }
        if (tetrominoDraw && currentTetronimo != null)
        {
            currentTetronimo.Update();
        }
        draw = false;
        tetrominoDraw = false;
    }

    public void AddAI(AI ai)
    {
        AddChild(ai);
        this.ai = ai;
    }

    public void Tick(float delta)
    {
        if (!gameOver)
        {
            if (ai != null)
            {
                ai.Tick(delta);
            }
            ControlCurrent(delta);
        }
        else if (inputer.reset)
        {
            Reset();
        }
    }

    public void Reset()
    {
        rng = new Random();
        this.grid = new int[height, width];
        nxt = rng.Next(7);
        nxtColor = rng.Next(1, 7);
        currentFallDelay = 0f;
        gameOver = false;
        score = 0;
        draw = true;
        tetrominoDraw = true;
    }

    private void ControlCurrent(float delta)
    {
        currentFallDelay -= delta;
        timeHoldingPiece += delta;

        if (inputer.rotate)
        {
            currentTetronimo.Rotate();
        }
        if (inputer.fall)
        {
            currentFallDelay = 0f;
        }
        if (inputer.moveLeft)
        {
            currentTetronimo.Move(-1, 0);
            tetrominoDraw = true;
        }
        if (inputer.moveRight)
        {
            currentTetronimo.Move(1, 0);
            tetrominoDraw = true;
        }
        if (currentFallDelay <= 0f)
        {
            currentFallDelay = fallDelay;
            bool move = currentTetronimo.Move(0, 1);
            tetrominoDraw = true;
            if (!move)
            {
                MergeCurrentTetronimo();
                ScanLines();

                timeHoldingPiece = 0f;

                currentTetronimo.QueueFree();
                currentTetronimo = null;

                SpawnTetronimo();

                if (!currentTetronimo.Move(0, 0))
                {
                    OnGameOver();
                }

                draw = true;
                tetrominoDraw = true;
            }
        }

        timeHoldingPiece += delta;
        if (timeHoldingPiece >= 10f)
        {
            currentTetronimo.QueueFree();
            currentTetronimo = null;

            SpawnTetronimo();

            tetrominoDraw = true;

            timeHoldingPiece = 0f;

            /*currentTetronimo.QueueFree();
            currentTetronimo = null;
            gameOver = true;
            EmitSignal("GameOver");
            Modulate *= new Color(.5f, .5f, .5f);*/
        }
    }

    private void MergeCurrentTetronimo()
    {
        for (int y = 0; y < currentTetronimo.sizeY; y++)
        {
            for (int x = 0; x < currentTetronimo.sizeX; x++)
            {
                int idxX = x + currentTetronimo.posX;
                int idxY = y + currentTetronimo.posY;

                if (currentTetronimo.grid[y, x] == 1)
                {
                    this.grid[idxY, idxX] = currentTetronimo.colorIdx;
                }
            }
        }
    }

    private void ScanLines()
    {
        int completedLines = 0;
        for (int y = height - 1; y >= 0; y--)
        {
            bool completedLine = true;
            for (int x = 0; x < width; x++)
            {
                if (grid[y, x] == 0)
                {
                    completedLine = false;
                    break;
                }
            }
            if (completedLine)
            {
                for (int y2 = y; y2 > 0; y2--)
                {
                    for (int x = 0; x < width; x++)
                    {
                        grid[y2, x] = grid[y2 - 1, x];
                    }
                }
                completedLines++;
                y++;
            }
        }
        if (completedLines > 0)
        {
            float addPoints = Mathf.Pow(2, completedLines - 1) * 1000;
            score += addPoints;
        }
    }

    private Tetronimo SpawnTetronimo()
    {
        Tetronimo t = GenerateTetronimo();
        AddChild(t);
        currentTetronimo = t;
        t.Move(2, 2);
        gameBorder.Raise();

        return t;
    }

    private Tetronimo GenerateTetronimo()
    {
        int curr = nxt;
        int currColor = nxtColor;

        while (curr == nxt) nxt = rng.Next(7);
        while (currColor == nxtColor) nxtColor = rng.Next(1, 7);

        return new Tetronimo(this, (Tetronimo.TetronimoType)curr, currColor);
    }

    private void OnGameOver() {
        if (gameOver) return;
        currentTetronimo.QueueFree();
        currentTetronimo = null;
        gameOver = true;
        CalculateScore();
        EmitSignal("GameOver");
        Modulate *= new Color(.5f, .5f, .5f);
    }

    private void CalculateScore() {
        for (int y = 0; y < height; y++) {
            int holes = 0;
            for (int x = 0; x < width; x++) {
                if (grid[y, x] == 0) {
                    holes++;
                }
            }
            float d1 = 1f - (float)holes / (width - 1);
            d1 = Mathf.Pow(d1, 1f);
            float d2 = (float)y / (height - 1);
            d2 = Mathf.Pow(d2, 1f);

            score += d1 * d2 * 50f;
        }
    }

    public override void _Draw()
    {
        /*Transform2D t = GetTransform();
        t.origin = currentPosition;
        VisualServer.CanvasItemSetTransform(GetCanvasItem(), t);*/

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[y, x] != 0)
                {
                    DrawRect(
                        new Rect2(
                            x * squareSize, y * squareSize,
                            squareSize, squareSize
                        ),
                        TetrominosTable.GetColor(grid[y, x])
                    );
                }
            }
        }
    }
}
