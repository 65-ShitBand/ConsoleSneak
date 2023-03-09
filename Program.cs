using System.Drawing;
using System.Text;

namespace ConsoleSneak {
    class Program {
        public static void Main() {
            Console.CursorVisible = false;
            var a = new Game(10);
            while (true) a.StartGame();
        }
    }
}