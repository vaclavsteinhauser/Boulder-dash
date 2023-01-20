using Boulder_Dash_Core.Bludiste;
using Boulder_Dash_Core.Bludiste.objekty;

namespace Boulder_Dash_Core;

internal class Policko
{
    /* flags obsahuje postupně po bitech informace od nejnizzsich mocnin
     * 0.Je volno
     * 1.Je to hlína
     * 2.Je prvek pohyblivý
     * 3.Je to balvan
     * 4.Je to diamant
     * 5.Je to příšera
     * 6.Je to hrdina
     * 7.Je východ
     * 8.Je otevřený východ 
     * 9.Je zed
     * ...zbytek nevyužitý a muze se hodit k rozšiření
     * U pohyblivého prvku ještě držím referenci na objekt
     */

    /// <summary>
    /// flags obsahuje postupně po bitech informace od nejnizzsich mocnin
    /// 0.Je volno
    /// 1.Je to hlína
    /// 2.Je prvek pohyblivý
    /// 3.Je to balvan
    /// 4.Je to diamant
    /// 5.Je to příšera
    /// 6.Je to hrdina
    /// 7.Je východ
    /// 8.Je otevřený východ
    /// 9.Je zed
    /// ...zbytek nevyužitý a muze se hodit k rozšiření
    /// </summary>
    private ushort _flags;
    /// <summary>
    /// Na každém políčku je vždy nějaký prvek. i Volno je prvek
    /// </summary>
    private Prvek _instance = Volno.Instance;
    private readonly Mapa _mapa;
    public Policko(Mapa mapa)
    {
        NastavVolno();
        this._mapa = mapa;
    }

    public bool JeVolno => (_flags & 0x1) != 0;
    public bool JePohyblivyPrvek => (_flags & 0x4) != 0;

    public bool JeBalvan => (_flags & 0x8) != 0;

    public bool JeDiamant => (_flags & 0x10) != 0;

    public bool JePrisera => (_flags & 0x20) != 0;
    public bool JeHrdina => (_flags & 0x40) != 0;
    public bool JeOtevrenyVychod => (_flags & 0x100) != 0;

    public bool JeVolnoNeboHlina => (_flags & 0x3) != 0;
    public bool JeBalvanNeboDiamant => (_flags & 0x18) != 0;
    public bool JeVolnoNeboHrdina => (_flags & 0x41) != 0;

    public void NastavZed()
    {
        _flags = 0x200;
        _instance = Zed.Instance;
    }
    public void NastavVolno()
    {
        _flags = 0x1;
        _instance = Volno.Instance;
    }
    public void NastavHlinu()
    {
        _flags = 0x2;
        _instance = Hlina.Instance;
    }
    public void NastavBalvan(Balvan b)
    {
        _flags = 0xC;
        _instance = b;
    }
    public void NastavDiamant(Diamant d)
    {
        _flags = 0x14;
        _instance = d;
    }
    public void NastavPriseru(Prisera p)
    {
        _flags = 0x24;
        _instance = p;
    }
    public void NastavHrdinu(Hrdina h)
    {
        _flags = 0x44;
        _instance = h;
    }
    public void NastavVychod()
    {
        _flags = 0x80;
        _instance = Vychod.Instance;
    }
    public void OtevriVychod()
    {
        _flags = 0x180;
        _instance = OtevrenyVychod.Instance;
    }

    public void PresunSe(Souradnice na)
    {
        ((PohyblivyPrvek)_instance).Pozice = na;
        _mapa[na]._flags = _flags;
        _mapa[na]._instance = _instance;
        NastavVolno();
    }

    public Prisera DejPriseru => (JePrisera ? (Prisera)_instance : null) ?? throw new InvalidOperationException();

    public PohyblivyPrvek? DejPohyblivyPrvek => _instance as PohyblivyPrvek;

    public Bitmap? DejIkonku() => _instance.DejIkonku();
    public override string ToString() => _instance.ToString() ?? " ";
}