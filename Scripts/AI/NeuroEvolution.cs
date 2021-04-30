using System;
using System.Collections.Generic;
using Godot;
using MachineLearning;

public class NeuroEvolution : Control
{
    public Random rng { get; private set; }

    public GameInstance[] population { get; private set; }
    public PackedScene instanceScene { get; private set; }
    public GridContainer grid { get; private set; }
    public Range iterationsSlider { get; private set; }
    public Label iterationsLabel { get; private set; }
    public Label generationsLabel { get; private set; }
    public Graph generationsGraph { get; private set; }
    public int runningInstances { get; private set; }
    public int currentGeneration { get; private set; }
    public List<float> generationHistory { get; private set; }

    [Export] public int populationSize = 50;
    [Export] public int iterations = 50;
    [Export] public float maxWaitTime = 1f;

    public NeuroEvolution() {
        generationHistory = new List<float>();
    }

    public override void _Ready()
    {
        rng = new Random();

        instanceScene = GD.Load<PackedScene>("res://Scenes/GameInstance.tscn");
        grid = GetNode<GridContainer>("Scroll/VBox/Grid");
        iterationsSlider = GetNode<Range>("Panel/IterationsSlider");
        iterationsLabel = GetNode<Label>("Panel/Iterations");
        generationsLabel = GetNode<Label>("Panel/Generations");
        generationsGraph = GetNode<Graph>("Panel/GenerationsGraph");

        iterationsSlider.Connect("value_changed", this, "OnIterationsChanged");
        OnIterationsChanged(iterations);

        currentGeneration = 1;

        //ClearPopulation();
        CreatePopulation();
    }

    public override void _PhysicsProcess(float delta)
    {
        uint start = OS.GetTicksMsec();
        for (int i = 0; i < iterations; i++)
        {
            UpdatePopulation(delta);

            uint current = OS.GetTicksMsec();
            float elapsed = (current - start) / 1000f;
            if (elapsed >= maxWaitTime) break;
        }
    }

    private void UpdatePopulation(float delta)
    {
        for (int i = 0; i < populationSize; i++)
        {
            population[i].Tick(delta);
        }
    }

    private void ClearPopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            population[i].QueueFree();
        }
    }

    private void CreatePopulation(NeuralNetwork[] baseNNs = null)
    {
        population = new GameInstance[populationSize];

        float maxMutationRate = .1f;
        int pickedNNIdx = 0;

        for (int i = 0; i < populationSize; i++)
        {
            GameInstance instance = instanceScene.Instance<GameInstance>();
            AI ai = new AI();
            if (baseNNs != null)
            {
                NeuralNetwork baseNN = baseNNs[pickedNNIdx];
                pickedNNIdx++;
                pickedNNIdx %= baseNNs.Length;

                float mutationWeight = (float)i / (populationSize - 1);
                mutationWeight = Mathf.Pow(mutationWeight, 1f);
                ai.brain = new NeuralNetwork(baseNN);
                ai.brain.MutateBias(
                    (b) => b + ((float)rng.NextDouble() * 2f - 1f) * maxMutationRate * mutationWeight
                );
                ai.brain.MutateWeights(
                    (b) => b + ((float)rng.NextDouble() * 2f - 1f) * maxMutationRate * mutationWeight
                );
            }

            instance.AddAI(ai);
            instance.manualUpdate = true;
            population[i] = instance;
            grid.AddChild(instance);
            instance.Connect("GameOver", this, "OnInstanceGameOver");
        }

        runningInstances = populationSize;

        generationsLabel.Text = $"Generation: {currentGeneration}";
        generationsGraph.Plot(generationHistory.ToArray());
    }

    private void NextPopulation()
    {
        OrderPopulation();

        NeuralNetwork[] picks = PickBestBrains();

        currentGeneration++;

        ClearPopulation();
        CreatePopulation(picks);
    }

    private void OrderPopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            for (int j = i; j < populationSize; j++) {
                if (population[i].score > population[j].score) {
                    var temp = population[i];
                    population[i] = population[j];
                    population[j] = temp;
                }
            }
        }

        float sum = 0f;
        for (int i = 0; i < populationSize; i++) {
            sum += population[i].score;
        }
        
        generationHistory.Add(sum);
        if (generationHistory.Count > 16) {
            generationHistory.RemoveAt(0);
        }
    }

    private NeuralNetwork[] PickBestBrains()
    {
        int picks = Mathf.Min(populationSize, 2);
        NeuralNetwork[] picked = new NeuralNetwork[picks];
        
        for (int i = 0; i < picks; i++) {
            picked[i] = population[i].ai.brain;
        }

        return picked;
    }

    public void OnInstanceGameOver()
    {
        runningInstances--;
        if (runningInstances <= 0)
        {
            NextPopulation();
        }
    }

    public void OnIterationsChanged(float val)
    {
        this.iterations = Mathf.FloorToInt(val);
        iterationsLabel.Text = $"Iterations: {this.iterations}";
    }
}
