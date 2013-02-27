using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.World.Entities;
using Platformer.World.Entities.Items;
using Platformer.World.Entities.Components;
using Platformer.World.TileEngine;
using Platformer.World.Entities.Puzzle;
using Platformer.Math;

namespace Platformer.World.Tiles
{
    public class LockTile : StaticTile
    {
        #region Properties
        /// <summary>
        /// Gets the type of the item that is needed to open the lock.
        /// </summary>
        public virtual Type KeyType
        {
            get { return typeof(Item); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Opens this instance.
        /// </summary>
        public void Open(Level level, TileReference reference)
        {
            Lock lockEntity = new Lock(level, reference);
            level.Add(lockEntity);

            lockEntity.Open();
            lockEntity.Texture = reference.Tile.Texture;

            lockEntity.Position = new Vector2(
                reference.X * Tile.Width,
                reference.Y * Tile.Height);

            reference.Destroy();
        }
        /// <summary>
        /// Called when the tile collides with an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="reference">The reference.</param>
        public override void Collide(Entity entity, TileReference reference)
        {
            if (entity.HasComponent<InventoryComponent>())
            {
                InventoryComponent inventory = entity.GetComponent<InventoryComponent>();
                Item key = inventory.Items.FirstOrDefault(
                    item => this.KeyType.IsAssignableFrom(item.GetType()));

                if (key != null)
                {
                    this.Open(entity.Level, reference);
                    inventory.Destroy(key);
                }
            }
        }
        #endregion
    }
}
