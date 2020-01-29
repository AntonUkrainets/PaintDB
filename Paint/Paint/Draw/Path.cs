using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Paint.Draw
{
    public class Path
    {
        public Path()
        {
            Points = new List<Point>();
        }

        [JsonConstructor]
        public Path(List<Point> points, Color color, float line_width)
        {
            Points = points;
            Color = color;
            LineWidth = line_width;
        }
        
        [JsonProperty("points")]
        public List<Point> Points { get; }
        
        [JsonProperty("color")]
        public Color Color { get; set; }

        public static object Combine(string documents, string v)
        {
            throw new NotImplementedException();
        }

        [JsonProperty("line_width")]
        public float LineWidth { get; set; }
    }
}