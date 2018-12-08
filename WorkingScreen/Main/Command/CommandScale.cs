using Command;

namespace WorkingScreen
{
    class CommandScale : ICommand
    {
        public void Act(int index)
        {
            if (Auxiliary.SearchChild().listRegionRectangle.Count > index)
            {
                var currentNumberObject = Auxiliary.SearchChild();

                var newChildNumberObject = new WorkObject
                {
                    //Добавление родителя
                    ParantNumberObject = currentNumberObject,
                    GenerationNumber = currentNumberObject.GenerationNumber + 1
                };

                //Добавление потомка
                currentNumberObject.ChildNumberObject = newChildNumberObject;

                currentNumberObject.Visible = false;

                Drawing.MethodMain(newChildNumberObject,
                                     currentNumberObject.listRegionRectangle[index].Width, currentNumberObject.listRegionRectangle[index].Height,
                                     currentNumberObject.listRegionRectangle[index].StartX, currentNumberObject.listRegionRectangle[index].StartY);
            }
        }
    }
}
