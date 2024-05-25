using Unity.VisualScripting;

namespace App.SimplesScipts
{
    public interface IEntity
    {
        T Get<T>() where T : class;
    }

}