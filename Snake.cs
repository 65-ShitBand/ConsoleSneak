using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSneak {
    class Snake {
        string moveDirection = "W";
        Int32 speed = 1000;

        public Queue<Point> body { get; private set; }
        private int fieldSize;
        Game game;
        
        public Snake(int placeOfBirth, int speed = 0)
        {
            fieldSize = placeOfBirth;
            body = new Queue<Point>();
            body.Enqueue(new Point(placeOfBirth / 2, placeOfBirth / 2));
            this.speed = speed;
        }
        public void EndGame()
        {
            Console.WriteLine("GAME OVER!");
            Environment.Exit(0);
        }
        public void CheckAlive(int x, int y)
        {
            foreach (Point pnt in body)
            {
                if (pnt.X == x && pnt.Y == y)
                {
                    EndGame();
                }
            }
        }
        public Point Move(out Point furtherStep) {

            Point handler = body.Last();
            bool pressed = false;

            for (int i = 0; i < speed * 1000; i += 1)
            {
                if (Console.KeyAvailable && !pressed)
                {
                    Console.ForegroundColor = Console.BackgroundColor;
                    var nmd = Console.ReadKey().Key.ToString();
                    Console.ForegroundColor = ConsoleColor.White;
                    if (nmd == "D" || nmd == "S" || nmd == "A" || nmd == "W" || nmd == "RightArrow" || nmd == "LeftArrow" || nmd == "UpArrow" || nmd == "DownArrow")
                    {
                        moveDirection = nmd;
                        pressed = true;
                    }   
                }
            }
            
            switch (moveDirection) {
            
                case "RightArrow":
                case "D":
                    if (handler.X + 1 == fieldSize - 1) handler.X = 1;
                    else handler.X += 1;
                    break;

                case "LeftArrow":
                case "A":
                    if (handler.X - 1 == 0) handler.X = fieldSize - 2;
                    else handler.X -= 1;
                    break;

                case "UpArrow":
                case "W":
                    if (handler.Y - 1 == 0) handler.Y = fieldSize - 2;
                    else handler.Y -= 1;
                    break;

                case "DownArrow":
                case "S":
                    if (handler.Y + 1 == fieldSize - 1) handler.Y = 1;
                    else handler.Y += 1;
                    break;
            }
            if (body.Count > 1)
            {
                CheckAlive(handler.X, handler.Y);
            }
            body.Enqueue(handler);
            furtherStep = handler;
            return body.First();
        }

        public void ChangeLength(Point furtherStep) {
            body.Enqueue(new Point(furtherStep.X, furtherStep.Y));
        }
    }
}
