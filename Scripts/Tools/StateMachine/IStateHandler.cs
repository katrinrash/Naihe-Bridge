
/// <summary>
/// Provides a communication bridge between state execution logic and external systems (such as a state machine)
/// </summary>

public interface IStateHandler
{
    public void CompleteStep();
}
