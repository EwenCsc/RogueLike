using System;
using System.Collections.Generic;

namespace MapRogueLike.Engine
{
    class ToolBox : Singleton<ToolBox>
    {
        Dictionary<Type, Manager> managers = new Dictionary<Type, Manager>();

        public ToolBox()
        {

        }

        public void AddManager<T>() where T : Manager, new()
        {
            if(!managers.ContainsKey(typeof(T)))
            {
                managers[typeof(T)] = new T();
            }
        }

        public T Get<T>() where T : Manager, new()
        {
            if (managers.ContainsKey(typeof(T)))
            {
                return (T)managers[typeof(T)];
            }
            else
            {
                AddManager<T>();
                return Get<T>();
            }
        }
    }
}
