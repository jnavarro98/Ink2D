using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    class WelcomeScreen : Screen
    {
        bool exit;
        Image imgWelcome;
        Audio audio;

        public WelcomeScreen(Hardware hardware) : base(hardware)
        {
            Random rnd = new Random();
            exit = false;
            audio = new Audio(44100, 2, 4096);
            audio.AddMusic("sounds/MainTheme.wav");
            imgWelcome = new Image("images/WelcomeScreen.png", 800, 600);
            imgWelcome.MoveTo(0, 0);
        }

        public override void Show()
        {
            bool escPressed = false, spacePressed = false;
            hardware.DrawImage(imgWelcome);
            hardware.UpdateScreen();
            audio.PlayMusic(0, -1);
            

            do
            {
                int keyPressed = hardware.KeyPressed();
                if (keyPressed == Hardware.KEY_ESC)
                {
                    escPressed = true;
                    exit = true;
                }
                else if (keyPressed == Hardware.KEY_SPACE)
                {
                    spacePressed = true;
                    exit = false;
                }
            }
            while (!escPressed && !spacePressed);
            audio.StopMusic();
        }

        public bool GetExit()
        {
            return exit;
        }
    }
}
