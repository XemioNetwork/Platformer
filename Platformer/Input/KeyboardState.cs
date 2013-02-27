using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Platformer.Input
{
    public class KeyboardState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardState"/> class.
        /// </summary>
        public KeyboardState()
        {
            this.KeyStates = new Dictionary<Keys, bool>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the key states.
        /// </summary>
        internal Dictionary<Keys, bool> KeyStates { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the specified key is held.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyDown(Keys key)
        {
            return this.KeyStates.ContainsKey(key) && this.KeyStates[key];
        }
        /// <summary>
        /// Determines whether the specified key is up.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyUp(Keys key)
        {
            return !this.KeyStates.ContainsKey(key) || !this.KeyStates[key];
        }
        #endregion
    }
}
