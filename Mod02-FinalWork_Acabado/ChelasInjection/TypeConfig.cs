using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; // ... adicionado using
using System.Text;
using ChelasInjection.Exceptions; // ... adicionado using

namespace ChelasInjection
{

    /// <summary>
    /// Classe que serve de base a classe genérica a ser utilizada na configuracao.
    /// </summary>
    public abstract class TypeConfig
    {
        public abstract Type TypeV
        {
            get;
        }

        #region campos e propriedades adicionados - TypeConfig<T> - para IActivationBinder<T>

        /// <summary>
        /// Campo que guarda modo de activacao.
        /// </summary>
        private ActivationMode? _activationMode = null;

        /// <summary>
        /// Propriedade que define PerRequest como default no modo de activacao, apenas se modo ainda nao foi definido.
        /// </summary>
        public ActivationMode ActivationModeV
        {
            get { return _activationMode ?? ActivationMode.PerRequest; }
            set { _activationMode = value; }
        }

        /// <summary>
        /// Propriedade que indica se o modo de activacao ja foi definido.
        /// </summary>
        public bool IsActivationModeDefined { get { return _activationMode.HasValue; } }

        #endregion

        #region metodos adicionados privados - SetActivationMode

        /// <summary>
        /// Define modo de activacao com valor indicado.
        /// </summary>
        /// <param name="activationMode">Nova definicao de modo de activacao.</param>
        /// <exception cref="DuplicateConfigurationException">Ocorre, se definicao for dupla, ou seja, se nao for 1a definicao.</exception>
        public void SetActivationMode(ActivationMode activationMode)
        {
            if (IsActivationModeDefined)
            {
                throw new DuplicateConfigurationException("Activação".ErrMsgAttemptedDoubleConfig(ActivationModeV.ToString(), activationMode.ToString()));
            }
            ActivationModeV = activationMode;
        }

        #endregion

        #region campos e propriedades adicionados - TypeConfig<T> - para IConstructorBinder<T>

        /// <summary>
        /// Campo que guarda modo de construcao.
        /// </summary>
        private ConstructionMode? _constructionMode = null;

        /// <summary>
        /// Propriedade que define DefaultAttributeOrMoreBindedArguments como default no modo de construcao, apenas se modo ainda nao foi definido.
        /// </summary>
        public ConstructionMode ConstructionModeV
        {
            get { return _constructionMode ?? ConstructionMode.DefaultAttributeOrMoreBindedArguments; }
            set { _constructionMode = value; }
        }

        /// <summary>
        /// Propriedade que indica se o modo de construcao ja foi definido.
        /// </summary>
        public bool IsConstructionModeDefined { get { return _constructionMode.HasValue; } }

        /// <summary>
        /// Lista utilizada para guardar tipos de parametros de construtores.
        /// </summary>
        public List<Type> ConstructorParamsTypes;

        /// <summary>
        /// Lista utilizada para guardar valores de parametros de construtores.
        /// </summary>
        public Dictionary<string, Object> ConstructorParamsValues;

        #endregion

        //...falta_codigo...simples
        //...falta_codigo...extra
    }

    /// <summary>
    /// Classe genérica que extende classe base criada e interfaces previamente definidas, a ser utilizada na configuracao.
    /// </summary>
    public class TypeConfig<T> : TypeConfig, IActivationBinder<T>, IConstructorBinder<T>, ITypeBinder<T>
    {
        public override Type TypeV
        {
            get { return typeof(T); }
        }

        public Action<T> InitAction;

        //...falta_codigo...extra

        #region 2 metodo(s) implementado(s) - de IActivationBinder<T> - PerRequest Singleton - retorna(m) ITypeBinder<T>

        public ITypeBinder<T> PerRequest() // de IActivationBinder<T>
        {
            //throw new NotImplementedException();
            SetActivationMode(ActivationMode.PerRequest);
            return this;
        }

        public ITypeBinder<T> Singleton() // de IActivationBinder<T>
        {
            //throw new NotImplementedException();
            SetActivationMode(ActivationMode.Singleton);
            return this;
        }

        #endregion

        #region 1 metodo(s) implementado(s) - de IConstructorBinder<T> - WithValues - retorna(m) ITypeBinder<T>

        public ITypeBinder<T> WithValues(Func<object> values) // de IConstructorBinder<T>
        {
            //throw new NotImplementedException();
            var valuesObject = values();
            // ToDictionary atira ArgumentException se keySelector produces duplicate keys for two elements
            ConstructorParamsValues = valuesObject.GetType().GetProperties().ToDictionary(p => p.Name,
                                                                                               p =>
                                                                                               p.GetValue(valuesObject,
                                                                                                          null));
            return this;
        }

        #endregion

        #region 5 metodo(s) implementado(s) - de ITypeBinder<T> ... WithConst WithNoArgsConst WithAct InitObjWith WhenArgHas - retorna(m) IC IT IA IT

        public IConstructorBinder<T> WithConstructor(params Type[] constructorArguments) // de ITypeBinder<T>
        {
            //throw new NotImplementedException();
            ConstructionModeV = ConstructionMode.WithConstructor;
            ConstructorParamsTypes = constructorArguments == null ? new List<Type>() : constructorArguments.ToList();
            return this;
        }

        public ITypeBinder<T> WithNoArgumentsConstructor() // de ITypeBinder<T>
        {
            //throw new NotImplementedException();
            ConstructionModeV = ConstructionMode.NoArgumentsConstructor;
            return this;
        }

        //public ITypeBinder<T> WithSingletonActivation() // de ITypeBinder<T> - comentado porque estava esquecido em vez de ter sido apagado
        //{
        //    throw new NotImplementedException();
        //}

        //public ITypeBinder<T> WithPerRequestActivation() // de ITypeBinder<T> - comentado porque estava esquecido em vez de ter sido apagado
        //{
        //    throw new NotImplementedException();
        //}

        public IActivationBinder<T> WithActivation // de ITypeBinder<T>
        {
            //get { throw new NotImplementedException(); }
            //set { throw new NotImplementedException(); }
            get
            {
                //throw new NotImplementedException();
                return this; // ActivationModeV
            }
            //set
            //{
            //    //throw new NotImplementedException();
            //    ; // this = value; // ActivationModeV
            //}
        }

        public ITypeBinder<T> InitializeObjectWith(Action<T> initialization) // de ITypeBinder<T>
        {
            //throw new NotImplementedException();
            InitAction = (T => initialization(T));
            return this;
        }

        public void WhenArgumentHas<TAttribute>() where TAttribute : Attribute // de ITypeBinder<T>
        {
            //throw new NotImplementedException();
            //...falta_codigo...simples
            return;
            //...falta_codigo...extra
        }

        #endregion
    }
}
