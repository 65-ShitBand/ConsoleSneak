using System;
using System.Drawing;
using System.Text;

namespace ZXC {
    class Program {
        public static void Main() {
            var a = new Game(10);
            while (true) a.StartGame();
        }
    }

    class Game {
        StringBuilder[] map;
        Snake snake;
        Int32 size;
        static Int32 timer = 0;
        Point furtherStep;

        public Game (int size) {
            this.size = size;
            furtherStep = new Point();

            #region mapInicialization
            map = new StringBuilder[size];
            map[0] = new StringBuilder(new string('-', size), size);
            map[size - 1] = new StringBuilder(new string('-', size), size);

            for (Int32 i = 1; i < size - 1; i++) {
                map[i] = new StringBuilder($"|{new String(' ', size - 2)}|");
            }
            #endregion

            snake = new Snake(size);
        }
        
        public void ShowMap() {
            foreach (StringBuilder str in map) {
                Console.WriteLine(str);
            }
        }

        public void ChangeSnakePosition() {
            var deletedAss = snake.Move(out furtherStep);

            if (map[furtherStep.Y][furtherStep.X] == 'J') {
                snake.ChangeLength(furtherStep);
            }

            foreach (Point partOfBody in snake.body) {
                map[partOfBody.Y][partOfBody.X] = '0';
                map[deletedAss.Y][deletedAss.X] = ' ';
            }
            map[snake.body.Last().Y][snake.body.Last().X] = '@';
        }

        public void AddFruit() {
            var random = new Random(Guid.NewGuid().GetHashCode());

            map[random.Next(1, size - 1)][random.Next(1, size - 1)] = 'J';
        }

        public void DrawAll() {
            ShowMap();
            ChangeSnakePosition();
        }

        public void StartGame() {

            

            Console.Clear();
            DrawAll();

            if (timer == 7) {
                AddFruit();
                timer = 0;
            } else timer++;
        }
    }

    class Snake {
        public Queue<Point> body { get; private set; }

        public Snake(int placeOfBirth) {
            body = new Queue<Point>();
            body.Enqueue(new Point(placeOfBirth / 2, placeOfBirth / 2));
        }

        public Point Move(out Point furtherStep) {
            var pressedKey = Console.ReadKey();
            Point handler = body.Last(); 
            
            switch (pressedKey.Key.ToString()) {
                case "RightArrow":
                case "D":
                    handler.X += 1;
                    break;

                case "LeftArrow":
                case "A":
                    handler.X -= 1;
                    break;

                case "UpArrow":
                case "W":
                    handler.Y -= 1;
                    break;

                case "DownArrow":
                case "S":
                    handler.Y += 1;
                    break;
            }

            body.Enqueue(handler);
            furtherStep = handler;
            return body.Dequeue();
        }

        public void ChangeLength(Point furtherStep) {
            body.Enqueue(new Point(furtherStep.Y, furtherStep.X));
        }
    }

    //class Game {
    //    Snake snake;
    //    Game map;
    //    Int32 size;

    //    public Game () {
    //        while (!Int32.TryParse(Console.ReadLine().Trim(), out size));
    //        map = new Game(size);
    //        snake = new Snake(size);
    //    }

    //    public void Drawing() {

    //    }
    //}
}