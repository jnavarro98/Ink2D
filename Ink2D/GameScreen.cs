using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tao.Sdl;

namespace Ink2D
{
    [Serializable]
    class GameScreen : Screen
    {

        public const int UPPER_LIMIT = 50;
        public const int BOTTOM_LIMIT = 550;
        public const int LEFT_LIMIT = 38;
        public const int RIGHT_LIMIT = 762;
        public const int POWERUP_CHANCE = 20;
        public const byte LOOP_MS_SLEEP = 7;

        Random rnd;

        Audio music;
        Audio hit;
        Audio shoot;
        Audio boing;
        Audio lazer;


        Inker p1;
        Inker p2;
        Inker currentPlayer;
        Inker enemy;

        Image background;
        Image heartSprite;
        Timer timer;

        Obstacle obstacle;

        GameMode gameMode;

        internal DateTime timeStampFromLastShot;
        internal DateTime timeStampFromLazerCD;
        Font font;
        bool gameOver;
        Triangle projectileUI;
        PowerUp powerUp;

        public enum GameState
        {

            player1Turn, player2Turn
            
        };

        public GameState CurrentGameState { get; set; }
        internal Inker Enemy { get => enemy; set => enemy = value; }
        internal Inker P1 { get => p1; set => p1 = value; }
        internal List<Projectile> DrawnProjectiles { get; set; }
        internal Inker CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        internal PowerUp PowerUp { get => powerUp; set => powerUp = value; }
        internal Audio Hit { get => hit; set => hit = value; }
        internal Obstacle Obstacle { get => obstacle; set => obstacle = value; }

        public GameScreen(Hardware hardware, Random rnd, GameMode gameMode) : base(hardware)
        {

            if(gameMode.IAJ1)
                p1 = new IA(1,rnd);
            else
                p1 = new Player(1);
            if (gameMode.IAJ2)
                p2 = new IA(2, rnd);
            else
                p2 = new Player(2);

            background = new Image("images/Mirrors.png",800,500);
            heartSprite = new Image("images/Heart.png", 50, 50);
            projectileUI = new Triangle();
            obstacle = new Obstacle(12, 150, rnd);
            obstacle.Reposition();

            music = new Audio(44100, 2, 4096);
            hit = new Audio(44100, 2, 4096);
            shoot = new Audio(44100, 2, 4096);
            boing = new Audio(44100, 2, 4096);
            lazer = new Audio(44100, 2, 4096);

            music.AddMusic("sounds/MainTheme.wav");
            lazer.AddWAV("sounds/lazer.wav");
            hit.AddWAV("sounds/hit.wav");
            shoot.AddWAV("sounds/shoot.wav");
            boing.AddWAV("sounds/boing.wav");

            background.MoveTo(0, 50);

            font = new Font("fonts/ARCADE_I.TTF", 50);

            gameOver = false;
            CurrentGameState = 0;

            DrawnProjectiles = new List<Projectile>();

            this.rnd = rnd;
            this.gameMode = gameMode;

        }
        

        public void DrawBackground()
        {

            hardware.DrawImage(background);

        }
        public void MovePlayers()
        {

            p1.Move(this);
            p2.Move(this);

        }
        
        public void Shoot()
        {

            Projectile tempProj = (currentPlayer.Beam.Shoot());
            if (tempProj != null)
            {
                currentPlayer.AmmoUsed++;
                gameMode.TotalProj++;
                tempProj.SetCoordinates(currentPlayer.Beam, CurrentGameState);  
                shoot.PlayWAV(0, 1, 0);
                DrawnProjectiles.Add(tempProj);
                SpawnPowerUp();
            }
            else if(DrawnProjectiles.Count == 0)
            {
                ChangeTurn();
            }

        }

        public void SpawnPowerUp()
        {
            if(powerUp == null)
            {
                if(rnd.Next(0, POWERUP_CHANCE) == 0)
                {
                    powerUp = new PowerUp(rnd);
                }

            }
        }

        public void DrawAmmo()
        {

            short x = 0;
            short y;
            if(CurrentGameState == GameState.player2Turn)
            {
                y = 0;
            }
            else
            {
                y = 550;
            }
            for (int i = 0; i < currentPlayer.Beam.Ammo.Count; i++)
            {
                if (i >= 20)
                    break;
                projectileUI.SetCoordinatesAsSprite(x, y, 20);
                hardware.DrawTriangle(projectileUI);
                x += 25;
                if (x >= 250)
                {
                    x = 0;
                    y += 25;
                }
            }

        }
        public void DrawUI()
        {
            if (gameMode.Unlimited)
                DrawAmmoUsed();
            else
                DrawAmmo();
            DrawHearts();
            DrawUILines();
            DrawScore();
            DrawObstacle();

        }
        public void DrawObstacle()
        {
            if(gameMode.TotalProj%3==0)
                hardware.DrawPolygon(obstacle, 0, 255, (byte)(DateTime.Now.Second*3));
            else if(gameMode.TotalProj % 2== 0)
                hardware.DrawPolygon(obstacle, (byte)(DateTime.Now.Second * 3), 0, 255);
            else
                hardware.DrawPolygon(obstacle, 255, 0, (byte)(DateTime.Now.Second * 3));

        }
        public void DrawUILines()
        {

            hardware.DrawLine(0, 50, 800, 50, 0, 255, 255, 255);
            hardware.DrawLine(0, 550, 800, 550, 0, 255, 255, 255);
            hardware.DrawLine(250, 0, 250, 50, 0, 255, 255, 255);
            hardware.DrawLine(250, 550, 250, 600, 0, 255, 255, 255);
            hardware.DrawLine(550, 0, 550, 50, 0, 255, 255, 255);
            hardware.DrawLine(550, 550, 550, 600, 0, 255, 255, 255);

        }
        public void DrawPUpEffect()
        {
            hardware.DrawPolygon(currentPlayer.Beam.Lazer, rnd);            
        }
        public void DrawPowerUp()
        {
            if (powerUp != null)
                hardware.DrawImage(powerUp.Sprite);
        }
        public void DrawScore()
        {

            hardware.WriteText(gameMode.Score[0].ToString(), 375, 550, font);
            hardware.WriteText(gameMode.Score[1].ToString(), 375, 0, font);

        }
        public void DrawAmmoUsed()
        {

            hardware.WriteText(p1.AmmoUsed.ToString("00"), 85, 550, font);
            hardware.WriteText(p2.AmmoUsed.ToString("00"), 85, 0, font);

        }
        public void DrawHearts()
        {

            short x = 600;
            short y;
            if (CurrentGameState == GameState.player1Turn)
            {
                y = 0;
                for (int i = 0; i < enemy.Lives; i++)
                {
                    heartSprite.MoveTo(x, y);
                    hardware.DrawImage(heartSprite);
                    x += 50;
                }
            }
            else
            {
                y = 550;
                for (int i = 0; i < enemy.Lives; i++)
                {
                    heartSprite.MoveTo(x, y);
                    hardware.DrawImage(heartSprite);
                    x += 50;
                }
            }

        }
        public void SetScore()
        {

            if(gameMode.Unlimited)
            {
                if (p1.AmmoUsed < p2.AmmoUsed)
                    gameMode.Score[0] += 1;
                if (p1.AmmoUsed > p2.AmmoUsed)
                    gameMode.Score[1] += 1;
                CheckWin();
            }
            else
            {
                if (p1.Lives > p2.Lives)
                    gameMode.Score[0] += 1;
                if (p1.Lives < p2.Lives)
                    gameMode.Score[1] += 1;
                CheckWin();
            }

        }
        public void CheckWin()
        {

            if ((gameMode.Score[0] >= ((GameMode)gameMode).RoundsToWin && gameMode.Score[1] != gameMode.Score[0]))
            {
                gameOver = true;
                RenderGame();
                hardware.WriteText("PLAYER 1 WINS!", 65, 300, font);
                hardware.ClearBottom();
                hardware.UpdateScreen();
                Thread.Sleep(2000);
            }
            if ((gameMode.Score[1] >= ((GameMode)gameMode).RoundsToWin && gameMode.Score[1] != gameMode.Score[0]))
            {
                gameOver = true;
                RenderGame();
                hardware.WriteText("PLAYER 2 WINS!", 65, 300, font);
                hardware.ClearBottom();
                hardware.UpdateScreen();
                Thread.Sleep(2000);

            }

        }
        public void ChangeTurn()
        {
            if(!currentPlayer.Beam.Lazer.Damage)
            {
                CleanPowerUps();
                if (CurrentGameState == GameState.player1Turn)
                {
                    currentPlayer = p2;
                    enemy = p1;
                    CurrentGameState = GameState.player2Turn;
                }

                else
                {
                    gameMode.TotalRounds++;
                    SetScore();
                    currentPlayer = p1;
                    enemy = p2;
                    p1.RefillLives();
                    p2.RefillLives();
                    p1.AmmoUsed = 0;
                    p2.AmmoUsed = 0;
                    CurrentGameState = GameState.player1Turn;
                }
                obstacle.Reposition();
                DrawnProjectiles.Clear();
                currentPlayer.SetBeam();
                currentPlayer.Beam.Reload(gameMode.AmmoPerTurn);
            }
            

        }
        public void AnimateProjectiles(Random rnd)
        {

            for(int i = 0; i < DrawnProjectiles.Count; i++)
            {
                hardware.DrawTriangle(DrawnProjectiles[i], rnd);
                DrawnProjectiles[i].Move();
                DrawnProjectiles[i].Bounce(this);

                if (DrawnProjectiles[i].hasBounced && !currentPlayer.Beam.Lazer.Damage)
                {
                    boing.PlayWAV(0, 1, 0);
                }
                if (DrawnProjectiles[i].Crashes(Enemy) 
                || DrawnProjectiles[i].IsOutOfBounds()
                || DrawnProjectiles[i].Crashes(PowerUp))
                {
                    if(DrawnProjectiles[i].Crashes(Enemy))
                    {
                        enemy.Hit();
                        if(!currentPlayer.Beam.Lazer.Damage)
                            hit.PlayWAV(0, 1, 0);
                    }
                    if(DrawnProjectiles[i].Crashes(PowerUp))
                    {
                        lazer.PlayWAV(0, 1, 0);
                        gameMode.PowerUpsConsumed++;
                        currentPlayer.Beam.Lazer.Activate(currentPlayer.Beam);
                        timer = new Timer(CleanPowerUps, null, 2000, Timeout.Infinite);
                        powerUp = null;
                    }
                    DrawnProjectiles.Remove(DrawnProjectiles[i]);
                }
            }

        }

        private void CleanPowerUps(object state)
        {
            if(p1.Beam != null)
                p1.Beam.Lazer.Deactivate(p1.Beam);
            if (p2.Beam != null)
                p2.Beam.Lazer.Deactivate(p2.Beam);
        }
        private void CleanPowerUps()
        {
            if (p1.Beam != null)
                p1.Beam.Lazer.Deactivate(p1.Beam);
            if (p2.Beam != null)
                p2.Beam.Lazer.Deactivate(p2.Beam);
        }
        public void DrawBeams()
        {

            if (CurrentGameState == GameState.player1Turn)
                hardware.DrawPolygon(p1.Beam, 237, 28, 36);
                
            else
                hardware.DrawPolygon(p2.Beam, 255, 242, 0);

        }
        public void CheckShootAvaliable(bool shootKey, ref DateTime timeStampFromLastShot)
        {

            if (shootKey)
            { 
                if ((DateTime.Now - timeStampFromLastShot).TotalMilliseconds > currentPlayer.ShotInterval && !currentPlayer.Beam.Lazer.Damage)
                {
                    timeStampFromLastShot = DateTime.Now;
                    Shoot();
                }
            }
            else if (currentPlayer.Beam.Ammo.Count == 0)
                Shoot();

        }
        private void CheckStatus()
        {
            
            if(currentPlayer.Beam.Lazer.Crashes(enemy))
            {
                if ((DateTime.Now - timeStampFromLazerCD).TotalMilliseconds > currentPlayer.Beam.Lazer.CD)
                {
                    timeStampFromLazerCD = DateTime.Now;
                    enemy.Hit();
                }
            }
            if (enemy.Lives == 0)
            {
                ChangeTurn();
            }

        }
        public void RenderGame()
        {

            hardware.ClearScreen();

            DrawBackground();
            hardware.DrawInker(p1);
            hardware.DrawInker(p2);
            DrawUI();
            AnimateProjectiles(rnd);
            DrawPowerUp();
            DrawBeams();
            DrawPUpEffect();

            hardware.UpdateScreen();

        }
        public void UpdateExtras()
        {
            if (powerUp != null)
                powerUp.Move(this);
        }
        public void GameLoop()
        {

            RenderGame();
            UpdateExtras();
            MovePlayers();
            currentPlayer.Shoot(this);
            currentPlayer.Beam.Wiggle(currentPlayer);


            CheckStatus();
            Thread.Sleep(LOOP_MS_SLEEP);

        }
        public void Start()
        {
            
            Thread.Sleep(500);
            timeStampFromLastShot = DateTime.Now;
            timeStampFromLazerCD = DateTime.Now;
            p1.Spawn();
            p2.Spawn();
            p1.SetBeam();
            currentPlayer = p1;
            enemy = p2;
            music.PlayMusic(0, -1);
            currentPlayer.Beam.Reload(gameMode.AmmoPerTurn);

        }
        public void Show(Random rnd, GameMode gameMode)
        {

            Start();

            do
            { 
                GameLoop();                
            }
            while (!gameOver && !hardware.IsKeyPressed(Hardware.KEY_ESC));

            music.StopMusic();

        }

    }
}