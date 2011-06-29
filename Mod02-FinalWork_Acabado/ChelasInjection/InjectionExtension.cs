using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChelasInjection.Exceptions;

namespace ChelasInjection
{
    /// <summary>
    /// Classe onde sao definidos todos os metodos de extensao que sao mais especificos do projecto.
    /// </summary>
    static public class InjectionExtension
    {
        #region 4 Metodos de Criacao de Mensagens de Erro Especificas

        /// <summary>
        /// Cria mensagem de tentativa de dupla configuracao.
        /// </summary>
        /// <param name="definitionContext">Contexto ou Ambito da dupla configuracao.</param>
        /// <param name="oldDefinitionValue">Valor antigo que foi anteriormente definido.</param>
        /// <param name="newDefinitionValueAttempted">Valor novo que se utilizou para tentativa de definicao.</param>
        /// <returns>Mensagem de erro, que pode ser utilizada em excepcoes, por exemplo.</returns>
        static public string ErrMsgAttemptedDoubleConfig(this string definitionContext, string oldDefinitionValue, string newDefinitionValueAttempted)
        {
            return string.Format("{0}: ja estava definida como {1}, e foi solicitada 2a definicao como {2}.", definitionContext ?? "", oldDefinitionValue ?? "", newDefinitionValueAttempted ?? "");
        }

        /// <summary>
        /// Cria mensagem de item nao encontrado.
        /// </summary>
        /// <param name="notFoundContext">Contexto ou Ambito do item nao encontrado.</param>
        /// <param name="itemContext">Caracteristica do Item nao encontrado.</param>
        /// <param name="itemNotFound">Item nao encontrado.</param>
        /// <returns>Mensagem de erro, que pode ser utilizada em excepcoes, por exemplo.</returns>
        static public string ErrMsgNotFound(this string notFoundContext, string itemContext, string itemNotFound)
        {
            return string.Format("{0}: nao se encontrou {1} em {2}.", notFoundContext ?? "", itemContext ?? "", itemNotFound ?? "");
        }

        /// <summary>
        /// Cria mensagem de multiplos construtores terem sido encontrados com DefaultConstructorAttribute.
        /// </summary>
        /// <param name="definitionContext">Contexto ou Ambito da deteccao de multiplos construtores com DefaultConstructorAttribute.</param>
        /// <param name="itemContext">Item do contexto (o Type) que contem os construtores.</param>
        /// <returns>Mensagem de erro, que pode ser utilizada em excepcoes, por exemplo.</returns>
        static public string ErrMsgGotMultipleConstructorsWithDefaultAttrib(this string definitionContext, string itemContext)
        {
            return string.Format("{0}: varios construtores com Default Attribute encontrados em {1}.", definitionContext ?? "", itemContext ?? "");
        }

        /// <summary>
        /// Cria mensagem de dependencia circular de Types ter sido encontrada.
        /// </summary>
        /// <param name="definitionContext">Contexto ou Ambito da deteccao de dependencia circular de Types.</param>
        /// <param name="itemContext">Item do contexto (o Type) que originou a dependencia circular.</param>
        /// <returns>Mensagem de erro, que pode ser utilizada em excepcoes, por exemplo.</returns>
        static public string ErrMsgCircularDependency(this string definitionContext, string itemContext)
        {
            return string.Format("{0}: dependencia circular de Types encontrada com Type {1}.", definitionContext ?? "", itemContext ?? "");
        }

        #endregion

        #region 3 Metodos de Escolha de Construtor com base em Regras

        /// <summary>
        /// Escolhe Metodo Construtor de um Type com base na regra DefaultConstructorAttribute, ou entao, More Binded Arguments.
        /// </summary>
        /// <param name="constructorInfos">Conjunto de Construtores possiveis.</param>
        /// <param name="typeName">Nome do Type que contem os Construtores.</param>
        /// <returns>O Construtor escolhido (ultimo se nao ha atributo, primeiro com atributo se houver atributo).</returns>
        static public ConstructorInfo ChooseConstructorDefaultAttribOrMoreArgs(this IEnumerable<ConstructorInfo> constructorInfos, string typeName)
        {
            // IEnumerable<ConstructorInfo> constructorInfos já está ordenado por número de argumentos, e tem construtores
            if ((constructorInfos == null) || (constructorInfos.Count() <= 0)) // nao ha construtores, logo, ocorre excepcao
            {
                throw new UnboundTypeException("Minimo de um Construtor".ErrMsgNotFound("construtor", typeName));
            }
            var moreRestrictConstructorInfos = constructorInfos.Where(
                c =>
                ((c.GetCustomAttributes(true).Count() > 0) &&
                 (c.GetCustomAttributes(true).Where(
                     ca => (ca.GetType().Name ?? "") == "DefaultConstructorAttribute").Count() > 0))); // nova restricao - existencia de atributo
            switch (moreRestrictConstructorInfos.Count())
            {
                case 0:
                    return constructorInfos.Last(); // nao existem construtores com atributo, logo, escolhe-se o construtor que tem mais argumentos
                    break;
                case 1:
                    return moreRestrictConstructorInfos.First(); // existe apenas um construtor com atributo, logo, escolhe-se esse mesmo
                    break;
                default:
                    // existem varios construtores com atributo, logo, ocorre excepcao
                    throw new MultipleDefaultConstructorAttributesException(
                        "Construtor por defeito em Tipo".ErrMsgGotMultipleConstructorsWithDefaultAttrib(typeName));
                    break;
            }
        }

        /// <summary>
        /// Escolhe Metodo Construtor de um Type com base na regra de Bind que escolhe o Construtor Sem Argumentos.
        /// </summary>
        /// <param name="constructorInfos">Conjunto de Construtores possiveis.</param>
        /// <param name="typeName">Nome do Type que contem os Construtores.</param>
        /// <returns>O Construtor escolhido (primeiro se este nao tiver parametros/argumentos).</returns>
        static public ConstructorInfo ChooseConstructorWithNoArgs(this IEnumerable<ConstructorInfo> constructorInfos, string typeName)
        {
            // IEnumerable<ConstructorInfo> constructorInfos já está ordenado por número de argumentos
            if ((constructorInfos == null) || (constructorInfos.Count() <= 0)) // nao ha construtores, logo, ocorre excepcao
            {
                throw new UnboundTypeException("Minimo de um Construtor".ErrMsgNotFound("construtor", typeName));
            }
            else // ha construtores
            {
                if (constructorInfos.First().GetParameters().Count() == 0) // primeiro construtor nao tem parametros/argumentos
                {
                    return constructorInfos.First();
                }
                else // primeiro construtor tem parametros/argumentos, logo, ocorre excepcao
                {
                    throw new UnboundTypeException(
                        "Minimo de um Construtor sem Argumentos".ErrMsgNotFound("construtor sem argumentos", typeName));
                }
            }
        }

        /// <summary>
        /// Escolhe Metodo Construtor de um Type com base na regra de Bind que escolhe o Construtor Com Determinados Tipos, Argumentos e Valores.
        /// </summary>
        /// <param name="constructorInfos">Conjunto de Construtores possiveis.</param>
        /// <param name="typeName">Nome do Type que contem os Construtores.</param>
        /// <param name="types">Conjunto de Tipos Determinados.</param>
        /// <param name="arguments">Conjunto de Pares de Argumentos e Valores Determinados.</param>
        /// <returns>O Construtor escolhido (1o se este for o unico cumpridor, ChooseConstructorDefaultAttribOrMoreArgs se existirem mais).</returns>
        static public ConstructorInfo ChooseConstructorWithArgsValues(this IEnumerable<ConstructorInfo> constructorInfos, string typeName,
                                                                        IEnumerable<Type> types, Dictionary<string, object> arguments)
        {
            // IEnumerable<ConstructorInfo> constructorInfos já está ordenado por número de argumentos
            if ((constructorInfos == null) || (constructorInfos.Count() <= 0)) // nao ha construtores, logo, ocorre excepcao
            {
                throw new UnboundTypeException("Minimo de um Construtor".ErrMsgNotFound("construtor", typeName));
            }
            types = types ?? new List<Type>();
            arguments = arguments ?? new Dictionary<string, object>();
            var moreRestrictConstructorInfos = constructorInfos.Where(
                c =>
                ((c.GetParameters().Count() == types.Count()) &&
                 (c.GetParameters().Select(p => p.GetType()).SequenceEqual(types)) &&
                 (c.GetParameters().Join(arguments, pi => pi.Name, kvp => kvp.Key,
                                         (pi2, kvp2) => (kvp2.Value.GetType().IsSubclassOf(pi2.ParameterType))).Count(
                                             pikvp => pikvp) == arguments.Keys.Count))); // (pi2.Name == kvp2.Key)
            // (kvp2.Value.GetType().IsSubclassOf(pi2.ParameterType)) // .Where(pikvp => pikvp).Count() // .Count(pikvp => pikvp)
            /* novas restricoes - igual nº(d)e tipos ao Conjunto Determinado ; todos Argumentos do Conjunto Determinado com nome igual a parametros
             * ; todos Argumentos sao de Type (igual ou subclasse de Type) do parametro com nome igual
             */
            switch (moreRestrictConstructorInfos.Count())
            {
                case 0: // nao ha construtores, logo, ocorre excepcao
                    throw new UnboundTypeException(
                        "Minimo de um Construtor Com Tipos Determinados".ErrMsgNotFound("construtor com Tipos Indicados", typeName));
                    break;
                case 1:
                    return moreRestrictConstructorInfos.First(); // existe apenas um construtor com as restricoes, logo, escolhe-se esse mesmo
                    break;
                default: // existem varios construtores com as restricoes, logo, escolhe-se o resultado da escolha default do sub-conjunto restrito
                    return moreRestrictConstructorInfos.ChooseConstructorDefaultAttribOrMoreArgs(typeName);
                    break;
            }
        }

        #endregion

        #region Metodo(s) para verificar Singleton e retornar Obj

        //static public object CheckingIfSingletonAndObtaining(this Dictionary<Type, TypeConfig> configs, List<object> singletons,
        //                                                        List<Type> types, Type type)
        //{
        //    types.Add(type);
        //    if (configs.ContainsKey(type) && (configs[type].ActivationModeV == ActivationMode.Singleton))
        //    {
        //        return singletons.Where(o => (o.GetType() == type)).FirstOrDefault();
        //    }
        //    return null;
        //}

        static public object PrepareReturnAfterCheckingAndTreatingIfSingleton(this object result, ActivationMode mode,
                                                                                ref Dictionary<string, object> singletons,
                                                                                ref List<Type> types, ref Type type) // List<object> singletons
        {
            //if ((mode == ActivationMode.Singleton) && !singletons.Select(o => o.GetType()).Contains(result.GetType()))
            if ((mode == ActivationMode.Singleton) && !singletons.ContainsKey(type.FullName ?? ""))
            {
                //singletons.Add(result);
                singletons.Add(type.FullName ?? "", result);
            }
            types.Remove(type);
            return result;
        }

        #endregion
    }
}