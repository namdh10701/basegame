using UnityEngine;
namespace Online.Interface
{
	public abstract class BaseOnlineService
	{
		protected IPlayfabManager Manager { get; private set; }

		public abstract void LogSuccess(string message);
		public abstract void LogError(string error);

		public virtual void Initialize(IPlayfabManager manager)
		{
			Manager = manager;
		}

		protected void LogEvent(bool error, string message, string title = "")
		{
			var logEvent = $"[{title}] {message}";
			if (error)
			{
				Debug.LogError(logEvent);
			}
			else
			{
				Debug.Log(logEvent);
			}
		}
	}
}