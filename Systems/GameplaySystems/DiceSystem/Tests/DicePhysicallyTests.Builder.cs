using UnityEngine;

namespace UnityFoundation.DiceSystem.Tests
{
    public partial class DicePhysicallyTests
    {
        public class Builder
        {
            internal static DiceRollManager DiceRollManager()
            {
                var diceManager = new GameObject("dice_roll_manager")
                    .AddComponent<DiceRollManager>();

                diceManager.DestroyOnLoad = true;
                diceManager.Awake();
                return diceManager;
            }

            internal static DiceSideChecker DiceChecker()
            {
                var floor = new GameObject("floor").AddComponent<DiceSideChecker>();
                floor.Start();
                return floor;
            }

            internal static NumberedDiceMono OneSidedNumberedDice(int value)
            {
                var dice = new GameObject("dice").AddComponent<NumberedDiceMono>();
                dice.gameObject.AddComponent<Rigidbody>();
                dice.Awake();

                var diceSide = GameObject.CreatePrimitive(PrimitiveType.Quad)
                    .AddComponent<NumberedDiceSideHolder>();
                diceSide.Setup(value);

                diceSide.transform.parent = dice.transform;
                diceSide.transform.LookAt(-Vector3.up);

                dice.AddSideHolder(diceSide);
                dice.UpdateDiceSides();
                return dice;
            }

            internal static MultiplierDiceMono OneSidedCustomDice(MultiplierValue value)
            {
                var dice = new GameObject("dice").AddComponent<MultiplierDiceMono>();
                dice.gameObject.AddComponent<Rigidbody>();
                dice.Awake();

                var sideHolder = GameObject.CreatePrimitive(PrimitiveType.Quad)
                    .AddComponent<MultiplierDiceSideHolder>();

                sideHolder.Setup(value);

                sideHolder.transform.parent = dice.transform;
                sideHolder.transform.LookAt(-Vector3.up);

                dice.AddSideHolder(sideHolder);
                dice.UpdateDiceSides();
                return dice;
            }

        }
    }
}