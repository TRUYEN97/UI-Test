using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTest.Model.Cell;

namespace UiTest.Functions.Interface
{
    public interface IInputEvent: IFunction<object>
    {
        CellData CellData { get; }
    }
}
