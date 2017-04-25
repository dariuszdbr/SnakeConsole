using System;
using System.Collections.Generic;
using System.Threading;
using SnakeConsole.Direction;
using SnakeConsole.Interfaces;


namespace SnakeConsole
{
    public class GameEngine
    {
        #region Fields
        
        
        public static readonly Random rand = new Random();
        private Snake snake;
        private List<Obstacle> obstaclesList = new List<Obstacle>();
        private List<MovingObstacle> movingObstaclesList = new List<MovingObstacle>();
        private List<Food> AppleList = new List<Food>();
        private List<MovingFood> movingFoodsList = new List<MovingFood>();
        private List<MoveDirection> allowedDirectionsList = new List<MoveDirection>();
        private bool pause = false;
        private int movingObstaclesPrescaller;
        private int movingFoodPrescaller;

        public int size { get; set; }
        public Position Position { get; set; }
        public static int score { get; set; }

        #endregion

        #region Constructor
 
        public GameEngine()
        {
            TheBoard.DrawBoard();
            initializeSnake();
            initializeMovingObstacles();
            initializeObstacles();
            initializeFood();
            StartGame();
        }
         #endregion

        #region Methods

        #region Start Game
        public void StartGame()
        {
            int ticksCounterObstacle = 0;
            int ticksCounterFoods = 0;
            movingObstaclesPrescaller = rand.Next(Constants.MinMovingObstaclesPrescaller, Constants.MaxMovingObstaclesPrescaller);
            movingFoodPrescaller = rand.Next(Constants.MinMovingFoodPrescaller, Constants.MaxMovingFoodPrescaller);

            while (!snake.IsDestroyed)
            {
                ReadKey();
                if (!pause)
                {
                    snake.Move();
                    CheckSnakeDead();
                    CheckApple();

                    #region Change the direction of moving Obstacle at a specified interval
                    if (ticksCounterObstacle >= movingObstaclesPrescaller)
                    {
                        foreach(var movingObstacle in this.movingObstaclesList)
                            ShouldChangeDirection<MovingObstacle>(movingObstacle); // Get a little bit random move
                       MoveObstacles();
                       ticksCounterObstacle = 0;
                    }
                    #endregion

                    #region Change the direction of moving Food at a specified interval
                    if (ticksCounterFoods >= movingFoodPrescaller)
                    {
                        foreach (var movingFood in this.movingFoodsList)
                            ShouldChangeDirection<MovingFood>(movingFood);
                        MoveFood();
                        ticksCounterFoods = 0;
                    }
                    #endregion
                }
                Thread.Sleep(80);
                ticksCounterObstacle++;
                ticksCounterFoods++;
            }
            
        }
        #endregion

        //*************************************************************************** GAME OVER ***************************************************************************
        #region Game Over
        private void GameOver()
        {
            score = 0;
            Console.BackgroundColor = ConsoleColor.Black;
            string gameover = "Game Over!!!";
            snake.Destroy();
            Console.SetCursorPosition((Console.WindowWidth - gameover.Length) / 2, Console.WindowHeight / 2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(gameover);
            Console.ReadKey(); 
            Menu.StartMenu();
        }
        #endregion

        //*************************************************************************** INITIALIZE ***************************************************************************
        #region initialize snake
        private void initializeSnake()
        {
            var x = Constants.width / 2;
            var y = Constants.height / 2;
            var position = new Position(x, y);
            var size = Constants.defaultSnakeSize;
            snake = new Snake(position, size);
        }
        #endregion

        #region initialize obstacle
        private void initializeObstacles()
        {
            var obstacleCount = rand.Next(Constants.minObstacleCount, Constants.minObstacleCount);
            for (int i = 0; i < obstacleCount; i++)
            {
                var x = rand.Next() % (Constants.width - 3) + 2;
                var y = 0;
                do
                {
                    y = rand.Next() % (Constants.height - 2) + 2;
                } while (y > 10 && y < 14); // snake starting position
                Position obstaclePosition = new Position(x, y);
                Obstacle disturber = new Obstacle(obstaclePosition, size);
                obstaclesList.Add(disturber);
            }
            foreach (var obstacle in obstaclesList)
            {
                obstacle.Draw();
            }
        }
        #endregion
        
        #region initialize moving obstacle
        private void initializeMovingObstacles()
        {
            Position movingObstaclePosition = new Position();
            var obstacleCount = rand.Next(Constants.minObstacleCount, Constants.maxObstacleCount);
            for (int i = 0; i < obstacleCount; i++)
            {
                do
                {   
                    var x = rand.Next() % (Constants.width - 3) + 2;
                    var y = rand.Next() % (Constants.height - 2) + 2;
                    movingObstaclePosition = new Position(x, y);

                } while (CheckObjectOverObstacleAndBounds(new MovingObstacle(movingObstaclePosition)) && CheckRandomPositionOverSnake(movingObstaclePosition));
               
                var movingDisturber = new MovingObstacle(movingObstaclePosition);
                movingObstaclesList.Add(movingDisturber);
            }
            foreach (var movingObstacle in movingObstaclesList)
            {
                movingObstacle.Draw();
            }
        }
        #endregion

        #region initialize food
        private void initializeFood()
        {
            var foodCount = rand.Next(Constants.minFoodCount, Constants.maxFoodCount);
            Position applePosition;
            for (int i = 0; i < foodCount; i++)
            {
                do
                {
                    var x = rand.Next() % (Constants.width - 3) + 2;
                    var y = rand.Next() % (Constants.height - 2) + 2;
                    applePosition = new Position(x, y);

                } while ( CheckObjectOverObstacleAndBounds(new Food(applePosition)) && CheckRandomPositionOverSnake(applePosition) );

                AppleList.Add(new Food(applePosition));
            }
            foreach (var apple in AppleList)
            {
                apple.Draw();
            }
            InitializeMovingFood();
        }
        #endregion

        #region initialize moving food
        private void InitializeMovingFood()
        {
            var movingFoodCount = Constants.MovingFoodCount;
            Position movingFoodPos;
            for (int i = 0; i < movingFoodCount; i++)
            {
                do
                {
                    var x = rand.Next() % (Constants.width - 3) + 2;
                    var y = rand.Next() % (Constants.height - 2) + 2;
                    movingFoodPos = new Position(x, y);
                } while (CheckObjectOverObstacleAndBounds(new MovingFood (movingFoodPos)) && CheckRandomPositionOverSnake(movingFoodPos));

                MovingFood movingFood = new MovingFood(movingFoodPos);
                movingFoodsList.Add(movingFood);
            }

            foreach (var movingFood in this.movingFoodsList)
            {
                movingFood.Draw();
            }
        }
        #endregion

        //*************************************************************************** COLLISION DETECT ***************************************************************************
        #region Check object position over obstacle position
        private bool CheckObjectOverObstacles(Position objPos)
        {
            foreach (var obstacle in this.obstaclesList)
            {
                foreach (var point in obstacle.obstacleBigPosition)
                {
                    if (point.Equals(objPos))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Check object position over snake parts position
        private bool CheckRandomPositionOverSnake(Position objPos) 
        {
            foreach (var snakePart in snake.Parts)
            {
                if (snakePart.Position.Equals(objPos))
                {
                    return true;
                }  
            }
            return false;
        }
        #endregion

        #region Check wheter the snake crashed in moving obstacle
        private bool snakeCrashedMovingObstacle()
        {
            foreach (var movingObstacle in this.movingObstaclesList)
            {
                foreach(var snakePart in snake.Parts)
                if (snakePart.Position.Equals(movingObstacle.Position))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Check wheter the static aplle or moving food was ate
        private void CheckApple()
        {
            foreach (var apple in this.AppleList)
            {  
                if (snake.Parts.First.Value.Position.Equals(apple.Position))
                {
                    ++score;
                    snake.Grow();
                    TheBoard.DrawBoard(); //  there is a problem with. When snake is going near bounds and eat apple on the bounds appear black "space"
                    TheBoard.WritePoints();
                    TheBoard.WriteLengthOfSnake(snake.Parts.Count);
                    ChangeFoodPosition(apple);
                }
            }

            foreach (var movingFood in this.movingFoodsList) 
            {
                if (snake.Parts.First.Value.Position.Equals(movingFood.Position))
                {
                    
                    score += 2;
                    snake.Grow();
                    TheBoard.DrawBoard();
                    TheBoard.WritePoints();
                    TheBoard.WriteLengthOfSnake(snake.Parts.Count);
                    ChangeFoodPosition(movingFood);
                }
            }
        }
        #endregion
        #region If was eaten, change position of food
        private void ChangeFoodPosition(Food food)
        {
            Position newPos = new Position();
            do
            {
                int x = rand.Next() % (Constants.width - 3) + 2;
                int y = rand.Next() % (Constants.height - 2) + 2;
                food.Position = new Position(x, y);
            } while (CheckObjectOverObstacleAndBounds<Food>(food) && CheckRandomPositionOverSnake(food.Position));

            food.Draw();
        }
        #endregion

        #region Check object over bounds
        private bool CheckBounds(Position objPos)
        { 
            if (objPos.X > Constants.width - 2 || objPos.X < 1)
            {
                return true;
            }
            if (objPos.Y > Constants.height - 1 || objPos.Y < 1)
            {
                return true;
            }
            return false;
        } 
        #endregion

        #region Check wheter the moving objects are going to move over other moving objects
        private bool CheckMovingObjOverObj<T>(T obj) where T: ICollisionDetectable
        {
            #region Depending on the object direction add to the actual object position value of x and y in the next move 

            Position checkThisPosition = new Position();
            var direction = (int)obj.Direction;
            checkThisPosition = obj.Position.Copy().Add(Constants.chooseNewPosition[direction]);
            #endregion

            bool checking = false;

            #region If the position of moving object in the next move will be eqaul to the food position return true <and dont let him move this time> MoveOstacle() Method
            if (obj is MovingObstacle)
            { 
                foreach (var food in this.movingFoodsList)
                {
                    if (checkThisPosition.Equals(food.Position))
                        checking = true;
                }
              
            }
            #endregion
           
            #region If the position of moving food in the next move will be equal to the moving obstacle position, or snake parts position dont let them move  - MoveFood() Method
            else if (obj is MovingFood)
            {
                foreach(var obstacle in this.movingObstaclesList)
                {
                    if (checkThisPosition.Equals(obstacle.Position))
                        checking = true;
                }

                foreach (var snakePart in this.snake.Parts)
                {
                    if (checkThisPosition.Equals(snakePart.Position))
                        checking = true;
                }
            }
            #endregion

            #region Dont forget to check the apple position
            foreach (var food in this.AppleList)
            {
               if (checkThisPosition.Equals(food.Position))
                        checking = true;
            }
            #endregion

            return checking;
        }
        #endregion

        #region Check wheter the Objects are going on Static Obstacle, or Bounds
        private bool CheckObjectOverObstacleAndBounds<T>(T obj) where T : ICollisionDetectable//, IMovable 
        {
            Position checkThisPosition = new Position();

            if (obj is MovingFood || obj is MovingObstacle)
            {
                var direction = (int)obj.Direction;
                checkThisPosition = obj.Position.Copy().Add(Constants.chooseNewPosition[direction]); // Check in next movement object Position whether it is over obstacle Position  
            }

            else if (obj is Snake)
                checkThisPosition = snake.Parts.First.Value.Position.Copy();

            else 
                checkThisPosition = obj.Position.Copy(); // should work for apple, but sometimes apple spawn at static obstacle :|

                foreach (var obstacle in obstaclesList)
                {
                    foreach (var point in obstacle.obstacleBigPosition)
                    {
                    if (checkThisPosition.Equals(point.X, point.Y) || CheckBounds(checkThisPosition) )
                        return true;
                    }
                }
            return false;
            }
        #endregion

        #region Check if the snake ate itself
        public bool CheckIfAteItSelf()
        {
            var Head = snake.Parts.First.Value.Position;
            bool skipTheHead = true;
            foreach(var tail in snake.Parts)
            {
                if (!skipTheHead)
                {
                    if (Head.Equals(tail.Position))
                        return true;
                }
                skipTheHead = false;
            }
            return false;
        }
        #endregion

        #region Check wheather the snake should die
        private void CheckSnakeDead()
        {
            var overMovingObstacle = snakeCrashedMovingObstacle();
            var overObstacleOrBounds = CheckObjectOverObstacleAndBounds<Snake>(this.snake);
            var ateItSelf = CheckIfAteItSelf();

            if (overMovingObstacle || overObstacleOrBounds || ateItSelf)
                GameOver();
        }
        #endregion

        //*************************************************************************** MOVEMNETS *************************************************************************** 
        #region Move Obstacle
        private void MoveObstacles()
        {
            foreach (var obstacle in movingObstaclesList)
            {
                if (!CheckObjectOverObstacleAndBounds(obstacle) && !CheckMovingObjOverObj(obstacle))
                {
                    obstacle.Move();
                    obstacle.Draw();
                }
            }
        }
        #endregion

        #region Move Food   
        private void MoveFood()
        {
            foreach (var movingFood in this.movingFoodsList)
            {
                if (!CheckObjectOverObstacleAndBounds(movingFood) && !CheckMovingObjOverObj(movingFood))
                {
                    movingFood.Move();
                    movingFood.Draw();
                }
            }
        }
        #endregion
       
        #region Make a random change of direction for the moving objects
        private void ShouldChangeDirection<T>(T obj) where T : IMovable
        {
            var shouldChangeDirection = rand.Next() % 3 == 0;
            if (shouldChangeDirection)
            {
                MoveDirection newDirection = (MoveDirection)rand.Next(0, 4);
                obj.ChangeDirection(newDirection);
            }
        }
        #endregion

        // *************************************************************************** READ KEYS ***************************************************************************
        #region Read Keys
        private void ReadKey()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.LeftArrow && snake.snakeDirection != MoveDirection.right)
                {
                    snake.ChangeDirection(MoveDirection.left);
                }

                if (pressedKey.Key == ConsoleKey.RightArrow && snake.snakeDirection != MoveDirection.left)
                {
                    snake.ChangeDirection(MoveDirection.right);
                }

                if (pressedKey.Key == ConsoleKey.UpArrow && snake.snakeDirection != MoveDirection.down)
                {
                    snake.ChangeDirection(MoveDirection.up);
                }

                if (pressedKey.Key == ConsoleKey.DownArrow && snake.snakeDirection != MoveDirection.up)
                {
                    snake.ChangeDirection(MoveDirection.down);
                }

                if (pressedKey.Key == ConsoleKey.Spacebar)
                {
                    pause = !pause;
                }

                if (pressedKey.Key == ConsoleKey.Escape)
                {
                    Menu.StartMenu();
                }

                while (Console.KeyAvailable) { Console.ReadKey(true); }
            }
        }
        #endregion


        #endregion
    }
}

