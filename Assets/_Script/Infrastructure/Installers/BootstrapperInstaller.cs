using _Script.Infrastructure.Bootstrap;
using _Script.Infrastructure.Data.StaticData;
using _Script.Infrastructure.Factorys;
using _Script.Infrastructure.FSM;
using _Script.Infrastructure.ScenesLoader;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Script.Infrastructure.Installers
{
    public class BootstrapperInstaller : LifetimeScope
    {
        [SerializeField] private AllData _allData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrapper>().AsSelf();
            
            builder.Register<IStateFactory, StateFactory>(Lifetime.Singleton);
            builder.Register<IStateMachine, StateMachine>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IStaticDataProvider, StaticDataProvider>(Lifetime.Singleton).WithParameter(_allData);
        }
    }
}