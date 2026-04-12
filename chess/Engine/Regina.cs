public class Regina : Piesa
{
    public Regina(bool isWhite, Coordonate positie) : base(isWhite, positie)
    {

    }
    public override Coordonate[] GetMiscarePermisa(TablaSah tabla)
    {
        Coordonate[] buffer = new Coordonate[27];
        int nrMutari = 0;
        int[] dirX = { 0, 0, -1, 1, -1, 1, -1, 1 };
        int[] dirY = { -1, 1, 0, 0, -1, -1, 1, 1 };
        for(int d = 0; d < 8; d++)
        {
            int pas = 1;
            while (true)
            {
                int xNou = pozitie.x + (dirX[d] * pas);
                int yNou = pozitie.y + (dirY[d] * pas);
                
                if(xNou<0 || xNou>7 ||  yNou<0 || yNou > 7)
                {
                    break;
                }
                Piesa piesaTinta=tabla.grid[xNou,yNou];
                if (piesaTinta == null)
                {
                    buffer[nrMutari]=new Coordonate(xNou,yNou);
                    nrMutari++;
                    pas++;
                }
                else if(piesaTinta.isWhite != this.isWhite)
                {
                    buffer[nrMutari]=new Coordonate(xNou, yNou);
                    nrMutari++;
                    break;
                }
                else
                {
                    break;
                }

            }
        }
        Coordonate[] mutariFinale=new Coordonate[nrMutari];
        for(int i = 0; i < nrMutari; i++)
        {
            mutariFinale[i]=buffer[i];
        }
        return mutariFinale;
    }
}