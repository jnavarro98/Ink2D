using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tao.Sdl;

namespace Ink2D
{
    class Hardware
    {
        public const int KEY_ESC = Sdl.SDLK_ESCAPE;
        public const int KEY_UP = Sdl.SDLK_UP;
        public const int KEY_DOWN = Sdl.SDLK_DOWN;
        public const int KEY_LEFT = Sdl.SDLK_LEFT;
        public const int KEY_RIGHT = Sdl.SDLK_RIGHT;
        public const int KEY_SPACE = Sdl.SDLK_SPACE;
        public const int KEY_J = Sdl.SDLK_j;
        public const int KEY_L = Sdl.SDLK_l;
        public const int KEY_Z= Sdl.SDLK_z;
        public const int KEY_C = Sdl.SDLK_c;
        public const int KEY_ENTER = Sdl.SDLK_RETURN;

        short screenWidth;
        short screenHeight;
        short colorDepth;
        IntPtr screen;

        public Hardware(short width, short height, short depth, bool fullScreen)
        {

            screenWidth = width;
            screenHeight = height;
            colorDepth = depth;

            int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT;
            if (fullScreen)
                flags = flags | Sdl.SDL_FULLSCREEN;

            Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
            screen = Sdl.SDL_SetVideoMode(screenWidth, screenHeight, colorDepth, flags);
            Sdl.SDL_Rect rect = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
            Sdl.SDL_SetClipRect(screen, ref rect);
            SdlTtf.TTF_Init();

        }

        ~Hardware()
        {
            Sdl.SDL_Quit();
        }

        public void DrawImage(Image img)
        {
            Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, img.ImageWidth,
                img.ImageHeight);
            Sdl.SDL_Rect target = new Sdl.SDL_Rect(img.X, img.Y,
                img.ImageWidth, img.ImageHeight);
            Sdl.SDL_BlitSurface(img.ImagePtr, ref source, screen, ref target);
        }
        public void DrawSprite(Image image, short xScreen, short yScreen, short x, short y, short width, short height)
        {
            Sdl.SDL_Rect src = new Sdl.SDL_Rect(x, y, width, height);
            Sdl.SDL_Rect dest = new Sdl.SDL_Rect(xScreen, yScreen, width, height);
            Sdl.SDL_BlitSurface(image.ImagePtr, ref src, screen, ref dest);
        }
        public void DrawInker(Inker inker)
        {
            Sdl.SDL_Rect src = new Sdl.SDL_Rect(0, 0, inker.Width, inker.Height);
            Sdl.SDL_Rect dest = new Sdl.SDL_Rect(inker.GetX(), inker.GetY(), inker.Width, inker.Height);
            Sdl.SDL_BlitSurface(inker.GetSprite().ImagePtr, ref src, screen, ref dest);
        }
        public void UpdateScreen()
        {
            Sdl.SDL_Flip(screen);
        }
        public int KeyPressed()
        {
            int pressed = -1;

            Sdl.SDL_PumpEvents();
            Sdl.SDL_Event keyEvent;
            if (Sdl.SDL_PollEvent(out keyEvent) == 1)
            {
                if (keyEvent.type == Sdl.SDL_KEYDOWN)
                {
                    pressed = keyEvent.key.keysym.sym;
                }
            }

            return pressed;
        }
        public bool IsKeyPressed(int key) 
        {
            bool pressed = false;
            Sdl.SDL_PumpEvents();
            Sdl.SDL_Event evt;
            Sdl.SDL_PollEvent(out evt);
            int numKeys;
            byte[] keys = Sdl.SDL_GetKeyState(out numKeys);
            if (keys[key] == 1)
                pressed = true;
            return pressed;
        }
        public void DrawPolygon(Polygon beam, byte r, byte g, byte b)
        {
            DrawRectangle(beam.Coordinates[0, 0], beam.Coordinates[0, 1],
                beam.Coordinates[1, 0], beam.Coordinates[1, 1],
                beam.Coordinates[2, 0], beam.Coordinates[2, 1],
                beam.Coordinates[3, 0], beam.Coordinates[3, 1],
                r, g, b, 255);
        }
        public void DrawPolygon(Polygon polygon, Random rnd)
        {
            DrawRectangle(polygon.Coordinates[0, 0], polygon.Coordinates[0, 1],
                polygon.Coordinates[1, 0], polygon.Coordinates[1, 1],
                polygon.Coordinates[2, 0], polygon.Coordinates[2, 1],
                polygon.Coordinates[3, 0], polygon.Coordinates[3, 1],
                (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), 255);
        }
        public void DrawTriangle(Triangle triangle, Random rnd)
        {
            DrawRectangle(triangle.Coordinates[0,0], triangle.Coordinates[0,1], 
                          triangle.Coordinates[1,0], triangle.Coordinates[1,1],
                          triangle.Coordinates[2,0], triangle.Coordinates[2,1],
                          triangle.Coordinates[2,0], triangle.Coordinates[2,1],
                          (byte)rnd.Next(0,256), (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), 255);
        }
        public void DrawTriangle(Triangle triangle)
        {
            Random rnd = new Random();
            DrawRectangle(triangle.Coordinates[0, 0], triangle.Coordinates[0, 1],
                          triangle.Coordinates[1, 0], triangle.Coordinates[1, 1],
                          triangle.Coordinates[2, 0], triangle.Coordinates[2, 1],
                          triangle.Coordinates[2, 0], triangle.Coordinates[2, 1],
                          (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), 255);
        }
        public void ClearScreen()
        {
            Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
            Sdl.SDL_FillRect(screen, ref source, 0);
        }
        public void ClearBottom()
        {
            Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, GameController.SCREEN_HEIGHT, screenWidth, (short)(screenHeight - GameController.SCREEN_HEIGHT));
            Sdl.SDL_FillRect(screen, ref source, 0);
        }
        public void WriteText(string text, short x, short y,
        Font fontType)
        {
            byte r = 0, g = 255, b = 0;
            Sdl.SDL_Color color = new Sdl.SDL_Color(r, g, b);
            IntPtr textAsImage = SdlTtf.TTF_RenderText_Solid(fontType.GetFontType(),
            text, color);
            if (textAsImage == IntPtr.Zero)
                Environment.Exit(5);
            Sdl.SDL_Rect src = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
            Sdl.SDL_Rect dest = new Sdl.SDL_Rect(x, y, screenWidth, screenHeight);
            Sdl.SDL_BlitSurface(textAsImage, ref src, screen, ref dest);
        }

        public void DrawLine(short x, short y, short x2, short y2, byte r, byte g, byte b, byte alpha)
        {
            SdlGfx.lineRGBA(screen, x, y, x2, y2, r, g, b, alpha);
        }
        public void DrawRectangle(short x1, short y1, short x2, short y2, short x3, short y3, short x4, short y4, byte r, byte g, byte b, byte alpha)
        {
            short[] vx = { x1, x3, x4, x2 };
            short[] vy = { y1, y3, y4, y2 };
            SdlGfx.filledPolygonRGBA(screen, vx, vy, vx.Length, r, g, b, alpha);
        }

    }
}
