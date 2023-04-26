using System;
using UnityFoundation.Code;

namespace UnityFoundation.DiceSystem
{
    public class DiceRollManager : Singleton<DiceRollManager>
    {
        public event Action<ThrowDicesEvent> OnDiceThrow;

        public ThrowDicesEvent ThrowDice(
            IThrowDiceHandler throwHandler,
            params IDiceMono[] dices
        )
        {
            var throwDicesEvent = new ThrowDicesEvent(dices);

            throwHandler.Handle(throwDicesEvent);
            OnDiceThrow?.Invoke(throwDicesEvent);

            return throwDicesEvent;
        }
    }
}