using System.Drawing;
using System.Text;

namespace ConsoleSneak {
    class Program {
        public static void Main() {
            Program p = new Program();
            Console.WriteLine("Задайте размер поля(min = 5): ");            
            int n = int.Parse(Console.ReadLine());
            Console.Write("Задайте скорость змейки (рекомендуемая скорость - 10): ");
            int speed = int.Parse(Console.ReadLine());
            if (n < 5) n = 5;
            Console.CursorVisible = false;
            var a = new Game(n, speed);
            while (true)
            { 
                a.StartGame();
            }
        }
    }
}