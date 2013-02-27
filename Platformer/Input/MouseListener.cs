using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Components;
using System.Windows.Forms;
using Platformer.Math;

namespace Platformer.Input
{
    public class MouseListener : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseListener"/> class.
        /// </summary>
        public MouseListener(IntPtr handle)
        {
            Control control = Control.FromHandle(handle);

            control.MouseMove += new MouseEventHandler(control_MouseMove);
            control.MouseDown += new MouseEventHandler(control_MouseDown);
            control.MouseUp += new MouseEventHandler(control_MouseUp);

            this._buttonStates = new Dictionary<MouseButtons, bool>();
        }
        #endregion

        #region Fields
        private Dictionary<MouseButtons, bool> _buttonStates;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the relative mouse position.
        /// </summary>
        public Vector2 Position { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the state for the specified button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="state">The button state.</param>
        private void SetState(MouseButtons button, bool state)
        {
            if (!this._buttonStates.ContainsKey(button))
            {
                this._buttonStates.Add(button, state);
            }

            this._buttonStates[button] = state;
        }
        /// <summary>
        /// Determines whether a mouse button is held.
        /// </summary>
        /// <param name="button">The button.</param>
        public bool IsButtonHeld(MouseButtons button)
        {
            return this._buttonStates.ContainsKey(button) && this._buttonStates[button];
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the MouseMove event of the attached control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            this.Position = new Vector2(e.X, e.Y);
        }
        /// <summary>
        /// Handles the MouseDown event of the attached control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            this.SetState(e.Button, true);
        }
        /// <summary>
        /// Handles the MouseUp event of the attached control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            this.SetState(e.Button, false);
        }
        #endregion
    }
}
