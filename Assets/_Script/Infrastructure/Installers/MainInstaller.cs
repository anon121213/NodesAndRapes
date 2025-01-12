using System.Collections.Generic;
using _Script.Gameplay.Ropes.Checker;
using _Script.Gameplay.ScoreSystem;
using _Script.Gameplay.SkipButton;
using _Script.Gameplay.SoundSystem;
using _Script.Gameplay.WinSystem.Checker;
using _Script.Gameplay.WinSystem.WinUi;
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
    public class MainInstaller : LifetimeScope
    {
        [SerializeField] private AllData _allData;
        [SerializeField] private Canvas _dynamicCanvas;
        [SerializeField] private List<SoundSource> _audioSources = new ();

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrapper>().AsSelf();

            FsmRegisters(builder);
            RegisterFactories(builder);
            RegisterDataServices(builder);
            RegisterGameServices(builder);
            RegisterUiPresenters(builder);
        }

        private void FsmRegisters(IContainerBuilder builder) => 
            builder.Register<IStateMachine, StateMachine>(Lifetime.Singleton);

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<IStateFactory, StateFactory>(Lifetime.Singleton);
            builder.Register<IRopeFactory, RopeFactory>(Lifetime.Singleton);
            builder.Register<INodeFactory, NodeFactory>(Lifetime.Singleton);
            builder.Register<IWinWindowFactory, WinWindowFactory>(Lifetime.Singleton).WithParameter(_dynamicCanvas);
            builder.Register<IScoresFactory, ScoresFactory>(Lifetime.Singleton).WithParameter(_dynamicCanvas);
            builder.Register<ISkipButtonFactory, SkipButtonFactory>(Lifetime.Singleton).WithParameter(_dynamicCanvas);
        }

        private void RegisterDataServices(IContainerBuilder builder)
        {
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAddressablesLoader, AddressablesLoader>(Lifetime.Singleton);
            builder.Register<IStaticDataProvider, StaticDataProvider>(Lifetime.Singleton).WithParameter(_allData);
        }

        private void RegisterGameServices(IContainerBuilder builder)
        {
            builder.Register<IGameGenerator, GameGenerator>(Lifetime.Singleton);
            builder.Register<IIntersectionChecker, IntersectionChecker>(Lifetime.Singleton);
            builder.Register<IWinService, WinService>(Lifetime.Singleton);
            builder.Register<IScoreService, ScoreService>(Lifetime.Singleton);
            builder.Register<ISoundService, SoundService>(Lifetime.Singleton).WithParameter(_audioSources);
        }

        private void RegisterUiPresenters(IContainerBuilder builder)
        {
            builder.Register<IWinPresenter, WinPresenter>(Lifetime.Singleton);
            builder.Register<IScorePresenter, ScorePresenter>(Lifetime.Singleton);
            builder.Register<ISkipPresenter, SkipPresenter>(Lifetime.Singleton);
        }
    }
}