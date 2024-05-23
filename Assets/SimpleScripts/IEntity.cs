using Unity.VisualScripting;

namespace App.Runtime.Content
{
    public interface IEntity
    {
        T Get<T>() where T : class;
    }

}