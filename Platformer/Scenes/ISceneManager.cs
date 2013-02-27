using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Game;

namespace Platformer.Scenes
{
    public interface ISceneManager : IGameHandler
    {
        /// <summary>
        /// Adds the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        void Add(Scene scene);
        /// <summary>
        /// Removes the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        void Remove(Scene scene);
    }
}
