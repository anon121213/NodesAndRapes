using _Script.Infrastructure.Bootstrap;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using _Script.Infrastructure.Factories;
using _Script.Infrastructure.FSM;
using _Script.Infrastructure.Generator;
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

            FsmRegisters(builder);
            RegisterFactories(builder);
            RegisterDataServices(builder);
        }

        private void FsmRegisters(IContainerBuilder builder)
        {
            builder.Register<IStateFactory, StateFactory>(Lifetime.Singleton);
            builder.Register<IStateMachine, StateMachine>(Lifetime.Singleton);
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<IRopeFactory, RopeFactory>(Lifetime.Singleton);
            builder.Register<INodeFactory, NodeFactory>(Lifetime.Singleton);
            builder.Register<IGameGenerator, GameGenerator>(Lifetime.Singleton);
        }

        private void RegisterDataServices(IContainerBuilder builder)
        {
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IStaticDataProvider, StaticDataProvider>(Lifetime.Singleton).WithParameter(_allData);
        }
    }
}