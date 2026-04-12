public class Pion : Piesa
{
    public Pion(bool isWhite, Coordonate positie) : base(isWhite, positie)
    {

    }
    public override Coordonate[] GetMiscarePermisa(TablaSah tabla)
    {
        Coordonate[] buffer=new Coordonate[4];
        int nrMutari = 0;
        int directie= isWhite? -1 : 1; //daca e negru directie pozitiva pe y daca e alb directie negativa pe y
        int randStart = isWhite ? 6 : 1;
        //inaintarea simpla
        int fata1X = pozitie.x;
        int fata1Y=pozitie.y+directie;

        if (fata1Y >= 0 && fata1Y <= 7)
        {
            if (tabla.grid[fata1X, fata1Y] == null)
            {
                buffer[nrMutari] = new Coordonate(fata1X, fata1Y);
                nrMutari++;

                //inaintarea dubla
                if (pozitie.y == randStart)
                {
                    int fata2Y = pozitie.y + (directie * 2);
                    if (tabla.grid[fata1X, fata2Y] == null)
                    {
                        buffer[nrMutari] = new Coordonate(fata1X, fata2Y);
                        nrMutari++;
                    }
                }
            }
        }

        //atacul
        int[] atacX = { pozitie.x - 1, pozitie.x + 1 };
        for(int i = 0; i < 2; i++)
        {
            int ax = atacX[i];
            int ay=pozitie.y+directie;
            if(ax >= 0 && ay >= 0 && ax <= 7 && ay <= 7)
            {
                Piesa piesaTinta = tabla.grid[ax, ay];

                if(piesaTinta != null && piesaTinta.isWhite != this.isWhite)
                {
                    buffer[nrMutari]= new Coordonate(ax, ay);
                    nrMutari++;
                }
            }
        }
        //reduc nr mutari in functie de pozitia actuala
        Coordonate[] mutariFinale = new Coordonate[nrMutari];
        for (int i = 0; i < nrMutari; i++)
        {
            mutariFinale[i] = buffer[i];
        }
        return mutariFinale;
    }
}