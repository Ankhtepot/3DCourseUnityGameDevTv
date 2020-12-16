using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts
{
    public class Level
    {
        public string LevelDescription;
        public string WinItem;
        public string[] Words;

        private Level(string levelDescription, string winItem, string[] words)
        {
            LevelDescription = levelDescription;
            WinItem = winItem;
            Words = words;
        }

        public static Level CreateLevel(string levelDescription, string winItem, string[] words)
        {
            return new Level(levelDescription, winItem, words);
        }
    }
}
