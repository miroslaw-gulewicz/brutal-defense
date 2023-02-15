using Effect;

public interface IEffectCommand
{
    void DoCommand(IEffectContextHolder context);
}
