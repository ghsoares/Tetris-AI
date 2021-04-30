using System.Collections.Generic;
using Godot;

public static class TetrominosTable {
    public static List<int[,]> tetrominoGrids {get; private set;}
    public static List<int[]> tetrominoBounds {get; private set;}

    static TetrominosTable() {
        InitGrids();
        InitBounds();
    }

    private static void InitGrids() {
        tetrominoGrids = new List<int[,]>();
        // I
        tetrominoGrids.Add(new int[,] {
            {0, 0, 0, 0},
            {1, 1, 1, 1},
            {0, 0, 0, 0},
            {0, 0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 0, 1, 0},
            {0, 0, 1, 0},
            {0, 0, 1, 0},
            {0, 0, 1, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 0, 0, 0},
            {0, 0, 0, 0},
            {1, 1, 1, 1},
            {0, 0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 0, 0},
            {0, 1, 0, 0},
            {0, 1, 0, 0},
            {0, 1, 0, 0},
        });
        // J
        tetrominoGrids.Add(new int[,] {
            {1, 0, 0},
            {1, 1, 1},
            {0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 1},
            {0, 1, 0},
            {0, 1, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 0, 0},
            {1, 1, 1},
            {0, 0, 1},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 0},
            {0, 1, 0},
            {1, 1, 0},
        });
        // L
        tetrominoGrids.Add(new int[,] {
            {0, 0, 1},
            {1, 1, 1},
            {0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 0},
            {0, 1, 0},
            {0, 1, 1},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 0, 0},
            {1, 1, 1},
            {1, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {1, 1, 0},
            {0, 1, 0},
            {0, 1, 0},
        });
        // O
        tetrominoGrids.Add(new int[,] {
            {0, 1, 1, 0},
            {0, 1, 1, 0},
            {0, 0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 1, 0},
            {0, 1, 1, 0},
            {0, 0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 1, 0},
            {0, 1, 1, 0},
            {0, 0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 1, 0},
            {0, 1, 1, 0},
            {0, 0, 0, 0},
        });
        // S
        tetrominoGrids.Add(new int[,] {
            {0, 1, 1},
            {1, 1, 0},
            {0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 0},
            {0, 1, 1},
            {0, 0, 1},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 0, 0},
            {0, 1, 1},
            {1, 1, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {1, 0, 0},
            {1, 1, 0},
            {0, 1, 0},
        });
        // S
        tetrominoGrids.Add(new int[,] {
            {1, 1, 0},
            {0, 1, 1},
            {0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 0, 1},
            {0, 1, 1},
            {0, 1, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 0, 0},
            {1, 1, 0},
            {0, 1, 1},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 0},
            {1, 1, 0},
            {1, 0, 0},
        });
        // T
        tetrominoGrids.Add(new int[,] {
            {0, 1, 0},
            {1, 1, 1},
            {0, 0, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 0},
            {0, 1, 1},
            {0, 1, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 0, 0},
            {1, 1, 1},
            {0, 1, 0},
        });
        tetrominoGrids.Add(new int[,] {
            {0, 1, 0},
            {1, 1, 0},
            {0, 1, 0},
        });
    }

    private static void InitBounds() {
        tetrominoBounds = new List<int[]>();
        // I
        tetrominoBounds.Add(new int[] {
            0, 1,
            3, 1
        });
        tetrominoBounds.Add(new int[] {
            2, 0,
            2, 3
        });
        tetrominoBounds.Add(new int[] {
            0, 2,
            3, 2
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            1, 3
        });
        // J
        tetrominoBounds.Add(new int[] {
            0, 0,
            2, 1
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 1,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 0,
            1, 2
        });
        // L
        tetrominoBounds.Add(new int[] {
            0, 0,
            2, 1
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 1,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 0,
            1, 2
        });
        // O
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 1
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 1
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 1
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 1
        });
        // S
        tetrominoBounds.Add(new int[] {
            0, 0,
            2, 1
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 1,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 0,
            1, 2
        });
        // Z
        tetrominoBounds.Add(new int[] {
            0, 0,
            2, 1
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 1,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 0,
            1, 2
        });
        // T
        tetrominoBounds.Add(new int[] {
            0, 0,
            2, 1
        });
        tetrominoBounds.Add(new int[] {
            1, 0,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 1,
            2, 2
        });
        tetrominoBounds.Add(new int[] {
            0, 0,
            1, 2
        });
    }

    public static void GetTetromino(int type, int rotation, out int[,] grid, out int sizeX, out int sizeY, out int[] bounds) {
        int idx = type * 4 + rotation;
        grid = tetrominoGrids[idx];
        bounds = tetrominoBounds[idx];

        sizeX = grid.GetLength(1);
        sizeY = grid.GetLength(0);
    }

    public static void GetWallKickOffset(int type, int rotation, int test, out int offX, out int offY) {
        offX = 0;
        offY = 0;
        if (type == 0) {
            switch (rotation) {
                case 0: {
                    switch (test) {
                        case 1: {
                            offX = -2;
                            offY = 0;
                            break;
                        }
                        case 2: {
                            offX = 1;
                            offY = 0;
                            break;
                        }
                        case 3: {
                            offX = -2;
                            offY = 1;
                            break;
                        }
                        case 4: {
                            offX = 1;
                            offY = -2;
                            break;
                        }
                    }
                    break;
                }
                case 1: {
                    switch (test) {
                        case 1: {
                            offX = -1;
                            offY = 0;
                            break;
                        }
                        case 2: {
                            offX = 2;
                            offY = 0;
                            break;
                        }
                        case 3: {
                            offX = -1;
                            offY = -2;
                            break;
                        }
                        case 4: {
                            offX = 2;
                            offY = 1;
                            break;
                        }
                    }
                    break;
                }
                case 2: {
                    switch (test) {
                        case 1: {
                            offX = 2;
                            offY = 0;
                            break;
                        }
                        case 2: {
                            offX = -1;
                            offY = 0;
                            break;
                        }
                        case 3: {
                            offX = 2;
                            offY = -1;
                            break;
                        }
                        case 4: {
                            offX = -1;
                            offY = 2;
                            break;
                        }
                    }
                    break;
                }
                case 3: {
                    switch (test) {
                        case 1: {
                            offX = 1;
                            offY = 0;
                            break;
                        }
                        case 2: {
                            offX = -2;
                            offY = 0;
                            break;
                        }
                        case 3: {
                            offX = 1;
                            offY = 2;
                            break;
                        }
                        case 4: {
                            offX = -2;
                            offY = -1;
                            break;
                        }
                    }
                    break;
                }
            }
        } else if (type == 3) {
            offX = 0;
            offY = 0;
        } else {
            switch (rotation) {
                case 0: {
                    switch (test) {
                        case 1: {
                            offX = -1;
                            offY = 0;
                            break;
                        }
                        case 2: {
                            offX = -1;
                            offY = -1;
                            break;
                        }
                        case 3: {
                            offX = 0;
                            offY = 2;
                            break;
                        }
                        case 4: {
                            offX = -1;
                            offY = 2;
                            break;
                        }
                    }
                    break;
                }
                case 1: {
                    switch (test) {
                        case 1: {
                            offX = 1;
                            offY = 0;
                            break;
                        }
                        case 2: {
                            offX = 1;
                            offY = 1;
                            break;
                        }
                        case 3: {
                            offX = 0;
                            offY = -2;
                            break;
                        }
                        case 4: {
                            offX = 1;
                            offY = -2;
                            break;
                        }
                    }
                    break;
                }
                case 2: {
                    switch (test) {
                        case 1: {
                            offX = 1;
                            offY = 0;
                            break;
                        }
                        case 2: {
                            offX = 1;
                            offY = -1;
                            break;
                        }
                        case 3: {
                            offX = 0;
                            offY = 2;
                            break;
                        }
                        case 4: {
                            offX = 1;
                            offY = 2;
                            break;
                        }
                    }
                    break;
                }
                case 3: {
                    switch (test) {
                        case 1: {
                            offX = -1;
                            offY = 0;
                            break;
                        }
                        case 2: {
                            offX = -1;
                            offY = 1;
                            break;
                        }
                        case 3: {
                            offX = 0;
                            offY = -2;
                            break;
                        }
                        case 4: {
                            offX = -1;
                            offY = -2;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    public static Color GetColor(int colorIdx) {
        switch (colorIdx) {
            case 1: return new Color(0, 1, 0.788235f);
            case 2: return new Color(0, 0.321569f, 1);
            case 3: return new Color(1, 0.447059f, 0);
            case 4: return new Color(0.968627f, 0.956863f, 0.145098f);
            case 5: return new Color(0.317647f, 1, 0.278431f);
            case 6: return new Color(0.795837f, 0.34668f, 1);
            case 7: return new Color(1, 0.278431f, 0.431373f);
        }
        return Colors.White;
    }
}