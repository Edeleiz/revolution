using Code.Game.Map.Controller;
using Zenject;

namespace Code.Game.Map.Base
{
    public class BaseController : IInitializable
    {
        public virtual void Initialize()
        {
            
        }

        protected virtual void AddDataListeners()
        {
            
        }

        protected virtual void RemoveDataListeners()
        {
            
        }

        protected virtual void CommitData()
        {
        
        }

        protected virtual void Clear()
        {
        
        }
    }
    
    public class BaseController<T> : BaseController where T : BaseData
    {
        private T _data;
        public T Data
        {
            get { return _data; }
            set
            {
                if (_data != null)
                    RemoveDataListeners();
                
                _data = value;

                if (_data == null)
                {
                    Clear();
                    return;
                }
                
                AddDataListeners();
                CommitData();
            }
        }
    }
}