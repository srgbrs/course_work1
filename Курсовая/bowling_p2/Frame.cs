using System;
using System.Collections.Generic;

namespace bowling_p2
{
    public class Frame
    {
        public List<int> RollResults { get; } = new List<int>();
        public int first, second;

        private int ball = 1, frameScore;
        private bool isClosed = false;

        public Frame()
        { }

        public bool getIsClosed()
        {
            return isClosed;
        }

        public int getFrameScore()
        {
            return frameScore;
        }

        public bool Strike()
        {
            return first == 10;
        }
        public bool Spare()
        {
            return first + second == 10;
        }

        public int calcScore(bool str, bool spr)
        {
            if (spr || str)
                return 10;
            else return first + second;
        }


        public void Add(int a)
        {
            RollResults.Add(a);
            if (ball == 1)
            {
                NumberValidation(a);
                ball++;
                first = a;

                if (Strike())
                {
                    isClosed = true;
                    frameScore = calcScore(true, false);
                }
            }
            else
            {
                second = a;
                NumberValidation(a);
                isClosed = true;
                if (Spare())
                {
                    frameScore = calcScore(false, true);
                }
                else
                {
                    frameScore = calcScore(false, false);
                }
            }

        }

        void NumberValidation(int num)
        {
            if (ball == 1 && num > 10)
            {
                throw new InvalidOperationException("Нельзя сбить больше 10 кеглей");
            }
            else if (ball == 2 && (second + first > 10))
                throw new InvalidOperationException("Осталось " + (10 - first) + "кеглей");

        }
    }
}
