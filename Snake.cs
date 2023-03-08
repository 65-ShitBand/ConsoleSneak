using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSneak {
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
            return body.First();
        }

        public void ChangeLength(Point furtherStep) {
            body.Enqueue(new Point(furtherStep.X, furtherStep.Y));
        }
    }
}
