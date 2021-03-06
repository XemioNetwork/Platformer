﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Common;
using Platformer.Plugins;

namespace Platformer.Components
{
    public class ComponentManager : IComponentProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentManager"/> class.
        /// </summary>
        public ComponentManager()
        {
            this._valueMappings = new Dictionary<Type, IValueProvider>();
            this._componentMappings = new Dictionary<Type, IComponent>();

            this._pluginLoader = new PluginLoader<IComponent>();

            this.Components = new List<IComponent>();
        }
        #endregion

        #region Fields
        private Dictionary<Type, IValueProvider> _valueMappings;
        private Dictionary<Type, IComponent> _componentMappings;

        private PluginLoader<IComponent> _pluginLoader;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        public List<IComponent> Components { get; private set; }
        #endregion

        #region Singleton
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        internal static ComponentManager Instance
        {
            get { return Singleton<ComponentManager>.Value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Constructs all loaded components.
        /// </summary>
        public void Finalize()
        {
            foreach (IComponent component in this.Components)
            {
                IConstructable constructable = component as IConstructable;
                if (constructable != null)
                {
                    constructable.Construct();
                }
            }
        }
        /// <summary>
        /// Loads all components found in the current directory.
        /// </summary>
        public void LoadComponents()
        {
            this.LoadComponents(".");
        }
        /// <summary>
        /// Loads all components found in the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void LoadComponents(string directory)
        {
            this._pluginLoader.LoadPlugins(directory);
            foreach (IComponent component in this._pluginLoader.Plugins)
            {
                this.Add(component);
            }
        }
        #endregion

        #region IComponentProvider Member
        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Add(IComponent component)
        {
            if (component is IValueProvider)
            {
                IValueProvider valueProvider = component as IValueProvider;
                this._valueMappings.Add(valueProvider.Identifier, valueProvider);
            }

            this._componentMappings.Add(component.GetType(), component);
            this.Components.Add(component);
        }
        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Remove(IComponent component)
        {
            if (component is IValueProvider)
            {
                IValueProvider valueProvider = component as IValueProvider;
                this._valueMappings.Remove(valueProvider.Identifier);
            }

            this._componentMappings.Remove(component.GetType());
            this.Components.Remove(component);
        }
        /// <summary>
        /// Gets a component by a specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : IComponent
        {
            if (this._componentMappings.ContainsKey(typeof(T)))
            {
                return (T)this._componentMappings[typeof(T)];
            }

            if (this._valueMappings.ContainsKey(typeof(T)))
            {
                return (T)this._valueMappings[typeof(T)].Value;
            }

            return default(T);
        }
        #endregion
    }
}
