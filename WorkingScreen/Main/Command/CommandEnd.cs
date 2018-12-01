using static Command.Auxiliary;

namespace Command
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
            var currentWorkObject = SearchChild();

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
            while (SearchChild().ParantNumberObject != null)
            {
                CommandEndSector();
            }
        }

        private void CommandEndSector()
        {
            //метод отменяет разбитие сектора
            if (SearchChild().ParantNumberObject != null)
            {
                var ParantNumberObject = SearchChild().ParantNumberObject;

                if (ParantNumberObject.ChildNumberObject != null)
                {
                    ParantNumberObject.ChildNumberObject = null;

                    ParantNumberObject.Visible = true;
                }
            }
        }
    }
}
