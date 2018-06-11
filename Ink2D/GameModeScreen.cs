using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    class GameModeScreen: Screen
    {
        Image imgBackground, imgChosenPlayer;
        int[] option = new int[] { 1, 1, 3 };
        Audio moveAudio;
        Audio selectAudio;
        bool input, nextScreen;
        int keyPressed;

        public GameModeScreen(Hardware hardware) : base(hardware)
        {
            moveAudio = new Audio(44100, 2, 4096);
            moveAudio.AddWAV("sounds/move.wav");
            selectAudio = new Audio(44100, 2, 4096);
            selectAudio.AddWAV("sounds/select.wav");
            imgBackground = new Image("images/FullMenu.png", 2400, 600);
            imgChosenPlayer = new Image("images/PointerMenu.png", 48, 48);
            nextScreen = true;
        }
        public void Menu1()
        {
            option[0] = 1;
            input = false;
            imgChosenPlayer.MoveTo(200, 90);
            do
            {
                hardware.ClearScreen();
                hardware.DrawSprite(imgBackground, 0, 0, 0, 0, 800, 600);
                hardware.DrawImage(imgChosenPlayer);
                hardware.UpdateScreen();

                keyPressed = hardware.KeyPressed();
                if (keyPressed == Hardware.KEY_UP && option[0] > 1)
                {
                    moveAudio.PlayWAV(0, 1, 0);
                    option[0]--;
                    imgChosenPlayer.MoveTo(200, (short)(imgChosenPlayer.Y - 123));
                }
                else if (keyPressed == Hardware.KEY_DOWN && option[0] < 4)
                {
                    moveAudio.PlayWAV(0, 1, 0);
                    option[0]++;
                    imgChosenPlayer.MoveTo(200, (short)(imgChosenPlayer.Y + 123));
                }
                else if (keyPressed == Hardware.KEY_SPACE)
                {
                    selectAudio.PlayWAV(0, 2, 0);
                    input = true;
                    nextScreen = true;
                }
                else if (keyPressed == Hardware.KEY_ESC)
                {
                    selectAudio.PlayWAV(0, 2, 0);
                    input = true;
                    nextScreen = false;
                }
            }
            while (!input);
            if (nextScreen && option[0] < 4)
                Menu2();
            else if(option[0] != 4)
                option[0] = 5;

        }
        public void Menu2()
        {
            option[1] = 1;
            input = false;
            imgChosenPlayer.MoveTo(200, 190);
            do
            {
                hardware.ClearScreen();
                hardware.DrawSprite(imgBackground, 0, 0, 800, 0, 800, 600);
                hardware.DrawImage(imgChosenPlayer);
                hardware.UpdateScreen();

                keyPressed = hardware.KeyPressed();
                if (keyPressed == Hardware.KEY_UP && option[1] > 1)
                {
                    moveAudio.PlayWAV(0, 1, 0);
                    option[1]--;
                    imgChosenPlayer.MoveTo(200, (short)(imgChosenPlayer.Y - 123));
                }
                else if (keyPressed == Hardware.KEY_DOWN && option[1] < 3)
                {
                    moveAudio.PlayWAV(0, 1, 0);
                    option[1]++;
                    imgChosenPlayer.MoveTo(200, (short)(imgChosenPlayer.Y + 123));
                }
                else if (keyPressed == Hardware.KEY_SPACE)
                {
                    selectAudio.PlayWAV(0, 2, 0);
                    input = true;
                    nextScreen = true;
                }
                else if (keyPressed == Hardware.KEY_ESC)
                {
                    selectAudio.PlayWAV(0, 2, 0);
                    input = true;
                    nextScreen = false;
                }
            }
            while (!input);
            if (nextScreen)
                Menu3();
            else
                Menu1();
               
        }
        public void Menu3()
        {

            input = false;
            imgChosenPlayer.MoveTo(170, 190);
            do
            {
                hardware.ClearScreen();
                hardware.DrawSprite(imgBackground, 0, 0, 1600, 0, 800, 600);
                hardware.DrawImage(imgChosenPlayer);
                hardware.UpdateScreen();

                keyPressed = hardware.KeyPressed();
                if (keyPressed == Hardware.KEY_UP && option[2] > 3)
                {
                    moveAudio.PlayWAV(0, 1, 0);
                    option[2] -= 3;
                    imgChosenPlayer.MoveTo(170, (short)(imgChosenPlayer.Y - 123));
                }
                else if (keyPressed == Hardware.KEY_DOWN && option[2] < 9)
                {
                    moveAudio.PlayWAV(0, 1, 0);
                    option[2] += 3;
                    imgChosenPlayer.MoveTo(170, (short)(imgChosenPlayer.Y + 123));
                }
                else if (keyPressed == Hardware.KEY_SPACE)
                {
                    selectAudio.PlayWAV(0, 2, 0);
                    input = true;
                    nextScreen = true;
                }
                else if (keyPressed == Hardware.KEY_ESC)
                {
                    selectAudio.PlayWAV(0, 2, 0);
                    input = true;
                    nextScreen = false;
                }
            }
            while (!input);
            if (!nextScreen)
                Menu2();
                
        }
        public override void Show()
        {

            Menu1();

        }

        public int[] GetChosenOption()
        {
            return option;
        }
    }
}
