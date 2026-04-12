public class TablaSah
{
    public Piesa?[,] grid = new Piesa?[8, 8];
    
    public void IntializareJoc()
    {
        Array.Clear(grid, 0, grid.Length);

        grid[0, 0] = new Tura(false, new Coordonate(0, 0));
        grid[1,0]=new Cal(false,new Coordonate(1, 0));
        grid[2,0]=new Nebun(false,new Coordonate(2, 0));
        grid[3, 0] = new Regina(false, new Coordonate(3, 0));
        grid[4,0]=new Rege(false,new Coordonate(4, 0));
        grid[5, 0] = new Nebun(false, new Coordonate(5, 0));
        grid[6, 0] = new Cal(false, new Coordonate(6, 0));
        grid[7, 0]=new Tura(false,new Coordonate(7, 0));

        for(int i = 0; i < 8; i++)
        {
            grid[i, 1] = new Pion(false, new Coordonate(i, 1));
        }

        grid[0, 7] = new Tura(true, new Coordonate(0, 7));
        grid[1, 7] = new Cal(true, new Coordonate(1, 7));
        grid[2, 7] = new Nebun(true, new Coordonate(2, 7));
        grid[3, 7] = new Regina(true, new Coordonate(3, 7));
        grid[4, 7] = new Rege(true, new Coordonate(4, 7));
        grid[5, 7] = new Nebun(true, new Coordonate(5, 7));
        grid[6, 7] = new Cal(true, new Coordonate(6, 7));
        grid[7, 7] = new Tura(true, new Coordonate(7, 7));

        for (int i = 0; i < 8; i++)
        {
            grid[i, 6] = new Pion(true, new Coordonate(i, 6));
        }
    }
    public Coordonate[] GetMutariValide(Piesa piesaSelectata)
    {
        Coordonate[] mutariTeoretice = piesaSelectata.GetMiscarePermisa(this);

        Coordonate[] bufferFiltrat = new Coordonate[mutariTeoretice.Length+2];
        int nrMutariValide = 0;
        for (int i = 0; i < mutariTeoretice.Length; i++)
        {
            Coordonate mutare = mutariTeoretice[i];
            Piesa? piesaTinta = grid[mutare.x, mutare.y];

            if (piesaTinta == null || piesaTinta.isWhite!=piesaSelectata.isWhite)
            {
                bufferFiltrat[nrMutariValide] = mutare;
                nrMutariValide++;
            }
       
        }
        if(piesaSelectata is Rege && piesaSelectata.aMutat == false)
        {
            int rand = piesaSelectata.isWhite ? 7 : 0;
            Piesa? turaDreapta = grid[7, rand];
            if(turaDreapta is Tura && turaDreapta.aMutat == false)
            {
                if (grid[5,rand]== null && grid[6, rand] == null)
                {
                    bufferFiltrat[nrMutariValide] = new Coordonate(6, rand);
                    nrMutariValide++;
                }
            }
            Piesa? turaStanga= grid[0, rand];
            if(turaStanga is Tura && turaStanga.aMutat == false)
            {
                if (grid[1,rand]==null && grid[2,rand] == null && grid[3,rand]==null)
                {
                    bufferFiltrat[nrMutariValide]= new Coordonate(2, rand);
                    nrMutariValide++;
                }
            }
        }
        Coordonate[] mutariFinale = new Coordonate[nrMutariValide];
        for (int i = 0; i < nrMutariValide; i++)
        {
            mutariFinale[i] = bufferFiltrat[i];
        }
        return mutariFinale;
    }
}