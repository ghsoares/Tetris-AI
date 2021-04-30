using Godot;
using System;

public class Graph : ColorRect
{
    public float[] plottedData {get; private set;}
    public int dataSize {get; private set;}
    public float min {get; private set;}
    public float max {get; private set;}

    [Export] public Color lineColor = Colors.White;
    [Export] public float lineWidth = 1f;
    [Export] public Font font;
    [Export] public float graphMargin = 4f;

    public void Plot(float[] data) {
        plottedData = data;
        dataSize = data.Length;
        min = float.MaxValue;
        max = float.MinValue;
        for (int i = 0; i < dataSize; i++) {
            if (data[i] > max) {
                max = data[i];
            }
            if (data[i] < min) {
                min = data[i];
            }
        }
    }

    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        if (dataSize <= 1) return;

        Vector2[] line = new Vector2[dataSize];

        for (int i = 0; i < dataSize; i++) {
            float x = (float)i / (dataSize - 1);
            float y = plottedData[i];

            y = Mathf.InverseLerp(min, max, y);

            line[i] = new Vector2(
                Mathf.Lerp(graphMargin, RectSize.x - graphMargin, x),
                Mathf.Lerp(graphMargin, RectSize.y - graphMargin, y)
            );
        }

        DrawPolyline(line, lineColor, lineWidth);
        DrawString(
            font, new Vector2(0, font.GetHeight()), $"Max: {max.ToString("0.00")}"
        );
        DrawString(
            font, new Vector2(0, RectSize.y), $"Min: {min.ToString("0.00")}"
        );
    }
}
