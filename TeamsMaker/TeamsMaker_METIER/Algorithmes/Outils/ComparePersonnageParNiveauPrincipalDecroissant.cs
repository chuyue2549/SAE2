using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Outils
{
    internal class ComparePersonnageParNiveauPrincipalDecroissant : Comparer<Personnage>
    {
        public override int Compare(Personnage? x, Personnage? y)
        {
            return y.LvlPrincipal - x.LvlPrincipal;
        }
    }
}
