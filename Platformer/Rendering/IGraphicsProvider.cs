using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Components;
using Platformer.Math;

namespace Platformer.Rendering
{
    public interface IGraphicsProvider : IComponent
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        IntPtr Handle { get; set; }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        ITextureFactory TextureFactory { get; }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        IRenderManager RenderManager { get; }
    }
}
