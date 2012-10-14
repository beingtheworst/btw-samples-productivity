using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    
    public interface IDocumentReader<in TKey,TView>
    {

        bool TryGet(TKey key, out TView view);
    }


