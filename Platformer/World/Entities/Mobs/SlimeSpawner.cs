using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.World.Entities.Particles;

namespace Platformer.World.Entities.Mobs
{
    public class SlimeSpawner : EntitySpawner
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SlimeSpawner"/> class.
        /// </summary>
        public SlimeSpawner()
        {
            this.SpawnDelay = 2000;
            this.MaximumEntities = 1;
            this.SpawnRadius = 500;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the emitter.
        /// </summary>
        private void CreateEmitter()
        {
            DustParticleEmitter emitter = new DustParticleEmitter();
            emitter.MaximumEntities = 10;
            emitter.SpawnDelay = 0;
            emitter.VelocityMultiplier = 0.8f;
            emitter.LifeTime = 200;

            emitter.Position = this.Position + new Vector2(
                Art.SlimeSpawner.Width / 2, 
                Art.SlimeSpawner.Height / 2);

            this.Spawn(emitter);
        }
        /// <summary>
        /// Creates an entity.
        /// </summary>
        /// <returns></returns>
        public override Entity CreateEntity()
        {
            Slime slime = new Slime();

            slime.Position = this.Position;
            slime.Position += new Vector2(14, 16);

            return slime;
        }
        /// <summary>
        /// Spawns an entity.
        /// </summary>
        /// <param name="entity"></param>
        public override void SpawnEntity(Entity entity)
        {
            this.CreateEmitter();
            base.SpawnEntity(entity);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            this.RenderManager.Render(
                Art.SlimeSpawner,
                new Rectangle(
                    this.Position.X,
                    this.Position.Y, 
                    Art.SlimeSpawner.Width, 
                    Art.SlimeSpawner.Height));

            base.Render(elapsed);
        }
        #endregion
    }
}
