using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Platformer.Input;
using Platformer.Components;
using Platformer.Math;
using Platformer.World.Entities.Particles;
using Platformer.World.Entities.Animation;
using Platformer.World.Entities.Components.Physics;
using Platformer.World.Entities.Mobs;

namespace Platformer.World.Entities.Components
{
    public class InputComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputComponent"/> class.
        /// </summary>
        /// <param name="mob">The mob.</param>
        public InputComponent(Mob mob) : base(mob)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the last state.
        /// </summary>
        public KeyboardState LastState { get; private set; }
        /// <summary>
        /// Gets the mob entity.
        /// </summary>
        public Mob Mob
        {
            get { return this.Entity as Mob; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Enabled)
            {
                KeyListener keyboard = ComponentManager.Instance.GetComponent<KeyListener>();
                KeyboardState state = keyboard.State;

                PhysicsComponent component = this.Entity.GetComponent<PhysicsComponent>();
                component.Velocity = Vector2.Zero;

                if (state.IsKeyDown(Keys.Right))
                {
                    this.Mob.Right();
                }
                if (state.IsKeyDown(Keys.Left))
                {
                    this.Mob.Left();
                }
                if (state.IsKeyUp(Keys.Right) && state.IsKeyUp(Keys.Left))
                {
                    this.Mob.Decelerate();
                }
                if (state.IsKeyDown(Keys.Up))
                {
                    this.Mob.Jump();
                }

                this.LastState = keyboard.State;
            }
        }
        #endregion
    }
}
