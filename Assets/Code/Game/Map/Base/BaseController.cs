using Code.Game.Map.Controller;

namespace Code.Game.Map.Base
{
    public class BaseController<T> where T : BaseData
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

        protected MapController _mapController;

        public BaseController(MapController map)
        {
            _mapController = map;
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
}