using UnityFoundation.Code;
using UnityEngine;
using System;

namespace UnityFoundation.Zombies
{

    public class SimpleBrainContext
    {
        public bool IsEnabled;
        public bool IsAttacking;
        public bool IsRunning;
        public bool IsWalking;
        public bool IsWandering;
        public bool IsChasing;
        public bool DebugMode;
        public float NextAttackTime;

        public Optional<Transform> Target;
        public Optional<Vector3> TargetPosition;

        public Transform Body { get; set; }
        public float NextWanderPositionTime { get; internal set; }

        public void ResetStates()
        {
            IsAttacking = false;
            IsRunning = false;
            IsWalking = false;
            IsWandering = false;
            IsChasing = false;
            TargetPosition = Optional<Vector3>.None();
        }
    }

    public class SimpleBrain : DecisionTree<SimpleBrainContext>, IAIBrain
    {
        private readonly SimpleBrainContext context;
        private readonly Settings settings;

        private ResetStateHandler resetStateHandler;
        private DebugModeBrainHander debugModeHandler;
        private SetupWandeningHandler setupWanderStateHandler;

        private PlayerRangeHandler playerAttackRangeHandler;
        private CanAttackHandler canAttackHandler;
        private PlayerRangeHandler playerChaseRangeHandler;
        private SetupAttackHandler setupAttackHandler;
        private SetupChaseHandler setupChaseHandler;

        public bool IsEnabled => context.IsEnabled;
        public bool IsAttacking => context.IsAttacking;
        public bool IsRunning => context.IsRunning;
        public bool IsWalking => context.IsWalking;
        public bool IsWandering => context.IsWandering;
        public bool IsChasing => context.IsChasing;
        public Transform Body { get; }
        public Optional<Vector3> TargetPosition => context.TargetPosition;
        public bool DebugMode => context.DebugMode;

        public Optional<Transform> Target => context.Target;

        public SimpleBrain(
            Settings settings,
            Transform body
        )
        {
            context = new SimpleBrainContext();
            this.settings = settings;
            context.Body = body;

            DefaultDecisionTree();
        }

        private void DefaultDecisionTree()
        {
            resetStateHandler = new ResetStateHandler();
            debugModeHandler = new DebugModeBrainHander(settings);
            setupWanderStateHandler = new SetupWandeningHandler(settings);

            resetStateHandler.SetNext(
                debugModeHandler.SetNext(
                    setupWanderStateHandler
                )
            );

            SetRootHandler(resetStateHandler);
        }

        public void SetPlayer(GameObject player)
        {
            var playerTransform = player.transform;
            PlayerDecisionTree(playerTransform);
        }

        private void PlayerDecisionTree(Transform playerTransform)
        {
            playerAttackRangeHandler = new PlayerRangeHandler(
                settings.MinAttackDistance, playerTransform);
            canAttackHandler = new CanAttackHandler(playerTransform);
            playerChaseRangeHandler = new PlayerRangeHandler(
                settings.MinDistanceForChasePlayer, playerTransform);
            setupAttackHandler = new SetupAttackHandler(settings, playerTransform);
            setupChaseHandler = new SetupChaseHandler(playerTransform);

            debugModeHandler
            .SetNext(
                playerAttackRangeHandler
                .SetNext(
                    canAttackHandler
                    .SetNext(setupAttackHandler)
                    .SetFailed(new ResetStateHandler())
                )
                .SetFailed(
                    playerChaseRangeHandler
                    .SetNext(setupChaseHandler)
                    .SetFailed(
                        setupWanderStateHandler
                        .SetFailed(new ResetStateHandler())
                    )
                )
            );
        }

        public void Update()
        {
            if(!IsEnabled) return;

            try
            {
                EvaluateDecisions(context);
            }
            catch(MissingReferenceException)
            {
                DefaultDecisionTree();
            }
        }

        public void Enabled()
        {
            context.IsEnabled = true;
        }

        public void Disabled()
        {
            context.IsEnabled = false;
            context.ResetStates();
            context.TargetPosition = Optional<Vector3>.None();
        }

        [Serializable]
        public class Settings
        {
            public float MinDistanceForChasePlayer;
            public float WanderingDistance;
            public float MinAttackDistance;
            public float MinNextAttackDelay;
            public float WanderingReevaluateTime;
        }
    }
}