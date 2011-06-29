namespace ChelasInjection
{
    #region TiposEnum Auxiliares para TypeConfig<T> e Injector - _ ConstructionMode

    /// <summary>
    /// Enumerado para os modos de escolha de construtor.
    /// </summary>
    public enum ConstructionMode
    {
        DefaultAttributeOrMoreBindedArguments = 1,
        NoArgumentsConstructor = 2,
        WithConstructor = 3
    }

    #endregion
}