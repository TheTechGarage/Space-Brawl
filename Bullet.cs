using System;
using SplashKitSDK;
using System.Collections.Generic;
using System.Linq;

namespace Messy
{
    public class Bullet
    {
        private Bitmap _bulletBitmap;
        private double _x, _y, _angle;
        private bool _active = false;

        public List<Enemy> removeEnemy = new List<Enemy>();
        public Bullet(double x, double y, double angle)
        {
            _bulletBitmap = SplashKit.BitmapNamed("Bullet");
            _x = x - _bulletBitmap.Width / 2;
            _y = y - _bulletBitmap.Height / 2;
            _angle = angle;
            _active = true;
        }

        public Bullet()
        {
            _active = false;
        }

        public void Update()
        {
            const int TOAST = 8;
            Vector2D movement = new Vector2D();
            Matrix2D rotation = SplashKit.RotationMatrix(_angle);
            movement.X += TOAST;
            movement = SplashKit.MatrixMultiply(rotation, movement);
            _x += movement.X;
            _y += movement.Y;
            //Console.WriteLine("It is" + _y);
            if ((_x > SplashKit.ScreenWidth() || _x < 0) || _y > SplashKit.ScreenHeight() || _y < 0)
            {
                _active = false;
            }
        }

        public void Draw()
        {
            if (_active)
            {
                DrawingOptions options = SplashKit.OptionRotateBmp(_angle);
                _bulletBitmap.Draw(_x, _y, options);
            }

        }

        public Circle CollisionCircle
        {
            get
            {
                
                return SplashKit.CircleAt(  _x-26,_y, 200);
                
            }
        }
        public bool CollidedWith(Player p, Bullet b)
        {
            //return _bulletBitmap.CircleCollision(_x, _y, p.CollisionCircle);
            //Console.WriteLine("bull" + (_x - 62));
            //return _bulletBitmap.CircleCollision(_x, _y, p.CollisionCircle);
            // return SplashKit.CirclesIntersect(p.CollisionCircle,b.CollisionCircle);
            b.Update();
             //Console.WriteLine( _bulletBitmap.BitmapCollision(_x,_y,p._shipBitmap,p._x, p._y));
            // if(b._y > 530)
            // {
            //     b._y = 530;
            // }
            // if(p._x == (b._x-26) && b._y == 530)
            // {
            //     return true;
            // }
            // return false;
           return _bulletBitmap.BitmapCollision(_x,_y,p._shipBitmap,p._x, p._y);
        }

        public bool CollidedWith(Enemy e, Bullet b)
        {
            //return _bulletBitmap.CircleCollision(_x, _y, p.CollisionCircle);
            //Console.WriteLine("bull" + (_x - 62));
            //return _bulletBitmap.CircleCollision(_x, _y, p.CollisionCircle);
            // return SplashKit.CirclesIntersect(p.CollisionCircle,b.CollisionCircle);
            b.Update();
            
           return _bulletBitmap.BitmapCollision(_x,_y,e._shipBitmap,e.X, e.Y);;
        }

        

        
    }
}