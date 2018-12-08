using Command;

namespace WorkingScreen
{
    class CommandScale : ICommand
    {
        Drawing _drawing;

        public CommandScale(Drawing drawing)
        {
            _drawing = drawing;
        }

        public void Act(int index)
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

            _drawing.StandardLineDrawingMain(newChildNumberObject,
                                 currentNumberObject.listRegionRectangle[index].Width, currentNumberObject.listRegionRectangle[index].Height,
                                 currentNumberObject.listRegionRectangle[index].StartX, currentNumberObject.listRegionRectangle[index].StartY);
        }
    }
}
