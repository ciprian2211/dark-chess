public class Tura : Piesa
{
    public Tura(bool isWhite, Coordonate positie) : base(isWhite, positie)
    {
    }
    public override Coordonate[] GetMiscarePermisa(TablaSah tabla)
    {
        Coordonate[] buffer = new Coordonate[14];
        int nrMutari = 0;
        int[] dirX = { 0, 1, 0, -1, };
        int[] dirY = { 1, 0, -1, 0 };
        for (int d = 0; d < 4; d++)
        {
            int pas = 1;
            while (true)
            {
                int xNou = pozitie.x + (dirX[d] * pas);
                int yNou = pozitie.y + (dirY[d] * pas);
                if (xNou > 7 || xNou < 0 || yNou > 7 || yNou < 0)
                {
                    break;
                }
                Piesa piesaTinta = tabla.grid[xNou, yNou];
                if (piesaTinta == null)
                {
                    buffer[nrMutari] = new Coordonate(xNou, yNou);
                    nrMutari++;
                    pas++;
                }
                else if (piesaTinta.isWhite != this.isWhite)
                {
                    buffer[nrMutari] = new Coordonate(xNou, yNou);
                    nrMutari++;
                    break;
                }
                else
                {
                    break;
                }
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