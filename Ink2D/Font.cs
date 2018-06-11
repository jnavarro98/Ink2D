using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace Ink2D
{
    class Font
    {
        IntPtr fontType;

        public Font(string fileName, int fontSize)
        {
            fontType = SdlTtf.TTF_OpenFont(fileName, fontSize);
            if (fontType == IntPtr.Zero)
            {
                Console.WriteLine("Font type not found");
                Environment.Exit(2);
            }
        }

        public IntPtr GetFontType()
        {
            return fontType;
        }
    }
}
