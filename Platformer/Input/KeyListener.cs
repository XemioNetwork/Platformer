using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Platformer.Components;

namespace Platformer.Input
{
    public class KeyListener : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyListener"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public KeyListener(IntPtr handle)
        {
            Control control = Control.FromHandle(handle);

            control.KeyDown += new KeyEventHandler(control_KeyDown);
            control.KeyUp += new KeyEventHandler(control_KeyUp);

            this._keyStates = new Dictionary<Keys, bool>();
        }
        #endregion

        #region Fields
        private Dictionary<Keys, bool> _keyStates;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current state.
        /// </summary>
        public KeyboardState State
        {
            get 
            {
                KeyboardState state = new KeyboardState();

                List<Keys> keys = this._keyStates.Keys.ToList();
                List<bool> states = this._keyStates.Values.ToList();

                for (int i = 0; i < keys.Count; i++)
                {
                    state.KeyStates.Add(keys[i], states[i]);
                }

                return state;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the state for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void SetState(Keys key, bool state)
        {
            if (!this._keyStates.ContainsKey(key))
            {
                this._keyStates.Add(key, state);
            }

            this._keyStates[key] = state;
        }
        /// <summary>
        /// Determines whether the specified key is held.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool IsKeyDown(Keys key)
        {
            return this._keyStates.ContainsKey(key) && this._keyStates[key];
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the KeyDown event of the attached handle.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            this.SetState(e.KeyCode, true);
        }
        /// <summary>
        /// Handles the KeyUp event of the attached handle.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void control_KeyUp(object sender, KeyEventArgs e)
        {
            this.SetState(e.KeyCode, false);
        }
        #endregion
    }
}
