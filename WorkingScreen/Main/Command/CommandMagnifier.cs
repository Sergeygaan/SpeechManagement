using Command.Magnif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Command.Auxiliary;

namespace Command
{
    class CommandMagnifier : ICommand
    {
        MagnifierForm magnifierForm = null;
       
        public void Act(int index)
        {
            if (magnifierForm == null)
            {
                var currentNumberObject = SearchChild();

                magnifierForm = new MagnifierForm();
                magnifierForm.Show();

                currentNumberObject.Magnifier = new Magnifier(magnifierForm, currentNumberObject.listRegionRectangle[index], currentNumberObject.GenerationNumber);
            }
        }
    }
}
