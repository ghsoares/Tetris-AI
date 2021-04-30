using System;
using Godot;
using MachineLearning;

public class AI : Node
{
    public Sensors sensors { get; private set; }
    public Inputer inputer { get; private set; }
    public NeuralNetwork brain { get; set; }

    public AI()
    {
        brain = new NeuralNetwork(13, 14, 4);
    }

    public override void _Ready()
    {
        sensors = GetNode<Sensors>("../Sensors");
        inputer = GetNode<Inputer>("../Inputer");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            brain.Randomize();
        }
    }

    public void Tick(float delta)
    {
        sensors.UpdateSensors();

        Matrix inputs = new Matrix(sensors.sensorGameStateSize + 3, 1);

        int i = 0;
        for (i = 0; i < sensors.sensorGameStateSize; i++) {
            inputs[i,0] = sensors.sensorGameState[i];
        }

        inputs[i+0,0] = sensors.sensorPosX;
        inputs[i+1,0] = sensors.sensorPosY;
        inputs[i+2,0] = sensors.sensorTetrominoTypeRotation;

        Matrix outputs = brain.Predict(inputs);

        inputer.moveLeft = outputs[0, 0] >= .5f;
        inputer.moveRight = outputs[1, 0] >= .5f;
        inputer.rotate = outputs[2, 0] >= .5f;
        inputer.fall = outputs[3, 0] >= .5f;
    }
}