using System.Drawing;
using System.Text;

namespace ConsoleSneak {
    class Program {
        public static void Main() {
            var a = new Game(10);
            while (true) a.StartGame();
        }
    }
}