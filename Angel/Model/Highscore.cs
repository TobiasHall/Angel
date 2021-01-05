using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Angel.DataSaverExtension;


namespace Angel
{
    [Serializable]
    public class Highscore
    {
        public int HighestScore { get; private set; }
        public int LowestScore { get; private set; }
        public Player LatestRoundPlayer { get; set; }
        public List<Player> Players { get; private set; } = new List<Player>();

        private string fileName  = "hsl.bin";       
        
        public Highscore()
        {
            LoadPlayersFromeFile();
        }
        private void LoadPlayersFromeFile()
        {
            try
            {
                Players = ReadFromBinaryFile<List<Player>>(fileName);
                LatestRoundPlayer = Players[Players.Count - 1];
                Players.OrderByDescending(x => x.Score);
                HighestScore = FindMaxValue(Players, x => x.Score);
                LowestScore = FindMinValue(Players, x => x.Score);
            }
            catch (Exception e)
            {
                var message = e.Message;
            }
        }
        public void SaveNewHigscoreList()
        {            
            try
            {
                WriteToBinaryFile<List<Player>>(fileName, Players);
            }
            catch (Exception e)
            {
                var message = e.Message;
                //MessageBox.Show(message);
            }
        }
        private void DeletePlayersFile()
        {
            try
            {
                DeleteBinaryFile<Highscore>(fileName);
            }
            catch (Exception e)
            {
                var message = e.Message;
                //MessageBox.Show(message);
            }
        }
        public bool AddtoHighscore(Player player)
        {
            if (Players.Count < 10)
            {
                Players.Add(player);
                //Players = (List<Player>)Players.OrderByDescending(x => x.Score);
                LatestRoundPlayer = player;
                SaveNewHigscoreList();
                return true;
            }
            else if (player.Score >= LowestScore)
            {
                Players.RemoveAt(Players.Count - 1);
                Players.Add(player);
                //Players = Players.OrderByDescending(x => x.Score).ToList();
                LatestRoundPlayer = player;
                LowestScore = FindMinValue(Players, x => x.Score);
                SaveNewHigscoreList();
                return true;
            }
            return false;
        }
        //Använd och sätt proppen till private get
        public List<Player> GetPlayers()
        {
            var returnPlayers = (List<Player>)Players.OrderByDescending(x => x.Score);
            return returnPlayers;
        }

        public int FindMaxValue<T>(List<T> list, Converter<T, int> projection)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            int maxValue = int.MinValue;
            foreach (T item in list)
            {
                int value = projection(item);
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
            return maxValue;
        }
        public int FindMinValue<T>(List<T> list, Converter<T, int> projection)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            int minValue = int.MaxValue;
            foreach (T item in list)
            {
                int value = projection(item);
                if (value < minValue)
                {
                    minValue = value;
                }
            }
            return minValue;
        }
    }
}
