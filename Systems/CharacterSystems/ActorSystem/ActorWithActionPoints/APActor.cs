using System;
using UnityFoundation.Code;
using UnityFoundation.Code.DebugHelper;
using UnityFoundation.ResourceManagement;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public class APActor : IAPActor, IBilucaLoggable
    {
        public Optional<IAPIntent> Intent { get; private set; }
        public IResourceManager ActionPoints { get; private set; }
        public IBilucaLogger Logger { get; set; }

        public event Action OnCantExecuteAction;
        public event Action OnActionFinished;

        private IAction currentAction;
        private uint actionPointsCost;

        public APActor(IResourceManager actionPoints)
        {
            Intent = Optional<IAPIntent>.None();
            ActionPoints = actionPoints;
        }

        public void Execute()
        {
            if(!Intent.IsPresentAndGet(out IAPIntent intent))
                return;

            if(ActionPoints.CurrentAmount < (uint)intent.ActionPointsCost)
            {
                InvokeCantExecuteAction();
                return;
            }

            var action = intent.Create();

            if(action == null)
            {
                InvokeCantExecuteAction();
                return;
            }

            action.OnCantExecuteAction -= InvokeCantExecuteAction;
            action.OnCantExecuteAction += InvokeCantExecuteAction;

            action.OnFinishAction -= InvokeFinishAction;
            action.OnFinishAction += InvokeFinishAction;

            currentAction = action;

            action.Execute();
        }

        private void InvokeCantExecuteAction()
        {
            OnCantExecuteAction?.Invoke();
        }

        public void InvokeFinishAction()
        {
            Logger?.Log(nameof(currentAction.OnFinishAction));
            ActionPoints.TrySubtract(actionPointsCost);
            OnActionFinished?.Invoke();
        }

        public void Set(IAPIntent intent)
        {
            if(intent == null)
                throw new ArgumentNullException(
                    "Set action factory should not be null, use UnsetAction instead."
                );

            Intent = Optional<IAPIntent>.Some(intent);
            actionPointsCost = (uint)intent.ActionPointsCost;
            if(intent.ExecuteImmediatly)
                Execute();
        }

        public void UnsetAction()
        {
            Intent = Optional<IAPIntent>.None();
        }
    }
}
