using Command.Magnif;

namespace WorkingScreen
{
    class CommandMagnifier : ICommand
    {
        public static MagnifierForm magnifierForm = null;
       
        public void Act(int index)
        {
            if (Auxiliary.SearchChild().listRegionRectangle.Count > index)
            {
                if (magnifierForm == null)
                {
                    var currentNumberObject = Auxiliary.SearchChild();

                    magnifierForm = new MagnifierForm();
                    magnifierForm.Show();

                    currentNumberObject.Magnifier = new Magnifier(magnifierForm, currentNumberObject.listRegionRectangle[index], currentNumberObject.GenerationNumber);
                }
            }
        }
    }
}
