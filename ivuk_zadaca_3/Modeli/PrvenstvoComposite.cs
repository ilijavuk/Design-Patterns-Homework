using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.Modeli
{
    public abstract class PrvenstvoComposite
    {
        public string NazivRazine { get; set; }
        protected List<PrvenstvoComposite> Djeca;

        public virtual void DodajDijete(PrvenstvoComposite prvenstvoComposite) {
            throw new NotImplementedException();
        }
        
        public virtual void UkloniDijete(PrvenstvoComposite prvenstvoComposite)
        {
            throw new NotImplementedException();
        }

        public virtual List<PrvenstvoComposite> DohvatiDjecu()
        {
            throw new NotImplementedException();
        }

        public abstract void IspisiInfo();
    }
}
