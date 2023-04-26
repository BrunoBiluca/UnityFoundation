using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.DiceSystem
{
    [ExcludeFromCoverage]
    public class DiceRollThrowInputs : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> dicesObjs;

        private List<IDiceMono> dices;

        private LogDiceEvaluate logDiceEvaluate;

        private float result;

        public void Start()
        {
            dices = dicesObjs.Select(x => x.GetComponent<IDiceMono>()).ToList();
            logDiceEvaluate = new LogDiceEvaluate();
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                var throwEvent = DiceRollManager.Instance.ThrowDice(
                    new RandomThrow(
                        AsyncProcessor.Instance,
                        new UnityRandomGenerator()
                    ),
                    dices.ToArray()
                );

                throwEvent.OnDiceEvaluate += logDiceEvaluate.Handle;

                throwEvent.OnDicesEvaluateFinish += (dicesEvaluate) => {
                    result = 0;
                    foreach(var evaluate in dicesEvaluate)
                    {
                        if(evaluate.Dice is NumberedDiceMono numberedDice)
                        {
                            result += numberedDice.GetValue(evaluate.Side);
                        }
                    }
                    foreach(var evaluate in dicesEvaluate)
                    {
                        if(evaluate.Dice is MultiplierDiceMono multiplierDice)
                        {
                            result *= multiplierDice.GetValue(evaluate.Side).Multiplier;
                        }
                    }

                    Debug.Log($"Result: {result}");
                };
            }
        }
    }
}