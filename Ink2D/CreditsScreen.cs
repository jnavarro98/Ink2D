using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ink2D
{
    class CreditsScreen : Screen
    {
        Image imgCredits;
        Image imgSaveCredits;

        Audio confirm;

        int keyPressed;
        bool gameSaved;
        public CreditsScreen(Hardware hardware) : base(hardware)
        {
            confirm = new Audio(44100, 2, 4096);

            confirm.AddWAV("sounds/select.wav");

            imgCredits = new Image("images/CreditsScreen.png", 800, 600);
            imgSaveCredits = new Image("images/CreditsV3.png", 800, 600);
            imgSaveCredits.MoveTo(0, 0);
            imgCredits.MoveTo(0, 0);
            gameSaved = false;
        }

        public override void Show()
        {
            hardware.DrawImage(imgCredits);
            hardware.UpdateScreen();

            while (hardware.KeyPressed() != Hardware.KEY_SPACE) ;
        }
        public void Show(GameMode gameMode)
        {
            
            while (keyPressed!= Hardware.KEY_SPACE)
            { 
                hardware.DrawSprite(imgSaveCredits, 0, 0, imgSaveCredits.X, 0, 800, 600);
                keyPressed = hardware.KeyPressed();
                hardware.UpdateScreen();
                if ( keyPressed == Hardware.KEY_ENTER && !gameSaved)
                {
                    confirm.PlayWAV(0, 1, 0);
                    SaveStats(gameMode);
                    imgSaveCredits.MoveTo(800, 0);
                    gameSaved = true;
                }
            }
            

            
        }

        private void SaveStats(GameMode gameMode)
        {
            if(!File.Exists("GameStats " + DateTime.Now.Day + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + "h " + DateTime.Now.Minute + "m " + DateTime.Now.Second + "s.txt"))
            {
                try
                {
                    StreamWriter streamWriter = new StreamWriter(File.OpenWrite("GameStats " + DateTime.Now.Day + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + "h " + DateTime.Now.Minute + "m " + DateTime.Now.Second + "s.txt"));

                    if (gameMode.IAJ2)
                        streamWriter.WriteLine("Player 1: " + gameMode.Score[0] + " - IA: " + gameMode.Score[0]);
                    else
                        streamWriter.WriteLine("Player 1: " + gameMode.Score[0] + " - Player 2: " + gameMode.Score[0]);
                    streamWriter.WriteLine("Number of projectiles shot this match: " + gameMode.TotalProj);
                    streamWriter.WriteLine("Number of rounds it lasted: " + gameMode.TotalRounds);
                    streamWriter.WriteLine("PowerUps consumed: " + gameMode.PowerUpsConsumed);

                    streamWriter.Close();
                }
                catch (FileNotFoundException)
                {
                    throw new Exception("No se pudo encontrar el archivo");
                }
                catch (PathTooLongException)
                {
                    throw new Exception("Error de programacion fatal");
                }
                catch (IOException)
                {
                    throw new Exception("Error de programacion fatal");
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            
            
        }
    }
}
