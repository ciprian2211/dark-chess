public class Manager
{
    public TablaSah tabla;
    public bool RandAlb;
    public Piesa? piesaSelectata;
    public bool jocTerminat = false;
    public string Castigator = "";
    public bool[,] vizibilitate = new bool[8, 8];
    public Manager()
    {
        tabla=new TablaSah();
        tabla.IntializareJoc();
        RandAlb = true;
        piesaSelectata = null;

        ActualizareFog();
    }
    public void ProcesareClick(int x,int y)
    {
        if (piesaSelectata == null)
        {
            Piesa? piesaClick = tabla.grid[x, y];
            if (piesaClick != null && piesaClick.isWhite == RandAlb)
            {
                piesaSelectata = piesaClick;
            }
        }
        else
        {
            if (EsteMutareLegala(piesaSelectata, x, y))
            {
                ExecutaMutare(piesaSelectata,x,y);
            }
            else if (tabla.grid[x, y] is Piesa piesaNoua && piesaNoua.isWhite == RandAlb)
            {
                //A selectat alta piesa
                piesaSelectata = piesaNoua;
            }
            else
            {
                piesaSelectata = null;
            }
        }   
    }
    private bool EsteMutareLegala(Piesa piesa, int x, int y)
    {
        Coordonate[] dateMutare = tabla.GetMutariValide(piesa);
        for (int i = 0; i < dateMutare.Length; i++)
        {
            if (dateMutare[i].x == x && dateMutare[i].y == y)
            {
                return true;
            }
        }

        if (piesa is Rege && piesa.aMutat == false)
        {
            int rand = piesa.isWhite ? 7 : 0;

            if (x == 6 && y == rand)
            {
                Piesa? turaDreapta = tabla.grid[7, rand];
                if (turaDreapta is Tura && turaDreapta.aMutat == false)
                {
                    if (tabla.grid[5, rand] == null && tabla.grid[6, rand] == null)
                    {
                        return true;
                    }
                }
            }

            if (x == 2 && y == rand)
            {
                Piesa? turaStanga = tabla.grid[0, rand];
                if (turaStanga is Tura && turaStanga.aMutat == false)
                {
                    if (tabla.grid[1, rand] == null &&
                        tabla.grid[2, rand] == null &&
                        tabla.grid[3, rand] == null)
                    {
                        return true;

                    }
                }
            }
        }
        return false;
    }
    
    private void ExecutaMutare(Piesa piesa,int xNou,int yNou)
    {
       Piesa? piesaTinta=tabla.grid[xNou,yNou];
        if(piesaTinta is Rege && piesa.isWhite !=piesaTinta.isWhite)
        {
            jocTerminat = true;
            Castigator = piesa.isWhite ? "Alb" : "Negru";

            tabla.grid[piesa.pozitie.x, piesa.pozitie.y] = null;
            tabla.grid[xNou, yNou] = piesa;
            piesa.Mutare(new Coordonate(xNou, yNou));

            return;
        }
        if(piesa is Rege && Math.Abs(piesa.pozitie.x - xNou) == 2)
        {
            if (xNou == 6) //rocada mica
            {
                Piesa? tura = tabla.grid[7, yNou];
                tabla.grid[7, yNou] = null;
                tabla.grid[5,yNou] = tura;
                if(tura != null)
                {
                    tura.Mutare(new Coordonate(5,yNou));
                    tura.aMutat = true;
                }
            }
            else if (xNou == 2) //rocada mare
            {
                Piesa? tura = tabla.grid[0, yNou];
                tabla.grid[0,yNou] = null;
                tabla.grid[3,yNou] = tura;
                if( tura != null)
                {
                    tura.Mutare(new Coordonate(3,yNou));
                    tura.aMutat = true;
                }
            }
        }

        tabla.grid[piesa.pozitie.x, piesa.pozitie.y] = null;
        tabla.grid[xNou, yNou] = piesa;
        piesa.Mutare(new Coordonate(xNou,yNou));
        piesa.aMutat=true;
        RandAlb = !RandAlb;
        piesaSelectata = null;
        ActualizareFog();
    }
    public void ActualizareFog()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                vizibilitate[x, y] = false;
            }
        }
        for(int x = 0;x < 8; x++)
        {
            for(int y=0; y < 8; y++)
            {
                Piesa? piesaCurenta=tabla.grid[x, y];

                if(piesaCurenta != null && piesaCurenta.isWhite == RandAlb)
                {
                    vizibilitate[x, y] = true;

                    Coordonate[] PatrateLuminate = tabla.GetMutariValide(piesaCurenta);

                    for(int i = 0;i< PatrateLuminate.Length; i++)
                    {
                        vizibilitate[PatrateLuminate[i].x, PatrateLuminate[i].y] = true;
                    }
                }
            }
        }
    }
}
// rocada, fog of war, 