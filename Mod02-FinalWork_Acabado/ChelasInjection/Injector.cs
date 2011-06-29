using System;
using System.Collections.Generic; // adicionado using
using System.Linq; // adicionado using
using System.Reflection; // adicionado using
using ChelasInjection.Exceptions; // adicionado using

namespace ChelasInjection
{
    public class Injector
    {
        private readonly Binder _myBinder; // adicionada keyword readonly

        #region campos adicionados

        /// <summary>
        /// Lista utilizada para casos de singletons.
        /// </summary>
        //private List<object> _objectsSingletons = new List<object>();
        private Dictionary<string, object> _objectsSingletons = new Dictionary<string, object>();
        /// <summary>
        /// Lista utilizada para prevenir dependencias circulares.
        /// </summary>
        //private List<ConstructorInfo> _construtoresInvocados = new List<ConstructorInfo>();
        private List<Type> _typesInvoked = new List<Type>();
        //...falta_codigo...simples
        //...falta_codigo...extra

        #endregion

        public Injector(Binder myBinder)
        {
            _myBinder = myBinder;
            _myBinder.Configure();
        }

        public T GetInstance<T>()
        {
            //throw new NotImplementedException();
            var type = typeof (T);
            var configs = _myBinder.Configuration;
            //switch (typeConfig.ActivationModeV) // verifica o modo de activacao em typeConfig = configs[type]
            //{
            //    case ActivationMode.Singleton:
            //        break;
            //    case ActivationMode.PerRequest:
            //        break;
            //    default:
            //        //throw new ArgumentOutOfRangeException();
            //        //=igual ao PerRequest
            //        break;
            //}
            if (configs.ContainsKey(type) && ((configs[type]).ActivationModeV == ActivationMode.Singleton)) // (TypeConfig<object>)
            {
                var typeConfigSingleton = (configs[type]); // (TypeConfig<object>)
                //var objSingleton = _objectsSingletons.Where(o => (o.GetType() == configs[type].TypeV)).FirstOrDefault(); // passo 1 - verificar Singletons
                object objSingleton = null;
                if (_objectsSingletons.ContainsKey(typeConfigSingleton.TypeV.FullName ?? ""))
                {
                    objSingleton = _objectsSingletons[typeConfigSingleton.TypeV.FullName ?? ""];
                }
                if (objSingleton != null)
                {
                    //objSingleton.PrepareReturnAfterCheckingAndTreatingIfSingleton(default(ActivationMode), _objectsSingletons, _typesInvoked,
                    //                                                       configs[type].TypeV); // se sim, remover tipo do conjunto de types invocados
                    //if (typeConfigSingleton.InitAction != null)
                    //{
                    //    typeConfigSingleton.InitAction.Invoke(objSingleton);
                    //}
                    //objSingleton
                    return (T)objSingleton; // se sim, retorna o resultado
                }
            }
            if (!type.IsPrimitiveOrString()) // restricao de tipos a processar - apenas tipos nao primitivos e nao string
            {
                // while
                if (!configs.ContainsKey(type)) // passo 2 - verificar se T (nao primitivo e nao string) esta na configuracao, senao, adiciona-lo
                {
                    _myBinder.Bind<T>();
                }
            }
            if (configs.ContainsKey(type)) // passo 3 - verificar se T esta na configuracao
            {
                var typeConfig = (configs[type]); // (TypeConfig<object>)
                var typeConfigType = typeConfig.TypeV;
                if (_typesInvoked.Contains(typeConfigType)) // se conjunto de types invocados contem tipo, significa que ha dependencia circular
                {
                    throw new CircularDependencyException("GetInstance<T>()".ErrMsgCircularDependency(typeConfigType.Name));
                }
                else // senao, adicionar tipo ao conjunto de types invocados
                {
                    _typesInvoked.Add(typeConfigType);
                }
                var obj = _myBinder.ResolveWithCustomResolverHandlers(typeConfigType); // passo 4 - verificar se CustomResolver da resultado
                if (obj != null)
                {
                    obj.PrepareReturnAfterCheckingAndTreatingIfSingleton(default(ActivationMode), ref _objectsSingletons, ref _typesInvoked,
                                                                   ref typeConfigType); // se sim, adicionar singleton?, e remover type dos invocados
                    //if (typeConfig.InitAction != null)
                    //{
                    //    typeConfig.InitAction.Invoke(obj);
                    //}
                    return (T)obj; // se sim, retorna o resultado
                }
                var typeConfigTypeConstructors = typeConfigType.GetConstructors().ToList();
                if (typeConfigTypeConstructors.Count() > 0) // existem construtores
                {
                    typeConfigTypeConstructors =
                        typeConfigTypeConstructors.OrderBy(
                            c => c.GetParameters().Where(p => !p.ParameterType.IsPrimitiveOrString()).Count()).ToList();
                    // ordenam-se por numero parametros/argumentos
                    ConstructorInfo constructorInfo;
                    var typeConfigConstructorParamsValues = typeConfig.ConstructorParamsValues;
                    switch (typeConfig.ConstructionModeV) // verifica o modo de construcao
                    {
                        case ConstructionMode.NoArgumentsConstructor:
                            constructorInfo = typeConfigTypeConstructors.ChooseConstructorWithNoArgs(typeConfigType.Name);
                            break;
                        case ConstructionMode.WithConstructor:
                            constructorInfo = typeConfigTypeConstructors.ChooseConstructorWithArgsValues(
                                typeConfigType.Name, typeConfig.ConstructorParamsTypes, typeConfigConstructorParamsValues);
                            break;
                        case ConstructionMode.DefaultAttributeOrMoreBindedArguments:
                            constructorInfo =
                                typeConfigTypeConstructors.ChooseConstructorDefaultAttribOrMoreArgs(typeConfigType.Name);
                            break;
                        default:
                            //throw new ArgumentOutOfRangeException();
                            //=igual ao DefaultAttributeOrMoreBindedArguments
                            constructorInfo =
                                typeConfigTypeConstructors.ChooseConstructorDefaultAttribOrMoreArgs(typeConfigType.Name);
                            break;
                    }
                    List<object> parameterList;
                    var parameterInfos = constructorInfo.GetParameters().ToDictionary(p => p.Name, p => p.ParameterType);
                    if (parameterInfos.Keys.Count() > 0) // se existem parametros no construtor escolhido, verificar as sub-dependencias
                    {
                        var selfMethod =
                            this.GetType().GetMethods().FirstOrDefault(
                                m =>
                                ((m.Name == "GetInstance") && (m.IsGenericMethod) &&
                                 (m.GetGenericArguments().Length == 1) && (m.GetParameters().Length == 0)));
                        if (selfMethod == null)
                        {
                            throw new UnboundTypeException(
                                "Metodo Auto-Referenciado para Dependencias".ErrMsgNotFound("GetInstance<T>()",
                                                                                            this.GetType().Name));
                        }
                        if (typeConfig.ConstructionModeV == ConstructionMode.WithConstructor)
                        {
                            parameterList =
                                parameterInfos.Select(
                                    kvp =>
                                    (typeConfigConstructorParamsValues.ContainsKey(kvp.Key)
                                         ? typeConfigConstructorParamsValues[kvp.Key]
                                         : selfMethod.MakeGenericMethod(kvp.Value).Invoke(this, new object[0]))).ToList();
                        }
                        else
                        {
                            parameterList =
                                parameterInfos.Select(
                                    kvp => selfMethod.MakeGenericMethod(kvp.Value).Invoke(this, new object[0])).ToList();
                        }
                    }
                    else
                    {
                        parameterList = new List<object>();
                    }
                    obj = constructorInfo.Invoke(parameterList.ToArray());
                    // injectar propriedades e metodos conforme activacao
                    //...falta_codigo...simples
                    //...falta_codigo...extra
                    // initialize
                    //...falta_codigo...simples
                    //...falta_codigo...extra
                    obj.PrepareReturnAfterCheckingAndTreatingIfSingleton(typeConfig.ActivationModeV, ref _objectsSingletons, ref _typesInvoked,
                                                                   ref typeConfigType); // remover tipo dos de types invocados // retorna o resultado
                    //if (typeConfig.InitAction != null)
                    //{
                    //    typeConfig.InitAction.Invoke(obj);
                    //}
                    return (T)obj;
                }
                else
                {
                    throw new UnboundTypeException("Minimo de um Construtor".ErrMsgNotFound("construtor", typeConfigType.Name));
                }
            }
            else
            {
                throw new UnboundTypeException("Tipo na Configuracao Bind".ErrMsgNotFound(type.Name, "Bind"));
            }
        }

        public T GetInstance<T, TA>() where TA : Attribute
        {
            throw new NotImplementedException();
            //...falta_codigo...extra
        }

        public T GetInstance<T>(string name)
        {
            throw new NotImplementedException();
            //...falta_codigo...extra
        }
    }
}