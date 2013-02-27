using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.World.Entities.Components
{
    public class SpawnerComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpawnerComponent"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public SpawnerComponent(Entity entity, EntitySpawner spawner) : base(entity)
        {
            this.Spawner = spawner;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the spawner.
        /// </summary>
        public EntitySpawner Spawner { get; private set; }
        #endregion
    }
}
