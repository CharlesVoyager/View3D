using System;
using System.Windows.Forms;
using View3D.model;
using View3D.model.geom;

namespace View3D.view
{
    public partial class ObjectInformation : Form
    {
        public static string ResinVolum = "", moduleX = "", moduleY = "", moduleZ = "";
        public static double moduleWidth = 0, moduleLen = 0, moudleHigh = 0;

        public void Analyse(PrintModel pm)
        {
            double volume = 0;
            foreach (TopoTriangle t in pm.Model.triangles)
            {
                volume += t.SignedVolume();
            }

            RHBoundingBox bbox = pm.BoundingBoxWOSupport;

            string CubicCM = (0.001 * Math.Abs(volume)).ToString("0.000");
            if (CubicCM == "0.000")
            { CubicCM = "0.001"; }

            infoVolume.Text = CubicCM + " cm³";
            infoSizeX.Text = bbox.Size.x.ToString("0.000") + " mm";
            infoSizeY.Text = bbox.Size.y.ToString("0.000") + " mm";
            infoSizeZ.Text = bbox.Size.z.ToString("0.000") + " mm";

            label_collision_flag.Text = pm.outside.ToString();
            lb_ObjInfoFileName.Text = "";
            labelFileFormat.Text = System.IO.Path.GetExtension(pm.name).ToLower();
            label_positionX.Text = bbox.Center.x.ToString("0.000");
            label_positionY.Text = bbox.Center.y.ToString("0.000");
            label_positionZ.Text = bbox.zMin.ToString("0.000");
        }

        public ObjectInformation()
        {
            InitializeComponent();
        }

        public void translate()
        {
            Text = Trans.T("W_OBJECT_INFOMATION");
            label_filename.Text = Trans.T("L_FILENAME");
            label_format.Text = Trans.T("L_FILE_FORMAT");
            label_volume.Text = Trans.T("L_VOLUME");
            label_collision.Text = Trans.T("L_COLLISION");
            label_dimension.Text = Trans.T("L_DIMENSION");
            label_position.Text = Trans.T("L_POSITION");
            label_size.Text = Trans.T("L_SIZE");
            buttonClose.Text = Trans.T("B_CLOSE");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main.main.Focus();
        }

        private void ObjectInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.main.objectInformation = null;
        }

        private void ObjectInformation_Load(object sender, EventArgs e)
        {
            translate();
            Main.main.languageChanged += translate;
        }
    }
}
