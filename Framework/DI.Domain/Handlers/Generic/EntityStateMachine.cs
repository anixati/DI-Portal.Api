using System;
using DI.Domain.Core;
using Stateless;

namespace DI.Domain.Handlers.Generic
{
    public class EntityStateMachine
    {
        private readonly StateMachine<State, Trigger> _machine;

        private EntityStateMachine(IEntity entity)
        {
            _machine = new StateMachine<State, Trigger>(() => GetState(entity), s => SetState(entity, s));
            _machine.Configure(State.Active)
                .Permit(Trigger.Lock, State.Locked)
                .Permit(Trigger.Disable, State.Disabled)
                .Permit(Trigger.Delete, State.Deleted);
            _machine.Configure(State.Locked)
                .Permit(Trigger.Unlock, State.Active);
            _machine.Configure(State.Disabled)
                .Permit(Trigger.Enable, State.Active)
                .Permit(Trigger.Delete, State.Deleted);
            _machine.Configure(State.Deleted)
                .Permit(Trigger.Activate, State.Active);
        }


        private static void SetState(IEntity entity, State state)
        {
            entity.Deleted = state == State.Deleted;
            entity.Disabled = state == State.Disabled;
            entity.Locked = state == State.Locked;
        }

        private static State GetState(IEntity entity)
        {
            if (entity.Locked) return State.Locked;
            if (entity.Deleted) return State.Deleted;
            return entity.Disabled ? State.Disabled : State.Active;
        }

        private void Fire(StatusAction action)
        {
            var trigger = action switch
            {
                StatusAction.Enable => Trigger.Enable,
                StatusAction.Disable => Trigger.Disable,
                StatusAction.Lock => Trigger.Lock,
                StatusAction.UnLock => Trigger.Unlock,
                StatusAction.Delete => Trigger.Delete,
                _ => throw new NotImplementedException("Not valid")
            };
            trigger.ThrowIfNull("trigger");
            _machine.Fire(trigger);
        }

        public static void Set(IEntity entity, StatusAction action)
        {
            var sm = new EntityStateMachine(entity);
            sm.Fire(action);
        }

        private enum Trigger
        {
            Enable,
            Disable,
            Lock,
            Unlock,
            Delete,
            Activate
        }

        private enum State
        {
            Active,
            Locked,
            Disabled,
            Deleted
        }
    }
}