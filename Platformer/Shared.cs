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
            get { return Control.FromHandle(Shared.Handle).ClientSize.Width; }
        }
        /// <summary>
        /// Gets or sets the height of the screen.
        /// </summary>
        public static int ScreenHeight
        {
            get { return Control.FromHandle(Shared.Handle).ClientSize.Height; }
        }
        #endregion
    }
}
