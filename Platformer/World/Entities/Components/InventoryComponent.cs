using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Items;

namespace Platformer.World.Entities.Components
{
    public class InventoryComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryComponent"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public InventoryComponent(Entity entity) : base(entity)
        {
            this.Items = new List<Item>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the items.
        /// </summary>
        public List<Item> Items { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Drops the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Destroy(Item item)
        {
            this.Items.Remove(item);

            DestroyedItem destroyedItem = new DestroyedItem(this.Entity, item);
            destroyedItem.Position = this.Entity.Position;

            this.Entity.Level.Add(destroyedItem);
        }
        #endregion
    }
}
