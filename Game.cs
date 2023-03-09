using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleSneak {
    class Game {
        StringBuilder[] map;
        Snake snake;
        Int32 size;
        static Int32 timer = 0;
        Point furtherStep;
        Point deletedAss;

        public Game(int size, int speed) {
            this.size = size;
            furtherStep = new Point();

            #region mapInicialization
            map = new StringBuilder[size];
            map[0] = new StringBuilder(new string('#', size), size);
            map[size - 1] = new StringBuilder(new string('#', size), size);

            for (Int32 i = 1; i < size - 1; i++) {
                map[i] = new StringBuilder($"|{new String(' ', size - 2)}|");
            }
            #endregion

            snake = new Snake(size, speed);
        }

        public void ShowMap() {
            Int32 lot = 0;
            foreach (StringBuilder str in map) {
                string line = str.ToString();
                Console.SetCursorPosition(Console.WindowWidth / 2 - size / 2, Console.WindowHeight / 2 + lot - size / 2);
                foreach (char c in line)
                {
                    switch(c)
                    {
                        case '#':
                        case '|':
                            ShowColored(c.ToString(), ConsoleColor.Gray); 
                            break;

                        case '@':
                            ShowColored(c.ToString(), ConsoleColor.Cyan);
                            break;

                        case '§':
                            ShowColored(c.ToString(), ConsoleColor.Green);
                            break;

                        case '¤':
                            ShowColored(c.ToString(), ConsoleColor.Magenta);
                            break;

                        default:
                            ShowColored(c.ToString(), ConsoleColor.White);
                            break;
                    }
                }
                Console.WriteLine();
                lot++;
            }   
        }

        public void ChangeSnakePosition() {

            var deletedAss = snake.Move(out furtherStep);
            bool eat = map[furtherStep.Y][furtherStep.X] == '¤';

            if (!eat)
            {
                snake.body.Dequeue();
            }


            foreach (Point partOfBody in snake.body)
            {
                map[partOfBody.Y][partOfBody.X] = '§';                
            }

            if (!eat) map[deletedAss.Y][deletedAss.X] = ' ';
            map[snake.body.Last().Y][snake.body.Last().X] = '@';
            
        }

        public void AddFruit() {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var x = random.Next(1, size - 1);
            var y = random.Next(1, size - 1);

            while (map[x][y] == '§' || map[x][y] == '@' || map[x][y] == '¤')
            {
                x = random.Next(1, size - 1);
                y = random.Next(1, size - 1);
            }

            map[x][y] = '¤';
        }

        public void DrawAll() {
            ShowMap();
            ShowScore();
            ChangeSnakePosition();
        }

        public void StartGame() {
            DrawAll();

            if (timer == 7) {
                AddFruit();
                timer = 0;
            } else timer++;
        }

        public void ShowScore()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - size / 2, Console.WindowHeight / 2 + size - size / 2);
            ShowColored("Score: " + snake.body.Count().ToString(), ConsoleColor.Red);
        }

        /// <summary>
        /// Выводит строку str в цвете color.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        public void ShowColored(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
