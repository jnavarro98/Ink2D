using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    class Screen
    {
        protected Hardware hardware;

        public Screen(Hardware hardware)
        {
            this.hardware = hardware;

        }

        public Hardware Hardware { get => hardware; set => hardware = value; }

        public virtual void Show()
        {
        }
    }
}

