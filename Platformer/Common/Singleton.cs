﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Common
{
    public static class Singleton<T> where T : class, new()
    {
        #region Fields
        private static Lazy<T> _lazy = new Lazy<T>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the static singleton instance.
        /// </summary>
        public static T Value
        {
            get { return _lazy.Value; }
        }
        #endregion
    }
}
