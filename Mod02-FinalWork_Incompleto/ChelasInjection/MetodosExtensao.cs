using System; // adicionado using

namespace ChelasInjection
{
    #region Classe Auxiliar para TypeConfig<T> e Injector - MetodosExtensao

    /// <summary>
    /// Classe onde sao definidos todos os metodos de extensao que sao mais genericos (para qualquer projecto).
    /// </summary>
    static public class MetodosExtensao
    {
        static public bool IsPrimitiveOrString(this Type type)
        {
            return (type.IsPrimitive || (type == typeof (string)));
        }
    }

    #endregion
}