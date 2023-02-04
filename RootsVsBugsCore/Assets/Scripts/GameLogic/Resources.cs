using System;

[Serializable]
public class Resources
{
    public int Water;
    public int Seeds;
    public int Minerals;

    public override bool Equals(object obj)
    {
        return obj is Resources resources &&
               Water == resources.Water &&
               Seeds == resources.Seeds &&
               Minerals == resources.Minerals;
    }

    public static bool operator ==(Resources a, Resources b)
    {
        return a.Water == b.Water && a.Seeds == b.Seeds && a.Minerals == b.Minerals;
    }

    public static bool operator !=(Resources a, Resources b)
    {
        return a.Water != b.Water || a.Seeds != b.Seeds || a.Minerals != b.Minerals;
    }

    public static bool operator >(Resources a, Resources b)
    {
        return a != b && a.Water >= b.Water && a.Seeds >= b.Seeds && a.Minerals >= b.Minerals;
    }

    public static bool operator <(Resources a, Resources b)
    {
        return a != b && a.Water <= b.Water && a.Seeds <= b.Seeds && a.Minerals <= b.Minerals;
    }

    public static bool operator >=(Resources a, Resources b)
    {
        return a == b || a > b;
    }

    public static bool operator <=(Resources a, Resources b)
    {
        return a == b || a < b;
    }

    public static Resources operator -(Resources a, Resources b)
    {
        return new Resources { 
            Water = a.Water - b.Water, 
            Minerals = a.Minerals - b.Minerals, 
            Seeds = a.Seeds - b.Seeds 
        };
    }
}
