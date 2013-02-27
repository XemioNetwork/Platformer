using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Components;
using Platformer.Plugins;

namespace Platformer.Rendering
{
    public static class RenderingPipeline
    {
        #region Methods
        /// <summary>
        /// Sets up the rendering pipeline and creates related components.
        /// </summary>
        public static void Setup(string library, IntPtr windowHandle)
        {
            PluginLoader<IGraphicsProvider> loader = new PluginLoader<IGraphicsProvider>();
            loader.LoadAssembly(library);

            IGraphicsProvider provider = loader.Plugins.FirstOrDefault();
            if (provider == null)
            {
                throw new InvalidOperationException("The requested assembly does not contain any graphics providers.");
            }

            //Set the window handle to enable rendering.
            provider.Handle = windowHandle;

            ComponentManager.Instance.Add(new ValueProvider<ITextureFactory>(provider.TextureFactory));
            ComponentManager.Instance.Add(new ValueProvider<IRenderManager>(provider.RenderManager));

            ComponentManager.Instance.Add(new ValueProvider<IGraphicsProvider>(provider));
        }
        #endregion
    }
}
