using System;
using SplashKitSDK;
using System.Collections.Generic;
using System.Linq;

namespace Messy
{
    public abstract class Enemy
    {
        public double _x, _y;
        public double _angle;
        public Bitmap _shipBitmap;
        public Bullet _bullet = new Bullet();

        public List<Bullet> _bulletList = new List<Bullet>();

        // public Enemy()
        // { 
        //     Angle = 90; 
        //     _shipBitmap = SplashKit.BitmapNamed("Pegasi");
        // }

        public double X {get { return _x; } set { _x = value; }}
        public double Y{get { return _y; } set { _y = value; }}
        public double Angle{ get { return _angle; } set { _angle = value; }}
        public void Rotate(double amount)
        {
            _angle = (_angle + amount) % 360;
        }
        // public void Draw()
        // {
        //     _shipBitmap.Draw(_x, _y, SplashKit.OptionRotateBmp(_angle));
        //     _bullet.Draw();
        // }
        public abstract void Draw();
        public void Shoot()
        {
            Matrix2D anchorMatrix = SplashKit.TranslationMatrix(SplashKit.PointAt(_shipBitmap.Width / 2, _shipBitmap.Height / 2));

            // Move centre point of picture to origin
            Matrix2D result = SplashKit.MatrixMultiply(SplashKit.IdentityMatrix(), SplashKit.MatrixInverse(anchorMatrix));
            // Rotate around origin
            result = SplashKit.MatrixMultiply(result, SplashKit.RotationMatrix(_angle));
            // Move it back...
            result = SplashKit.MatrixMultiply(result, anchorMatrix);

            // Now move to location on screen...
            result = SplashKit.MatrixMultiply(result, SplashKit.TranslationMatrix(X, Y));

            // Result can now transform a point to the ship's location
            // Get right/centre
            Vector2D vector = new Vector2D();
            vector.X = _shipBitmap.Width;
            vector.Y = _shipBitmap.Height / 2;
            // Transform it...
            vector = SplashKit.MatrixMultiply(result, vector);
            _bullet = new Bullet(vector.X, vector.Y, Angle);
            _bulletList.Add(new Bullet(vector.X, vector.Y, Angle));
        }

        public void Update()
        {
            _bullet.Update();
        }

        public void Move(double amountForward, double amountStrafe)
        {
            Vector2D movement = new Vector2D();
            Matrix2D rotation = SplashKit.RotationMatrix(_angle);
            movement.X += amountForward;
            movement.Y += amountStrafe;
            movement = SplashKit.MatrixMultiply(rotation, movement);
            _x += movement.X;_y += movement.Y;
        }
    }


    public class LocalEnemy : Enemy
    {

        public LocalEnemy()
        {
            Angle = 90; 
           _shipBitmap = SplashKit.BitmapNamed("Pegasi");
        }
        public override void Draw()
        {
            _shipBitmap.Draw(_x, _y, SplashKit.OptionRotateBmp(_angle));
            _bullet.Draw();
        }
    }

    public class Boss : Enemy
    {

        public Boss()
        {
            Angle = 90; 
           _shipBitmap = SplashKit.BitmapNamed("Boss");
        }
        public override void Draw()
        {
            _shipBitmap.Draw(_x, _y, SplashKit.OptionRotateBmp(_angle));
            _bullet.Draw();
        }
    }
}