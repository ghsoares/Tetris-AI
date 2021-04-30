using System;
using System.Collections.Generic;
using Godot;
public class Tetronimo : Node2D
{
    public enum TetronimoType
    {
        I,
        J,
        L,
        O,
        S,
        Z,
        T
    }

    public GameInstance gameInstance { get; private set; }
    public TetronimoType type { get; private set; }
    public int[,] grid;
    public int sizeX;
    public int sizeY;
    public int boundsMinX;
    public int boundsMinY;
    public int boundsMaxX;
    public int boundsMaxY;
    public int rotation;
    public int posX;
    public int posY;
    public int colorIdx;

    public Tetronimo(GameInstance gameInstance, TetronimoType type, int colorIdx)
    {
        this.gameInstance = gameInstance;
        this.type = type;
        this.colorIdx = colorIdx;

        UpdateGrid();
    }

    public bool TestPosition(
        int posX, int posY, int[,] grid, int sizeX, int sizeY, int boundsMinX, int boundsMinY,
        int boundsMaxX, int boundsMaxY
    ) {
        if (posX + boundsMinX < 0 || posX + boundsMaxX >= gameInstance.width) {
            return false;
        }
        if (posY + boundsMinY < 0 || posY + boundsMaxY >= gameInstance.height) {
            return false;
        }

        for (int y = 0; y < sizeY; y++) {
            for (int x = 0; x < sizeX; x++) {
                int idxX = x + posX;
                int idxY = y + posY;

                if (grid[y,x] == 1) {
                    if (gameInstance.grid[idxY, idxX] != 0) return false;
                }
            }
        }

        return true;
    }

    public bool Move(int offX, int offY)
    {
        int nxtPosX = posX + offX;
        int nxtPosY = posY + offY;

        if (!TestPosition(
            nxtPosX, nxtPosY, grid, sizeX, sizeY, boundsMinX, boundsMinY, boundsMaxX, boundsMaxY
        )) return false;

        this.posX = nxtPosX;
        this.posY = nxtPosY;

        return true;
    }

    public bool Rotate()
    {
        int rot = rotation;
        int[,] grid = null;
        int sizeX = 0;
        int sizeY = 0;
        int[] bounds = null;
        int posX = this.posX;
        int posY = this.posY;
        bool success = false;

        rot++;
        rot %= 4;
        TetrominosTable.GetTetromino(
            (int)type, rot, out grid, out sizeX, out sizeY, out bounds
        );

        for (int i = 0; i < 5; i++) {
            TetrominosTable.GetWallKickOffset(
                (int)type, rot, i, out int offX, out int offY
            );
            posX = this.posX + offX;
            posY = this.posY + offY;
            if (TestPosition(
                posX, posY, grid, sizeX, sizeY, bounds[0], bounds[1], bounds[2], bounds[3]
            )) {
                success = true;
                break;
            }
        }
        if (!success) return false;

        this.posX = posX;
        this.posY = posY;
        this.rotation = rot;
        this.grid = grid;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.boundsMinX = bounds[0];
        this.boundsMinY = bounds[1];
        this.boundsMaxX = bounds[2];
        this.boundsMaxY = bounds[3];

        return true;
    }

    public void UpdateGrid()
    {
        TetrominosTable.GetTetromino(
            (int)type, rotation, out grid, out sizeX, out sizeY, out int[] bounds
        );

        this.boundsMinX = bounds[0];
        this.boundsMinY = bounds[1];
        this.boundsMaxX = bounds[2];
        this.boundsMaxY = bounds[3];
    }

    public override void _Draw()
    {
        float squareSize = gameInstance.squareSize;
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                if (grid[y, x] == 1)
                {
                    DrawRect(
                        new Rect2(
                            (x + posX) * squareSize, (y + posY) * squareSize,
                            squareSize, squareSize
                        ),
                        TetrominosTable.GetColor(colorIdx)
                    );
                }
            }
        }
        int boundsSizeX = (boundsMaxX - boundsMinX) + 1;
        int boundsSizeY = (boundsMaxY - boundsMinY) + 1;

        /*DrawRect(new Rect2(
            (posX + boundsMinX) * squareSize, (posY + boundsMinY) * squareSize,
            boundsSizeX * squareSize, boundsSizeY * squareSize
            ),
            new Color(.25f, 1f, .25f, .5f)
        );*/
    }
}
