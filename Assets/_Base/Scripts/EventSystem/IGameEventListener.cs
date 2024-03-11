
public interface IGameEventListener
{
    void OnEventRaised();
}

public interface IGameEventListener<T>
{
    void OnEventRaised(T eventData);
}

public interface IGameEventListener<T, U>
{
    void OnEventRaised(T eventData1, U eventData2);
}
public interface IGameEventListener<T, U, R>
{
    void OnEventRaised(T eventData1, U eventData2, R eventData3);
}