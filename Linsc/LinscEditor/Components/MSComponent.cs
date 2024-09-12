using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinscEditor.Components
{
    internal interface IMSComponent
    {

    }

    internal abstract class MSComponent<T> : ViewModelBase, IMSComponent where T : Component
    {
            
    }
}
