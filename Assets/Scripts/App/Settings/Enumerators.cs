namespace App.Settings
{
    public class Enumerators
    {
        public enum AppState
        {
            Unknown,

            Main,
            Game
        }

        public enum RoadDirection
        {
            Forward,
            Right,
            Backward,
            Left,
        }
        
        public enum TrafficLightState
        {
            Red,
            RedAmber,
            Amber,
            Green,
        }

        public enum TrafficLightColor
        {
            Red,
            Yellow,
            Green
        }

    }
}