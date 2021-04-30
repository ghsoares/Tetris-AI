using System;
using Godot;

namespace MachineLearning
{
    public class Sensors : Node2D
    {
        public GameInstance gameInstance { get; private set; }
        public int sensorGameStateSize { get; private set; }

        public float[] sensorGameState { get; private set; }
        public float sensorPosX { get; private set; }
        public float sensorPosY { get; private set; }
        public float sensorTetrominoTypeRotation { get; private set; }

        [Export] public bool debug = false;

        int frames = 0;

        public override void _Ready()
        {
            gameInstance = GetParent<GameInstance>();
            sensorGameState = new float[gameInstance.width];
            sensorGameStateSize = gameInstance.width;
        }

        public override void _PhysicsProcess(float delta)
        {
            if (frames % 4 == 0) UpdateSensors();
            frames++;
        }

        public void UpdateSensors()
        {
            for (int x = 0; x < gameInstance.width; x++)
            {
                sensorGameState[x] = 2f;
            }
            sensorPosX = 2f;
            sensorPosY = 2f;
            sensorTetrominoTypeRotation = 2f;
            Tetronimo t = gameInstance.currentTetronimo;
            if (t == null) return;
            for (int x = 0; x < gameInstance.width; x++)
            {
                float columnState = 0;
                float maxState = 0;
                for (int y = 0; y < gameInstance.height; y++)
                {
                    if (gameInstance.grid[y, x] != 0)
                    {
                        columnState += Mathf.Pow(2, y);
                    }
                    maxState += Mathf.Pow(2, y);
                }
                float d = columnState / maxState;
                sensorGameState[x] = d * 2f;
            }

            sensorPosX = t.posX / (float)gameInstance.width;
            sensorPosY = t.posY / (float)gameInstance.height;
            float rotationType = (int)t.type * 4f + t.rotation;
            sensorTetrominoTypeRotation = rotationType / (7f * 4f);
            /*sensorTetrominoType = (int)t.type / 7f;
            sensorTetrominoRotation = t.rotation / 4f;*/

            if (debug) Update();
        }

        public override void _Draw()
        {
            if (!debug) return;
            Tetronimo t = gameInstance.currentTetronimo;
        }
    }
}