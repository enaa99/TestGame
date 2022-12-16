public abstract class State<T>
{
    protected PlayerController player;


    protected State(BossController dragon, PlayerController player)
    {
        this.player = player;
    }
       
    public virtual void OnEnter(T sender)
    {

    }
    public virtual void OnExit(T sender)
    {

    }
    public virtual void OnFixedUpdate(T sender)
    {

    }
    public virtual void OnUpdate(T sender)
    {

    }
}
