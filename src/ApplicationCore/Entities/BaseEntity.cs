namespace Synword.ApplicationCore.Entities;

public abstract class BaseEntity 
{
    public virtual int Id { get; protected set; }
}

public abstract class BaseEntity<T>
{
    public virtual T Id { get; protected set; }
}
