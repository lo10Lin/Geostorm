using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Geostorm
{
    class GameData
    {
        public GameData()
        {
            entities.Add(Player);
        }

        public bool Start = false;
        public bool Pause = false;

        public int Score = 0;
        public int BestScore = 0;

        public EntityPlayer Player = new EntityPlayer();

        List<Entity> entities = new List<Entity>();
        List<EntityBullet> bullets = new List<EntityBullet>();
        List<EntityEnemy> enemies = new List<EntityEnemy>();
        List<EntityBlackHole> blackHoles = new List<EntityBlackHole>();

        public IEnumerable<Entity> Entities { get { return entities; } }
        public IEnumerable<EntityBullet> Bullets { get { return bullets; } }
        public IEnumerable<EntityEnemy> Enemies { get { return enemies; } }
        public IEnumerable<EntityBlackHole> BlackHoles { get { return blackHoles; } }

        List<EntityEnemy> addedEnemies = new List<EntityEnemy>();
        List<EntityBullet> addedBullets = new List<EntityBullet>();
        List<EntityBlackHole> addedBlackHoles = new List<EntityBlackHole>();

        public void AddEnemyDelayed(EntityEnemy enemy)
        {
            addedEnemies.Add(enemy);
        }
        public void AddBulletDelayed(EntityBullet bullet)
        {
            addedBullets.Add(bullet);
        }
        public void AddBackHoleDelayed(EntityBlackHole blackHole)
        {
            addedBlackHoles.Add(blackHole);
        }
        public void Synchronize()
        {
            // Add new entities
            entities.AddRange(addedEnemies);
            entities.AddRange(addedBullets);

            enemies.AddRange(addedEnemies);
            bullets.AddRange(addedBullets);

            // Clear transient lists
            addedEnemies.Clear();
            addedBullets.Clear();

            entities.RemoveAll(entity => entity.isDead);
            enemies.RemoveAll(entity => entity.isDead);
            bullets.RemoveAll(entity => entity.isDead);

        }
        public void Reset(Vector2 ScreenSize)
        {
            Player = new EntityPlayer();
            Player.pos.X = ScreenSize.X / 2;
            Player.pos.Y = ScreenSize.Y / 2;

            if (Score > BestScore)
                BestScore = Score;
            Score = 0;

            entities.Clear();
            bullets.Clear();
            enemies.Clear();
            blackHoles.Clear();
            addedEnemies.Clear();
            addedBullets.Clear();
            addedBlackHoles.Clear();

            entities.Add(Player);

            Start = false;
        }
    }
}
