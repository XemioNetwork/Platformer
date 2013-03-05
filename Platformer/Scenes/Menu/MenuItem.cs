using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Scenes.Menu
{
    /// <summary>
    /// Menu item implementation.
    /// </summary>
    public class MenuItem
    {
        #region Properties
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public Action OnTrigger { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Triggers this instance.
        /// </summary>
        public void Trigger()
        {
            if (this.OnTrigger != null) this.OnTrigger();
        }
        #endregion
    }
}
