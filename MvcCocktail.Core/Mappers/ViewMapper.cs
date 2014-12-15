using AutoMapper;
using AutoMapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapper
{
    public abstract class ViewMapper<TView, TModel> where TView : new()
    {
        private IMappingEngine engine = null;
        private ConfigurationStore configuration = null;

        private ConfigurationStore Configuration
        {
            get
            {
                if (configuration != null)
                    return configuration;
                return (configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers));
            }
        }

        private IMappingEngine Engine
        {
            get
            {
                if (engine != null)
                    return engine;

                DefineViewMap();
                DefineModelMap();

                return (engine = new MappingEngine(Configuration));
            }
        }

        protected virtual IMappingExpression<TModel, TView> DefineViewMap()
        {
            return Configuration.CreateMap<TModel, TView>();
        }

        protected virtual IMappingExpression<TView, TModel> DefineModelMap()
        {
            return Configuration.CreateMap<TView, TModel>();
        }

        public virtual TView ToView() { return new TView(); }
        public virtual TView ToView(TModel source) { return Engine.Map<TModel, TView>(source); }
        public virtual IEnumerable<TView> ToView(IEnumerable<TModel> source) { return from item in source select Engine.Map<TModel, TView>(item); }
        public virtual TView ToView(TModel source, TView destination) { return Engine.Map(source, destination); }

        public virtual TModel ToModel(TView source) { return Engine.Map<TView, TModel>(source); }
        public virtual IEnumerable<TModel> ToModel(IEnumerable<TView> source) { return from item in source select Engine.Map<TView, TModel>(item); }
        public virtual TModel ToModel(TView source, TModel destination) { return Engine.Map(source, destination); }
    }
}
