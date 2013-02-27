using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Platformer.Game;
using Platformer.Components;
using Platformer.Scenes;
using System.Reflection;
using Platformer.Rendering;
using Platformer.Events;
using Platformer.Input;

namespace Platformer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Form form = new Form();
            form.ClientSize = new Size(750, 530);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Platformer";
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            Shared.Handle = form.Handle;

            RenderingPipeline.Setup("Platformer.GDIPlus.dll", form.Handle);
            Program.SetupGame(form.Handle);

            Application.Run(form);
        }

        static void SetupGame(IntPtr handle)
        {            
            SceneManager sceneManager = new SceneManager();
            sceneManager.Add(new GameOverScene());

            ComponentManager.Instance.Add(new GameLoop());
            ComponentManager.Instance.Add(new EventManager());

            ComponentManager.Instance.Add(new KeyListener(handle));
            ComponentManager.Instance.Add(new MouseListener(handle));

            ComponentManager.Instance.Add(new DebugComponent());

            ComponentManager.Instance.Add(sceneManager);

            ComponentManager.Instance.Finalize();

            Art.LoadContent(ComponentManager.Instance.GetComponent<ITextureFactory>());
            Program.SetupLoop();
        }

        static void SetupLoop()
        {
            GameLoop gameLoop = ComponentManager.Instance.GetComponent<GameLoop>();
            gameLoop.TargetFrameTime = 16.666;
            gameLoop.TargetTickTime = 16.666;
            gameLoop.Run();
        }
    }
}
