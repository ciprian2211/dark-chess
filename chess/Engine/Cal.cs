public class Cal : Piesa
{
    public Cal(bool isWhite, Coordonate positie) : base(isWhite, positie)
    {

    }
    public override Coordonate[] GetMiscarePermisa(TablaSah tabla)
    {
        Coordonate[] buffer = new Coordonate[8];
        int nrMutari = 0;
        int[] mutariX = { 2, 1, -1, -2, -2, -1, 1, 2 };
        int[] mutariY = { 1, 2, 2, 1, -1, -2, -2, -1 };
        for (int i = 0; i < 8; i++)
        {
            int xNou = pozitie.x + mutariX[i];
            int yNou=pozitie.y + mutariY[i];

            if(xNou>=0 && xNou<=7 && yNou>=0 && yNou <= 7)
            {
                buffer[nrMutari]=new Coordonate(xNou, yNou);
                nrMutari++;
            }
           
        }
        Coordonate[] mutariFinale = new Coordonate[nrMutari];
        for (int i = 0; i < nrMutari; i++)
        {
            mutariFinale[i] = buffer[i];
        }
        return mutariFinale;
    }
}