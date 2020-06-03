using System;
namespace bowling_p2
{
    public class Player
    {
        private string name;
        private int resultScore;

        public void SetName(string _name)
        {
            this.name = _name;
        }

        public string GetName()
        {
            return name;
        }

        public void SetResult(int _result)
        {
            this.resultScore = _result;
        }
    }
}
