public abstract class Piesa
{
    public bool isWhite;
    public Coordonate pozitie;
    public bool aMutat=false;
    public Piesa(bool isWhite, Coordonate pozitie)
    {
        this.isWhite = isWhite;
        this.pozitie = pozitie;
    }

    public void Mutare(Coordonate pozitieNoua)
    {

        pozitie = pozitieNoua;
    }
    public abstract Coordonate[] GetMiscarePermisa(TablaSah tabla);
    
    
}