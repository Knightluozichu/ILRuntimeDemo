
namespace DownLoad
{
    public delegate void Callback();
    public delegate void Callback<T>(T t);
    public delegate void Callback<T, B>(T t, B b);
    public delegate void Callback<T, B, V>(T t, B b, V v);
    public delegate void Callback<T, B, V, A>(T t, B b, V v, A a);
}