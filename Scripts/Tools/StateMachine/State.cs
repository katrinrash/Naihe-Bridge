
/// <summary>
/// Represents a single state inside the state machine.
/// </summary>

public class State 
{
    private readonly IExecutable _executable;
    public int ID {  get; private set; }

    public State(IExecutable executable, int id, IStateHandler stateHandler)
    {
        _executable = executable;
        _executable.StateHandler = stateHandler;   
        ID = id;
    }

    public virtual void Enter() 
    { 
        _executable.Execute();
    }
    public virtual void Exit() 
    {
        _executable.EndExecution();
    }
}
