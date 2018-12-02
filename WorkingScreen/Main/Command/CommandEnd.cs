using Command;

namespace WorkingScreen
{
    class CommandEnd : ICommand
    {
        public void Act(int index)
        {
            switch(index)
            {
                case 0:

                    CommandEndMagnifier(Auxiliary.SearchChild());
                    CommandEndSector(Auxiliary.SearchChild());

                    break;

                case 1:
                case 2:

                    CommandEndFull(index);

                    break;
            }
        }

        private void CommandEndMagnifier(WorkObject SearchChild)
        {
            var currentWorkObject = SearchChild;

            if (currentWorkObject.Magnifier != null)
            {
                //Метод отменяет лупу
                currentWorkObject.Magnifier.Dispose();
                currentWorkObject.Magnifier = null;

                CommandMagnifier.magnifierForm.Close();
                CommandMagnifier.magnifierForm.Dispose();
                CommandMagnifier.magnifierForm = null;
            }
        }

        private void CommandEndSector(WorkObject Object)
        {
            ////метод отменяет разбитие сектора

            var ParantNumberObject = Object.ParantNumberObject;

            if (ParantNumberObject?.ChildNumberObject != null)
            {
                ParantNumberObject.ChildNumberObject = null;

                ParantNumberObject.Visible = true;
            }
        }

        private void CommandEndFull(int index)
        {
            WorkObject currentWorkObject = Auxiliary.SearchChild();

            do
            {
                CommandEndMagnifier(currentWorkObject);

                if(index == 2)
                {
                    CommandEndSector(currentWorkObject);
                }

                currentWorkObject = currentWorkObject.ParantNumberObject;
            }
            while (currentWorkObject != null);
        }       
    }
}
