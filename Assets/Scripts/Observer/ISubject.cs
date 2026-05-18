
public interface ISubject 
{
    public void Attach(IObserver observer);

    public void Detached(IObserver observer);

    public void Notify();
}
