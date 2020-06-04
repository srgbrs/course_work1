using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace bowling_p2
{
    public class Game
    {

        public List<Frame> frames = new List<Frame>();
        private int currentFrame = 0;
        public bool extraRoll = false;
        private string Log;
        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=userdb;Integrated Security=True;");

        public string ShowLog()
        {
            return Log;
        }

        public void Add(int a)
        {
            if (frames.Count == 0)
            {
                Log += "Фреймов нет, создаем фрейм,добавляем кидок: " + a + "\n";
                frames.Add(new Frame());
                frames[currentFrame].Add(a);
            }
            else if (frames[currentFrame].getIsClosed() == true)
            {
                Log += "Фрейм заполнен, добавляем в след. фрейм: " + a + "\n";
                frames.Add(new Frame());
                currentFrame++;
                frames[currentFrame].Add(a);
            }
            else if (frames[currentFrame].getIsClosed() == false)
            {
                Log += "Фрейм не заполнен, добавляем : " + a + "\n";
                frames[currentFrame].Add(a);
            }

            if (currentFrame == 9)
                if (frames[9].Spare() || frames[9].Strike())
                {
                    extraRoll = true;    
                }
        }
        public int Score()
        {
            int score = 0;

            for (int i = 0; i < 10; i++)
            {
                Log += "Фрейм:" + (i + 1) + "\n";
                if (!frames[i].Strike() && !frames[i].Spare()) //обычный фрейм
                {
                    score += frames[i].getFrameScore();
                    Log += "обычный фрейм,добавляем: " + frames[i].getFrameScore() + "\n";
                }
                else
                {
                    score += (frames[i].getFrameScore() + bonusPart(i, frames));
                    Log += "Страйк или спэр, добавляем: " + (frames[i].getFrameScore() + +bonusPart(i, frames)) + "\n";
                }
            }
            if (frames.Count == 11)
            {
                if (frames[9].Strike())
                {
                    score += frames[9].getFrameScore() + frames[10].RollResults[0] + frames[10].RollResults[1];
                }
                else if (frames[9].Spare())
                { score += frames[9].getFrameScore() + frames[10].RollResults[0]; }
                else
                {
                    score += frames[9].getFrameScore();
                }
            }
            return score;

        }

        int bonusPart(int frameIndex, List<Frame> Frames)
        {
            int part = 0;

            if (Frames[frameIndex].Spare())
            {
                part = Frames[frameIndex + 1].RollResults[0];
            }
            if (Frames[frameIndex].Strike())
            {
                if (Frames[frameIndex + 1].Strike())
                {
                    part = Frames[frameIndex + 1].getFrameScore() + Frames[frameIndex + 2].RollResults[0];
                }
                else
                {
                    part = Frames[frameIndex + 1].getFrameScore();
                }
            }

            return part;
        }

        public void displayResult()
        {
            string displayString = "";
            for (int i = 0; i < 10; i++)
            {
                if (frames[i].Strike())
                    displayString += "X- | ";
                else if (frames[i].Spare())
                    displayString += (frames[i].first + "-/ | ");
                else displayString += (frames[i].first + "-" + frames[i].second + " | ");

            }
            Console.WriteLine(displayString);
        }

        public void writeToDB(Game game)
        {

            conn.Open();
            string sqlExpression17 = String.Format("INSERT INTO Table_1 (frame1, frame2,frame3,frame4,frame5,frame6,frame7,frame8,frame9,frame10) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", game.frames[0].first,game.frames[1].first,game.frames[2].first,game.frames[3].first,game.frames[4].first,game.frames[5].first,game.frames[6].first,game.frames[7].first,game.frames[8].first,game.frames[9].first);
            string sqlExpression18 = String.Format("INSERT INTO Table_1 (frame1, frame2,frame3,frame4,frame5,frame6,frame7,frame8,frame9,frame10) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", game.frames[0].second,game.frames[1].second,game.frames[2].second,game.frames[3].second,game.frames[4].second,game.frames[5].second,game.frames[6].second,game.frames[7].second,game.frames[8].second,game.frames[9].second);
            SqlCommand command = new SqlCommand(sqlExpression17, conn);
            int number = command.ExecuteNonQuery();
            SqlCommand command1 = new SqlCommand(sqlExpression18, conn);
            int number1 = command1.ExecuteNonQuery();
        }
    }
}
