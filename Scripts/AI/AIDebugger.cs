using Godot;
using System;

public class AIDebugger : Control
{
    public static AIDebugger instance {get; private set;}

    public AIDebugger() {
        instance = this;
    }
}
