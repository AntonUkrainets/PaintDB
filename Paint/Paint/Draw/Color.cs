using Newtonsoft.Json;

namespace Paint.Draw
{
    public struct Color
    {

        [JsonConstructor]
        public Color(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        [JsonProperty("red")]
        public byte Red { get; }
        
        [JsonProperty("green")]
        public byte Green { get; }
        
        [JsonProperty("blue")]
        public byte Blue { get; }
    }
}