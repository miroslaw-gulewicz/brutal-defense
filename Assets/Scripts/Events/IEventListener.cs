public interface IEventListener
{
	void RegisterCancelEvent(CancelEventHandler handler);
	void UnRegisterCancelEvent(CancelEventHandler handler);

	public delegate void CancelEventHandler(IEventListener listener);
}