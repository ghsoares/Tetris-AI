using Godot;
using System;

public class Inputer : Node
{
    private bool _moveLeft {get; set;}
    private bool _moveRight {get; set;}
    private bool _rotate {get; set;}
    private bool _fall {get; set;}

    public bool moveLeft {get; set;}
    public bool moveRight {get; set;}
    public bool rotate {get; set;}
    public bool fall {get; set;}
    public bool reset {get; set;}

    [Export] public float holdDelay = .5f;
    [Export] public bool capturePlayerInput = true;

    float holdLeft = 0f;
    float holdRight = 0f;

    public void Tick(float delta) {
        if (capturePlayerInput) {
            moveLeft = Input.IsActionJustPressed("move_left");
            moveRight = Input.IsActionJustPressed("move_right");

            if (Input.IsActionPressed("move_left")) {
                holdLeft += delta;
                if (holdLeft >= holdDelay) {
                    moveLeft = true;
                } else {
                    holdLeft = 0f;
                }
            }

            if (Input.IsActionPressed("move_right")) {
                holdRight += delta;
                if (holdRight >= holdDelay) {
                    moveRight = true;
                } else {
                    holdRight = 0f;
                }
            }

            rotate = Input.IsActionJustPressed("rotate");
            fall = Input.IsActionPressed("fall");
            reset = Input.IsActionJustPressed("reset");
        }
    }
}
