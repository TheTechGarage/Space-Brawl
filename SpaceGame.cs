using System;
using Messy;
using SplashKitSDK;
using System.Collections.Generic;
using System.Linq;
namespace Messy
{
    



    public class SpaceGame
    {
        private Player _player;
        private Window _gameWindow;

        private Bullet _bullet = new Bullet();
        private List<Enemy> _enemyList = new List<Enemy>();
        private List<Enemy> _bossList = new List<Enemy>();

        
        private Enemy _enemy;
        private Bitmap bg;

        private Bitmap bg2;
        public bool eFlag = false;

        public bool bFlag = false;
        int posX = 0;
        int posY = 0;
        int pos = -740;

        static int counter;
        static int counter2;

        static int counterboss2;
        public SpaceGame()
        {
            _gameWindow = new Window("SpaceBrawl", 600, 600);
            Load();
            _player = new Player { X = _gameWindow.Width-70, Y = _gameWindow.Height-70 };
            //_enemy = new Enemy { X = 70, Y = 70 };
            
            for(int i =0; i < 1; i++)
            {
                _enemyList.Add(new LocalEnemy { X = 0 + 100 * i, Y = 50 });
                _bossList.Add(new Boss { X = 0 + 100 * i, Y = 50 });
            }
        }
        private void Load()
        {
            SplashKit.LoadBitmap("Bullet", "Fire.png");
            SplashKit.LoadBitmap("Gliese", "Gliese.png");
            SplashKit.LoadBitmap("Pegasi", "Pegasi.png");
            SplashKit.LoadBitmap("Aquarii", "Aquarii.png");
            SplashKit.LoadBitmap("Boss", "b1.png");
            bg = SplashKit.LoadBitmap("BG", "bg4.jpg");
            bg2 = SplashKit.LoadBitmap("BG2", "bg5.jpg");
        }
        public void Update()
        {
            
            while (!_gameWindow.CloseRequested)
            {
                EnemyStayOnWindow(_gameWindow);
                //BossStayOnWindow(_gameWindow);
                SplashKit.ProcessEvents();
                StayOnWindow(_gameWindow);
                
                if (SplashKit.KeyDown(KeyCode.UpKey))
                {
                    _player.Move(0, 4);
                    
                }
                if (SplashKit.KeyDown(KeyCode.DownKey))
                {
                    _player.Move(0, -4);
                    
                }
                // if (SplashKit.KeyDown(KeyCode.LeftKey))
                // {
                //     _enemy.Move(0,4);
                // }
                // if (SplashKit.KeyDown(KeyCode.RightKey))
                // {
                //     _enemy.Move(0,-4);
                // }
                // if (SplashKit.KeyDown(KeyCode.LeftKey))
                // {
                //     a.Rotate(-4);
                // }
                // if (SplashKit.KeyDown(KeyCode.RightKey))
                // {
                //     a.Rotate(4);
                // }
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    _player.Shoot();
                     
                }

                    //_enemy.Move(0,-4);
                    if(eFlag)
                    {
                        foreach(var item in _enemyList)
                        {
                            item.Move(0, 10 * SplashKit.Rnd());
                        }
                        //_enemy.Move(0,4);
                    }else
                    {
                        foreach(var item in _enemyList)
                        {
                            item.Move(0, -10 * SplashKit.Rnd());
                        }
                        //_enemy.Move(0,-4);
                    }


                    // if(bFlag)
                    // {
                    //     foreach(var item in _bossList)
                    //     {
                    //         item.Move(0, 10 * SplashKit.Rnd());
                    //     }
                    //     //_enemy.Move(0,4);
                    // }else
                    // {
                    //     foreach(var item in _bossList)
                    //     {
                    //         item.Move(0, -10 * SplashKit.Rnd());
                    //     }
                    //     //_enemy.Move(0,-4);
                    // }
                    _player.Update();   
                    //_enemy.Update();
                    if(_enemyList.Count < 1)
                    {
                        _enemyList.Add(new LocalEnemy { X = 0 + 50, Y = 50 });
                    }
                    foreach(var item in _enemyList)
                    {
                        item.Update();
                    }

                    // foreach(var item in _bossList)
                    // {
                    //     item.Update();
                    // }

                    //_bossList.ElementAt(0).Update();
                    
                    Draw();

                    checkHit(_player);

                    checkHit(_enemyList);

                    //checkHitBoss(_bossList);

                   
            }
                _gameWindow.Close();_gameWindow = null;
        }

        
        private void Draw()
        {
            posY = pos;

            Timer t = new Timer("BG Timer");
            t.Start();
            SplashKit.Delay(100);
            
            _gameWindow.Clear(Color.Black);
            if(t.Ticks/1000<1)
            {
                 pos+=2;
                 if(pos == 10){
                     pos = -740;
                    //_gameWindow.DrawBitmap(bg2, posX,posY);
                 }
            }
            _gameWindow.DrawBitmap(bg, posX,posY);
           //_gameWindow.DrawBitmap(bg, 0,0);
            _player.Draw();
            foreach(var item in _enemyList)
            {
                item.Draw();
            }
            if(_player.score % 5 == 0)
            {
                //_bossList.Add(new Boss { X = 0 + 350, Y = 50 });
                _bossList.ElementAt(0).Draw();
            }
            // if(_player.score == 10)
            // {
            //     _bossList.Add(new Boss { X = 0 + 250, Y = 50 });
                
            // }

            //_bossList.ElementAt(0).Draw();
            
            //_enemy.Draw();
            _gameWindow.Refresh(60);
        }

        public void StayOnWindow(Window w)
        {
            const double gap = 10;
            if(_player.X < gap){
                _player.X = gap;
            }else if( _player.X > w.Width - 90){
                _player.X = w.Width - 90; 
            }
        }

        public void EnemyStayOnWindow(Window w)
        {
            const double gap = 10;
            // if(_enemy.X < gap){
            //     _enemy.X = gap;
            //     eFlag = false;
            //     _enemy.Shoot();
                
            // }else if( _enemy.X > w.Width - 90){
            //     _enemy.X = w.Width - 90;
            //     eFlag = true;
            //     _enemy.Shoot();
                
            // }
            foreach (var item in _enemyList)
            {
                if(item.X < gap)
                {
                    item.X = gap;
                    eFlag = false;
                    //_enemy.Shoot();
                    for(int i =0; i< _enemyList.Count; i++)
                    _enemyList.ElementAt(i).Shoot();
                
                }else if( item.X > w.Width - 90)
                {
                    item.X = w.Width - 90;
                    eFlag = true;
                    //_enemy.Shoot();
                    for(int i = 0; i< _enemyList.Count; i++)
                    _enemyList.ElementAt(i).Shoot();
                
                }
            }
            
        }


        public void BossStayOnWindow(Window w)
        {
            const double gap = 10;
            // if(_enemy.X < gap){
            //     _enemy.X = gap;
            //     eFlag = false;
            //     _enemy.Shoot();
                
            // }else if( _enemy.X > w.Width - 90){
            //     _enemy.X = w.Width - 90;
            //     eFlag = true;
            //     _enemy.Shoot();
                
            // }
            foreach (var item in _bossList)
            {
                if(item.X < gap)
                {
                    item.X = gap;
                    bFlag = false;
                    //_enemy.Shoot();
                    for(int i =0; i< _bossList.Count; i++)
                    _bossList.ElementAt(i).Shoot();
                
                }else if( item.X > w.Width - 90)
                {
                    item.X = w.Width - 90;
                    bFlag = true;
                    //_enemy.Shoot();
                    for(int i = 0; i< _bossList.Count; i++)
                    _bossList.ElementAt(i).Shoot();
                
                }
            }
            
        }


        void checkHit(Player player)
        {
            bool playerHit = false;
             
            int lifeGone;
            //Console.WriteLine("Inside Hit");
            for(int i = 0; i<_enemyList.Count; i++){
            foreach (var item in _enemyList[i]._bulletList)
            {
                if(item.CollidedWith(_player, item))
                {
                    playerHit = true;
                    break;
                }
            }
            }
            //
            if(playerHit)
            {
                playerHit = false;
                counter ++;
                if(counter > 4)
                {
                    lifeGone = _player._life--;
                    counter = 0;
                if(lifeGone<=0)
                {
                   SplashKit.CloseCurrentWindow();
                }else
                {
                    _player.RemoveLife(lifeGone);
                }
                }
            }
            //Console.WriteLine("Hit out");
        }

        void checkHit(List<Enemy> enemy)
        {
            List<Enemy> RemoveEnemy = new List<Enemy>();
            bool enemyHit = false;
            //Console.WriteLine("Inside Hit");
            foreach (var item2 in _enemyList)
            {
                
            
            foreach (var item in _player._bulletList)
            {
                if(item.CollidedWith(item2, item))
                {
                    enemyHit = true;
                    _player.score++;
                    RemoveEnemy.Add(item2);
                    //Console.WriteLine("hit check" + enemyHit);
                    
                    break;
                }
            }
            }

            if(enemyHit)
            {
                enemyHit = false;
                counter2 ++;
                if(counter2 > 3)
                {
                    _player.score++;
                    counter2 = 0;
                
                }
            }
            
            foreach(var item in RemoveEnemy)
            {
                //Console.WriteLine("Hit out" + _enemyList.Count);
                enemy.Remove(item);
            }
            //Console.WriteLine("Hit out");
        }

        void AddBoss()
        {
            
            foreach(var item in _bossList)
            {
                item.Draw();
            }
        }



        void checkHitBoss(List<Enemy> boss)
        {
            List<Enemy> RemoveBoss = new List<Enemy>();
            bool bossHit = false;
            //Console.WriteLine("Inside Hit");
            foreach (var item2 in _bossList)
            {
                
            
            foreach (var item in _player._bulletList)
            {
                if(item.CollidedWith(item2, item))
                {
                    bossHit = true;
                    _player.score++;
                    RemoveBoss.Add(item2);
                    //Console.WriteLine("hit check" + enemyHit);
                    
                    break;
                }
            }
            }

            if(bossHit)
            {
                bossHit = false;
                counterboss2 ++;
                if(counterboss2 > 3)
                {
                    _player.score++;
                    counterboss2 = 0;
                
                }
            }
            
            foreach(var item in RemoveBoss)
            {
                //Console.WriteLine("Hit out" + _enemyList.Count);
                boss.Remove(item);
            }
            //Console.WriteLine("Hit out");
        }
    }
    
}