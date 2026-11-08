
/// <summary>
/// Defines the interface for executable logic, including execution, termination, and completion handling.
/// </summary>

public interface IExecutable
{
    public IStateHandler StateHandler { get; set; }

    #region Functions

    public void Execute();

    public void EndExecution();

    public void SetAsCompleted()
    {            
        StateHandler?.CompleteStep();
    }

    #endregion
}