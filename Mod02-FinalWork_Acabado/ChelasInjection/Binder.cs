using System;
using System.Collections.Generic; // adicionado using
using System.Linq; // adicionado using

namespace ChelasInjection
{
    public delegate object ResolverHandler(Binder sender, Type t);

    public abstract class Binder
    {
        #region campos adicionados - Binder

        /// <summary>
        /// Armazena a configuracao dos binded types e respectivos typeconfigs.
        /// </summary>
        internal Dictionary<Type, TypeConfig> Configuration; // private _configuracao

        #endregion

        public void Configure()
        {
            //throw new NotImplementedException();
            Configuration = new Dictionary<Type, TypeConfig>();
            InternalConfigure();
            //...falta_codigo...extra
        }

        protected abstract void InternalConfigure();


        public event ResolverHandler CustomResolver;

        public ITypeBinder<Target> Bind<Source, Target>()
        {
            //throw new NotImplementedException();
            var configtype = new TypeConfig<Target>();
            Configuration[typeof(Source)] = configtype; // se ja existe key, substitui value, senao, adiciona par key-value
            return configtype;
        }


        public ITypeBinder<Source> Bind<Source>()
        {
            //throw new NotImplementedException();
            return Bind<Source, Source>();
        }

        public object ResolveWithCustomResolverHandlers(Type type) // adicionado metodo para se poder utilizar CustomResolver e GetInvocationList
        {
            return CustomResolver.GetInvocationList().Select(h => h.DynamicInvoke(this, type)).Where(o => (o != null)).FirstOrDefault();
        }
    }
}