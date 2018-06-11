using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    class GameMode
    {
        public short RoundsToWin { get; set; }
        protected static short delayBetweenRounds = 2000;
        public bool Unlimited { get; set; }
        public short AmmoPerTurn { get; set; }
        public short[] Score { get; set; }
        public bool IAJ2 { get; set; }
        public bool IAJ1 { get; set; }
        public short TotalProj { get; set; }
        public short TotalRounds { get; set; }
        public short PowerUpsConsumed { get; set; }
        public GameMode(short roundsToWin, bool iaj1, bool iaj2, short ammoPerTurn)
        {
            Score = new short[] { 0, 0 };
            TotalProj = 0;
            TotalRounds = 0;
            AmmoPerTurn = ammoPerTurn;
            IAJ1 = iaj1;
            IAJ2 = iaj2;
            if (ammoPerTurn == 999)
                Unlimited = true;
            else
                Unlimited = false;
            RoundsToWin = roundsToWin;
        }
    }
}
