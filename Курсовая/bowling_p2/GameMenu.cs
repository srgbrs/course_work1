using System;
using System.Data.SqlClient;

namespace bowling_p2
{
    public class GameMenu
    {
        private int key = 0;
        private bool exit = false;
        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=userdb;Integrated Security=True;");
        Player player = new Player();
        public void Start()
        {
            while (!exit)
            {
                Console.WriteLine("1. Новая игра \n2. Выход");
                key = Int32.Parse(Console.ReadLine());
                Game game = new Game();

                switch (key)
                {
                    case 1:
                        {
                            Console.Write("Введите имя игрока: ");
                            player.SetName(Console.ReadLine());

                            for (int i = 0; i < 10; i++)
                            {
                                int ball_1, ball_2;
                                Console.WriteLine("Фрейм: " + (i + 1));
                                Console.Write("Первый бросок: ");
                                ball_1 = Int32.Parse(Console.ReadLine());
                                game.Add(ball_1);
                                if (ball_1 != 10)
                                {
                                    Console.Write("Второй бросок: ");
                                    ball_2 = Int32.Parse(Console.ReadLine());
                                    game.Add(ball_2);
                                }
                            }
                            if (game.extraRoll == true)
                            {
                                int ball_2;
                                Console.Write("Третий бросок: ");
                                ball_2 = Int32.Parse(Console.ReadLine());
                                game.Add(ball_2);
                                ball_2 = Int32.Parse(Console.ReadLine());
                                game.Add(ball_2);
                            }
                            game.displayResult();
                            Console.Write("Player: " + player.GetName() + " ");
                            Console.WriteLine(game.Score());

                            Console.WriteLine("\n\n\nПодробный лог");   
                                Console.WriteLine(game.ShowLog());

                            game.writeToDB(game);

                            break;

           
                        }
                    case 2:
                        {
                            

                          //  exit = true;
                            break;
                        }
                    default:
                        break;
                        {

                        }
                }
            }

        }
    }
}
