using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter
{
    public class GameData
    {
        public int GameID { get; private set; }
        public string GameName { get; private set; }
        public Icon Icon { get; private set; }
        public GameData(int GameID, string GameName)
        {
            this.GameID = GameID;
            this.GameName = GameName;
            this.Icon = null;
        }
        public GameData(int GameID, string GameName, Icon Icon)
        {
            this.GameID = GameID;
            this.GameName = GameName;
            this.Icon = Icon;
        }
        public GameData(int GameID, string GameName, Bitmap bmp) : this(GameID, GameName, ImageConvert.BitmapToIcon(bmp)) { }
        public GameData(int GameID, string GameName, Image img) : this(GameID, GameName, ImageConvert.ImageToIcon(img)) { }
    }
}
