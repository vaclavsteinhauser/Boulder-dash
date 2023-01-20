using Boulder_Dash_Core.Formulare;

namespace Boulder_Dash_Core;
/// <summary>
/// Základní informace o hře, které hráč nastaví pomocí na začátku spuštěných oken a reference na tuto instanci předávají každé vytvořené třídě
/// </summary>
public sealed class Parametry
{
    private Parametry() { }
    public static readonly Parametry Programu = new();
    public string Ikonky = "", Planek = "";
    public string Mapa = "";
    //speciální proměnná, která se nastaví false když hráč zavře některé z oken, pak předpokládáme, že chce ukončit celý program.
    public bool Success = true;
}
internal static class Program
{
    /// <summary>
    /// Spousteci Metoda.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        //vybere cesty k souborům obsahujícím plánek a ikonky, správnost nekontrolujeme, chybu formátu souborů program vyhodí připadně až při pokusu o načtení
        Application.Run(new FileChooser(Parametry.Programu));
        if (!Parametry.Programu.Success)
        {
            return;
        }

        //Na začítku si celý soubor s plány levelu nacte do stringu a soubor zavre

        try
        {
            var sr = new StreamReader(Parametry.Programu.Planek);
            while (sr.ReadLine() is { } line)
            {
                Parametry.Programu.Mapa += line + "\n";
            }
            sr.Close();
        }
        catch
        {
            MessageBox.Show(@"Nepodařilo se otevřít soubor obsahující mapu. Program se ukončí.", @"Soubor nejde otevřít");
            return;
        }

        try
        {
            Application.Run(new Gui(Parametry.Programu));
        }
        catch (Exception e)
        {

            MessageBox.Show(e.Message);
        }
    }
}