namespace Code.Game.Map.Base
{
	public class BaseData 
	{
		protected object _source;
		public object Source
		{
			get { return _source; }
			set
			{
				_source = value;
				UpdateSource();
			}
		}

		protected virtual void UpdateSource()
		{
		
		}
	}

	public class BaseData<T> : BaseData
	{
		public T ParsedSource { get; protected set; }

		public void SetParsedSource(T source)
		{
			ParsedSource = source;
			UpdateSource();
		}
	}
}
