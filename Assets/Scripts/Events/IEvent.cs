
namespace Event
{

    public interface IEvent
    {
        
    }


    public struct EventData
    {
        public long broadcasterId;
        public int intField1;
        public int intField2;
        public float floatField1;
    }

}