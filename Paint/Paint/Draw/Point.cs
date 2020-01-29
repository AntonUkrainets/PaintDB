using Newtonsoft.Json;

namespace Paint.Draw
{
    public struct Point
    {
        [JsonConstructor]
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        [JsonProperty("x")]
        public float X { get; }
        
        [JsonProperty("y")]
        public float Y { get; }
    }
}