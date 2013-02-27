using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities;
using Platformer.World.Entities.Components;
using Platformer.Math.Collision;
using Platformer.Math;
using Platformer.World.Entities.Events;
using Platformer.World.TileEngine;
using Platformer.World.Entities.Components.Physics;

namespace Platformer.World
{
    public class LevelCollision
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelCollision"/> class.
        /// </summary>
        /// <param name="level">The level.</param>
        public LevelCollision(Level level)
        {
            this._collisionManager = new CollisionManager();

            this.Level = level;
            this.Depth = 2;
        }
        #endregion

        #region Fields
        private CollisionManager _collisionManager;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the level.
        /// </summary>
        public Level Level { get; private set; }
        /// <summary>
        /// Gets or sets the amount of surrounding tiles to check for collision.
        /// </summary>
        public int Depth { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            List<Entity> collidableEntities = this.Level.Entities
                .Where(entity => entity.HasComponent<CollidableComponent>())
                .ToList();

            List<Entity> dirtyEntities = collidableEntities
                .Where(entity => entity.GetComponent<CollidableComponent>().Dirty)
                .ToList();

            for (int i = 0; i < dirtyEntities.Count; i++)
            {
                for (int j = 0; j < collidableEntities.Count; j++)
                {
                    Entity currentEntity = dirtyEntities[i];
                    Entity targetEntity = collidableEntities[j];

                    if (currentEntity == targetEntity) continue;

                    CollidableComponent collisionA = currentEntity.GetComponent<CollidableComponent>();
                    CollidableComponent collisionB = targetEntity.GetComponent<CollidableComponent>();
                    
                    Vector2 velocity = PhysicsHelper.MeasureForce(currentEntity);

                    CollisionResult result = this._collisionManager.Intersect(
                        collisionA.BoundingBox + currentEntity.Position, 
                        collisionB.BoundingBox + targetEntity.Position,
                        velocity);
                    
                    if (result.Intersecting)
                    {
                        foreach (Type excludedType in collisionA.ExcludedTypes)
                        {
                            if (excludedType.IsAssignableFrom(targetEntity.GetType()))
                            {
                                result.MinimumTranslation = Vector2.Zero;
                            }
                        }
                        foreach (Type excludedType in collisionB.ExcludedTypes)
                        {
                            if (excludedType.IsAssignableFrom(currentEntity.GetType()))
                            {
                                result.MinimumTranslation = Vector2.Zero;
                            }
                        }

                        if (collisionA.Seperatation && collisionB.Seperatation)
                        {
                            if (!collisionA.Static && collisionB.Static)
                            {
                                currentEntity.Position += result.MinimumTranslation;
                            }
                            else if (collisionA.Static && !collisionB.Static)
                            {
                                targetEntity.Position -= result.MinimumTranslation;
                            }
                            else
                            {
                                currentEntity.Position += result.MinimumTranslation / 2.0f;
                                targetEntity.Position -= result.MinimumTranslation / 2.0f;
                            }
                        }
                        else
                        {
                            result.MinimumTranslation = Vector2.Zero;
                        }

                        EntityCollisionEvent collisionEvent = new EntityCollisionEvent(
                            currentEntity, targetEntity, result);

                        currentEntity.Send(collisionEvent);
                        targetEntity.Send(collisionEvent);
                    }
                }
            }

            foreach (Entity entity in dirtyEntities)
            {
                Vector2 velocity = PhysicsHelper.MeasureForce(entity);

                CollidableComponent component = entity.GetComponent<CollidableComponent>();
                Rectangle bounds = component.BoundingBox + entity.Position;

                int tileX = (int)bounds.X / Tile.Width;
                int tileY = (int)bounds.Y / Tile.Height;

                int width = (int)bounds.Width / Tile.Width;
                int height = (int)bounds.Height / Tile.Height;

                if (entity.Level != null)
                {
                    TileMap map = entity.Level.TileLayer[entity.LayerIndex];

                    for (int x = -this.Depth; x < width + this.Depth; x++)
                    {
                        for (int y = -this.Depth; y < height + this.Depth; y++)
                        {
                            TileReference reference = map.GetTile(tileX + x, tileY + y);

                            if (reference != TileReference.Empty && !reference.Tile.Walkable)
                            {
                                CollisionResult result = this._collisionManager.Intersect(
                                    component.BoundingBox + entity.Position,
                                    reference.CalculateDestination(),
                                    velocity);

                                if (result.Intersecting)
                                {
                                    entity.Position += result.MinimumTranslation;
                                    entity.Send(new TileCollisionEvent(entity, reference, result));

                                    reference.Tile.Collide(entity, reference);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
