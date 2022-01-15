using ivuk_zadaca_3.ObradaPodataka.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.ObradaPodataka
{
    public interface IIspisTabliceVisitor
    {
        void Visit(IspisT ispisT);
        void Visit(IspisS ispisS);
        void Visit(IspisK ispisK);
        void Visit(IspisR ispisR);
        void Visit(IspisD ispisD);
        void Visit(IspisSU ispisSU);
        void Visit(IspisGR ispisGR);
        void Visit(IspisIR ispisIR);
        void Visit(IspisIK ispisIK);
        void Visit(IspisIG ispisIG);
        void Visit(IspisVR ispisVR);
    }
}
