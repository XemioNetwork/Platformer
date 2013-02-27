using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Components;
using Platformer.Math;

namespace Platformer.World.Entities
{
    public abstract class EntitySpawner : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DustParticleEmitter"/> class.
        /// </summary>
        public EntitySpawner()
        {
            this.SpawnDelay = 5000;
            this.Enabled = true;

            this.LifeTime = -1;
            this.MaximumEntities = 4;
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DustParticleEmitter"/> is enabled.
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Gets or sets the life time.
        /// </summary>
        public float LifeTime { get; set; }
        /// <summary>
        /// Gets or sets the spawn delay between 2 entity spawns.
        /// </summary>
        public float SpawnDelay { get; set; }
        /// <summary>
        /// Gets or sets the spawn radius.
        /// </summary>
        public float SpawnRadius { get; set; }
        /// <summary>
        /// Gets or sets the maximum entities.
        /// </summary>
        public int MaximumEntities { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Tries to spawn an entity.
        /// </summary>
        private void TrySpawnEntity()
        {
            int entityCount = this.Level.Entities
                .Where(e => (e.Position - this.Position).Length < this.SpawnRadius &&
                            e.HasComponent<SpawnerComponent>() &&
                            e.GetComponent<SpawnerComponent>().Spawner == this)
                .Count();

            if (entityCount < this.MaximumEntities)
            {
                Entity entity = this.CreateEntity();
                entity.Components.Add(new SpawnerComponent(entity, this));

                this.SpawnEntity(entity);
            }
        }
        /// <summary>
        /// Creates an entity.
        /// </summary>
        public virtual Entity CreateEntity()
        {
            return null;
        }
        /// <summary>
        /// Spawns an entity.
        /// </summary>
        public virtual void SpawnEntity(Entity entity)
        {
            this.Spawn(entity);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Enabled)
            {
                this._elapsed += elapsed;
                while (this._elapsed >= this.SpawnDelay && this.SpawnDelay > 0)
                {
                    this._elapsed -= this.SpawnDelay;
                    this.TrySpawnEntity();
                }

                if (this.SpawnDelay == 0)
                {
                    this._elapsed = 0;
                    this.TrySpawnEntity();
                }

                if (this.LifeTime >= 0)
                {
                    this.LifeTime -= elapsed;
                    if (this.LifeTime <= 0)
                    {
                        this.Destroy();
                    }
                }
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
