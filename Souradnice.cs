namespace Boulder_Dash_Core;

/// <summary>
/// Struktura k vyjádření dvourozměrných souřadnic
/// </summary>
internal readonly struct Souradnice : IEquatable<Souradnice>
{
    public readonly int X;
    public readonly int Y;

    public Souradnice(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public Souradnice Doleva() => new(X - 1, Y);
    public Souradnice Doprava() => new(X + 1, Y);
    public Souradnice Dolu() => new(X, Y + 1);
    public Souradnice Nahoru() => new(X, Y - 1);
    public bool DopravaOd(Souradnice cizi) => Y == cizi.Y && X - 1 == cizi.X;
    public bool DolevaOd(Souradnice cizi) => Y == cizi.Y && X + 1 == cizi.X;
    public static Souradnice operator +(Souradnice a, Souradnice b) => new(a.X + b.X, a.Y + b.Y);
    public static Souradnice operator -(Souradnice a, Souradnice b) => new(a.X - b.X, a.Y - b.Y);
    public static bool operator ==(Souradnice a, Souradnice b) => a.Equals(b);
    public static bool operator !=(Souradnice a, Souradnice b) => !a.Equals(b);

    public override string ToString() => $"({X},{Y})";

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;
        var other = (Souradnice)obj;
        return X == other.X &&
               Y == other.Y;
    }

    public bool Equals(Souradnice other)
    {
        return X == other.X &&
               Y == other.Y;
    }

    public override int GetHashCode()
    {
        var hashCode = 1502939027;
        hashCode = hashCode * -1521134295 + X.GetHashCode();
        hashCode = hashCode * -1521134295 + Y.GetHashCode();
        return hashCode;
    }
}