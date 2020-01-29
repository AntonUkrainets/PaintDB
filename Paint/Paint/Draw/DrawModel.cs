using System.Collections.Generic;
using Newtonsoft.Json;

namespace Paint.Draw
{
    public class DrawModel
    {
        public DrawModel()
        {
            Paths = new List<Path>();
        }

        [JsonConstructor]
        public DrawModel(List<Path> paths, Color current_color, float current_line_width)
        {
            Paths = paths;
            CurrentColor = current_color;
            CurrentLineWidth = current_line_width;
        }

        [JsonProperty("path")]
        public List<Path> Paths { get; }
        
        [JsonProperty("current_color")]
        public Color CurrentColor { get; set; }
        
        [JsonProperty("current_line_width")]
        public float CurrentLineWidth { get; set; }

        public void Back()
        {
            if (Paths != null && Paths.Count > 0)
            {
                Paths.Remove(Paths[Paths.Count - 1]);
            }
        }

        public void Clear()
        {
            Paths?.Clear();
        }

        public void StartPath(Point start)
        {
            var path = new Path()
            {
                Points = {start},
                Color = CurrentColor,
                LineWidth = CurrentLineWidth
            };
            Paths.Add(path);
        }

        public void AppendPath(Point toPoint)
        {
            if (Paths != null && Paths.Count > 0)
            {
                var path = Paths[Paths.Count - 1];
                path.Points.Add(toPoint);
            }
        }
    }
}