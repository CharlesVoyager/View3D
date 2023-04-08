using System.Collections.Generic;
using View3D.model;

namespace View3D.view
{
    public delegate void onObjectMoved(float dx, float dy);
    public delegate void onObjectSelected(ThreeDModel selModel);

    public class ThreeDView
    {
        public onObjectMoved eventObjectMoved;
        public onObjectSelected eventObjectSelected;
        public bool editor = false;
        public bool autoupdateable = false;
        public int slowCounter = 0; // Indicates slow framerates
        public uint timeCall = 0;
        public bool objectsSelected = false;
        public LinkedList<ThreeDModel> models;

        public ThreeDView()
        {
            models = new LinkedList<ThreeDModel>();
        }

        public void SetEditor(bool ed)
        {
            editor = ed;
        }

        public LinkedList<ThreeDModel> OnGetAllViewModels()
        {
            return models;
        }

    }
}
