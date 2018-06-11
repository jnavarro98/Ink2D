using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    class GameController
    {
        public const short SCREEN_WIDTH = 800;
        public const short SCREEN_HEIGHT = 600;
        public void Start()
        {
            Hardware hardware = new Hardware(800, 600, 24, false);
            WelcomeScreen welcomeScreen = new WelcomeScreen(hardware);
            do
            {
                int[] options = new int[3];
                GameModeScreen gameModeScreen = new GameModeScreen(hardware);
                CreditsScreen creditsScreens = new CreditsScreen(hardware);
                Random rnd = new Random();
                GameMode selectedMode;
                hardware.ClearScreen();
                welcomeScreen.Show();
                if(!welcomeScreen.GetExit())
                {
                    hardware.ClearScreen();
                    gameModeScreen.Show();
                    hardware.ClearScreen();
                    options = gameModeScreen.GetChosenOption();
                    switch (options[0])
                    {
                        case 1:
                            switch (options[1])
                            {
                                case 1:
                                    selectedMode = new GameMode((short)options[2],false,true, 10);
                                    GameScreen gameScreen = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen.Show(rnd, selectedMode);
                                    creditsScreens.Show(selectedMode);
                                    break;
                                case 2:
                                    selectedMode = new GameMode((short)options[2], false, true, 20);
                                    GameScreen gameScreen2 = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen2.Show(rnd, selectedMode);
                                    creditsScreens.Show(selectedMode);
                                    break;
                                case 3:
                                    selectedMode = new GameMode((short)options[2], false, true, 999);
                                    GameScreen gameScreen3 = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen3.Show(rnd, selectedMode);
                                    creditsScreens.Show(selectedMode);
                                    break;
                            }
                            break;
                        case 2:
                            switch (options[1])
                            {
                                case 1:
                                    selectedMode = new GameMode((short)options[2], false, false, 10);
                                    GameScreen gameScreen = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen.Show(rnd, selectedMode);
                                    creditsScreens.Show(selectedMode);
                                    break;
                                case 2:
                                    selectedMode = new GameMode((short)options[2], false, false, 20);
                                    GameScreen gameScreen2 = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen2.Show(rnd, selectedMode);
                                    creditsScreens.Show(selectedMode);
                                    break;
                                case 3:
                                    selectedMode = new GameMode((short)options[2], false, false, 999);
                                    GameScreen gameScreen3 = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen3.Show(rnd, selectedMode);
                                    creditsScreens.Show(selectedMode);
                                    break;
                            }
                            break;
                        case 3:
                            switch (options[1])
                            {
                                case 1:
                                    selectedMode = new GameMode((short)options[2], true, true, 10);
                                    GameScreen gameScreen = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen.Show(rnd, selectedMode) ;
                                    creditsScreens.Show(selectedMode);
                                    break;
                                case 2:
                                    selectedMode = new GameMode((short)options[2], true, true, 20);
                                    GameScreen gameScreen2 = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen2.Show(rnd, selectedMode);
                                    creditsScreens.Show(selectedMode);
                                    break;
                                case 3:
                                    selectedMode = new GameMode((short)options[2], true, true, 999);
                                    GameScreen gameScreen3 = new GameScreen(hardware, rnd, selectedMode);
                                    gameScreen3.Show(rnd, selectedMode);
                                    creditsScreens.Show(selectedMode);
                                    break;
                            }
                            break;
                        case 4:
                            creditsScreens.Show();
                            break;
                    }
                    
                }
            } while (!welcomeScreen.GetExit());
        }
    }
}
