using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Final
{
    class Timer
    {
        int TimeToDelay;
        int TimeElapsed;
        public bool IsTimerDone;

        public void StartTimer(GameTime gt, int AmountOfTime)
        {
            while (AmountOfTime < TimeElapsed)
            {
                IsTimerDone = false;
                TimeToDelay = AmountOfTime;
                if (IsTimerDone == false)
                {
                    if (TimeToDelay == gt.ElapsedGameTime.TotalSeconds)
                    {
                        IsTimerDone = true;

                    }
                    else TimeElapsed += 1;
                }
            }
        }
    }
}
