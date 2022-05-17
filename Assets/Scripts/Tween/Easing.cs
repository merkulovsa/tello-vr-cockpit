using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Easing
{
    public abstract float ease(float amount);
}

public class LinearNone : Easing
{
    public override float ease(float amount)
    {
        return amount;
    }
}

public class BounceIn : Easing
{
    BounceOut bounceOut;

    public BounceIn()
    {
        bounceOut = new BounceOut();
    }

    public override float ease(float amount)
    {
        return 1 - bounceOut.ease(1 - amount);
    }
}

public class BounceOut : Easing
{
    public override float ease(float amount)
    {
        if (amount < 1 / 2.75f)
        {
            return 7.5625f * amount * amount;
        }
        else if (amount < 2 / 2.75f)
        {
            return 7.5625f * (amount -= 1.5f / 2.75f) * amount + 0.75f;
        }
        else if (amount < 2.5f / 2.75f)
        {
            return 7.5625f * (amount -= 2.25f / 2.75f) * amount + 0.9375f;
        }
        else
        {
            return 7.5625f * (amount -= 2.625f / 2.75f) * amount + 0.984375f;
        }
    }
}

public class BounceInOut : Easing
{
    BounceIn bounceIn;
    BounceOut bounceOut;

    public BounceInOut()
    {
        bounceIn = new BounceIn();
        bounceOut = new BounceOut();
    }

    public override float ease(float amount)
    {
        if (amount < 0.5f)
        {
            return bounceIn.ease(amount * 2) * 0.5f;
        }
        return bounceOut.ease(amount * 2 - 1) * 0.5f + 0.5f;
    }
}
