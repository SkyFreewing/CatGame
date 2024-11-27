using System;
using System.Collections.Generic;

namespace CatMerge
{
    internal class Systems : IExecuteSystem, IStartupSystem
    {
        readonly List<IExecuteSystem> _executeSystems = new List<IExecuteSystem>(); 
        readonly List<IStartupSystem> _startupSystems = new List<IStartupSystem>();

        public virtual Systems Add(ISystem system) 
        {
            if(system is IStartupSystem startupSystem) _startupSystems.Add(startupSystem);
            if(system is IExecuteSystem executeSystem) _executeSystems.Add(executeSystem);
            return this;
        }

        public void Startup()
        {
            for(var i = 0; i < _startupSystems.Count; i++)
                _startupSystems[i].Startup();
        }
        public void Execute()
        {
            for (var i = 0; i < _executeSystems.Count; i++)
                _executeSystems[i].Execute();
        }
    }
}
