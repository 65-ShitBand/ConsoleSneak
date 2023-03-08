using System;
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

        public Game(int size) {
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
            bool eat = map[furtherStep.Y][furtherStep.X] == 'J';

            if (!eat)
            {
                snake.body.Dequeue();
            }

            foreach (Point partOfBody in snake.body)
            {
                map[partOfBody.Y][partOfBody.X] = '0';                
            }

            if (!eat) map[deletedAss.Y][deletedAss.X] = ' ';
            map[snake.body.Last().Y][snake.body.Last().X] = '@';
        }

        public void AddFruit() {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var x = random.Next(1, size - 1);
            var y = random.Next(1, size - 1);

            while (map[x][y] == '0' || map[x][y] == '@' || map[x][y] == 'J')
            {
                x = random.Next(1, size - 1);
                y = random.Next(1, size - 1);
            }

            map[x][y] = 'J';
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
}
