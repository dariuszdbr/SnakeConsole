using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeConsole
{
    public class Constants
    {
        public const int  height = 26;
        public const int  width = 80;
        public const int defaultSnakeSize = 5;

        public static readonly int minObstacleCount;
        public static readonly int maxObstacleCount;
        public static readonly int MinMovingObstaclesPrescaller;
        public static readonly int MaxMovingObstaclesPrescaller;
        public static readonly int MinMovingFoodPrescaller;
        public static readonly int MaxMovingFoodPrescaller;
        public static readonly int minFoodCount;
        public static readonly int maxFoodCount;
        public static readonly int MovingFoodCount;

        public static List<Position> chooseNewPosition = new List<Position>(4);

        static Constants()
        {
        chooseNewPosition.Add(new Position(0, -1)); // up
        chooseNewPosition.Add(new Position(1, 0));  // right
        chooseNewPosition.Add(new Position(0, 1));  // down
        chooseNewPosition.Add(new Position(-1, 0)); // left

        minObstacleCount = 8;
        maxObstacleCount = 12;

        MinMovingObstaclesPrescaller =5;
        MaxMovingObstaclesPrescaller = 15;

        MinMovingFoodPrescaller = 5;
        MaxMovingFoodPrescaller = 10;

        minFoodCount = 2;
        maxFoodCount = 5;

        MovingFoodCount = 5;
        }   
    }
}
