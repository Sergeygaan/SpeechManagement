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

                    CommandEndSector();

                    break;

                case 1:
                    CommandEndMagnifier();

                    break;

                case 2:

                    CommandEndMagnifier();
                    CommandEndFullSector();

                    break;
            }
        }

        private void CommandEndMagnifier()
        {
            var currentWorkObject = Auxiliary.SearchChild();

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

        private void CommandEndFullSector()
        {
            while (Auxiliary.SearchChild().ParantNumberObject != null)
            {
                CommandEndSector();
            }
        }

        private void CommandEndSector()
        {
            //метод отменяет разбитие сектора
            if (Auxiliary.SearchChild().ParantNumberObject != null)
            {
                var ParantNumberObject = Auxiliary.SearchChild().ParantNumberObject;

                if (ParantNumberObject.ChildNumberObject != null)
                {
                    ParantNumberObject.ChildNumberObject = null;

                    ParantNumberObject.Visible = true;
                }
            }
        }
    }
}
