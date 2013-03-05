using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Common.Randomization;
using System.Windows.Forms;

namespace Platformer
{
    public static class Shared
    {
        #region Constructors
        /// <summary>
        /// Initializes the <see cref="Shared"/> class.
        /// </summary>
        static Shared()
        {
            Shared.Random = new RandomProxy();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the random.
        /// </summary>
        public static IRandom Random { get; set; }
        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        public static IntPtr Handle { get; set; }
        /// <summary>
        /// Gets or sets the width of the screen.
        /// </summary>
        public static int ScreenWidth
        {
            get { return Shared.GetSurface().ClientSize.Width; }
        }
        /// <summary>
        /// Gets or sets the height of the screen.
        /// </summary>
        public static int ScreenHeight
        {
            get { return Shared.GetSurface().ClientSize.Height; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// The placeholder control.
        /// </summary>
        private static Control _placeholder;
        /// <summary>
        /// Gets the placeholder.
        /// </summary>
        private static Control GetPlaceholder()
        {
            if (_placeholder == null)
            {
                _placeholder = new Control();
                _placeholder.CreateControl();
            }

            return _placeholder;
        }
        /// <summary>
        /// Gets the surface.
        /// </summary>
        public static Control GetSurface()
        {
            Control control = Control.FromHandle(Shared.Handle);
            if (control == null)
            {
                control = Shared.GetPlaceholder();
            }

            return control;
        }
        #endregion
    }
}
