
namespace Aim
{
    public static class TargetingSystemFactory
    {
        public static TargetingSystem Supply(TargetingSystemType targetingSystemType)
        {
            TargetingSystem targetingSystem;
            switch (targetingSystemType)
            {    
                case TargetingSystemType.FIFO:
                    targetingSystem = new QueueTargetingSystem(QueueTargetingSystem.NextTarget.FIRST);
                    break;
                case TargetingSystemType.LIFO:
                    targetingSystem = new QueueTargetingSystem(QueueTargetingSystem.NextTarget.LAST);
                    break;
                case TargetingSystemType.PRIORITY:
                    targetingSystem = new PriorityTargetingSystem();
                    break;
                case TargetingSystemType.SIMPLE:
                default:
                    targetingSystem = new TargetingSystem();
                    break;
            }

            targetingSystem.TargetingSystemID = (int)targetingSystemType;
            return targetingSystem;
        }

        public static TargetingSystemType AsTargetingSystemType(TargetingSystem targetingSystem) => (TargetingSystemType)targetingSystem.TargetingSystemID;
    }

    public enum TargetingSystemType : int
    {
        SIMPLE,
        FIFO,
        LIFO,
        PRIORITY
    }
}